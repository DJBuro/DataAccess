using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Data.Entity.Validation;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class ChainDAO : IChainDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.Chain> GetAll()
        {
            List<Domain.Chain> models = new List<Domain.Chain>();

            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                //var query = from s in entitiesContext.Chains.Include("Store")
                //            select s;

                var query = from c in entitiesContext.Chains.Include("Store")
                            join cc in entitiesContext.ChainChains
                            on c.Id equals cc.ChildChainId into ps
                            from p in ps.DefaultIfEmpty()
                            select new
                            {
                                c.Id,
                                c.Name,
                                c.Description,
                                c.MasterMenuId,
                                ParentChainId = (int?)p.ParentChainId,
                                c.Stores
                            };


                foreach (var entity in query)
                {
                    Domain.Chain model = new Domain.Chain()
                    {
                        Id = entity.Id,
                        Name = entity.Name,
                        Description = entity.Description,
                        MasterMenuId = entity.MasterMenuId,
                        ParentChainId = entity.ParentChainId
                    };

                    model.Stores = new List<Domain.Store>();
                    foreach (var store in entity.Stores)
                    {
                        model.Stores.Add(new Domain.Store { Id = store.Id, Name = store.Name, AndromedaSiteId = store.AndromedaSiteId });
                    }
                    models.Add(model);
                }
            }

            return models;
        }

        public Domain.Chain GetChainById(int id)
        {
            Domain.Chain chain = new Domain.Chain(); ;
            using (AndroAdminEntities entities = new AndroAdminEntities())
            {
                var result = entities.Chains.Include(e => e.Stores).Include(e => e.Stores.Select(s => s.StoreStatu)).Where(c => c.Id == id).FirstOrDefault();
                if (result != null)
                {
                    chain.Id = result.Id;
                    chain.Description = result.Description;
                    chain.Name = result.Name;
                    chain.MasterMenuId = result.MasterMenuId;

                    var chainChain = result.ChainChains.FirstOrDefault();
                    chain.ParentChainId = chainChain != null ? chainChain.ParentChainId : (int?)null;

                    if (result.Stores != null)
                    {
                        chain.Stores = new List<Domain.Store>();
                        foreach (var store in result.Stores)
                        {
                            chain.Stores.Add(new Domain.Store
                            {
                                Id = store.Id,
                                Name = store.Name,
                                AndromedaSiteId = store.AndromedaSiteId,
                                StoreStatus = (store.StoreStatu != null ? new Domain.StoreStatus { Id = store.StoreStatu.Id, Status = store.StoreStatu.Status, Description = store.StoreStatu.Description } : new Domain.StoreStatus())
                            });
                        }
                    }
                }
            }

            return chain;
        }

        public int Save(Domain.Chain chain)
        {
            Chain currentChain = new Chain
            {
                Id = chain.Id.HasValue ? chain.Id.Value : -1,
                MasterMenuId = chain.MasterMenuId,
                Name = chain.Name,
                Description = chain.Description == null ? string.Empty : chain.Description
            };

            // Add or update the chain
            using (AndroAdminEntities entities = new AndroAdminEntities())
            {
                Chain existingChain = entities.Chains.Where(c => c.Id == chain.Id).FirstOrDefault();

                if (chain.Id.HasValue && existingChain != null)
                {
                    // Existing chain - update
                    var entityWithSameName = entities.Chains.Where(c => c.Id != existingChain.Id && c.Name.Equals(currentChain.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                    if (entityWithSameName == null)
                    {
                        var masterStore = entities.Stores.Where(c => c.Id == chain.MasterMenuId).FirstOrDefault();
                        existingChain.Name = chain.Name;
                        existingChain.Description = string.IsNullOrEmpty(chain.Description) ? (string.IsNullOrEmpty(existingChain.Description) ? string.Empty : existingChain.Description) : chain.Description;
                        if (masterStore != null) { existingChain.MasterMenuId = masterStore.Id; }
                    }
                    else
                    {
                        // It's a new chain but an existing chain already has that name
                        throw new Exception("A chain with that name already exists");
                    }
                }
                else if (entities.Chains.Where(s => s.Name.Equals(chain.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault() != null)
                {
                    // It's a new chain but an existing chain already has that name
                    throw new Exception("A chain with that name already exists");
                }
                else
                {
                    // It's a new chain
                    entities.Chains.Add(currentChain);
                }

                entities.SaveChanges();
            }

            // Add or update the chains stores
            int currentChainId = 0;
            using (AndroAdminEntities entities = new AndroAdminEntities())
            {
                if (chain.Stores != null)
                {
                    currentChain.Stores = new List<Store>();
                    foreach (var store in chain.Stores)
                    {
                        currentChain.Stores.Add(new Store { Id = store.Id });
                    }
                }

                Chain existingChain = entities.Chains.Where(c => c.Id == chain.Id).FirstOrDefault();
                currentChainId = existingChain == null ? currentChain.Id : existingChain.Id;
                int otherChainId = entities.Chains.Where(c => c.Name.ToLower() == "other").FirstOrDefault().Id;

                IList<Store> chainOldStores = entities.Stores.Where(s => s.ChainId == currentChainId).ToList();

                chainOldStores.ToList().ForEach(f => f.ChainId = (currentChain.Stores.ToList().Where(s => s.Id == f.Id).FirstOrDefault() != null) ? currentChainId : otherChainId);

                foreach (var store in currentChain.Stores)
                {
                    if (!chainOldStores.Any(a => a.Id == store.Id))
                    {
                        var eStore = entities.Stores.Where(s => s.Id == store.Id).FirstOrDefault();
                        if (eStore != null)
                        {
                            eStore.ChainId = currentChainId;
                        }
                    }
                }
                try
                {
                    entities.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    List<string> errorMessages = new List<string>();
                    foreach (DbEntityValidationResult validationResult in e.EntityValidationErrors)
                    {
                        string entityName = validationResult.Entry.Entity.GetType().Name;
                        foreach (DbValidationError error in validationResult.ValidationErrors)
                        {
                            errorMessages.Add(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                        }
                    }
                }
            }

            if (chain.Id.HasValue)
            {
                // Remove all child chain<>chain relationships for this chain
                using (AndroAdminEntities entities = new AndroAdminEntities())
                {
                    var chainChainsToDelete = entities.ChainChains.Where(c => !c.ParentChainId.Equals(null) && c.ChildChainId == chain.Id);

                    foreach (ChainChain chainChainToDelete in chainChainsToDelete)
                    {
                        entities.ChainChains.Remove(chainChainToDelete);
                    }

                    entities.SaveChanges();
                }
            }

            // Add the new chain<>chain link
            if (chain.ParentChainId.HasValue)
            {
                using (AndroAdminEntities entities = new AndroAdminEntities())
                {                
                    ChainChain newChainChain = new ChainChain()
                    {
                        ParentChainId = chain.ParentChainId.Value,
                        ChildChainId = currentChain.Id
                    };
                    entities.ChainChains.Add(newChainChain);

                    entities.SaveChanges();
                }
            }

            return currentChainId;
        }
    }
}