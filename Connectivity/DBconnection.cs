using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Connectivity
{

    public class DBconnection
    {
        static void Main(string[] args)
        {

        }

        public static ISessionFactory GetSessionFactory()
        {
            var config = new Configuration();

            config.DataBaseIntegration(x =>
            {
                x.ConnectionString = "Data Source=DESKTOP-5BL7FN5\\SQLEXPRESS;" +
                                        "Initial Catalog=Interview;" +
                                        "Integrated Security=True";
                x.Driver<SqlClientDriver>();
                x.Dialect<MsSql2008Dialect>();
            });

            config.AddAssembly(Assembly.GetExecutingAssembly());

            var SessionFactory = config.BuildSessionFactory();

            return SessionFactory;
        }
    }
}
