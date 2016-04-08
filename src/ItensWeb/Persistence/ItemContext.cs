using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Collections.Generic;
 
using System;
 
namespace DAL
{
    public class ItemContext
    {
        public IList<Item> GetAll()
        {
            var sessionFactory = CreateSessionFactory();
 
            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    var item = new Item { Name = "Test"};
                    session.SaveOrUpdate(item);
                    trans.Commit();
                }
 
                using (session.BeginTransaction())
                {
                    var items = session.CreateCriteria(typeof(Item)).List<Item>();
                    return items;
                }
            }
        }
 
        private ISessionFactory CreateSessionFactory()
        {
            return Fluently
                .Configure()
                    .Database(
                        PostgreSQLConfiguration.Standard
                        .ConnectionString(c =>
                            c.Host("localhost")
                            .Port(5432)
                            .Database("mydb")
                            .Username("postgres")
                            .Password("admin")))
                    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ItemMap>())
                    .ExposeConfiguration(TreatConfiguration)
                .BuildSessionFactory();
        }
        private static void TreatConfiguration(Configuration configuration)
        {
            // dump sql file for debug
            Action<string> updateExport = x =>
            {
                using (var file = new System.IO.FileStream(@"update.sql", System.IO.FileMode.Append, System.IO.FileAccess.ReadWrite))
                using (var sw = new System.IO.StreamWriter(file))
                {
                    sw.Write(x);
                    sw.Close();
                }
            };
            var update = new SchemaUpdate(configuration);
            update.Execute(updateExport, true);
        }
    }
}