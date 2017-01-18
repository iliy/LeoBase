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
using AppPresentators.VModels;

namespace LeoBase.Components.CustomControls.NewControls
{
    public partial class PassesTableControl : CustomTablePage, IPassesTableControl
    {
        public int SelectedID { get; private set; }
        private CustomTable _customTable;

        public PassesTableControl():base()
        {
            InitializeComponent();
        }

        private List<Pass> _passes;

        public List<Pass> DataSource
        {
            get
            {
                return _passes;
            }

            set
            {
                _passes = value;
                
            }
        }

        public PassesOrderModel OrderModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public PageModel PageModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public PassesSearchModel SearchModel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool ShowForResult
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public List<Control> TopControls
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event Action AddPass;
        public event Action<int> EditPass;
        public event Action<object> MakeReport;
        public event Action<int> RemovePass;
        public event Action<int> ShowDetailsPass;
        public event Action UpdateTable;

        public Control GetControl()
        {
            throw new NotImplementedException();
        }

        public bool ShowDialog(string text)
        {
            throw new NotImplementedException();
        }

        public void ShowError(string text)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string text, string title)
        {
            throw new NotImplementedException();
        }

        void UIComponent.Resize(int width, int height)
        {
            throw new NotImplementedException();
        }
    }
}
