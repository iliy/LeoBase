using AppData.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppData.Entities;

namespace AppData.FakesRepositoryes
{
    public class FakePersonesRepository : IPersoneRepository
    {
        private List<Persone> _persones = new List<Persone> { new Persone()
                    //{
                    //    UserID = 1,
                    //    FirstName = "Иванов",
                    //    SecondName = "Иван",
                    //    MiddleName = "Васильевич",
                    //    DateBirthday = new DateTime(1990, 5, 10, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 2,
                    //    FirstName = "Сидоров",
                    //    SecondName = "Евгений",
                    //    MiddleName = "Эдуардович",
                    //    DateBirthday = new DateTime(1992, 10, 5, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 3,
                    //    FirstName = "Кирилов",
                    //    SecondName = "Илья",
                    //    MiddleName = "Петрововия",
                    //    DateBirthday = new DateTime(1989, 6, 7, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 4,
                    //    IsEmploeyr = true,
                    //    FirstName = "Сидоренко",
                    //    SecondName = "Илья",
                    //    MiddleName = "Игоревич",
                    //    PositionID = 1,
                    //    DateBirthday = new DateTime(1978, 2, 12, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 5,
                    //    IsEmploeyr = true,
                    //    FirstName = "Иванов1",
                    //    SecondName = "Юрий",
                    //    MiddleName = "Петрововия",
                    //    PositionID = 2,
                    //    DateBirthday = new DateTime(1995, 3, 15, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 6,
                    //    IsEmploeyr = true,
                    //    FirstName = "Иванов2",
                    //    SecondName = "Юрий",
                    //    MiddleName = "Петрововия",
                    //    PositionID = 2,
                    //    DateBirthday = new DateTime(1995, 3, 15, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 7,
                    //    IsEmploeyr = true,
                    //    FirstName = "Иванов3",
                    //    SecondName = "Юрий",
                    //    MiddleName = "Петрововия",
                    //    PositionID = 2,
                    //    DateBirthday = new DateTime(1995, 3, 15, 0, 0, 0)
                    //},
                    //new Persone
                    //{
                    //    UserID = 8,
                    //    IsEmploeyr = true,
                    //    FirstName = "Иванов4",
                    //    SecondName = "Юрий",
                    //    MiddleName = "Петрововия",
                    //    PositionID = 2,
                    //    DateBirthday = new DateTime(1995, 3, 15, 0, 0, 0)
                    //}
        };
        public IQueryable<Persone> Persons
        {
            get
            {
                return _persones.AsQueryable();
            }
        }

        public int Count
        {
            get
            {
                return _persones.Count();
            }
        }

        public int AddPersone(Persone persone)
        {
            int id = _persones.Max(p => p.UserID) + 1;

            persone.UserID = id;

            _persones.Add(persone);

            return id;
        }

        public bool Remove(int id)
        {
            var persone = _persones.FirstOrDefault(p => p.UserID == id);

            if (persone == null) return false;

            return _persones.Remove(persone);
        }

        public bool Update(Persone persone)
        {
            throw new NotImplementedException();
        }
    }
}
