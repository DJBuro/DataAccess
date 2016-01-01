using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudWCFHelper;
using MyAndromedaDataAccess.DataAccess;
using AndroCloudHelper;
using MyAndromedaDataAccessEntityFramework.Model;
using MyAndromedaDataAccess.Domain;
using MyAndromedaDataAccessEntityFramework.DataAccess;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class OpeningHoursDataAccess : IOpeningHoursDataAccess
    {
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
                // We have to be careful to join on site here.  We've already verified that the user is allowed to access the site but
                // the openingHoursId could be forged to access the opeing hours of another store.  By joining on the store id we
                // ensure that the day row belongs to the store that the user has permission to access.
                var query = from oh in entitiesContext.OpeningHours
                                       join s in entitiesContext.Stores
                                         on oh.SiteId equals s.Id
                                       where s.Id == siteId
                                         && oh.Id == openingHoursId
                                       select oh;

                MyAndromedaDataAccessEntityFramework.Model.OpeningHour entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entitiesContext.OpeningHours.Remove(entity);
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
                // We have to be careful to join on site here.  We've already verified that the user is allowed to access the site but
                // the openingHoursId could be forged to access the opeing hours of another store.  By joining on the store id we
                // ensure that the day row belongs to the store that the user has permission to access.
                var query = from oh in entitiesContext.OpeningHours
                                       join s in entitiesContext.Stores
                                         on oh.SiteId equals s.Id
                                       where s.Id == siteId
                                         && oh.Day.Description == day
                                       select oh;

                MyAndromedaDataAccessEntityFramework.Model.OpeningHour entity = query.FirstOrDefault();

                if (entity != null)
                {
                    entitiesContext.OpeningHours.Remove(entity);
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
                // Get the store
                var storeQuery = from s in entitiesContext.Stores
                                           where s.Id == siteId
                                           select s;

                MyAndromedaDataAccessEntityFramework.Model.Store storeEntity = storeQuery.FirstOrDefault();

                if (storeEntity == null)
                {
                    return "Unknown store";
                }

                // Get the day
                var daysQuery = from d in entitiesContext.Days
                                      where d.Description == timeSpanBlock.Day
                                      select d;

                MyAndromedaDataAccessEntityFramework.Model.Day dayEntity = daysQuery.FirstOrDefault();

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
                MyAndromedaDataAccessEntityFramework.Model.OpeningHour openingHour = new MyAndromedaDataAccessEntityFramework.Model.OpeningHour();
                openingHour.Day = dayEntity;
                openingHour.OpenAllDay = timeSpanBlock.OpenAllDay;
                openingHour.SiteId = storeEntity.Id;
                openingHour.TimeStart = startTimeSpan;
                openingHour.TimeEnd = endTimeSpan;

                // Add the opening times
                entitiesContext.OpeningHours.Add(openingHour);
                entitiesContext.SaveChanges();
            }

            return "";
        }
    }
}
