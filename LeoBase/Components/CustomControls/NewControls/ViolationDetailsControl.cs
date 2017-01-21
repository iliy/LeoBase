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
using System.IO;
using LeoMapV3.Map;
using LeoMapV3.Data;
using LeoBase.Components.TopMenu;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class ViolationDetailsControl : UserControl, IViolationDetailsControl
    {
        private Button _btnReport;
        private List<Control> _topControls;
        
        public event Action<int> ShowEmployerDetails;
        public event Action<int> ShowViolatorDetails;
        public event Action EditViolation;

        private LeoMap _map;
        private TitleDBRepository _titleRepo;

        public ViolationDetailsControl()
        {
            InitializeComponent();

            var reportButton = new PictureButton(Properties.Resources.reportEnabled, Properties.Resources.reportDisabled, Properties.Resources.reportPress);
            var editButton = new PictureButton(Properties.Resources.editEnabled, Properties.Resources.editDisabled, Properties.Resources.editPress);

            editButton.Enabled = true;
            reportButton.Enabled = true;

            editButton.Click += (s, e) =>
            {
                if (EditViolation != null) EditViolation();
            };

            reportButton.Click += (s, e) =>
            {
                if (Report != null) Report();
            };

            _topControls = new List<Control>();

            _topControls.Add(reportButton);
            _topControls.Add(editButton);
        }

        public bool ShowForResult { get; set; }

        public List<Control> TopControls
        {
            get
            {
                return _topControls;
            }

            set { }
        }

        private AdminViolation _violation;
        public AdminViolation Violation
        {
            get
            {
                return _violation;
            }

            set
            {
                _violation = value;
                if (value != null) RenderDataSource();
            }
        }

        public event Action Report;
        
        public Control GetControl()
        {
            return this;
        }

        private void RenderDataSource()
        {
            RenderProtocolInformation();
            RenderRulingInformation();
            RenderImages();
        }

        private void RenderImages()
        {
            if (Violation.Images == null)
            {
                groupBox3.Visible = false;
                return;
            }

            int x = 5;
            int y = 5;

            int count = 0;

            foreach(var image in Violation.Images)
            {
                PictureViewer pv = new PictureViewer();

                pv.CanSelected = true;

                pv.ShowDeleteButton = false;

                pv.Image = image.Image;

                pv.Left = x;
                pv.Top = y;

                count++;

                if(count % 5 != 0)
                {
                    x += pv.Width + 10;
                }else
                {
                    count = 0;
                    x = 5;
                    y += pv.Height + 10;
                }

                pv.ImageName = image.Name;

                imagePanel.Controls.Add(pv);
            }
        }

        private void RenderRulingInformation()
        {
            lbRulingNumber.Text = Violation.RulingNumber;
            lbDateConsideration.Text = Violation.Consideration.ToString();
            lbViolation.Text = Violation.Violation;
            lbSumViolation.Text = Math.Round(Violation.SumViolation, 2).ToString();

            if (Violation.GotPersonaly)
            {
                lbSent.Text = "Получил лично";
            } else if (Violation.WasSent)
            {
                lbSent.Text = string.Format("Отправлено: {0}; Номер отправления: {1}", Violation.DateSent.ToShortDateString(), Violation.NumberSent);
            }
            else
            {
                lbSent.Text = "Не отправлено";
            }

            if (Violation.WasSentBailiff)
            {
                lbSentBailiff.Text = string.Format("Отправлено: {0}; Номер отправления: {1}", Violation.DateSentBailiff.ToShortDateString(), Violation.NumberSentBailigg);
            }else
            {
                lbSentBailiff.Text = "Не отправлялось";
            }

            if (Violation.WasNotice)
            {
                lbNotice.Text = string.Format("Извещение получено: {0}; Номер извещения: {1} ", Violation.DateNotice.ToShortDateString(), Violation.NumberNotice);
            }else
            {
                lbNotice.Text = "Извещения не было";
            }

            if (Violation.WasAgenda2025)
            {
                lbAgenda2025.Text = Violation.DateAgenda2025.ToShortDateString();
            }
            else
            {
                lbAgenda2025.Text = "Повестки по статье 20.25 не было";
            }

            if (Violation.WasPaymant)
            {
                if(Violation.SumRecovery >= Violation.SumViolation)
                {
                    lbPaymant.Text = "Оплачено " + Violation.DatePaymant.ToShortDateString();
                    lbPaymant.ForeColor = Color.Green;
                }else
                {
                    lbPaymant.Text = string.Format("Оплачено не полностью ({0} из {1}): {2}", Violation.SumRecovery, Violation.SumViolation, Violation.DatePaymant.ToShortDateString());
                    lbPaymant.ForeColor = Color.Red;
                }
            }
            else
            {
                lbPaymant.Text = "Не оплачено";
                lbPaymant.ForeColor = Color.Red;
            }
        }

        private void RenderProtocolInformation()
        {
            lbPlaceCreatedProtocol.Text = Violation.PlaceCreatedProtocol;
            lbCoordsCreatedProtocol.Text = string.Format("N: {0}; E: {1}", Math.Round(Violation.CreatedProtocolN, 8), Math.Round(Violation.CreatedProtocolE, 8));
            lbDateTimeCreatedProtocol.Text = Violation.DateCreatedProtocol.ToString();
            lbCoordsViolation.Text = string.Format("N: {0}; E: {1}", Math.Round(Violation.ViolationN, 8), Math.Round(Violation.ViolationE, 8));
            lbDateTimeViolation.Text = Violation.DateViolation.ToString();
            lbKOAP.Text = Violation.KOAP;
            lbFindedNatureManagementProducts.Text = Violation.FindedNatureManagementProducts;
            lbFindedGunsHuntingAndFishing.Text = Violation.FindedGunsHuntingAndFishing;
            lbFindedWiapons.Text = Violation.FindedWeapons;
            lbWitnessFIO_1.Text = Violation.WitnessFIO_1;
            lbWitnessFIO_2.Text = Violation.WitnessFIO_2;
            lbWitnessLive_1.Text = Violation.WitnessLive_1;
            lbWitnessLive_2.Text = Violation.WitnessLive_2;
            lbViolationDetails.Text = Violation.ViolationDescription;

            lbEmployerInformation.Text = string.Format("{0} ; {1} {2} {3}", ConfigApp.EmploerPositions[Violation.Employer.Position_PositionID], Violation.Employer.FirstName, Violation.Employer.SecondName, Violation.Employer.MiddleName);

            string violatorInformation = string.Format("{0} {1} {2}; ", Violation.ViolatorPersone.FirstName, Violation.ViolatorPersone.SecondName, Violation.ViolatorPersone.MiddleName);

            violatorInformation += string.Format("Дата рождения: {0}; " , Violation.ViolatorPersone.DateBirthday.ToShortDateString());

            violatorInformation += string.Format("Место работы: {0}; ", Violation.ViolatorPersone.PlaceWork);

            violatorInformation += string.Format("Место рождения: {0}; ", Violation.ViolatorPersone.PlaceOfBirth);

            violatorInformation += string.Format("Документ: {0} {1} {2} {3} {4}", 
                                                            ConfigApp.DocumentsType[Violation.ViolatorDocument.Document_DocumentTypeID],
                                                            Violation.ViolatorDocument.Serial,
                                                            Violation.ViolatorDocument.Number,
                                                            Violation.ViolatorDocument.WhenIssued.ToShortDateString(),
                                                            Violation.ViolatorDocument.IssuedBy
                                                            );

            lbViolatorInformation.Text = violatorInformation;

            if(Violation.ViolationE != 0 && Violation.ViolationN != 0)
            {
                if (!File.Exists(ConfigApp.DefaultMapPath))
                {
                    MessageBox.Show("Не удается найти карту по указанному пути: " + ConfigApp.DefaultMapPath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    _titleRepo = new TitleDBRepository(ConfigApp.DefaultMapPath);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Не удается прочитать файл с картой: " + ConfigApp.DefaultMapPath, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _map = new LeoMap(_titleRepo);

                _map.ClearPoints();

                mapPanel.Controls.Clear();

                _map.Dock = DockStyle.Fill;
                
                var point = new LeoMapV3.Models.DPoint
                {
                    E = Violation.ViolationE,
                    N = Violation.ViolationN,
                    ToolTip = "Место правонарушения",
                    Zoom = ConfigApp.DefaultMapZoomForViolation
                };

                _map.SetPoint(point);

                _map.LookAt(point);

                //_map.MouseState = LeoMapV3.Models.MapDragAndDropStates.NONE;

                mapPanel.Controls.Add(_map);

                mapPanel.Visible = true;
            }
        }


        void UIComponent.Resize(int width, int height)
        {

        }

        private bool _wasSelectedAll = false;

        private void button1_Click(object sender, EventArgs e)
        {
            _wasSelectedAll = !_wasSelectedAll;

            button1.Text = _wasSelectedAll ? "Снять выделенное" : "Выделить все";

            foreach(var control in imagePanel.Controls)
            {
                var pv = control as PictureViewer;

                if (pv == null) continue;

                pv.Checked = _wasSelectedAll;
            }
        }

        private void btnSaveImages_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            browserDialog.Description = "Выберите папку, для сохранения выбранных изображений";

            if(browserDialog.ShowDialog() == DialogResult.OK)
            {
                string path = browserDialog.SelectedPath;

                if (!path.EndsWith("\\")) path += "\\";


                foreach (var control in imagePanel.Controls)
                {
                    var pv = control as PictureViewer;

                    if (pv == null) continue;

                    if (!pv.Checked) continue;

                    int index = 1;

                    string fileName = path + (pv.ImageName ?? (index).ToString()) + ".jpg";

                    while (File.Exists(fileName))
                    {
                        fileName = path + (string.IsNullOrEmpty(pv.ImageName) ? (index++).ToString() : pv.ImageName + (index++)) + ".jpg";
                    }

                    MemoryStream ms = new MemoryStream(pv.Image);

                    Image i = Image.FromStream(ms);

                    i.Save(fileName);
                }
            }
        }

        private void btnViolatorDetails_Click(object sender, EventArgs e)
        {
            if (ShowViolatorDetails != null)
            {
                ShowViolatorDetails(Violation.ViolatorPersone.UserID);
            }
        }

        private void btnEmployerDetails_Click(object sender, EventArgs e)
        {
            if(ShowEmployerDetails != null)
            {
                ShowEmployerDetails(Violation.Employer.UserID);
            }
        }
    }
}
