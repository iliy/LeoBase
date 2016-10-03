using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;
using AppData.Contexts;

namespace AppData.Repositrys
{
    public class PositionsRepository : IPersonePositionRepository
    {
        public IQueryable<EmploeyrPosition> Positions
        {
            get
            {
                var db = new LeoBaseContext();

                return db.Positions;
            }
        }

        public int AddPosition(EmploeyrPosition position)
        {
            using (var db = new LeoBaseContext())
            {
                var pos = db.Positions.Add(position);
                db.SaveChanges();
                return pos.PositionID;
            }
        }

        public bool Remove(int id)
        {
            using(var db = new LeoBaseContext())
            {
                var position = db.Positions.FirstOrDefault(p => p.PositionID == id);

                if(position != null)
                {
                    db.Positions.Remove(position);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
