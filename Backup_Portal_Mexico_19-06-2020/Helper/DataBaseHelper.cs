using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.Configuration;


namespace Helper
{
    public static class DataBaseHelper
    {
        public static string GetConnectionString(string ConnectionName)
        {            
            var connection = ConfigurationManager.ConnectionStrings[ConnectionName].ConnectionString;
            return connection;
        }

    }
}
