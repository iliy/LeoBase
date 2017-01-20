using AppData.Abstract;
using AppData.Contexts;
using AppData.Entities;
using AppData.Infrastructure;
using AppData.Repositrys;
using AppPresentators.Services;
using AppPresentators.VModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators
{
    public static class ConfigApp
    {
        public static string DefaultMapPath = "map\\map.lmap";

        public static int DefaultMapZoomForViolation = 14;

        private static Random rnd = new Random();
        public static void InitDefaultData()
        {
            List<Persone> employers = null;
            List<Persone> violators = null;

            using (var db = new LeoBaseContext())
            {
                employers = db.Persones.Where(p => p.IsEmploeyr).ToList();

                violators = db.Persones.Include("Documents").Where(p => !p.IsEmploeyr).ToList();
            }
             var repo = new AdminViolationsRepository();

            for (int i = 0; i < 10; i++)
            {
                try
                {

                    AdminViolation violation = new AdminViolation();

                    var employer = employers[RndIndex(0, employers.Count - 1)];

                    var violator = violators[RndIndex(0, violators.Count - 1)];

                    var document = violator.Documents.First();

                    violation.Employer = employer;

                    violation.ViolatorPersone = violator;

                    violation.ViolatorDocument = document;

                    violation.Consideration = RndDate();

                    violation.CreatedProtocolE = RndDouble(130, 2);

                    violation.CreatedProtocolN = RndDouble(40, 42);

                    violation.DateAgenda2025 = RndDate();

                    violation.WasAgenda2025 = RndBool();

                    violation.DateCreatedProtocol = violation.Consideration.AddDays(-4);

                    violation.DateNotice = violation.Consideration.AddDays(30);

                    violation.DatePaymant = violation.Consideration.AddDays(10);

                    violation.DateReceiving = violation.Consideration.AddDays(15);

                    violation.DateSave = DateTime.Now;

                    violation.DateSent = violation.Consideration.AddDays(1);

                    violation.DateSentBailiff = violation.Consideration.AddDays(90);

                    violation.DateUpdate = DateTime.Now;

                    violation.DateViolation = violation.DateCreatedProtocol;

                    violation.FindedGunsHuntingAndFishing = "Найденные орудия охоты и рыболовства " + i;

                    violation.FindedNatureManagementProducts = "Найденная продукция природопользования" + i;

                    violation.FindedWeapons = "Найденные оружия " + i;

                    violation.GotPersonaly = RndBool();

                    violation.KOAP = "8.39" + RndIndex(1, 10);

                    violation.NumberNotice = i.ToString();

                    violation.NumberSent = i.ToString();

                    violation.NumberSentBailigg = i.ToString();

                    violation.PlaceCreatedProtocol = "Место составления протокола " + i;

                    violation.PlaceViolation = "Место правонарушения " + i;

                    violation.RulingNumber = i.ToString();

                    violation.SumViolation = (decimal)RndDouble(1500, 3000);

                    violation.WasPaymant = RndBool();

                    violation.SumRecovery = violation.WasPaymant ? violation.SumViolation : 0;

                    violation.Violation = "8.39" + RndIndex(1, 10);

                    violation.ViolationDescription = "Находился на территории ООПТ и т.д. " + i;

                    violation.ViolationE = RndDouble(130, 132);
                    violation.ViolationN = RndDouble(41, 43);

                    violation.WasAgenda2025 = RndBool();

                    violation.WasNotice = RndBool();

                    violation.WasPaymant = RndBool();

                    violation.WasReceiving = RndBool();

                    violation.WasSent = RndBool();

                    violation.WasSentBailiff = RndBool();

                    violation.WitnessFIO_1 = "Иванов " + i;

                    violation.WitnessFIO_2 = "Сидоров " + i;

                    violation.WitnessLive_1 = "Живет " + i;

                    violation.WitnessLive_2 = "Живет " + i;

                    repo.SaveViolation(violation);
                }
                catch (Exception e)
                {
                    int a = 10;
                }
            }
            
        }

        private static bool RndBool()
        {
            return RndDouble(0, 1) > 0.5;
        }

        private static DateTime RndDate()
        {
            return new DateTime(1990 + RndIndex(0, 20), RndIndex(1, 12), RndIndex(1, 30));
        }

        private static double RndDouble(double start, double end)
        {
            return rnd.NextDouble() * end + start;
        }

        private static int RndIndex(int start, int end)
        {
            return (int)Math.Round(rnd.NextDouble() * (double)end + start);
        }

        public static VManager CurrentManager { get; set; }
        private static Dictionary<int, string> _docTypes;
        public static Dictionary<int, string> DocumentsType {
            get
            {
                if(_docTypes == null) {
                    _docTypes = new Dictionary<int, string>();
                    var repoDocTypes = RepositoryesFactory.GetInstance().Get<IDocumentTypeRepository>();
                    foreach (var dt in repoDocTypes.DocumentTypes)
                    {
                        _docTypes.Add(dt.DocumentTypeID, dt.Name);
                    }
                }

                return _docTypes;
            }
        }

        private static Dictionary<int, string> _employerPositions;
        private static List<EmploeyrPosition> _employerPositionsList;

        public static Dictionary<int, string> EmploerPositions
        {
            get
            {
                if(_employerPositions == null)
                {
                    _employerPositions = new Dictionary<int, string>();
                    var repo = RepositoryesFactory.GetInstance().Get<IPersonePositionRepository>();
                    foreach(var p in repo.Positions)
                    {
                        _employerPositions.Add(p.PositionID, p.Name);
                    }
                }

                return _employerPositions;
            }
        }

        public static List<EmploeyrPosition> EmployerPositionsList
        {
            get
            {
                if(_employerPositionsList == null)
                {
                    using (var db = new LeoBaseContext()) {
                        _employerPositionsList = db.Positions.ToList();
                    }
                }

                return _employerPositionsList;
            }
        }

        private static List<DocumentType> _documentTypesList;

        public static bool DocumentTypesWasUpdated = false;

        public static List<DocumentType> DocumentTypesList
        {
            get
            {
                if(_documentTypesList == null || DocumentTypesWasUpdated)
                {
                    _documentTypesList = new List<DocumentType>();

                    using (var db = new LeoBaseContext()) {
                        _documentTypesList = db.DocumentsType.ToList();
                    }

                    DocumentTypesWasUpdated = false;
                }

                return _documentTypesList;
            }
        }

        private static Dictionary<int, string> _protocolTypes;
        private static List<ProtocolType> _protocolTypesList;

        public static Dictionary<int, string> ProtocolTypes
        {
            get
            {
                if(_protocolTypes == null)
                {
                    _protocolTypes = new Dictionary<int, string>();
                    using(var db = new LeoBaseContext())
                    {
                        var types = db.ProtocolTypes;
                        foreach(var type in types)
                        {
                            _protocolTypes.Add(type.ProtocolTypeID, type.Name);
                        }
                    }
                }
                return _protocolTypes;
            }
        }

        public static List<ProtocolType> ProtocolTypesList
        {
            get
            {
                if(_protocolTypesList == null)
                {
                    using(var db = new LeoBaseContext())
                    {
                        _protocolTypesList = db.ProtocolTypes.ToList();
                    }
                }

                return _protocolTypesList;
            }
        }

        public static Dictionary<string, string> ManagerRoleTranslate
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"admin", "Администратор" },
                    {"user", "Пользователь"},
                    {"passesManager","Добавляет пропуски" }
                };
            }
        }
    }
}
