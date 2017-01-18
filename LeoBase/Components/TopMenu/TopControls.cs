using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Components.TopMenu
{
    public class TopControls:List<Control>
    {
        public event Action AddItem;
        public event Action DeleteItem;
        public event Action DetailsItem;
        public event Action ReportItem;
        public event Action EditItem;

        private PictureButton _btnAdd;
        private PictureButton _btnEdit;
        private PictureButton _btnDelete;
        private PictureButton _btnDetails;
        private PictureButton _btnReport;
        
        public bool EnabledAdd
        {
            get
            {
                return _btnAdd != null ? _btnAdd.Enabled : false;
            }
            set
            {
                if (_btnAdd != null) _btnAdd.Enabled = value;
            }
        }

        public bool EnabledEdit
        {
            get
            {
                return _btnEdit != null ? _btnEdit.Enabled : false;
            }
            set
            {
                if (_btnEdit != null) _btnEdit.Enabled = value;
            }
        }

        public bool EnabledDelete
        {
            get
            {
                return _btnDelete != null ? _btnDelete.Enabled : false;
            }
            set
            {
                if (_btnDelete != null) _btnDelete.Enabled = value;
            }
        }

        public bool EnabledDetails
        {
            get
            {
                return _btnDetails != null ? _btnDetails.Enabled : false;
            }
            set
            {
                if (_btnDetails != null) _btnDetails.Enabled = value;
            }
        }

        public bool EnabledReport
        {
            get
            {
                return _btnReport != null ? _btnReport.Enabled : false;
            }
            set
            {
                if (_btnReport != null) _btnReport.Enabled = value;
            }
        }

        public TopControls(string role, bool isOmissions = false):base()
        {
            if (role.Equals("admin") || isOmissions && role.Equals("passesManager"))
            {
                _btnAdd = new PictureButton(Properties.Resources.addEnabled, Properties.Resources.addDisabled, Properties.Resources.addPress);
                _btnEdit = new PictureButton(Properties.Resources.editEnabled, Properties.Resources.editDisabled, Properties.Resources.editPress);
                _btnDelete = new PictureButton(Properties.Resources.deleteEnabled, Properties.Resources.deleteDisabled, Properties.Resources.deletePress);
                _btnDetails = new PictureButton(Properties.Resources.detailsEnabled, Properties.Resources.detailsDisabled, Properties.Resources.detailsPress);
                _btnReport = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);

                _btnAdd.OnClick += (s, e) =>
                {
                    if (AddItem != null) AddItem();
                };

                _btnEdit.OnClick += (s, e) =>
                {
                    if (EditItem != null) EditItem();
                };

                _btnDetails.OnClick += (s, e) =>
                {
                    if (DetailsItem != null) DetailsItem();
                };

                _btnReport.OnClick += (s, e) =>
                {
                    if (ReportItem != null) ReportItem();
                };

                _btnDelete.OnClick += (s, e) =>
                {
                    if (DeleteItem != null) DeleteItem();
                };

                this.Add(_btnReport);
                this.Add(_btnAdd);
                this.Add(_btnEdit);
                this.Add(_btnDelete);
                this.Add(_btnDetails);
            }else if (role.Equals("user"))
            {
                _btnDetails = new PictureButton(Properties.Resources.detailsEnabled, Properties.Resources.detailsDisabled, Properties.Resources.detailsPress);
                _btnReport = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);

                _btnDetails.OnClick += (s, e) =>
                {
                    if (DetailsItem != null) DetailsItem();
                };

                _btnReport.OnClick += (s, e) =>
                {
                    if (ReportItem != null) ReportItem();
                };

                this.Add(_btnReport);
                this.Add(_btnDetails);
            }
        }
    }
}
