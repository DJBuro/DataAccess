﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroAdminDataAccess.Domain;

namespace AndroAdminDataAccess.DataAccess
{
    public interface IAMSServerDAO
    {
        IList<AMSServer> GetAll();
        void Add(AMSServer amsServer);
        void Update(AMSServer amsServer);
        void Delete(int amsServerId);
        AMSServer GetById(int id);
        AMSServer GetByName(string name);
    }
}
