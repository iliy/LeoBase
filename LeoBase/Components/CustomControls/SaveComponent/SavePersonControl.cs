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
using AppPresentators.VModels.Persons;
using LeoBase.Forms;
using System.IO;
using LeoBase.Components.CustomControls.SaveComponent.SavePhone;
using LeoBase.Components.CustomControls.SaveComponent.SaveAddress;
using LeoBase.Components.CustomControls.SaveComponent.SaveDocument;
using AppData.Entities;

namespace LeoBase.Components.CustomControls.SaveComponent
{
    public partial class SavePersonControl : UserControl, ISavePersonControl
    {
        public bool ShowForResult { get; set; } = false;
        private string ErrorMessage { get; set; } = "";

        private bool _wasCreated { get; set; } = true;

        public event Action SavePersone;

        private PhonesList Phones = new PhonesList();
        private AddressesList Addresses = new AddressesList();
        private DocumentsList Documents = new DocumentsList();

        public List<Control> TopControls { get; set; }
        private Button _saveButton;
        public SavePersonControl()
        {
            InitializeComponent();

            _saveButton = new Button();
            _saveButton.Text = "Сохранить";
            TopControls = new List<Control> { _saveButton };

            _saveButton.Click += (s, e) =>
            {
                if (SavePersone != null) SavePersone();
            };

            tableLayoutPanel1.Controls.Add(Phones, 0, 2);
            tableLayoutPanel1.Controls.Add(Addresses, 0, 3);
            
            Phones.Phones = new List<PhoneViewModel>
                    {
                        new PhoneViewModel { }
                    };

            Addresses.Addresses = new List<PersonAddressModelView>
                    {
                        new PersonAddressModelView {
                        Country = "Российская Федерация",
                        Subject = "Приморский край",
                        Area = "Хасанский район"
                        }
                    };

            if (!IsEmployer) { 
                tableLayoutPanel1.Controls.Add(Documents, 0, 4);
                Documents.Documents = new List<PersoneDocumentModelView>
                        {
                            new PersoneDocumentModelView
                            {
                                DocumentID = -1,
                                WhenIssued = new DateTime(1999, 1, 1)
                            }
                        };
            }

        }

        private bool IsEmployer { get; set; } = false;
        private DateTime _wasBeCreated { get; set; }
        private int UserID { get; set; }

        private Dictionary<int, string> _positions = new Dictionary<int, string>();
        public Dictionary<int, string> Positions
        {
            get
            {
                return _positions;
            }
            set
            {
                _positions = value;

                cmbPosition.Items.Clear();

                cmbPosition.Refresh();

                foreach(var p in value)
                {
                    cmbPosition.Items.Add(p.Value);
                }
            }
        }
        public IPersoneViewModel Persone
        {
            get
            {
                var result = MakePersonViewModel();

                if (result == null)
                {
                    ShowMessage(ErrorMessage);
                }

                return result;
            }

            set
            {
                if (value == null) return;

                IsEmployer = value.IsEmploeyr;
                
                if (value.IsEmploeyr)
                {
                    // Если это сотрудник то показываем дополнительные поля
                    //pageTitle.Text = "Добавление нового сотрудника";
                    cmbPosition.Visible = true;
                    lbPosition.Visible = true;
                    lbWorkPlace.Visible = false;
                    tbWorkPlace.Visible = false;
                }else
                {
                    //pageTitle.Text = "Добавление нового нарушителя";
                    tbWorkPlace.Visible = true;
                    lbWorkPlace.Visible = true;
                    lbPosition.Visible = false;
                    cmbPosition.Visible = false;
                }
                if(value.UserID > 0)
                {
                    // Заполняем данные
                    UserID = value.UserID;
                    tbFirstName.Text = value.FirstName;
                    tbSecondName.Text = value.SecondName;
                    tbMiddleName.Text = value.MiddleName;
                    tbPlaceBerth.Text = value.PlaceOfBirth;
                    dtpDateBerth.Value = value.DateBirthday;
                    _wasBeCreated = value.WasBeCreated;


                    Phones.Phones = value.Phones;
                    Addresses.Addresses = value.Addresses;
                    Documents.Documents = value.Documents;

                    if (value.Image != null && value.Image.Length != 0)
                    {
                        pbAvatar.BackgroundImage = ImageFromByteArray(value.Image);
                        pbAvatar.BackgroundImageLayout = ImageLayout.Stretch;
                    }

                    if (IsEmployer)
                    {
                        var emploeyr = (EmploeyrViewModel)value;
                        for (int i = 0; i < cmbPosition.Items.Count; i++)
                        {
                            if (cmbPosition.Items[i].ToString().Equals(emploeyr.Position))
                            {
                                cmbPosition.SelectedIndex = i;
                                break;
                            }
                        }
                    }else
                    {
                        var violator = (ViolatorViewModel)value;

                        tbWorkPlace.Text = violator.PlaceOfWork;
                    }
                    _wasCreated = false;
                }
                else
                {
                    
                    _wasCreated = true;
                }

                PageTitle.Text = (_wasCreated ? "Добавление " : "Редактирование ") + (IsEmployer ? "сотрудника" : "нарушителя"); 
            }
        }

