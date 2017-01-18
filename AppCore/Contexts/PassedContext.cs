using AppData.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Contexts
{
    public class PassedContext:DbContext
    {
        public PassedContext():base("DataSource='PassedDataBase.sdf';Password='abc45166bca'") { }

        /// <summary>
        /// Пропуска
        /// </summary>
        public DbSet<Pass> Passes { get; set; }
    }
}
