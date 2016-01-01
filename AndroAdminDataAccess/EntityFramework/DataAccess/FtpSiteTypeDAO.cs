using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndroAdminDataAccess.Domain;
using AndroAdminDataAccess.DataAccess;

namespace AndroAdminDataAccess.EntityFramework.DataAccess
{
    public class FtpSiteTypeDAO : IFTPSiteTypeDAO
    {
        public string ConnectionStringOverride { get; set; }

        public IList<Domain.FTPSiteType> GetAll()
        {
            List<Domain.FTPSiteType> ftpSiteTypes = new List<Domain.FTPSiteType>();

             
            using (AndroAdminEntities entitiesContext = new AndroAdminEntities())
            {
                DataAccessHelper.FixConnectionString(entitiesContext, this.ConnectionStringOverride);

                var query = from s in entitiesContext.FTPSiteTypes
                            select s;

                foreach (var entity in query)
                {
                    Domain.FTPSiteType model = new Domain.FTPSiteType()
                    {
                        Id = entity.Id,
                        Name = entity.Name
                    };

                    ftpSiteTypes.Add(model);
                }
            }

            return ftpSiteTypes;
        }
    }
}