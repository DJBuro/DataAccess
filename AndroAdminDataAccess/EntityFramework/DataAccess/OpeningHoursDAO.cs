using System;
using System.Data;
using System.Linq;
using AndroAdminDataAccess.DataAccess;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class OpeningHoursDAO : IOpeningHoursDAO
    {
        public string ConnectionStringOverride { get; set; }

        /// <summary>
        /// Deletes a specific opening hour
        /// </summary>
        /// <param name="externalSiteId"></param>
        /// <param name="openingHoursId"></param>
        /// <returns></returns>
        public string DeleteById(int siteId, int openingHoursId)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                entitiesContext.Database.Connection.Open();

                // Get the next data version (see comments inside the function)
                int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                // Get the store that needs to be updated
                var storeQuery = from s in entitiesContext.Stores
                                 where siteId == s.Id
                                 select s;

                Store storeEntity = storeQuery.FirstOrDefault();

                if (storeEntity == null)
                {
                    return "Unknown store"; 
                }

                var query = from oh in entitiesContext.OpeningHours
                                       join s in entitiesContext.Stores
                                         on oh.SiteId equals s.Id
                                       where s.Id == siteId
                                         && oh.Id == openingHoursId
                                       select oh;

                OpeningHour entity = query.FirstOrDefault();

                if (entity != null)
                {
                    // Increment the version of the store data
                    storeEntity.DataVersion = newVersion;

                    // Remove the opening hour
                    entitiesContext.OpeningHours.Remove(entity);

                    // Done
                    entitiesContext.SaveChanges();
                }
            }

            return "";
        }

        /// <summary>
        /// Deletes all opening hours for a whole day
        /// </summary>
        /// <param name="siteId"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public string DeleteBySiteIdDay(int siteId, string day)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                entitiesContext.Database.Connection.Open();

                // Get the next data version (see comments inside the function)
                int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                // Get the store that needs to be updated
                var storeQuery = from s in entitiesContext.Stores
                                 where siteId == s.Id
                                 select s;

                Store storeEntity = storeQuery.FirstOrDefault();

                if (storeEntity == null)
                {
                    return "Unknown store";
                }

                var query = from oh in entitiesContext.OpeningHours
                                       join s in entitiesContext.Stores
                                         on oh.SiteId equals s.Id
                                       where s.Id == siteId
                                         && oh.Day.Description == day
                                       select oh;

                OpeningHour entity = query.FirstOrDefault();

                if (entity != null)
                {
                    // Increment the version of the store data
                    storeEntity.DataVersion = newVersion;

                    // Remove the opening hour
                    entitiesContext.OpeningHours.Remove(entity);

                    // Done
                    entitiesContext.SaveChanges();
                }
            }

            return "";
        }

        /// <summary>
        /// Adds an opening hour to a day for the specified store
        /// </summary>
        /// <param name="timeSpanBlock"></param>
        /// <param name="siteId"></param>
        /// <returns></returns>
        public string Add(int siteId, TimeSpanBlock timeSpanBlock)
        {
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                entitiesContext.Database.Connection.Open();

                // Get the next data version (see comments inside the function)
                int newVersion = DataVersionHelper.GetNextDataVersion(entitiesContext);

                // Get the store
                var storeQuery = from s in entitiesContext.Stores
                                           where s.Id == siteId
                                           select s;

                Store storeEntity = storeQuery.FirstOrDefault();

                if (storeEntity == null)
                {
                    return "Unknown store";
                }

                // Get the day
                var daysQuery = from d in entitiesContext.Days
                                      where d.Description == timeSpanBlock.Day
                                      select d;

                Day dayEntity = daysQuery.FirstOrDefault();

                if (dayEntity == null)
                {
                    return "Unknown day";
                }

                // Take the textual representation of the start and end time and split them into seperate times
                TimeSpan startTimeSpan = new TimeSpan();
                TimeSpan endTimeSpan = new TimeSpan();

                if (!timeSpanBlock.OpenAllDay)
                {
                    string[] startTimeBits = timeSpanBlock.StartTime.Split(':');
                    startTimeSpan = new TimeSpan(int.Parse(startTimeBits[0]), int.Parse(startTimeBits[1]), 0);
                    string[] endTimeBits = timeSpanBlock.EndTime.Split(':');
                    endTimeSpan = new TimeSpan(int.Parse(endTimeBits[0]), int.Parse(endTimeBits[1]), 0);
                }

                // Create an object we can add
                OpeningHour openingHour = new OpeningHour();
                openingHour.Day = dayEntity;
                openingHour.OpenAllDay = timeSpanBlock.OpenAllDay;
                openingHour.SiteId = storeEntity.Id;
                openingHour.TimeStart = startTimeSpan;
                openingHour.TimeEnd = endTimeSpan;

                // Add the opening times
                entitiesContext.OpeningHours.Add(openingHour);

                // Increment the version of the store data
                storeEntity.DataVersion = newVersion;

                // Done
                entitiesContext.SaveChanges();
            }

            return "";
        }
    }
}

