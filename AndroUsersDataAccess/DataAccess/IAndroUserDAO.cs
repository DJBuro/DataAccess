﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndroUsersDataAccess.Domain;

namespace AndroUsersDataAccess.DataAccess
{
    public interface IAndroUserDAO
    {
        string ConnectionStringOverride { get; set; }
        List<AndroUser> GetAll();
        string Add(AndroUser androUser);
        AndroUser GetByEmailAddress(string emailAddress);
    }
}
