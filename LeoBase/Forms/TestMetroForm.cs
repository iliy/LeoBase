using LeoBase.Components.CustomControls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Forms
{
    public class Person
    {
        [DisplayName("Имя")]
        public string Name { get; set; }
        [DisplayName("Возраст")]
        public int Age { get; set; }
    }


    public partial class TestMetroForm : MetroForm
    {
        CustomGridView customGridView;
        public TestMetroForm()
        {
            InitializeComponent();

            List<Person> persons = new List<Person>();
            for(int i = 0; i < 40; i++)
            {
                persons.Add(new Person
                {
                    Name = "Name " + i,
                    Age = i + 20
                });
            }

            customGridView = new CustomGridView();

            customGridView.DataSource = new BindingList<Person>(persons);

            //row.Cells.Add(;

            //for(int i = 0; i < 10; i++)
            //{
            //    customGridView.Rows.Add();
            //    for(int j = 0; j < 4; j++)
            //    {
            //        customGridView.Rows[i].Cells[j].Value = "Test " + i + " " + j; 
            //    }
            //}

            customGridView.Width = metroPanel1.Width - metroPanel1.Margin.Left - metroPanel1.Margin.Right;
            customGridView.Height = metroPanel1.Height - metroPanel1.Margin.Bottom - metroPanel1.Margin.Top;

            customGridView.ReadOnly = true;
            customGridView.Sorted += CustomGridView_Sorted;
            this.metroPanel1.Controls.Add(customGridView);

            /**
             *1) Возможность добавления пользовательской сортировки (при клике на столбец).
             *2) Возможность добавления пользовательской фильтрации (под столбцом должен быть textBox).
             *3) Некоторые элементы при клике должны выбрасывать событие, передавая всю строку.
             *4) Добавить возможность стилизации таблицы.
             *5) Добавить паджинацию.
             *6) Убрать возможность редактировать строки прямо в таблице.
             **/
        }

        private void CustomGridView_Sorted(object sender, EventArgs e)
        {
            MessageBox.Show("IGF");
        }

        //private void CustomGridView_Sorted(object sender, EventArgs e)
        //{
        //    string type = sender.ToString();

        //    MessageBox.Show("Sorted " + type);
        //}
    }
}
