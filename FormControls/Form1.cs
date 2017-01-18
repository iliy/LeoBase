using FormControls.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormControls
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            LeoDataGridView table = new LeoDataGridView();

            table.Dock = DockStyle.Fill;

            List<TestDataModel> testData = new List<TestDataModel>();

            for (int i = 0; i < 30; i++)
                testData.Add(new TestDataModel
                {
                    Key = i,
                    Name = "Имя " + i,
                    DateBerthday = new DateTime(1980 + i, 1, i + 1),
                    SelectedSex = i % 2 == 0 ? "Муж" : "Жен"
                });

            table.SetData<TestDataModel>(testData);

            this.Controls.Add(table);
        }
    }
}
