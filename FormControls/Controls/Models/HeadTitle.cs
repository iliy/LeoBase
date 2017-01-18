using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FormControls.Controls.Events;
using FormControls.Controls.Enums;

namespace FormControls.Controls.Models
{
    public partial class HeadTitle : UserControl
    {
        public LeoDataViewHeadMetaData MetaData { get; set; }

        public event TableOrderEvent OnTableOrder;
        public event LeoTableResizeEvent OnResizeStart;
        public event HeaderMouseMove OnHeaderMouseMove;

        private bool _isLast = false;
        public bool IsLast
        {
            get
            {
                return _isLast;
            }
            set
            {
                _isLast = value;
                pbSeparator.Visible = !value;
            }
        }

        public bool IsFirst
        {
            get;set;
        }

        public override Cursor Cursor
        {
            get
            {
                return base.Cursor; 
            }

            set
            {
                base.Cursor = value;
                lbTitle.Cursor = value;
            }
        }

        private OrderTypes _orderType = OrderTypes.NONE;

        public OrderTypes OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                _orderType = value;

                switch (value)
                {
                    case OrderTypes.ASC:
                        lbTitle.Image = Properties.Resources.OrderAsc;
                        break;
                    case OrderTypes.DESC:
                        lbTitle.Image = Properties.Resources.OrderDesc;
                        break;
                    default:
                        lbTitle.Image = null;
                        break;
                }
            }
        }

        public HeadTitle(LeoDataViewHeadMetaData metaData)
        {
            InitializeComponent();

            MetaData = metaData;

            if (!metaData.Show) return;

            if (metaData.CanOrder)
            {
                Cursor = Cursors.Hand;

                Click += (s,e) => Order();

                lbTitle.Click += (s, e) => Order();

                lbTitle.MouseMove += (s, e) =>
                {
                    if (OnHeaderMouseMove != null) OnHeaderMouseMove(this, e);
                };

                lbTitle.MouseUp += (s, e) =>
                {
                    if (OnResizeStart != null) OnResizeStart(this, false);
                };


            }

            pbSeparator.MouseDown += (s, e) =>
            {
                if (OnResizeStart != null) OnResizeStart(this, true);
            };

            pbSeparator.MouseUp += (s, e) =>
            {
                if (OnResizeStart != null) OnResizeStart(this, false);
            };

            pbSeparator.MouseMove += (s, e) =>
            {
                if (OnHeaderMouseMove != null) OnHeaderMouseMove(this, e);
            };





            lbTitle.Text = metaData.DisplayName ?? metaData.FieldName;

            this.Dock = DockStyle.Fill;
        }
        
        private void Order()
        {
            if (OrderType == OrderTypes.NONE) OrderType = OrderTypes.ASC;
            else if (OrderType == OrderTypes.ASC) OrderType = OrderTypes.DESC;
            else if (OrderType == OrderTypes.DESC) OrderType = OrderTypes.ASC;

            if (OnTableOrder != null) OnTableOrder(this, OrderType);
        }
    }
}
