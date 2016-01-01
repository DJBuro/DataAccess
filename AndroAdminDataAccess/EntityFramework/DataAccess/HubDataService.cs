using AndroAdminDataAccess.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.EntityFramework.Extensions;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class HubDataService : IHubDataService
    {
        public IEnumerable<AndroAdminDataAccess.Domain.HubItem> GetAfterDataVersion(int fromVersion)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var dataResult = dbContext.HubAddresses.Where(e => e.DataVersion > fromVersion).ToArray();

                var results = dataResult.Select(e => e.ToDomain()).ToList();

                return results;
            }
        }

        public void Add(AndroAdminDataAccess.Domain.HubItem dbModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities()) 
            {
                var table = dbContext.HubAddresses;
                var entity = table.Create();
                entity.Active = dbModel.Active;
                entity.Address = dbModel.Address;
                entity.DataVersion = 0;
                entity.Name = dbModel.Name;
                entity.Removed = dbModel.Removed;

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;

                table.Add(entity);
                dbContext.SaveChanges();
            }
        }

        public void Update(AndroAdminDataAccess.Domain.HubItem dbModel)
        {
            using(var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HubAddresses;
                var entity = table.Single(e => e.Id == dbModel.Id);

                entity.Name = dbModel.Name;
                entity.Removed = dbModel.Removed;
                entity.Address = dbModel.Address;
                entity.Active = dbModel.Active;

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;

                dbContext.SaveChanges();
            }
        }

        public void Remove(AndroAdminDataAccess.Domain.HubItem dbModel)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HubAddresses;
                var entity = table.Single(e => e.Id == dbModel.Id);

                entity.Removed = true;

                int newVersion = DataVersionHelper.GetNextDataVersion(dbContext);
                entity.DataVersion = newVersion;

                dbContext.SaveChanges();
            }
        }

        public AndroAdminDataAccess.Domain.HubItem GetHub(Guid id)
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var table = dbContext.HubAddresses;
                var data = table.Where(e => e.Id == id).ToArray();
                var result = data.Select(e => e.ToDomain()).SingleOrDefault();

                return result;
            }
        }

        public IEnumerable<AndroAdminDataAccess.Domain.HubItem> GetHubs()
        {
            using (var dbContext = new EntityFramework.AndroAdminEntities())
            {
                var data = dbContext.HubAddresses.ToArray();
                var results = data.Select(e => e.ToDomain()).ToArray();

                return results;
            }
        }
    }
}
