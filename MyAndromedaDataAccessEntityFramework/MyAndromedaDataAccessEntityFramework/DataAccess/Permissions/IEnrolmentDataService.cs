using MyAndromeda.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyAndromeda.Core.Site;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Permissions
{
    public interface IEnrolmentDataService : IDependency 
    {
        IEnumerable<IEnrolmentLevel> List();
        IEnumerable<IEnrolmentLevel> Query(Expression<Func<Model.MyAndromeda.EnrolmentLevel, bool>> query);

        IEnrolmentLevel Create(string name, string description);
        void Update(IEnrolmentLevel model);
    }

    public class EnrolmentDataService : IEnrolmentDataService 
    {
        public IEnumerable<IEnrolmentLevel> List()
        {
            IEnumerable<IEnrolmentLevel> items; 
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.EnrolmentLevels;
                items = table.ToArray().Select(e=> e as IEnrolmentLevel).ToList();
            }

            return items;
        }

        public IEnumerable<IEnrolmentLevel> Query(Expression<Func<EnrolmentLevel, bool>> query)
        {
            IEnumerable<IEnrolmentLevel> items;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.EnrolmentLevels;
                items = table.Where(query).ToArray().Select(e => e as IEnrolmentLevel).ToList();
            }

            return items;
        }

        public IEnrolmentLevel Create(string name, string description) 
        {
            IEnrolmentLevel model;
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.EnrolmentLevels;

                var entity = new Model.MyAndromeda.EnrolmentLevel() { Name = name, Description = description };

                model = entity;
                table.Add(entity);
                dbContext.SaveChanges();
            }

            return model;
        }

        public void Update(IEnrolmentLevel model) 
        {
            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext())
            {
                var table = dbContext.EnrolmentLevels;
                var item = table.Where(e => e.Id == model.Id).Single();

                item.Name = model.Name;
                item.Description = model.Description;

                dbContext.SaveChanges();
            }
        }
    }
}