        //Persone ISavePersonControl.Persone
        //{
        //    get
        //    {
        //        throw new NotImplementedException();
        //    }

        //    set
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        private Image ImageFromByteArray(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

        public byte[] ImageToByteArray(Image imageIn)
        {
            if (imageIn == null) return new byte[0];

            using (var ms = new MemoryStream())
            {
                var a = System.Drawing.Imaging.ImageFormat.Jpeg;
                var format = new System.Drawing.Imaging.ImageFormat(imageIn.RawFormat.Guid);
                imageIn.Save(ms, a);
                return ms.ToArray();
            }
        }

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

        private IPersoneViewModel MakePersonViewModel()
        {
            IPersoneViewModel result = null;

            if (IsEmployer)
            {
                if (cmbPosition.SelectedIndex < 0)
                {
                    ErrorMessage = "Не выбрана должность для сотрудника";
                    return null;
                }

                string position = cmbPosition.Items[cmbPosition.SelectedIndex].ToString();

                if (string.IsNullOrEmpty(position))
                {
                    ErrorMessage = "Не выбрана должность для сотрудника";
                    return null;
                }

                if (!_positions.ContainsValue(position))
                {
                    ErrorMessage = "Неизвестная должность для сотрудника";
                    return null;
                }

                int positionID = _positions.FirstOrDefault(p => p.Value.Equals(position)).Key;

                EmploeyrViewModel employer = new EmploeyrViewModel
                {
                    Position = position,
                    PositionID = positionID.ToString(),
                    IsEmploeyr = true
                };

                result = employer;
            }else
            {
                if(string.IsNullOrWhiteSpace(tbWorkPlace.Text))
                {
                    ErrorMessage = "Не указано место работы";
                    return null;
                }

                var violator = new ViolatorViewModel
                {
                    PlaceOfWork = tbWorkPlace.Text,
                    IsEmploeyr = false
                };

                result = violator;
            }

            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                ErrorMessage = "Не указана фамилия";
                return null;
            }

            if (string.IsNullOrWhiteSpace(tbSecondName.Text))
            {
                ErrorMessage = "Не указано имя";
                return null;
            }

            if (string.IsNullOrWhiteSpace(tbMiddleName.Text))
            {
                ErrorMessage = "Не указано отчество";
                return null;
            }

            if (string.IsNullOrWhiteSpace(tbPlaceBerth.Text))
            {
                ErrorMessage = "Не указано место рождения";
                return null;
            }

            result.Phones = Phones.Phones;
            result.Addresses = Addresses.Addresses;

            if (!IsEmployer)
                result.Documents = Documents.Documents;

            result.DateBirthday = dtpDateBerth.Value;
            result.FirstName = tbFirstName.Text;

            if (!string.IsNullOrEmpty(_fileName))
            {
                FileStream fs = new FileStream(_fileName, FileMode.Open, FileAccess.Read);

                byte[] data = new byte[fs.Length];

                fs.Read(data, 0, System.Convert.ToInt32(fs.Length));

                fs.Close();
                
                string dd = data.ToString();
                result.Image = data;
            }else if (pbAvatar.BackgroundImage == null)
            {
                result.Image = new byte[0];
            }

            result.MiddleName = tbMiddleName.Text;
            result.SecondName = tbSecondName.Text;
            result.PlaceOfBirth = tbPlaceBerth.Text;

            if (_wasCreated)
            {
                result.WasBeUpdated = DateTime.Now;
                result.WasBeCreated = DateTime.Now;
            }else
            {
                result.UserID = UserID;
                result.WasBeUpdated = DateTime.Now;
                result.WasBeCreated = _wasBeCreated;
            }

            return result;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (SavePersone != null) SavePersone();
        }

        private string _fileName = null;

        private void btnImageOpen_Click_1(object sender, EventArgs e)
        {
            openImageDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.PNG;*.GIF;";
            if (openImageDialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Image.FromFile(openImageDialog.FileName);
                pbAvatar.BackgroundImage = image;
                pbAvatar.BackgroundImageLayout = ImageLayout.Stretch;
                _fileName = openImageDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _fileName = null;
            pbAvatar.BackgroundImage = null;
        }
    }
}
