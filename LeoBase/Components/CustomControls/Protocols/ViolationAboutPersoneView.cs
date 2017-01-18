using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components.Protocols;
using AppPresentators.VModels.MainMenu;
using AppPresentators.VModels.Persons;
using AppPresentators.VModels.Protocols;
using AppPresentators.Infrastructure;
using AppPresentators;

namespace LeoBase.Components.CustomControls.Protocols
{
    public partial class ViolationAboutPersoneView : UserControl, IProtocolAboutViolationView
    {
        private bool _useAutocomplete = false;
        public ViolationAboutPersoneView()
        {
            InitializeComponent();

            tbFirstNameEmployer.TextChanged += (s, e) =>
            {
                _useAutocomplete = chbAutocompliteEmplyer.Checked;
                AutoComopleteEmployer();
                _useAutocomplete = false;
            };

            tbSecondNameEmployer.TextChanged += (s, e) =>
            {
                _useAutocomplete = chbAutocompliteEmplyer.Checked;
                AutoComopleteEmployer();
                _useAutocomplete = false;
            }; 

            tbMiddleNameEmployer.TextChanged += (s, e) => 
            {
                _useAutocomplete = chbAutocompliteEmplyer.Checked;
                AutoComopleteEmployer();
                _useAutocomplete = false;
            };

            cbPositionEmployer.ValueMember = "PositionID";
            cbPositionEmployer.DisplayMember = "Name";

            cbPositionEmployer.DataSource = ConfigApp.EmployerPositionsList;
        }


        private void AutoComopleteEmployer()
        {
            if (_useAutocomplete)
            {
                if(_protocol != null)
                {
                    _protocol.Employer.FirstName = tbFirstNameEmployer.Text.Length > 1 ? tbFirstNameEmployer.Text : "";
                    _protocol.Employer.SecondName = tbSecondNameEmployer.Text.Length > 1 ? tbSecondNameEmployer.Text : "";
                    _protocol.Employer.MiddleName = tbMiddleNameEmployer.Text.Length > 1 ? tbMiddleNameEmployer.Text : "";
                    //_protocol.Employer.PositionID = cbPositionEmployer.SelectedIndex >= 0 ? Convert.ToInt32(cbPositionEmployer.SelectedValue) : -1;

                    if (ChangeEmployerData != null) ChangeEmployerData();
                }
            }
        }

        public EmploeyrViewModel Emploer
        {
            get
            {
                if (_protocol != null) return _protocol.Employer;
                return null;
            }

            set
            {
                if (_protocol != null) _protocol.Employer = value;
            }
        }

        private ProtocolAboutViolationPersoneViewModel _protocol;

        public ProtocolViewModel Protocol
        {
            get
            {
                return _protocol;
            }

            set
            {
                if (value is ProtocolAboutViolationPersoneViewModel)
                    _protocol = (ProtocolAboutViolationPersoneViewModel)value;
                else
                    throw new ArgumentException("Тип протокола передан не правильно");


            }
        }

        public PersoneViewModel Violator
        {
            get
            {
                if (_protocol != null) return _protocol.Violator;
                else return null;
            }

            set
            {
                if (_protocol != null) _protocol.Violator = value;

            }
        }

        public event Action ChangeEmployerData;
        public event Action ChangeViolatorData;
        public event Action Remove;
        public event Action SearchEmployer;
        public event Action SearchViolator;

        public Control GetControl()
        {
            return this;
        }

        public void SetResult(ResultTypes resultType, object data)
        {
            if(resultType == ResultTypes.SEARCH_EMPLOYER) // Поиск сотрудника
            {

            }else if(resultType == ResultTypes.SEARCH_VIOLATOR) // Поиск нарушителя
            {

            }
        }

        public void SetViolator(IPersoneViewModel violator)
        {
            //throw new NotImplementedException();

        }

        public void SetEmployer(IPersoneViewModel employer)
        {
            if (_protocol != null) _protocol.Employer = (EmploeyrViewModel)employer;

            bool buf = chbAutocompliteEmplyer.Checked;

            chbAutocompliteEmplyer.Checked = false;

            tbFirstNameEmployer.Text = employer.FirstName;
            tbSecondNameEmployer.Text = employer.SecondName;
            tbMiddleNameEmployer.Text = employer.MiddleName;

            cbPositionEmployer.SelectedValue = _protocol.Employer.PositionID;

            chbAutocompliteEmplyer.Checked = buf;
        }
    }
}
