﻿using System;
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
    public partial class EmployerDetailsControl : UserControl, IEmployerDetailsControl
    {
        public event Action MakeReport;
        public event Action EditEmployer;

        public EmployerDetailsControl()
        {
            InitializeComponent();

            this.Load += EmployerDetailsControl_Load;
        }

        private void EmployerDetailsControl_Load(object sender, EventArgs e)
        {
            tableAddresses.ClearSelection();
            tableViolations.ClearSelection();
        }

        private EmployerDetailsModel _employer;

        private int _selectedViolationID = -1;

        public EmployerDetailsModel Employer
        {
            get
            {
                return _employer;
            }

            set
            {
                _employer = value;
                if(value != null)
                {
                    UpdateControlData();
                }
            }
        }

        public bool ShowForResult { get; set; }

        public List<Control> TopControls
        {
            get
            {
                var reportButton = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);
                var editButton = new PictureButton(Properties.Resources.editEnabled, Properties.Resources.editDisabled, Properties.Resources.editPress);

                reportButton.Enabled = true;
                editButton.Enabled = true;

                reportButton.Click += (s, e) =>
                {
                    if (MakeReport != null) MakeReport();
                };

                editButton.Click += (s, e) =>
                {
                    if (EditEmployer != null) EditEmployer();
                };

                return new List<Control> { reportButton, editButton };
            }

            set
            {

            }
        }

        public event Action<int> ShowViolationDetails;

        public Control GetControl()
        {
            return this;
        }

        void UIComponent.Resize(int width, int height)
        {

        }

        private void UpdateControlData()
        {
            lbFIO.Text = Employer.FIO;
            lbDateBerth.Text = Employer.DateBerth;

            if (Employer.Phones != null)
                foreach (var p in Employer.Phones)
                    lbPhones.Text += p.PhoneNumber + "; ";

            lbPlaceBerth.Text = Employer.PlaceBerth;

            lbPosition.Text = Employer.Position;

            if(Employer.Image != null) { 
                PictureViewer picture = new PictureViewer();

                picture.Image = Employer.Image;

                picture.Margin = new Padding(10, 10, 10, 10);

                picture.Dock = DockStyle.Left;

                picture.ShowDeleteButton = false;
                picture.ShowZoomButton = false;
                picture.CanSelected = false;

                avatarPanel.Controls.Clear();

                avatarPanel.Controls.Add(picture);
            }

            tableAddresses.DataSource = Employer.Addresses;

            tableViolations.DataSource = Employer.Violations;
            
            tableAddresses.ClearSelection();
            tableViolations.ClearSelection();
        }

        private void tableAddresses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= tableAddresses.Rows.Count) return;

            tableAddresses.Rows[e.RowIndex].Selected = true;
        }

        private void tableViolations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= tableViolations.Rows.Count) return;

            tableViolations.Rows[e.RowIndex].Selected = true;

            _selectedViolationID = Employer.Violations[e.RowIndex].ViolationID;

            btnViolationDetails.Enabled = true;
        }

        private void btnViolationDetails_Click(object sender, EventArgs e)
        {
            if (_selectedViolationID <= 0 || ShowViolationDetails == null) return;

            ShowViolationDetails(_selectedViolationID);
        }
    }
}
