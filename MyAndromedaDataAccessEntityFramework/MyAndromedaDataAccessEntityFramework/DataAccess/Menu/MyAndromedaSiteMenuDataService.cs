using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using MyAndromedaDataAccessEntityFramework.Model.MyAndromeda;

namespace MyAndromedaDataAccessEntityFramework.DataAccess.Menu
{
    public class MyAndromedaSiteMenuDataService : IMyAndromedaSiteMenuDataService 
    {
        private readonly IMyAndromedaDbWorkContextAccessor dbWork;

        public MyAndromedaSiteMenuDataService(IMyAndromedaDbWorkContextAccessor dbWork)
        { 
            this.dbWork = dbWork;
        }

        public SiteMenu Create(int andromedaSiteId)
        {
            SiteMenu menu;
            using (var dbContext = NewContext())
            {
                var table = dbContext.SiteMenus;
                var entity = table.Create();

                entity.AndromediaId = andromedaSiteId;
                entity.LastUpdated = DateTime.UtcNow;
                table.Add(entity);

                dbContext.SaveChanges();
                menu = entity;
            }

            return menu;
        }

        public void Update(SiteMenu siteMenu)
        {
            using (var dbContext = NewContext()) 
            {
                var result = this.GetMenuWithContext(dbContext, siteMenu.AndromediaId);

                result.LastUpdated = siteMenu.LastUpdated;
                result.DataVersion = siteMenu.DataVersion;

                if (result.SiteMenuFtpBackupId == 0) {
                    result.SiteMenuFtpBackup = new SiteMenuFtpBackup();
                }

                result.SiteMenuFtpBackup.LastFtpCheckDateUtc = siteMenu.SiteMenuFtpBackup.LastFtpCheckDateUtc;
                result.SiteMenuFtpBackup.LastDownloadedDateUtc = siteMenu.SiteMenuFtpBackup.LastDownloadedDateUtc;
                result.SiteMenuFtpBackup.LastUploadedDateUtc = siteMenu.SiteMenuFtpBackup.LastUploadedDateUtc;
                result.SiteMenuFtpBackup.MenuVersion = siteMenu.SiteMenuFtpBackup.MenuVersion;

                dbContext.SaveChanges();
            }
        }

        public IEnumerable<SiteMenu> List(Expression<Func<SiteMenu, bool>> query)
        {
            IEnumerable<SiteMenu> results = Enumerable.Empty<SiteMenu>();
            using (var dbContext = NewContext()) 
            {
                var table = dbContext.SiteMenus;

                results = table.Include(e=> e.SiteMenuFtpBackup).Where(query).ToArray();

            }
            return results;
        }

        public void SetDownloadFlag(SiteMenuFtpBackup siteMenuFtp, bool value = true, bool inProgress = false)
        {
            using (var dbContext = NewContext()) 
            {
                var dbItem = dbContext.SiteMenuFtpBackups.Where(e => e.Id == siteMenuFtp.Id).Single();
                dbItem.CheckToDownload = value;
                dbItem.LastFtpCheckDateUtc = DateTime.UtcNow;
                siteMenuFtp.CheckToDownload = value;
                siteMenuFtp.CheckInProgress = inProgress;

                if (!value && !inProgress) { siteMenuFtp.LastFtpCheckDateUtc = DateTime.UtcNow; }

                dbContext.SaveChanges();
            }
        }


        public void SetUploadFlag(SiteMenuFtpBackup siteMenuFtp, bool value = true, bool inProgress = false)
        {
            using (var dbContext = NewContext())
            {
                var dbItem = dbContext.SiteMenuFtpBackups.Where(e => e.Id == siteMenuFtp.Id).Single();
                dbItem.CheckToUpload = value;
                siteMenuFtp.CheckToUpload = value;
                siteMenuFtp.CheckInProgress = inProgress;

                dbContext.SaveChanges();
            }
        }


        public void SetVersion(int andromedaSiteId, int version)
        {
            using (var dbContext = NewContext()) 
            {
                var dbItem = this.GetMenuWithContext(dbContext, andromedaSiteId);
                dbItem.SiteMenuFtpBackup.MenuVersion = version;

                dbContext.SaveChanges();
            }
        }

        public void SetVersion(SiteMenuFtpBackup siteMenuFtp)
        {
            using (var dbContext = NewContext())
            {
                var dbItem = dbContext.SiteMenuFtpBackups.Where(e => e.Id == siteMenuFtp.Id).Single();
                dbItem.MenuVersion = siteMenuFtp.MenuVersion;

                dbContext.SaveChanges();
            }
        }

        public SiteMenu GetMenu(int andromedaSiteId)
        {
            using (var dbContext = NewContext())
            {
                var result = this.GetMenuWithContext(dbContext, andromedaSiteId);

                if (result != null)
                {
                    return result;
                }
            }

            //obviously there isn't one already. Need to go make another. 
            return this.Create(andromedaSiteId);
        }

        private static MyAndromedaDbContext NewContext()
        {
            return new MyAndromedaDbContext();
        }

        private SiteMenu GetMenuWithContext(MyAndromedaDbContext dbContext, int andromedaSiteId)
        {
            var table = dbContext.SiteMenus;
            var query = table
                             .Include(e => e.SiteMenuFtpBackup)
                             .Where(e => e.AndromediaId == andromedaSiteId);

            var result = query.SingleOrDefault();
            if (result == null) 
            {
                this.Create(andromedaSiteId);
            }
            if (result.SiteMenuFtpBackupId == 0 || result.SiteMenuFtpBackupId== null) 
            {
                result.SiteMenuFtpBackup = new SiteMenuFtpBackup();
                dbContext.SaveChanges();
            }

            return result;
        }
    }
}