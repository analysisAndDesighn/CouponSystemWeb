using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CouponSystemWeb.App_Code
{
    public class DataAccess
    {
        private static CS_DBEntities3 db;

        public static CS_DBEntities3 getDataAccess()
        {
            if (db == null)
            {
                db = new CS_DBEntities3();
                return db;
            }
            else
            {
                return db;
            }
        }

    }
}