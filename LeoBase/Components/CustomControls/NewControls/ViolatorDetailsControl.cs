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
using LeoBase.Components.TopMenu;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class ViolatorDetailsControl : UserControl, IViolatorDetailsControl
    {
        public event Action MakeReport;
        public ViolatorDetailsControl()
        {
            InitializeComponent();
            
            var pictureButton = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);

            pictureButton.Enabled = true;

            pictureButton.Click += (s, e) =>
            {
                if (MakeReport != null) MakeReport();
            };

            TopControls = new List<Control>();

            TopControls.Add(pictureButton);
        }
        

        public bool ShowForResult { get; set; }

        public List<Control> TopControls { get; set; }

        private ViolatorDetailsModel _violator;

        public ViolatorDetailsModel Violator
        {
            get
            {
                return _violator;
            }

            set
            {
                _violator = value;
                if (value != null) RenderDataSource();
            }
        }

        private void RenderDataSource()
        {
            lbFIO.Text = Violator.FIO;
            lbPlaceBerth.Text = Violator.PlaceBerth;
            lbPlaceWork.Text = Violator.PlaceBerth;
            lbDateBerth.Text = Violator.DateBerth.ToShortDateString();

            string phones = "";

            if (Violator.Phones != null)
                foreach (var phone in Violator.Phones)
                    phones += phone.PhoneNumber + "; ";

            lbPhones.Text = phones;

            tableDocuments.DataSource = Violator.Documents;

            tableAddresses.DataSource = Violator.Addresses;

            tableViolations.DataSource = Violator.Violations;

            avatarPanel.Controls.Clear();

            PictureViewer picture = new PictureViewer();

            picture.Image = Violator.Image;

            picture.Margin = new Padding(10, 10, 10, 10);

            picture.Dock = DockStyle.Left;

            picture.ShowDeleteButton = false;
            picture.ShowZoomButton = false;
            picture.CanSelected = false;

            avatarPanel.Controls.Add(picture);
        }

        public event Action<int> ShowDetailsViolation;

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {

        }

        private void tableAddresses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= tableAddresses.Rows.Count) return;
            tableAddresses.Rows[e.RowIndex].Selected = true;
        }

        private void tableDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= tableDocuments.Rows.Count) return;
            tableDocuments.Rows[e.RowIndex].Selected = true;
        }

        private int selectedViolationID = -1;

        private void tableViolations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= tableViolations.Rows.Count) return;

            tableViolations.Rows[e.RowIndex].Selected = true;

            btnViolationDetails.Enabled = true;

            selectedViolationID = Violator.Violations[e.RowIndex].ViolationID;
        }

        private void btnViolationDetails_Click(object sender, EventArgs e)
        {
            if (selectedViolationID == -1) return;

            ShowDetailsViolation(selectedViolationID);
        }
    }
}
