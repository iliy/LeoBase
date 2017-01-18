using FormControls.Controls.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormControls
{
    public class TestDataModel
    {

        private LeoDataComboBox _sex = new LeoDataComboBox("Муж") { "Муж", "Жен" };

        [DisplayName("Код")]
        public int Key { get; set; }

        public string Name { get; set; }

        [DisplayName("Дата рождения")]
        public DateTime DateBerthday { get; set; }

        [DisplayName("Пол")]
        public LeoDataComboBox Sex { get
            {
                return _sex;
            }
        }

        [Browsable(false)]
        public string SelectedSex
        {
            get
            {
                return Sex.SelectedValue;
            }
            set
            {
                Sex.SelectedValue = value;
            }
        }
    }
}
