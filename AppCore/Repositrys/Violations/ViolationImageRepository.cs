using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys.Violations
{
    public class ViolationImageRepository : IViolationImagesRepository
    {
        public IQueryable<ViolationImage> ViolationImages
        {
            get
            {
                var db = new LeoBaseContext();
                return db.ViolationImages;
            }
        }
    }
}
