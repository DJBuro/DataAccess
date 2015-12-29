using MyAndromeda.Core;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Sites
{
    public interface IStoreEmployeeDataService : IDependency
    {
        /// <summary>
        /// Lists the store employees.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IEnumerable<Model.MyAndromeda.StoreEmployee> ListStoreEmployees(Expression<Func<Model.MyAndromeda.StoreEmployee, bool>> query);

        /// <summary>
        /// Adds the or update.
        /// </summary>
        /// <param name="andromedaSiteId">The andromeda site id.</param>
        /// <param name="employees">The employees.</param>
        /// <returns></returns>
        IEnumerable<StoreEmployee> AddOrUpdate(int andromedaSiteId, IEnumerable<StoreEmployee> employees);
    }

    public class StoreEmployeeDataService : IStoreEmployeeDataService 
    {
        public IEnumerable<StoreEmployee> ListStoreEmployees(Expression<Func<StoreEmployee, bool>> query)
        {
            var result = Enumerable.Empty<StoreEmployee>();

            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var table = dbContext.StoreEmployees.Include(e=> e.Person);
                var queryTable = table.Where(query);

                result = queryTable.ToArray();
            }

            return result;
        }

        public IEnumerable<StoreEmployee> AddOrUpdate(int andromedaSiteId, IEnumerable<StoreEmployee> employees) 
        {
            var storeEmployeeResult = new List<StoreEmployee>();
 
            IEnumerable<StoreEmployee> storeEmployeeAddList = Enumerable.Empty<StoreEmployee>();
            IEnumerable<StoreEmployee> updateList = Enumerable.Empty<StoreEmployee>();

            using (var dbContext = new Model.MyAndromeda.MyAndromedaDbContext()) 
            {
                var peopleTable = dbContext.People;
                var employeeTable = dbContext.StoreEmployees.Include(e => e.Person);
                var storeEmployees = employeeTable.Where(e=> e.AndromedaSiteId == andromedaSiteId).ToArray();

                Func<StoreEmployee, StoreEmployee, bool> equalityComparer = (employeeA, employeeB) => 
                    employeeA.EmployeeId == employeeB.EmployeeId 
                    //second pass if for some reason the employee id changes
                    &&(
                        employeeA.Person.FullName == employeeB.Person.FullName ||
                        employeeA.PayrollNumber == employeeB.PayrollNumber
                    )
                    //you would think they would add in NI insurance number
                    //|| (employeeA.Person.NationallnsuranceNumber == employeeB.Person.NationallnsuranceNumber)
                    ;

                Func<Person, Person, bool> existingPersonComparer = (personA, personB) =>
                        personA.NationallnsuranceNumber.Equals(personB.NationallnsuranceNumber, StringComparison.InvariantCultureIgnoreCase) &&
                        personA.FullName.Equals(personB.FullName, StringComparison.InvariantCultureIgnoreCase); 

                //find those not saved in the database
                storeEmployeeAddList = employees.Where(e => !storeEmployees.Any(em => equalityComparer(em,e))).ToArray();
                
                //find those with a newer Last Updated time
                var pairs = storeEmployees.Select(e=> 
                    new {
                         dbEntity = e,
                         updateWith = employees.First(item => equalityComparer(e, item))
                    });

                //try not to update every time employees are chucked in here
                var itemsToUpdate = pairs.Where(e => e.updateWith.LastUpdated > e.dbEntity.LastUpdated);

                foreach (var itemToUpdate in itemsToUpdate) 
                {
                    var dbEntity = itemToUpdate.dbEntity;
                    var externalEntity = itemToUpdate.updateWith;

                    dbEntity.EmployeeId = externalEntity.EmployeeId;
                    dbEntity.JobTitle = externalEntity.JobTitle;
                    dbEntity.Person.FirstName = externalEntity.Person.FirstName;
                    dbEntity.Person.LastName = externalEntity.Person.LastName;
                    dbEntity.LastUpdated = externalEntity.LastUpdated;
                }


                foreach (var item in storeEmployeeAddList) 
                {
                    //NI seems like a good limiter
                    var nationalInsuranceNumbers = storeEmployeeAddList
                        .Select(e=> e.Person.NationallnsuranceNumber)
                        .ToArray();

                    var fullNamesList = 
                        storeEmployeeAddList.Select(e => e.Person.FullName)
                        .ToArray();

                    var existingPeople = peopleTable
                        .Where(e => nationalInsuranceNumbers.Contains(e.NationallnsuranceNumber) || fullNamesList.Contains(e.FullName))
                        .ToArray();
                    
                    //This person exits already. Lets use the entity  
                    if (existingPeople.Any(existingPerson => existingPersonComparer(existingPerson, item.Person))) 
                    {
                        item.Person = existingPeople.FirstOrDefault(e => e.NationallnsuranceNumber == item.Person.NationallnsuranceNumber);
                    }

                    dbContext.StoreEmployees.Add(item);
                }

                storeEmployeeResult.AddRange(storeEmployees);
                storeEmployeeResult.AddRange(storeEmployeeAddList);

                //batch update 
                dbContext.SaveChanges();
            }

            return storeEmployeeResult;
        }
    }
}
