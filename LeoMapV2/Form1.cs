using LeoMapV2.Data;
using LeoMapV2.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoMapV2
{
    public partial class Form1 : Form
    {
        TitleDBRepository repo;
        LeoMap map;

        List<Models.DPoint> _points = new List<Models.DPoint>();

        public Form1()
        {
            InitializeComponent();

            repo = new TitleDBRepository("D:\\Maps\\test.lmap");

            repo.ClearCache();

            map = new LeoMap(repo);

            var point = new Models.DPoint
            {
                N = 42.74501533,
                E = 131.20658056,
                Zoom = 14
            };

            var point2 = new Models.DPoint
            {
                N = 43.47892223,
                E = 131.162419444,
                Zoom = 14,
                ToolTip = "Test 123"
            };

            map.SetPoint(point);
            map.SetPoint(point2);

            map.LookAt(point);
            map.LookAt(point2);


            for(double x = 131; x < 132; x+= 0.01)
            {
                for(double y = 42; y<44; y+= 0.01)
                {
                    _points.Add(new Models.DPoint
                    {
                        N = y,
                        E = x
                    });
                }
            }

            map.Dock = DockStyle.Fill;

            map.MouseMoving += Map_MouseMoving;

            map.RegionSelected += (s, e) =>
            {
                string message = e.ToString();

                map.ClearPoints();

                var points = _points.Where(p => p.E >= e.W && p.E <= e.E && p.N <= e.N && p.N >= e.S);

                if(points != null)
                {
                    map.SetPoints(points.ToList());
                }

                map.Redraw();
            };

            map.PointClicked += (s, e) =>
            {
                var a = e;
                MessageBox.Show(a.ToolTip);
            };

            panel2.Controls.Add(map);
        }

        private void Map_MouseMoving(object arg1, Models.MapEventArgs arg2)
        {
            lbCoords.Text = string.Format("N:{0:0.00000000}; E:{1:0.00000000}", arg2.N, arg2.E);
        }




        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var point = TextBoxsToPoint();

                map.SetPoint(point);

                map.RedrawAsync();
            }
            catch(Exception e1)
            {
                var a = e1;
            }
        }

        private Models.DPoint TextBoxsToPoint()
        {
            double n = Convert.ToDouble(textBox1.Text.Replace(".", ","));
            double e = Convert.ToDouble(textBox2.Text.Replace(".", ","));

            return new Models.DPoint
            {
                N = n,
                E = e
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var point = TextBoxsToPoint();

                map.LookAt(point);
            }
            catch (Exception e1)
            {
                var a = e1;
            }
        }
    }
}
