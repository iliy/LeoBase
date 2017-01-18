using AppPresentators.Presentators.Interfaces.ProtocolsPresentators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppPresentators.VModels.MainMenu;
using AppPresentators.VModels.Protocols;
using System.Windows.Forms;
using AppPresentators.Infrastructure;
using AppPresentators.Components.Protocols;
using AppPresentators.Services;
using AppPresentators.VModels.Persons;

namespace AppPresentators.Presentators.Protocols
{
    public class ProtocolAboutViolationPresentator : IProtocolAboutViolationPresentator
    {
        public int Key { get; set; }
        public event RemoveProtocolEvent OnRemove;

        public ProtocolViewModel Protocol { get
            {
                return _view.Protocol;
            }
            set
            {
                _view.Protocol = value;
            }
        }

        public IRulingForViolationPresentator Ruling { get; set; }

        private IApplicationFactory _appFactory;

        private IProtocolAboutViolationView _view;

        private IPersonesService _personeService;

        public ProtocolAboutViolationPresentator(IApplicationFactory appFactory)
        {
            _appFactory = appFactory;

            _view = appFactory.GetComponent<IProtocolAboutViolationView>();
            _personeService = appFactory.GetService<IPersonesService>();
            
            _view.SearchEmployer += SearchEmployer;
            _view.SearchViolator += SearchViolator;

            _view.ChangeEmployerData += AutocompliteEmployer;
            _view.ChangeViolatorData += AutocompliteViolator;

            _view.Protocol =  new ProtocolAboutViolationPersoneViewModel
                {
                    ProtocolID = -1,
                    ProtocolTypeID = 7,
                    Employer = new EmploeyrViewModel
                    {
                        UserID = -1
                    },
                    Violator = new PersoneViewModel
                    {
                        UserID = -1
                    }
                };
        }

        private void AutocompliteViolator()
        {
            var protocol = (ProtocolAboutViolationPersoneViewModel)_view.Protocol;

            var searchModel = new VModels.Persons.PersonsSearchModel();

            var violator = protocol.Violator;

            if (!string.IsNullOrEmpty(violator.FirstName)) searchModel.FirstName = violator.FirstName;
            if (!string.IsNullOrEmpty(violator.SecondName)) searchModel.SecondName = violator.SecondName;
            if (!string.IsNullOrEmpty(violator.MiddleName)) searchModel.MiddleName = violator.MiddleName;
            if (violator.DateBirthday.Year != 1) searchModel.DateBirthday = violator.DateBirthday;
            if (!string.IsNullOrEmpty(violator.PlaceOfBirth)) searchModel.PlaceOfBirth = violator.PlaceOfBirth;
            if (!string.IsNullOrEmpty(violator.PlaceWork)) searchModel.PlaceWork = violator.PlaceWork;

            searchModel.IsEmployer = false;

            _personeService.SearchModel = searchModel;

            var persones = _personeService.GetPersons();

            if (persones != null && persones.Count == 1) _view.SetViolator(persones.First());
        }

        private void AutocompliteEmployer()
        {
            var protocol = (ProtocolAboutViolationPersoneViewModel)_view.Protocol;

            var searchModel = new VModels.Persons.PersonsSearchModel();

            var employer = protocol.Employer;

            if (!string.IsNullOrEmpty(employer.FirstName)) searchModel.FirstName = employer.FirstName;
            if (!string.IsNullOrEmpty(employer.SecondName)) searchModel.SecondName = employer.SecondName;
            if (!string.IsNullOrEmpty(employer.MiddleName)) searchModel.MiddleName = employer.MiddleName;

            searchModel.IsEmployer = true;

            _personeService.SearchModel = searchModel;

            var persones = _personeService.GetPersons();

            if (persones != null && persones.Count == 1) _view.SetEmployer(persones.First());
        }

        private void SearchViolator()
        {
            throw new NotImplementedException();
        }

        private void SearchEmployer()
        {
            throw new NotImplementedException();
        }

        public Control GetControl()
        {
            return _view.GetControl();
        }

        public void Remove()
        {
            if (OnRemove != null) OnRemove(Key);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void SetResult(ResultTypes command, object data)
        {
            if(command == ResultTypes.SEARCH_VIOLATOR)
            {
                if(data is IPersoneViewModel)
                {
                    _view.SetViolator((IPersoneViewModel)data);
                }
            }

            // TODO: Добавить обработку других возможных позвращенных результатов
        }
    }
}
