using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;

namespace AppData.FakesRepositoryes
{
    public class FakePersonsPositionRepository : IPersonePositionRepository
    {
        private List<EmploeyrPosition> _positions = new List<EmploeyrPosition>
            {
                new EmploeyrPosition
                {
                    PositionID = 4,
                    Name = "Госинспектор"
                },
                new EmploeyrPosition
                {
                    PositionID = 5,
                    Name = "Участковый госинспектор"
                },
                new EmploeyrPosition
                {
                    PositionID = 6,
                    Name = "Старший госинпектор"
                }
            };

        public IQueryable<EmploeyrPosition> Positions
        {
            get
            {
                return _positions.AsQueryable();
            }
        }

        public int AddPosition(EmploeyrPosition position)
        {
            int id = Positions.Max(p => p.PositionID) + 1;

            position.PositionID = id;

            _positions.Add(position);

            return id;
        }

        public bool Remove(int id)
        {
            var position = Positions.FirstOrDefault(p => p.PositionID == id);

            if (position == null) return false;

            return _positions.Remove(position);
        }
    }
}
