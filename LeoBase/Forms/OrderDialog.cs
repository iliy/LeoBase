using AppPresentators.Infrastructure;
using AppPresentators.Infrastructure.OrderBuilders;
using AppPresentators.Infrastructure.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LeoBase.Forms
{
    public partial class OrderDialog : Form
    {
        public IOrderPage OrderPage { get; set; }

        public event Action<string> Error;

        public BackgroundWorker _backgroundWorker;
        
        public OrderDialog()
        {
            InitializeComponent();

            _backgroundWorker = new BackgroundWorker();

            _backgroundWorker.DoWork += MakeOrderAsync;

            _backgroundWorker.RunWorkerCompleted += MakeOrderComplete;

        }

        private void MakeOrderComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (cmbOrderType.SelectedIndex == 0)
            {
                panelLoad.Visible = false;

                panelComplete.Visible = true;
            }
            else
            {
                // Печать
            }
        }

        private void MakeOrderAsync(object sender, DoWorkEventArgs e)
        {
            var arg = (OrderConfigsArgument)e.Argument;

            OrderPage.BuildOrder(arg.OrderBuilder, arg.OrderConfigs);
        }

        private void OrderDialog_Load(object sender, EventArgs e)
        {
            cmbOrderFormat.SelectedIndex = 0;
            cmbOrderType.SelectedIndex = 0;

            if(OrderPage == null)
            {
                if (Error != null) Error("Не указана страница для отчета.");
                this.Close();
            }

            if(OrderPage.OrderType == AppPresentators.Infrastructure.Orders.OrderType.SINGLE_PAGE)
            {
                panelTableConfig.Visible = false;

                panelSingleWithImages.Visible = true;
            }else
            {
                panelTableConfig.Visible = true;

                panelSingleWithImages.Visible = false;
            }
        }

        private void cmbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbOrderType.SelectedIndex == 0)
            {
                btnOrderComplete.Text = "Сформировать отчет";
                formatPanel.Visible = true;
            }
            else
            {
                btnOrderComplete.Text = "Печать";
                formatPanel.Visible = false;
            }
        }

        private void btnOrderCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrderComplete_Click(object sender, EventArgs e)
        {
            OrderConfigs configs = new OrderConfigs();

            ///TODO: Заменить на реальные билдеры отчетов
            var builder = new PDFOrderBuilder();

            builder.SetOrderPath("D:/TestOrders/order1.pdf");

            builder.ErrorBuild += Builder_ErrorBuild;

            configs.OrderType = OrderPage.OrderType;

            if(cmbOrderType.SelectedIndex == 0)
            {
                //if (string.IsNullOrWhiteSpace(lbPath.Text))
                //{
                //    Builder_ErrorBuild("Не указан путь для отчета.");
                //    return;
                //}

                //if (string.IsNullOrWhiteSpace(tbOrderName.Text))
                //{
                //    Builder_ErrorBuild("Не указано имя отчета.");
                //    return;
                //}

                configs.OrderDirPath = lbPath.Text;
                configs.OrderName = tbOrderName.Text;
            }else
            {
                configs.OrderDirPath = Directory.GetCurrentDirectory();

                configs.OrderName = "OrderForPrint";
            }

            if(OrderPage.OrderType == OrderType.TABLE)
            {
                configs.TableConfig = new OrderTableConfig
                {
                    ConsiderFilter = chbSearchResult.Checked,
                    CurrentPageOnly = chbCurrentPage.Checked
                };
            }else
            {
                configs.SinglePageConfig = new OrderSinglePageConfig
                {
                    DrawImages = chbOutputImages.Checked
                };
            }

            OrderConfigsArgument arg = new OrderConfigsArgument
            {
                OrderBuilder = builder,
                OrderConfigs = configs
            };

            penelOrderType.Visible = false;

            formatPanel.Visible = false;

            panelTableConfig.Visible = false;

            panelSingleWithImages.Visible = false;

            btnsPunel.Visible = false;

            panelComplete.Visible = false;

            panelLoad.Visible = true;

            _backgroundWorker.RunWorkerAsync(arg);
        }

        private void Builder_ErrorBuild(string obj)
        {
            if (Error != null) Error(obj);
            else MessageBox.Show(obj, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(OrderPage.OrderDirPath);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectPathForOrder_Click(object sender, EventArgs e)
        {
            if(orderSaveFolderDialog.ShowDialog() == DialogResult.OK)
            {
                lbPath.Text = orderSaveFolderDialog.SelectedPath;
            }
        }
    }

    public class OrderConfigsArgument
    {
        public IOrderBuilder OrderBuilder { get; set; }
        public OrderConfigs OrderConfigs { get; set; }
    }
}
