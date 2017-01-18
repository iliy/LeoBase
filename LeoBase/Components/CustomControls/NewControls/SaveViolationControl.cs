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
using System.IO;
using AppPresentators;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class SaveViolationControl : UserControl, ISaveAdminViolationControl
    {
        private Button _saveButton;
        public SaveViolationControl()
        {
            InitializeComponent();

            _saveButton = new Button();
            _saveButton.Text = "Сохранить";

            cmbDocument.SelectedIndexChanged += (s, e) => SelectedNewDocument();

            _saveButton.Click += (s, e) =>
            {
                if (Save != null && ValidateData()) {
                    try { 
                        CreateViolation();
                        Save();
                    }catch(Exception ex)
                    {
                        ShowError(ex.Message);
                    }
                }
            };

            btnFindEmployer.Click += (s, e) =>
            {
                if (FindEmployer != null) FindEmployer();
            };

            btnCreateEmployer.Click += (s, e) =>
            {
                if (CreateEmployer != null) CreateEmployer();
            };

            btnCreateViolator.Click += (s, e) =>
            {
                if (CreateViolator != null) CreateViolator();
            };

            btnFindViolator.Click += (s, e) =>
            {
                if (FindViolator != null) FindViolator();
            };

            tbCoordsCreatedProtocolE.KeyPress += RealListener;

            tbCoordsCreatedProtocolN.KeyPress += RealListener;

            tbCoordsViolationE.KeyPress += RealListener;

            tbCoordsViolationN.KeyPress += RealListener;

            tbSumRecovery.KeyPress += RealListener;

            tbSumViolation.KeyPress += RealListener;

            chbWasPaymant.CheckedChanged += chbWasPaymant_CheckedChanged;

            chbGotPersonaly.CheckedChanged += chbGotPersonaly_CheckedChanged;

            chbWasSent.CheckedChanged += chbWasSent_CheckedChanged;

            chbWasReceiving.CheckedChanged += chbWasReceiving_CheckedChanged;

            chbWasSentBailiff.CheckedChanged += chbWasSentBailiff_CheckedChanged;

            chbWasNotice.CheckedChanged += chbWasNotice_CheckedChanged;

            chbWasAgenda2025.CheckedChanged += chbWasAgenda2025_CheckedChanged;
        }

        private void RealListener(object s, KeyPressEventArgs e)
        {
            if (e.KeyChar == 46 && (((TextBox)s).Text.Contains(".")))
            {
                e.Handled = true;
            }
            else if (!Char.IsDigit(e.KeyChar) && !e.KeyChar.ToString().Equals(".") && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private bool ValidateData()
        {
            if(Violation == null)
            {
                MessageBox.Show("Информация заполнена не верно");
                return false;
            }

            if (Violation.Employer == null) {
                MessageBox.Show("Не выбран сотрудник");
                return false;
            }

            if(Violation.ViolatorPersone == null)
            {
                MessageBox.Show("Не выбран нарушитель");
                return false;
            }

            if(Violation.ViolatorDocument == null)
            {
                MessageBox.Show("Не выбран документ правонарушителя");
                return false;
            }

            return true;
        }

        public bool ShowForResult { get; set; }

        public List<Control> TopControls { get
            {
                return new List<Control>
                {
                    _saveButton
                };
            }
            set
            {

            }
        }


        private AdminViolation _violation;
        public AdminViolation Violation {
            get
            {
                if(_violation == null)
                {
                    _violation = new AdminViolation
                    {
                        Images = new List<ViolationImage>()
                    };
                }
                return _violation;
            }
            set
            {
                _violation = value;

                lbTitle.Text = value == null || value.Employer == null ? "Создание правонарушения" : "Редактирование правонарушения";

                if(_violation != null)
                    UpdateDataSource();
            }
        }

        public event Action CreateEmployer;
        public event Action CreateViolator;
        public event Action FindEmployer;
        public event Action FindViolator;
        public event Action Save;

        
        public Control GetControl()
        {
            return this;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        void UIComponent.Resize(int width, int height)
        {

        }
        public void UpdateDataSource()
        {
            UpdateEmployer();
            UpdateViolator();
            UpdateImages();

            tbPlaceCreatedProtocol.Text = Violation.PlaceCreatedProtocol;

            tbCoordsCreatedProtocolN.Text = Violation.CreatedProtocolN.ToString().Replace(',', '.');
            tbCoordsCreatedProtocolE.Text = Violation.CreatedProtocolE.ToString().Replace(',', '.');

            tbCoordsViolationN.Text = Violation.ViolationN.ToString().Replace(',', '.');
            tbCoordsViolationE.Text = Violation.ViolationE.ToString().Replace(',', '.');

            dpCreatedProtocol.Value = Violation.DateCreatedProtocol;

            dpTimeCreatedProtocol.Value = Violation.DateCreatedProtocol;

            dpViolationDate.Value = Violation.DateViolation;

            dpTimeViolation.Value = Violation.DateViolation;

            tbDescriptionViolation.Text = Violation.ViolationDescription;

            tbKOAP.Text = Violation.KOAP;
            tbFindedNatureManagementProducts.Text = Violation.FindedNatureManagementProducts;
            tbFindedGunsHuntingAndFishing.Text = Violation.FindedGunsHuntingAndFishing;
            tbFindedWeapons.Text = Violation.FindedWeapons;
            tbWitnessFIO_1.Text = Violation.WitnessFIO_1;
            tbWitnessFIO_2.Text = Violation.WitnessFIO_2;
            tbWitnessLive_1.Text = Violation.WitnessLive_1;
            tbWitnessLive_2.Text = Violation.WitnessLive_2;


            // Информация по постановлению
            tbRulingNumber.Text = Violation.RulingNumber;

            dpConsideration.Value = Violation.Consideration;

            dpTimeConsideration.Value = Violation.Consideration;

            tbViolation.Text = Violation.Violation;

            chbWasPaymant.Checked = Violation.WasPaymant;

            if (Violation.WasPaymant) { 
                dpDatePaymant.Value = Violation.DatePaymant;
                
                tbSumRecovery.Text = Math.Round(Violation.SumRecovery, 2).ToString().Replace(',', '.');

                dpDatePaymant.Enabled = true;

                tbSumRecovery.Enabled = true;
            }else
            {
                dpDatePaymant.Enabled = false;

                tbSumRecovery.Enabled = false;
            }


            tbSumViolation.Text = Math.Round(Violation.SumViolation, 2).ToString().Replace(',', '.');
            
            if (Violation.GotPersonaly)
            {
                chbGotPersonaly.Checked = true;

                chbWasSent.Enabled = false;

                dpDateSent.Enabled = false;

                tbNumberSent.Enabled = false;
            }else
            {
                chbGotPersonaly.Checked = false;

                chbWasSent.Checked = Violation.WasSent;

                dpDateSent.Enabled = false;

                tbNumberSent.Enabled = false;

                if (Violation.WasSent) {

                    dpDateSent.Enabled = true;

                    tbNumberSent.Enabled = true;

                    dpDateSent.Value = Violation.DateSent;

                    tbNumberSent.Text = Violation.NumberSent;
                }
            }

            chbWasReceiving.Checked = Violation.WasReceiving;

            if (Violation.WasReceiving) {
                dpDateReceiving.Enabled = true;

                dpDateReceiving.Value = Violation.DateReceiving;
            }else
            {
                dpDateReceiving.Enabled = false;
            }

            chbWasSentBailiff.Checked = Violation.WasSentBailiff;

            if (Violation.WasSentBailiff)
            {
                dpDateSentBailiff.Value = Violation.DateSentBailiff;

                tbNumberSentBailigg.Text = Violation.NumberSentBailigg;

                dpDateSentBailiff.Enabled = true;

                tbNumberSentBailigg.Enabled = true;
            }else
            {
                dpDateSentBailiff.Enabled = false;

                tbNumberSentBailigg.Enabled = false;
            }

            chbWasNotice.Checked = Violation.WasNotice;

            if (Violation.WasNotice) { 
                dpDateNotice.Value = Violation.DateNotice;

                tbNumberNotice.Text = Violation.NumberNotice;

                dpDateNotice.Enabled = true;

                tbNumberNotice.Enabled = true;
            }
            else
            {
                dpDateNotice.Enabled = false;

                tbNumberNotice.Enabled = false;
            }

            chbWasAgenda2025.Checked = Violation.WasAgenda2025;

            if (Violation.WasAgenda2025) { 
                dpDateAgenda2025.Value = Violation.DateAgenda2025;

                dpDateAgenda2025.Enabled = true;
            }
            else
            {
                dpDateAgenda2025.Enabled = false;
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CreateViolation()
        {
            if (string.IsNullOrWhiteSpace(tbPlaceCreatedProtocol.Text)) throw new Exception("Не указано место составления протокола");
            Violation.PlaceCreatedProtocol = tbPlaceCreatedProtocol.Text;

            if (string.IsNullOrWhiteSpace(tbCoordsCreatedProtocolN.Text) || string.IsNullOrEmpty(tbCoordsCreatedProtocolE.Text))
                throw new Exception("Не указаны координаты составления протокола");
            Violation.CreatedProtocolN = Convert.ToDouble(tbCoordsCreatedProtocolN.Text.Replace('.', ','));
            Violation.CreatedProtocolE = Convert.ToDouble(tbCoordsCreatedProtocolE.Text.Replace('.', ','));

            if (string.IsNullOrWhiteSpace(tbCoordsViolationE.Text) || string.IsNullOrWhiteSpace(tbCoordsViolationN.Text))
                throw new Exception("Не указаны координаты правонарушения");
            Violation.ViolationN = Convert.ToDouble(tbCoordsViolationN.Text.Replace('.', ','));
            Violation.ViolationE = Convert.ToDouble(tbCoordsViolationE.Text.Replace('.', ','));
            
            Violation.DateCreatedProtocol = dpCreatedProtocol.Value.Date + dpTimeCreatedProtocol.Value.TimeOfDay;

            Violation.DateViolation = dpViolationDate.Value.Date + dpTimeViolation.Value.TimeOfDay;
            
            Violation.ViolationDescription = tbDescriptionViolation.Text;

            Violation.KOAP = tbKOAP.Text;

            Violation.FindedNatureManagementProducts = tbFindedNatureManagementProducts.Text;

            Violation.FindedGunsHuntingAndFishing = tbFindedGunsHuntingAndFishing.Text;

            Violation.FindedWeapons = tbFindedWeapons.Text;

            Violation.WitnessFIO_1 = tbWitnessFIO_1.Text;
            Violation.WitnessFIO_2 = tbWitnessFIO_2.Text;
            Violation.WitnessLive_1 = tbWitnessLive_1.Text;
            Violation.WitnessLive_2 = tbWitnessLive_2.Text;

            Violation.RulingNumber = tbRulingNumber.Text;

            Violation.Consideration = dpConsideration.Value.Date + dpTimeConsideration.Value.TimeOfDay;

            Violation.Violation = tbViolation.Text;
            Violation.DatePaymant = dpDatePaymant.Value;

            try { 
                Violation.SumViolation = Convert.ToDecimal(tbSumViolation.Text.Replace('.', ','));
            }catch(Exception e)
            {
                Violation.SumViolation = 0;
            }

            if (tbSumRecovery.Text == "") tbSumRecovery.Text = "0";

            Violation.SumRecovery = Convert.ToDecimal(tbSumRecovery.Text.Replace('.', ','));

            Violation.GotPersonaly = chbGotPersonaly.Checked;

            if (Violation.GotPersonaly)
            {
                Violation.DateSent = Violation.Consideration;
                Violation.NumberSent = "";
            }else
            {
                Violation.DateSent = dpDateSent.Value;
                Violation.NumberSent = tbNumberSent.Text;
            }

            Violation.DateReceiving = dpDateReceiving.Value;
            Violation.DateSentBailiff = dpDateSentBailiff.Value;
            Violation.NumberSentBailigg = tbNumberSentBailigg.Text;
            Violation.DateNotice = dpDateNotice.Value;
            Violation.NumberNotice = tbNumberNotice.Text;
            Violation.DateAgenda2025 = dpDateAgenda2025.Value;

        }

        

        public void UpdateEmployer()
        {
            if (Violation.Employer == null) return;

            string fio = Violation.Employer.FirstName + " " + Violation.Employer.SecondName + " " + Violation.Employer.MiddleName;
            string position = "";

            if (ConfigApp.EmploerPositions.ContainsKey(Violation.Employer.Position_PositionID))
            {
                position = ConfigApp.EmploerPositions[Violation.Employer.Position_PositionID];
            }

            lbEmployerFIO.Text = fio;
            lbEmployerPosition.Text = position;
        }

        public void UpdateViolator()
        {
            if (Violation.ViolatorPersone == null) return;

            string fio = Violation.ViolatorPersone.FirstName + " " + Violation.ViolatorPersone.SecondName + " " + Violation.ViolatorPersone.MiddleName;

            lbViolatorFIO.Text = fio;

            List<string> documents = new List<string>();

            cmbDocument.Items.Clear();

            foreach (var doc in Violation.ViolatorPersone.Documents)
            {
                string doc_str = "";

                if (ConfigApp.DocumentsType.ContainsKey(doc.Document_DocumentTypeID)) doc_str = ConfigApp.DocumentsType[doc.Document_DocumentTypeID] + " ";

                doc_str += doc.Serial + " " + doc.Number + " " + doc.IssuedBy + " " + doc.WhenIssued.ToShortDateString();

                cmbDocument.Items.Add(doc_str);                
            }

            cmbDocument.Enabled = true;

            cmbDocument.SelectedIndex = 0;

            foreach(var address in Violation.ViolatorPersone.Address)
            {
                if (address.Note == null || address.Note.Equals("Проживает и прописан"))
                {
                    lbViolatorLive.Text = address.ToString();
                    lbViolatorRegistration.Text = address.ToString();
                    break;
                }
                else if (address.Note != null && address.Note.Equals("Проживает"))
                {
                    lbViolatorLive.Text = address.ToString();
                }
                else
                {
                    lbViolatorRegistration.Text = address.ToString();
                }
            }

            if(string.IsNullOrWhiteSpace(lbViolatorLive.Text) && !string.IsNullOrWhiteSpace(lbViolatorRegistration.Text))
            {
                lbViolatorLive.Text = lbViolatorRegistration.Text;
            }else if(string.IsNullOrWhiteSpace(lbViolatorRegistration.Text) && !string.IsNullOrWhiteSpace(lbViolatorLive.Text))
            {
                lbViolatorRegistration.Text = lbViolatorLive.Text;
            }

            lbViolatorWork.Text = Violation.ViolatorPersone.PlaceWork;

            if (string.IsNullOrWhiteSpace(lbViolatorWork.Text)) lbViolatorWork.Text = "Временно безработный";

            lbViolatorDateB.Text = Violation.ViolatorPersone.DateBirthday.ToShortDateString();

            lbViolatorPlaceB.Text = Violation.ViolatorPersone.PlaceOfBirth;

            if (Violation.ViolatorPersone.Phones != null && Violation.ViolatorPersone.Phones.Count != 0)
                lbViolatorPhone.Text = Violation.ViolatorPersone.Phones.First().PhoneNumber;
        }

        private void SelectedNewDocument()
        {
            if (Violation.ViolatorPersone == null || Violation.ViolatorPersone.Documents == null) return;

            Violation.ViolatorDocument = Violation.ViolatorPersone.Documents[cmbDocument.SelectedIndex];
        }
        

        private void UpdateImages()
        {
            photosList.Controls.Clear();

            foreach (var image in Violation.Images)
            {
                PictureViewer pv = new PictureViewer();

                pv.OnDeleteClick += (p) =>
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить это избражение?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        this.photosList.Controls.Remove(pv);
                        _violation.Images.Remove(image);
                    }
                };

                pv.Image = image.Image;

                photosList.Controls.Add(pv);
            }
        }
        private void chbGotPersonaly_CheckedChanged(object sender, EventArgs e)
        {
            Violation.GotPersonaly = chbGotPersonaly.Checked;

            chbWasSent.Enabled = !Violation.GotPersonaly;

            if (Violation.GotPersonaly)
            {
                dpDateSent.Enabled = false;
                tbNumberSent.Enabled = false;
            }
            else
            {

                dpDateSent.Value = Violation.DateSent;
                tbNumberSent.Text = Violation.NumberSent;

                dpDateSent.Enabled = chbWasSent.Checked;
                tbNumberSent.Enabled = chbWasSent.Checked;
            }
        }

        private void chbWasPaymant_CheckedChanged(object sender, EventArgs e)
        {
            Violation.WasPaymant = chbWasPaymant.Checked;
            if (Violation.WasPaymant)
            {
                dpDatePaymant.Enabled = true;
                tbSumRecovery.Enabled = true;
            }else
            {
                dpDatePaymant.Enabled = false;
                tbSumRecovery.Enabled = false;
            }
        }

        private void chbWasSent_CheckedChanged(object sender, EventArgs e)
        {
            Violation.WasSent = chbWasSent.Checked;

            if (Violation.WasSent)
            {
                dpDateSent.Enabled = true;
                tbNumberSent.Enabled = true;
            }else
            {
                dpDateSent.Enabled = false;
                tbNumberSent.Enabled = false;
            }
        }

        private void chbWasReceiving_CheckedChanged(object sender, EventArgs e)
        {
            Violation.WasReceiving = chbWasReceiving.Checked;
            if (Violation.WasReceiving)
            {
                dpDateReceiving.Enabled = true;
            }else
            {
                dpDateReceiving.Enabled = false;
            }
        }

        private void chbWasSentBailiff_CheckedChanged(object sender, EventArgs e)
        {
            Violation.WasSentBailiff = chbWasSentBailiff.Checked;

            if (Violation.WasSentBailiff)
            {
                dpDateSentBailiff.Enabled = true;
                tbNumberSentBailigg.Enabled = true;
            }else
            {
                dpDateSentBailiff.Enabled = false;
                tbNumberSentBailigg.Enabled = false;
            }
        }

        private void chbWasNotice_CheckedChanged(object sender, EventArgs e)
        {
            Violation.WasNotice = chbWasNotice.Checked;
            if (Violation.WasNotice)
            {
                dpDateNotice.Enabled = true;
                tbNumberNotice.Enabled = true;
            }else
            {
                dpDateNotice.Enabled = false;
                tbNumberNotice.Enabled = false;
            }
        }

        private void chbWasAgenda2025_CheckedChanged(object sender, EventArgs e)
        {
            Violation.WasAgenda2025 = chbWasAgenda2025.Checked;

            if (Violation.WasAgenda2025)
            {
                dpDateAgenda2025.Enabled = true;
            }else
            {
                dpDateAgenda2025.Enabled = false;
            }
        }

        private void btnAddPhoto_Click_1(object sender, EventArgs e)
        {
            imageDialog.Filter = "Images (*.jpeg,*.png,*.jpg,*.gif) |*.jpeg;*.png;*.jpg;*.gif";

            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < imageDialog.FileNames.Length; i++)
                {
                    FileStream fs = new FileStream(imageDialog.FileNames[i], FileMode.Open, FileAccess.Read);

                    byte[] data = new byte[fs.Length];

                    fs.Read(data, 0, System.Convert.ToInt32(fs.Length));

                    fs.Close();

                    string name = imageDialog.SafeFileNames[i].Split(new[] { '.' })[0];

                    var violationImage = new ViolationImage
                    {
                        Image = data,
                        Name = name
                    };

                    Violation.Images.Add(violationImage);

                    PictureViewer pv = new PictureViewer();

                    pv.OnDeleteClick += (p) =>
                    {
                        if (MessageBox.Show("Вы уверены, что хотите удалить это избражение?", "Предупреждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                        {
                            this.photosList.Controls.Remove(pv);
                            _violation.Images.Remove(violationImage);
                        }
                    };

                    pv.Image = data;

                    photosList.Controls.Add(pv);
                }
            }
        }
    }
}
