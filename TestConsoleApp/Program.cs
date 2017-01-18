using AppData.Abstract;
using AppData.Contexts;
using AppData.Entities;
using AppData.Infrastructure;
using AppData.Repositrys;
using AppPresentators.Factorys;
using AppPresentators.Presentators;
using AppPresentators.Services;
using AppPresentators.VModels;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        #region Vars
        Image image;//= Image.FromFile("C:\\Dev\\Images\\LeoBase\\ImagesNew\\avatar.jpg");

        string[] first_names = new string[] {
                     "Иванов", "Петров", "Сидоров", "Григорьев", "Кузнецов", "Лаврентов"
                 };
        string[] second_names = new string[] {
                     "Иван", "Евгений", "Генадий", "Кирил", "Илья", "Семен"
                 };
        string[] middle_names = new string[] {
                     "Иванов", "Николаевич", "Генадьевич", "Лаврентов", "Евгеньевич", "Олегович"
                 };

        string[] citys = new string[] {
                     "Барабаш", "Славянка", "Безверхово", "Овчиниково", "Занадворовка", "Филиповка"
                 };
        string[] streets = new string[] {
                     "Героев Хасана", "Хасанская", "Лазо", "Молодежная", "Овчиниковская", "Дружба"
                 };
        string[] countys = new string[] {
                     "США", "Российская федерация"
                 };
        string[] subject = new string[] {
                     "Приморский край", "Хабаровский край"
                 };
        string[] area = new string[] {
                     "Хасанский район", "Надежденский район"
                 };
        string[] issuedBy = new string[] {
                     "Хасанский РОВД", "Приморским РОВД", "Барабашский ОМ"
                 };
        #endregion
        static void Main(string[] args)
        {
            new Program();
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        Random rand = new Random();
        private int getIndex(int number = 6)
        {
            return (int)Math.Ceiling(rand.NextDouble() * number);
        }

        private DateTime getDateBerthday()
        {
            return new DateTime(getIndex(100) + 1900, getIndex(12), getIndex(29));
        }

        private DateTime getIssuedWhen()
        {
            return new DateTime(getIndex(20) + 1996, getIndex(12), getIndex(29));
        }

        Program()
        {
            using(var db = new PassedContext())
            {
                db.Passes.Add(new Pass
                {
                    DocumentType = "Паспорт",
                    FirstName = "Иванов",
                    SecondName = "Иван",
                    MiddleName = "Иванович",
                    Number = "2123",
                    Serial = "3214",
                    WhenIssued = new DateTime(1999, 12, 1),
                    PassGiven = DateTime.Now,
                    PassClosed = DateTime.Now.AddYears(1)
                });

                db.SaveChanges();
            }
            Console.ReadKey();
        }

        private PersoneViewModel makePersone()
        {
            List<PersonAddressModelView> addresses = new List<PersonAddressModelView>();
            List<PersoneDocumentModelView> documents = new List<PersoneDocumentModelView>();
            List<PhoneViewModel> phones = new List<PhoneViewModel>();
            PersoneViewModel persone = new PersoneViewModel();

            addresses.Add(new PersonAddressModelView
            {
                Country = "Российская федерация",
                Subject = "Приморский край",
                Area = "Хасанский район",
                City = "пгт.Славянка",
                Street = "ул.Героев-Хасана",
                HomeNumber = "23",
                Flat = "22",
                Note = "Фактический"
            });
            addresses.Add(new PersonAddressModelView
            {
                Country = "Российская федерация",
                Subject = "Приморский край",
                Area = "Хасанский район",
                City = "пгт.Славянка",
                Street = "ул.Героев-Хасана",
                HomeNumber = "25",
                Flat = "21",
                Note = "Прописан"
            });


            documents.Add(new PersoneDocumentModelView
            {
                DocumentTypeName = "Паспорт",
                Serial = "4405",
                Number = "11233",
                IssuedBy = "Славянским РОВД Хасанского муниципального района",
                WhenIssued = new DateTime(2012, 1, 2),
                CodeDevision = "4500"
            });

            phones.Add(new PhoneViewModel { PhoneNumber = "8912123132" });
            phones.Add(new PhoneViewModel { PhoneNumber = "1231321233" });

            persone = new PersoneViewModel
            {
                Addresses = addresses,
                Documents = documents,
                DateBirthday = new DateTime(1990, 1, 1),
                FirstName = "Сидоров",
                SecondName = "Николай",
                MiddleName = "Петровович",
                Phones = phones,
                IsEmploeyr = true,
                //Position = "Госинспектор",
                //PlaceOfBirth = "Приморский край, г.Владивосток"
            };

            return persone;
        }

        
        

        private void AddPersone(int count, bool isEmployer = false)
        {

            var personeRepository = RepositoryesFactory.GetInstance().Get<IPersoneRepository>();

            

            byte[] imageBytes = imageToByteArray(image);
            for (int i = 0; i < count; i++)
            {
                Persone persone1;

                persone1 = new Persone
                {
                    WasBeCreated = DateTime.Now,
                    WasBeUpdated = DateTime.Now,
                    Address = new List<PersoneAddress>
                     {
                         new PersoneAddress
                         {
                             Country = countys[getIndex(countys.Length - 1)],
                             Subject = subject[getIndex(subject.Length - 1)],
                             Area = area[getIndex(area.Length - 1)],
                             City = citys[getIndex(citys.Length - 1)],
                             Street = streets[getIndex(streets.Length - 1)],
                             Flat = (getIndex(120) + 1).ToString(),
                             HomeNumber = (getIndex(20) + 1).ToString()
                         }
                     },
                    Documents = new List<Document>
                     {
                         new Document
                         {
                             Document_DocumentTypeID = getIndex(1) + 1,
                             IssuedBy = issuedBy[getIndex(issuedBy.Length - 1)],
                             WhenIssued = getIssuedWhen(),
                             Serial = (getIndex(2000) + 1).ToString(),
                             Number = (getIndex(6000) + 1).ToString()
                         }
                     },
                    Phones = new List<Phone>
                     {
                         new Phone
                         {
                             PhoneNumber = (getIndex(10000) + 10000).ToString()
                         }
                     },
                    DateBirthday = getDateBerthday(),
                    FirstName = first_names[getIndex(first_names.Length - 1)],
                    SecondName = second_names[getIndex(second_names.Length - 1)],
                    MiddleName = middle_names[getIndex(middle_names.Length - 1)],
                    IsEmploeyr = isEmployer,
                    Position_PositionID = isEmployer ? getIndex(3) + 1 : 0,
                    Image = imageBytes,
                    PlaceOfBirth = countys[getIndex(countys.Length - 1)]
                                    + "; " + subject[getIndex(subject.Length - 1)]
                                    + "; " + area[getIndex(area.Length - 1)]
                                    + "; " + citys[getIndex(citys.Length - 1)]
                };

                personeRepository.AddPersone(persone1);
            }
        }
    }
    
}
