using MyAndromedaDataAccessEntityFramework.Model.AndroAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.DeliveryZone
{
    public class DeliveryZoneNameDataService : IDeliveryZoneNameDataService
    {
        private readonly AndroAdminDbContext dataContext;

        public DeliveryZoneNameDataService(AndroAdminDbContext dataContext)
        {
            this.dataContext = dataContext;
            this.Table = this.dataContext.Set<DeliveryZoneName>();
            this.TableQuery = this.Table
                                .Include(e => e.Store)
                                .Include(e=>e.PostCodeSectors);
        }

        public DbSet<DeliveryZoneName> Table { get; private set; }
        public IQueryable<DeliveryZoneName> TableQuery { get; private set; }

        public IList<DeliveryZoneName> Get(int storeId)
        {
            IList<DeliveryZoneName> deliveryAreaList = this.dataContext.DeliveryZoneNames.Where(e => e.StoreId == storeId).ToList();
            return deliveryAreaList;
        }

        public void Create(DeliveryZoneName deliveryZoneName)
        {
            if (deliveryZoneName != null)
            {
                DeliveryZoneName existingDeliveryArea = this.dataContext.DeliveryZoneNames.Where(e => e.StoreId == deliveryZoneName.StoreId).FirstOrDefault();
                if (existingDeliveryArea != null)
                {
                    this.Update(deliveryZoneName);
                }
                else
                {
                    this.dataContext.DeliveryZoneNames.Add(deliveryZoneName);
                    dataContext.SaveChanges();
                }
            }
        }

        public void Update(DeliveryZoneName deliveryZoneName)
        {
            DeliveryZoneName existing = this.Table.Find(deliveryZoneName.Id);
            ((IObjectContextAdapter)this.dataContext).ObjectContext.Detach(existing);
            dataContext.Entry(deliveryZoneName).State = EntityState.Modified;
            dataContext.SaveChanges();
        }

        public bool Delete(DeliveryZoneName deliveryZoneName)
        {
            var tableQuery = this.TableQuery.Where(e => e.Id == deliveryZoneName.Id);
            var result = tableQuery.SingleOrDefault();
            if (result != null)
            {
                DeliveryZoneName retObj = this.Table.Remove(deliveryZoneName);
                return dataContext.SaveChanges() > 0;
            }
            return false;
        }

        public bool Delete(int storeId)
        {
            bool isDeleted = false;
            var tableQuery = this.TableQuery.Where(e => e.StoreId == storeId);
            var result = tableQuery.SingleOrDefault();
            if (result != null)
            {
                isDeleted = Delete(result);
            }
            return isDeleted;
        }

        public void ChangeIncludeScope<TPropertyModel>(System.Linq.Expressions.Expression<Func<DeliveryZoneName, TPropertyModel>> predicate)
        {
            throw new NotImplementedException();
        }

        public DeliveryZoneName New()
        {
            return new DeliveryZoneName() { };
        }

        public DeliveryZoneName Get(System.Linq.Expressions.Expression<Func<DeliveryZoneName, bool>> predicate)
        {
            var tableQuery = this.TableQuery.Where(predicate);
            return tableQuery.SingleOrDefault();
        }

        public IQueryable<DeliveryZoneName> List()
        {
            return this.TableQuery;
        }

        public IQueryable<DeliveryZoneName> List(System.Linq.Expressions.Expression<Func<DeliveryZoneName, bool>> predicate)
        {
            return this.TableQuery.Where(predicate);
        }
    }
}
