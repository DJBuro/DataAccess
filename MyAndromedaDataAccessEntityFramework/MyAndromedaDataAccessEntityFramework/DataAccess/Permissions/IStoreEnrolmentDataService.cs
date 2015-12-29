using System;
using System.Linq;
using MyAndromeda.Core;
using MyAndromeda.Core.Site;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Permissions
{
    public interface IStoreEnrolmentDataService : IDependency 
    {
        void UpdateStoreEnrolment(ISite site, IEnrolmentLevel enrolment);
    }

    public class StoreEnrolmentDataService : IStoreEnrolmentDataService 
    {
        public void UpdateStoreEnrolment(ISite site, IEnrolmentLevel enrolment)
        {
            this.ClearEnrolements(site);

            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.StoreEnrolments;
                var enrolmentModel = table.Create();

                enrolmentModel.Active = true;
                enrolmentModel.EnrolmentLevelId = enrolment.Id;
                enrolmentModel.StoreId = site.Id;

                table.Add(enrolmentModel);

                dbContext.SaveChanges();
            }
        }

        public void ClearEnrolements(ISite site)
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.StoreEnrolments;
                var query = table.Where(e => e.StoreId == site.Id);
                var results = query.ToArray();

                foreach(var result in results)
                {
                    table.Remove(result);
                }

                dbContext.SaveChanges();
            }
        }
    }
}