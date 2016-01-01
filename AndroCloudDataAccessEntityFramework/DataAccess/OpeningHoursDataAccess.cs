using System;
using System.Data;
using System.Data.Objects;
using System.Linq;
using AndroCloudDataAccess.DataAccess;
using System.Collections.Generic;
using AndroCloudDataAccessEntityFramework.Model;
using AndroCloudDataAccess.Domain;

namespace AndroCloudDataAccessEntityFramework.DataAccess
{
    public class OpeningHoursDataAccess : IOpeningHoursDataAccess
    {
        public string DeleteByIdMyAndromedaUserId(Guid openingHoursId, string myAndromedaUserId)
        {
            ACSEntities acsEntities = new ACSEntities();

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           join oh in acsEntities.OpeningHours
                             on s.ID equals oh.SiteID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && oh.ID == openingHoursId
                           select oh;

            Model.OpeningHour acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                acsEntities.OpeningHours.DeleteObject(acsEntity);
                acsEntities.SaveChanges();
            }

            return "";
        }

        public string DeleteBySiteIdDayMyAndromedaUserId(string externalSiteId, string day, string myAndromedaUserId)
        {
            ACSEntities acsEntities = new ACSEntities();

            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           join oh in acsEntities.OpeningHours
                             on s.ID equals oh.SiteID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && s.ExternalId == externalSiteId
                             && oh.Day.Description == day
                           select oh;

            Model.OpeningHour acsEntity = acsQuery.FirstOrDefault();

            if (acsEntity != null)
            {
                acsEntities.OpeningHours.DeleteObject(acsEntity);
                acsEntities.SaveChanges();
            }

            return "";
        }

        public string AddByMyAndromedaUserId(TimeSpanBlock timeSpanBlock, string externalSiteId, string myAndromedaUserId)
        {
            ACSEntities acsEntities = new ACSEntities();

            // Check that the myAndromeda user is allowed to access this site
            var acsQuery = from u in acsEntities.MyAndromedaUsers
                           join e in acsEntities.Employees
                             on u.EmployeeID equals e.ID
                           join g in acsEntities.Groups
                             on u.GroupID equals g.ID
                           join sg in acsEntities.SitesGroups
                             on g.ID equals sg.GroupID
                           join s in acsEntities.Sites
                             on sg.SiteID equals s.ID
                           where u.Username == myAndromedaUserId
                             && u.IsEnabled == true
                             && s.ExternalId == externalSiteId
                           select s;

            Model.Site siteACSEntity = acsQuery.FirstOrDefault();

            if (siteACSEntity != null)
            {
                // Get the day
                var acsQuery2 = from d in acsEntities.Days
                                where d.Description == timeSpanBlock.Day
                                select d;

                Model.Day dayACSEntity = acsQuery2.FirstOrDefault();

                if (dayACSEntity != null)
                {
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
                    openingHour.ID = Guid.NewGuid();
                    openingHour.Day = dayACSEntity;
                    openingHour.OpenAllDay = timeSpanBlock.OpenAllDay;
                    openingHour.Site = siteACSEntity;
                    openingHour.TimeStart = startTimeSpan;
                    openingHour.TimeEnd = endTimeSpan;

                    acsEntities.OpeningHours.AddObject(openingHour);
                    acsEntities.SaveChanges();
                }
            }

            return "";
        }
    }
}
