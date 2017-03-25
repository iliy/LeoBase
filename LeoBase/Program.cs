using AppData.Contexts;
using AppPresentators.Infrastructure;
using AppPresentators.Presentators.Interfaces;
using LeoBase.Forms;
using LeoBase.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.Infrastructure.Orders;
using System.Threading;
using System.Drawing;

namespace LeoBase
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Настройка ninject для создания абстрактных репозиториев
            ApplicationFactory appFactory = new ApplicationFactory();

            var mainView = new MainView();

            IMainPresentator mainPresentator = appFactory.GetMainPresentator(appFactory);

            LeoBaseContext.InitDefaultValue();

            mainPresentator.Run();

            //Application.Run(new OrderDialog() { OrderPage = new TestOrderPage() });
        }
    }

    public class TestOrderPage : IOrderPage
    {
        private string _orderDirPath;
        public string OrderDirPath
        {
            get
            {
                return _orderDirPath;
            }
        }

        public OrderType OrderType
        {
            get
            {
                return OrderType.SINGLE_PAGE;
            }
        }

        public void BuildOrder(IOrderBuilder orderBuilder, OrderConfigs configs)
        {
            if (configs.SinglePageConfig.DrawImages)
            {
                Image img = Image.FromFile("D:/Images/testimg.jpg");
                orderBuilder.DrawImage(img, Align.LEFT);
            }
            if (configs.SinglePageConfig.DrawImages)
            {
                Image img = Image.FromFile("D:/Images/testimg.jpg");
                orderBuilder.DrawImage(img, Align.LEFT);
            }



            orderBuilder.StartTable("Test table", new[] { "h1", "h2", "h3" });

            for (int i = 0; i < 10; i++)
            {
                orderBuilder.WriteRow(new[] { "фывфыв1", "c2", "c3" }, i % 3 == 0 ? RowColor.GREEN : i % 3 == 1 ? RowColor.RED : RowColor.YELLOW);
            }

            orderBuilder.EndTable("Test table");

            orderBuilder.StartPharagraph(Align.LEFT);

            orderBuilder.WriteText("Проверочный текст 1 ", Color.Black, TextStyle.BOLD, 15);

            orderBuilder.WriteText("Проверочный текст 2 ", Color.Red, TextStyle.ITALIC, 12);

            orderBuilder.WriteText("Проверочный текст 3 ", Color.Green, TextStyle.NORMAL, 22);
                            
            orderBuilder.EndPharagraph();

            orderBuilder.StartPharagraph(Align.CENTER);

            orderBuilder.WriteText("Снова проверочный текст", Color.Black, TextStyle.ITALIC, 15);

            orderBuilder.EndPharagraph();

            if (configs.SinglePageConfig.DrawImages)
            {
                Image img = Image.FromFile("D:/Images/testimg.jpg");
                orderBuilder.DrawImage(img, Align.LEFT);
            }


            _orderDirPath = orderBuilder.Save();

            

            //Thread.Sleep(3000);
        }
    }
}
