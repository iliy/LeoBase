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
using AppPresentators.VModels;
using LeoBase.Components.CustomControls.SaveComponent.SaveProtocol;
using AppPresentators;

namespace LeoBase.Components.CustomControls.SaveComponent
{
    public partial class SaveViolationControl : UserControl, ISaveOrUpdateViolationControl
    {
        SaveProtocolPanel _saveProtocolsPanel;
        public SaveViolationControl()
        {
            InitializeComponent();

            _saveProtocolsPanel = new SaveProtocolPanel();

            //this.documentsPanel.Controls.Add(_saveProtocolsPanel);

            cbProtocolTypes.ValueMember = "ProtocolTypeID";

            cbProtocolTypes.DisplayMember = "Name";

            cbProtocolTypes.DataSource = ConfigApp.ProtocolTypesList;

        }

        public bool ShowForResult { get; set; }
        
        public List<Control> TopControls
        {
            get
            {
                Button saveButton = new Button();
                saveButton.Text = "Сохранить";

                saveButton.Click += (s, e) =>
                {
                    if (Save != null) Save();
                };

                return new List<Control>
                {
                    saveButton
                };
            }

            set
            {
                
            }
        }

        private ViolationViewModel _violation;

        public ViolationViewModel Violation
        {
            get
            {
                if(_violation == null)
                {
                    _violation = new ViolationViewModel
                    {
                        Date = dtDate.Value,
                        Description = tbDescription.Text,
                        E = Convert.ToDouble(tbE.Text.Replace('.', ',')),
                        N = Convert.ToDouble(tbN.Text.Replace('.', ','))
                    };
                }
                return _violation;
            }

            set
            {
                _violation = value;
                if(value.ViolationID > 0)
                {

                }
            }
        }

        public event Action Save;
        public event AddNewProtocol AddNewProtocolEvent;
        public event Action FindEmplyer;
        public event Action FindViolator;
        public event Action FindRulingCreator;

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbProtocolTypes.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип документа");
                return;
            }

            if (AddNewProtocolEvent != null) AddNewProtocolEvent(Convert.ToInt32(cbProtocolTypes.SelectedValue));

            //MessageBox.Show(string.Format("ID: {0}; Name: {1}", cbProtocolTypes.SelectedValue, cbProtocolTypes.Text));
        }

        public void AddProtocol(Control control)
        {
            documentsPanel.Controls.Add(control);
        }

        public void RemoveProtocol(Control control)
        {
            documentsPanel.Controls.Remove(control);
        }

        public DialogResult ShowDialog(string title, string message)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
        }
    }
}
