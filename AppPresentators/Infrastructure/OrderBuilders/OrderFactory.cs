using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Infrastructure.OrderBuilders
{
    public class OrderFactory
    {
        public static IOrderBuilder GetOrderBuilder(OrdersTypes type, string dirName, string orderName, Action<string> ErrorAction = null)
        {
            IOrderBuilder builder = null;

            DirectoryInfo dirInfo = new DirectoryInfo(dirName);

            switch (type)
            {
                case OrdersTypes.EXCEL:
                    builder = new ExcelOrderBuilder();
                    break;
                case OrdersTypes.PDF:
                    builder = new PDFOrderBuilder();
                    break;
                case OrdersTypes.WORD:
                    builder = new WordOrderBuilder();
                    break;
            }

            builder.ErrorBuild += ErrorAction;

            builder.SetOrderPath(dirInfo, orderName);

            return builder;
        }
    }

    public enum OrdersTypes
    {
        EXCEL,
        WORD,
        PDF
    }
}
