using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp.TestDB
{
    public class TestContext: DbContext
    {
        public TestContext():base("DataSource='TestDataBase';Password='arcanecode'") { }
        public DbSet<Persone> Persons { get; set; }
    }

    public class Persone
    {
        [Key]
        public int PersoneID { get; set; }
        public string Name { get; set; }
    }
}
