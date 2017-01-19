using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Components;
using AppData.Entities;
using AppPresentators;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class SavePassControl : UserControl, IEditPassControl
    {
        public SavePassControl()
        {
            InitializeComponent();
        }

        private Pass _pass;

        public Pass Pass
        {
            get
            {
                MakePass();

                return _pass;
            }

            set
            {
                _pass = value;
                lbTitle.Text = value.PassID <= 0 ? "Добавление нового пропуска" : "Редактирование пропуска";
                RenderPass();
            }
        }

        public bool ShowForResult { get; set; }

        public List<Control> TopControls
        {
            get
            {
                var btnSave = new Button();

                btnSave.Text = "Сохранить";

                btnSave.Click += (s, e) =>
                {
                    if (Save != null) Save();
                };

                return new List<Control> { btnSave };
            }

            set { }
        }

        public event Action Save;

        public Control GetControl()
        {
            return this;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void UIComponent.Resize(int width, int height)
        {

        }

        private void MakePass()
        {
            _pass.FirstName = tbFirstName.Text;

            _pass.SecondName = tbSecondName.Text;

            _pass.MiddleName = tbMiddleName.Text;

            _pass.Number = tbNumber.Text;

            _pass.Serial = tbSerial.Text;

            _pass.WhoIssued = tbWhoIssued.Text;

            _pass.DocumentType = cmpDocumentType.Items[cmpDocumentType.SelectedIndex].ToString();

            _pass.PassClosed = dtpPassClosed.Value;

            _pass.PassGiven = dtpPassGiven.Value;

            _pass.WhenIssued = dtpWhenIssued.Value;
        }

        private void RenderPass()
        {
            tbFirstName.Text = _pass.FirstName;
            tbSecondName.Text = _pass.SecondName;
            tbMiddleName.Text = _pass.MiddleName;

            dtpPassGiven.Value = _pass.PassGiven;
            dtpPassClosed.Value = _pass.PassClosed;
            dtpWhenIssued.Value = _pass.WhenIssued;

            tbSerial.Text = _pass.Serial;
            tbNumber.Text = _pass.Number;
            tbWhoIssued.Text = _pass.WhoIssued;

            int i = 0;

            foreach(var doc in ConfigApp.DocumentTypesList)
            {
                cmpDocumentType.Items.Add(doc.Name);

                if (doc.Name.Equals(_pass.DocumentType)) cmpDocumentType.SelectedIndex = i;

                i++;
            }
        }
    }
}
