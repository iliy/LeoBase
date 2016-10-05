using AppData.Abstract;
using AppData.Contexts;
using AppData.Entities;
using AppData.Infrastructure;
using AppPresentators.Factorys;
using AppPresentators.Services;
using AppPresentators.VModels.Persons;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
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
        Image image = Image.FromFile("d:\\Images\\avatar.jpg");

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

            var prAr = (ProtocolAboutWithdraw)ProtocolFactory.GetProtocolDetails(92);

            prAr.FixingMethods = "Спутник";
            prAr.WithdrawAmmunitions = "Никакая амуниция не была изъята";
            prAr.WithdrawDocuments = "Никаие документы не были изъяты";
            prAr.WithdrawGunsHuntingAndFishing = "Ничего не было изъято";
            prAr.WithdrawNatureManagementProducts = "Ничего не было изъято";
            prAr.WithdrawWeapons = "Никакие оружия не были изъяты";
            prAr.ViolatorDocumentID = 16;

            prAr.Protocol.DateCreatedProtocol = new DateTime(2011, 11, 12);
            prAr.Protocol.EmployerID = 6;
            prAr.Protocol.PlaceCreatedProtocol = "р.Пойма Новое место";
            prAr.Protocol.PlaceCreatedProtocolE = 1112;
            prAr.Protocol.PlaceCreatedProtocolN = 3332;
            prAr.Protocol.ViolationID = 101;

            var a = ProtocolFactory.UpdateProtocol(prAr);

            Console.WriteLine(a);

            Console.ReadKey();

            return;

            Protocol protocol = new Protocol
            {
                DateCreatedProtocol = new DateTime(2016, 4, 2),
                EmployerID = 5,
                PlaceCreatedProtocol = "р.Рязановка",
                PlaceCreatedProtocolE = 123,
                ViolationID = 100,
                DateSave = DateTime.Now,
                DateUpdate = DateTime.Now,
                PlaceCreatedProtocolN = 231,
                WitnessFIO_1 = "Иванов У 1",
                WitnessFIO_2 = "Иванов У 2",
                WitnessLive_1 = "Живет У 1",
                WitnessLive_2 = "Живет У 2",
                ProtocolTypeID = (int)ProtocolsType.PROTOCOL_ABOUT_WITHDRAW
            };

            var protocolAboutArest = new ProtocolAboutWithdraw
            {
                Protocol = protocol,
                FixingMethods = "Фото/Видео съемка",
                ViolatorDocumentID = 17,
                WithdrawAmmunitions = "Какая-то амуниция изъята",
                WithdrawDocuments = "Какие-то документы изъяты",
                WithdrawGunsHuntingAndFishing = "Что-то еще изъято",
                WithdrawNatureManagementProducts = "И еще что-то изъято",
                WithdrawWeapons = "Какое-то оружие изъято"
            };


            ProtocolFactory.SaveProtocol(protocolAboutArest);


            //var protocol = (Injunction)ProtocolFactory.GetProtocolDetails(75);


            //List<InjunctionItem> items = new List<InjunctionItem>
            //{
            //    new InjunctionItem
            //    {
            //        BaseOrders = "Какие-то основания 1",
            //        Deedline = "Конец 2016 года 1",
            //        Description = "Что-то нарушено в природоохранном законодательстве 1"
            //    },
            //    new InjunctionItem
            //    {
            //        BaseOrders = "Еще какие-то основания 2",
            //        Deedline = "Конец 2016 года 2",
            //        Description = "Что-то нарушено опять 2"
            //    }
            //};

            //protocol.ViolatorDocumentID = 14;
            //protocol.InjunctionInfo = "Новая информация";
            //protocol.Protocol.ViolationID = 101;
            //protocol.Protocol.WitnessFIO_1 = "Новый кто-то 1";
            //protocol.Protocol.WitnessFIO_2 = "Новый кто-то 2";
            //protocol.Protocol.WitnessLive_1 = "Здесь живет новый 1";
            //protocol.Protocol.WitnessLive_2 = "Здесь живет новый 2";

            //protocol.Protocol.PlaceCreatedProtocol = "Новое место создания протокола";
            //protocol.Protocol.PlaceCreatedProtocolE = 1;
            //protocol.Protocol.PlaceCreatedProtocolN = 2;

            //protocol.Protocol.DateCreatedProtocol = new DateTime(2015, 1, 1);

            //protocol.InjuctionsItem = items;

            //protocol.ActInspectionDate = new DateTime(2015, 1, 1);

            //ProtocolFactory.UpdateProtocol(protocol);

            //var protocol = ProtocolFactory.GetProtocol(62);
            //var definition = (DefinitionAboutViolation)ProtocolFactory.GetProtocolDetails(74);

            //definition.FindedAmmunitions = "Что-то новое нашли!!!!!!";

            //definition.Protocol.PlaceCreatedProtocolN = 112;
            //definition.Protocol.PlaceCreatedProtocolE = 222;

            //definition.Protocol.EmployerID = 6;

            //definition.Protocol.PlaceCreatedProtocol = "И вспомнили, что протокол составлялся немного в другом месте 12";

            //definition.OrganisationID = 0;
            //definition.ViolatorDocumentID = 13;

            //bool result = ProtocolFactory.UpdateProtocol(definition);



            //Console.WriteLine(result);

            //Protocol protocol = ProtocolFactory.GetProtocol(71);

            //var pr = (Injunction)ProtocolFactory.GetProtocolDetails(protocol);

            //string forPrint = "";

            //forPrint += pr.InjunctionInfo + "\r\n";
            //forPrint += pr.Protocol.PlaceCreatedProtocol;

            //foreach(var item in pr.InjuctionsItem)
            //{
            //    forPrint += "\t" + item.BaseOrders + " " + item.Deedline + " " + item.Description + "\r\n";
            //}

            //Console.WriteLine(forPrint);

            //Protocol protocol = new Protocol
            //{
            //    DateCreatedProtocol = new DateTime(2014, 10, 1, 12, 2, 0),
            //    DateSave = DateTime.Now,
            //    DateUpdate = DateTime.Now,
            //    EmployerID = 6,
            //    PlaceCreatedProtocol = "Река нарва бла бла бла",
            //    PlaceCreatedProtocolN = 43.123123,
            //    PlaceCreatedProtocolE = 131.123123,
            //    ProtocolTypeID = (int)ProtocolsType.PROTOCOL_ABOUT_BRINGING,
            //    WitnessFIO_1 = "Иванов В.А.",
            //    WitnessLive_1 = "пгт.Славянка",
            //    WitnessFIO_2 = "Сидоров Н.Е.",
            //    WitnessLive_2 = "г.Владивосток",
            //    ViolationID = 100
            //};



            //ProtocolAboutBringing pr = new ProtocolAboutBringing
            //{
            //    Protocol = protocol,
            //    ViolatorDocumentID = 13,
            //    FindedAmmunitions = "Ничего",
            //    FindedDocuments = "Паспорт",
            //    FindedGunsHuntingAndFishing = "Ничего",
            //    FixingMethods = "Фото/видео камера"
            //};

            //ProtocolFactory.SaveProtocol(pr);


            Console.ReadLine();
            //AddPersone(1);

            //var service = new PersonesService();

            ////var persone = makePersone();

            ////service.AddNewPersone(persone);

            //service.PageModel = new AppPresentators.VModels.PageModel
            //{
            //    ItemsOnPage = -1
            //};

            //service.SearchModel = new PersonsSearchModel
            //{
            //    PlaceWork = "безработный"
            //};

            //service.OrderModel = new PersonsOrderModel
            //{
            //    OrderProperties = PersonsOrderProperties.WAS_BE_CREATED,
            //    OrderType = AppPresentators.VModels.OrderType.ASC
            //};

            //service.AddressSearchModel = new SearchAddressModel
            //{
            //    City = "Славянка",
            //    CompareCity = AppPresentators.VModels.CompareString.CONTAINS
            //};

            //service.DocumentSearchModel = new DocumentSearchModel
            //{
            //    DocumentTypeName = "Водительское удостоверение"
            //};


            //var persone = service.GetPerson(10, true);

            //persone.FirstName = "Григорьев";

            //persone.Addresses.Remove(persone.Addresses.First());
            //persone.Phones.Remove(persone.Phones.First());
            //persone.Documents.Remove(persone.Documents.First());

            //persone.Addresses.Add(new PersonAddressModelView
            //{
            //    Country = "Российская федерация",
            //    Subject = "Приморский край",
            //    Area = "Хасанский район",
            //    City = "пгт.Славянка",
            //    Street = "ул.Лазо",
            //    HomeNumber = "27",
            //    Flat = "56",
            //    Note = "Прописан/проживает"
            //});

            //persone.Documents.Add(new PersoneDocumentModelView
            //{
            //    DocumentTypeName = "Паспорт",
            //    Serial = "440512",
            //    Number = "112300",
            //    IssuedBy = "Приморским РОВД Хасанского муниципального района",
            //    WhenIssued = new DateTime(2009, 1, 2),
            //    CodeDevision = "14500"
            //});

            //persone.Phones.Add(new PhoneViewModel { PhoneNumber = "+71231321231" });

            //service.UpdatePersone(persone);

            //service.Remove(7);

            //var users = service.GetPersons();

            //foreach (var user in users)
            //{
            //    Console.WriteLine(user.UserID + "   " + user.FirstName + " " + user.SecondName + " " + user.MiddleName + " " + user.WasBeCreated);
            //}

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
                Position = "Госинспектор",
                PlaceOfBirth = "Приморский край, г.Владивосток"
            };

            return persone;
        }

        

        private void AddPersone(int count)
        {

            var personeRepository = RepositoryesFactory.GetInstance().Get<IPersoneRepository>();

            

            byte[] imageBytes = imageToByteArray(image);
            for (int i = 0; i < count; i++)
            {
                Persone persone1;

                bool isEmployer = getIndex(2) >= 1;
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
                    Position_PositionID = isEmployer ? getIndex(2) + 4 : 0,
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
