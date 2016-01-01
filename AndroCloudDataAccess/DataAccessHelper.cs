using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AndroCloudDataAccess;

namespace AndroCloudDataAccess
{
    public class DataAccessHelper
    {
        private static IDataAccessFactory dataAccessFactory = null;
        public static IDataAccessFactory DataAccessFactory 
        { 
            get { return dataAccessFactory; }
            set 
            { 
                dataAccessFactory = value; 
                Console.WriteLine("SET"); 
            } 
        }
    }
}