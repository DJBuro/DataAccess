using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data;
using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.Infrastructure;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.DeliveryZone
{
    public class DeliveryZoneDataService : IDeliveryZoneDataService
    {
        private readonly AndroAdminDbContext dataContext;

        public DeliveryZoneDataService(AndroAdminDbContext dataContext)
        {
            this.dataContext = dataContext;
            this.Table = this.dataContext.Set<DeliveryArea>();
            this.TableQuery = this.Table.Include(e => e.Store);
        }

        public DbSet<DeliveryArea> Table { get; private set; }

        public IQueryable<DeliveryArea> TableQuery { get; private set; }



        public IList<DeliveryArea> Get(int storeId)
        {
            IList<DeliveryArea> deliveryAreaList = this.dataContext.DeliveryAreas.Where(e => e.StoreId == storeId).ToList();
            return deliveryAreaList;
        }

        public void Create(DeliveryArea deliveryArea)
        {
            if (deliveryArea != null)
            {
                DeliveryArea existingDeliveryArea = this.dataContext.DeliveryAreas.Where(e => e.StoreId == deliveryArea.StoreId && e.DeliveryArea1 == deliveryArea.DeliveryArea1).FirstOrDefault();
                if (existingDeliveryArea != null)
                {
                    this.Update(deliveryArea);
                }
                else
                {
                    //deliveryArea.DataVersion = Model.DataVersionHelper.GetNextDataVersion(dataContext);
                    this.dataContext.DeliveryAreas.Add(deliveryArea);
                    dataContext.SaveChanges();
                }

            }
        }

        public void Update(DeliveryArea deliveryArea)
        {
            if (deliveryArea != null)
            {
                DeliveryArea existingDeliveryArea = this.dataContext.DeliveryAreas.Where(e => e.StoreId == deliveryArea.StoreId && e.DeliveryArea1 == deliveryArea.DeliveryArea1).FirstOrDefault();
                if (existingDeliveryArea != null && existingDeliveryArea.Removed == true)
                {
                    existingDeliveryArea.Removed = false;
                    existingDeliveryArea.DataVersion = deliveryArea.DataVersion;
                }
                else
                {
                    existingDeliveryArea = deliveryArea;
                }
               
            }

            dataContext.SaveChanges();
        }

        public bool Delete(DeliveryArea deliveryArea)
        {
            if (deliveryArea != null)
            {
                DeliveryArea existingDeliveryArea = this.dataContext.DeliveryAreas.Where(e => e.StoreId == deliveryArea.StoreId && e.DeliveryArea1 == deliveryArea.DeliveryArea1).FirstOrDefault();
                if (existingDeliveryArea != null)
                {
                    existingDeliveryArea.Removed = true;
                    existingDeliveryArea.DataVersion = deliveryArea.DataVersion;
                    dataContext.SaveChanges();
                }
                return true;
            }
            return false;
        }



        public DeliveryArea New()
        {
            return new DeliveryArea() { };
        }

        public DeliveryArea Get(Expression<Func<DeliveryArea, bool>> predicate)
        {
            var tableQuery = this.TableQuery.Where(predicate);
            return tableQuery.SingleOrDefault();
        }

        public IQueryable<DeliveryArea> List()
        {
            return this.TableQuery;
        }

        public IQueryable<DeliveryArea> List(Expression<Func<DeliveryArea, bool>> predicate)
        {
            
            return this.TableQuery.Where(predicate);
        }

        public void ChangeIncludeScope<TPropertyModel>(Expression<Func<DeliveryArea, TPropertyModel>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int storeId)
        {
            var existingDeliveryAreas = this.dataContext.DeliveryAreas.Where(e => e.StoreId == storeId).ToList();
            if (existingDeliveryAreas != null)
            {
                int dataVersion = Model.DataVersionHelper.GetNextDataVersion(dataContext);
                dataContext.DeliveryAreas.Where(d => d.StoreId == storeId).ToList().ForEach(d => { d.Removed = true; d.DataVersion = dataVersion; });
                
                dataContext.SaveChanges();
                return true;
            }

            return false;
        }

        public DeliveryZoneName GetDeliveryZonesByRadius(int storeId)
        {            
            DeliveryZoneName deliveryzoneName =  dataContext.DeliveryZoneNames.Include(e => e.PostCodeSectors).Where(e => e.StoreId == storeId).FirstOrDefault();
            return deliveryzoneName;
        }

        //Deliveryzones by Radius methods
        public bool SaveDeliveryZones(DeliveryZoneName deliveryZone)
        {
            try
            {
                var existingDeliveryZone = dataContext.DeliveryZoneNames.Where(e => e.Name.Equals(deliveryZone.Name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (existingDeliveryZone != null)
                {
                    UpdateDeliveryZones(deliveryZone);
                }
                else
                {
                    CreateDeliveryZones(deliveryZone);
                }

                return true;
            }
            catch (Exception e) { 

            }
            return false;
        }

        private void UpdateDeliveryZones(DeliveryZoneName deliveryZone)
        {
            var existingDeliveryzone = dataContext.DeliveryZoneNames.Where(f => f.Id == deliveryZone.Id).FirstOrDefault();
            existingDeliveryzone = deliveryZone;
            dataContext.SaveChanges();

            foreach(PostCodeSector item in deliveryZone.PostCodeSectors){
                var existingpostcode = existingDeliveryzone.PostCodeSectors.ToList().Where(f => f.DeliveryZoneId == item.DeliveryZoneId).FirstOrDefault();
                existingpostcode = item;
            }

            dataContext.SaveChanges();
        }

        public void CreateDeliveryZones(DeliveryZoneName deliveryzoneName)
        {
            dataContext.DeliveryZoneNames.Add(deliveryzoneName);
            dataContext.SaveChanges();

            deliveryzoneName.PostCodeSectors.ToList().ForEach(f=>f.DeliveryZoneId = deliveryzoneName.Id);
            foreach (var item in deliveryzoneName.PostCodeSectors) {
                dataContext.PostCodeSectors.Add(item);
            }
            dataContext.SaveChanges();
        }
        
    }
}
