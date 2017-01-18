using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.VModels;
using System.Collections;

namespace LeoBase.Components.CustomControls
{
    public delegate void SelectedIndex(int index);
    public delegate void RowCreated(DataGridViewRow row);
    public delegate void GenerateControl(Control control);
    public delegate void CellDataChanged(string columnName, object newValue, DataGridViewRow row);
    public partial class CustomTable : UserControl
    {
        public bool CanEdit { get; set; }
        public bool CanDelete { get; set; }
        public event SelectedIndex SelectedItemChange;
        public event RowCreated OnRowWasCreted;
        public event CellDataChanged OnCellDataChanged;
        public event Action DoubleClick;

        public int SelectedItemIndex
        {
            get
            {
                if (dataTable.SelectedRows.Count == 0) return -1;

                return dataTable.SelectedRows[0].Index;
            }
        }
        public bool VisibleOrderBy { get
            {
                return orderPanel.Visible;
            }
            set
            {
                orderPanel.Visible = value;
            }
        }

        public int SelectedOrderBy
        {
            get
            {
                if(cmbOrderBy.Items != null && cmbOrderBy.Items.Count != 0)
                {
                    string value = cmbOrderBy.Items[cmbOrderBy.SelectedIndex].ToString();
                    if (_orderProperties.ContainsKey(value))
                    {
                        return _orderProperties[value];
                    }
                }

                return cmbOrderBy.SelectedIndex;
            }
            set
            {
                cmbOrderBy.SelectedIndex = value;
            }
        }

        public string SelectedOrderByValue
        {
            get
            {
                return cmbOrderBy.Items.Count != 0 ? cmbOrderBy.Items[cmbOrderBy.SelectedIndex].ToString(): "";
            }
            set
            {
                for(int i = 0; i < cmbOrderBy.Items.Count; i++)
                {
                    if(value.Equals(cmbOrderBy.Items[i]))
                    {
                        cmbOrderBy.SelectedIndex = i;
                        return;
                    }
                }
            }
        }

        private Dictionary<string, int> _orderProperties;
        public Dictionary<string, int> OrderProperties
        {
            get
            {
                return _orderProperties;
            }
            set
            {
                _orderProperties = value;
                cmbOrderBy.Items.Clear();
                foreach(var item in _orderProperties)
                {
                    cmbOrderBy.Items.Add(item.Key);
                }
                cmbOrderBy.SelectedIndex = 0;
            }
        }

        private Dictionary<string, OrderType> _orderTypes;

        public OrderType OrderType
        {
            get
            {
                if(cmbOrderType.Items != null && cmbOrderType.Items.Count != 0)
                {
                    string value = cmbOrderType.Items[cmbOrderType.SelectedIndex].ToString();
                    if(!string.IsNullOrEmpty(value) && _orderTypes.ContainsKey(value))
                    {
                        return _orderTypes[value];
                    }
                }
                
                return OrderType.NONE;
            }
            set
            {
                foreach(var orderType in _orderTypes)
                {
                    if(orderType.Value == value)
                    {
                        for(int i = 0; i < cmbOrderType.Items.Count; i++)
                        {
                            if (orderType.Key.Equals(cmbOrderType.Items[i].ToString()))
                            {
                                cmbOrderType.SelectedIndex = i;
                                return;
                            }
                        }
                        return;           
                    }
                }
            }
        }

        private PageModel _pageModel = new PageModel();
        public PageModel PageModel {
            get
            {
                if (_pageModel == null) _pageModel = new PageModel();
               
                if (cmbBoxItemsOnPage.Items != null && cmbBoxItemsOnPage.Items.Count != 0)
                {
                    string value = cmbBoxItemsOnPage.Items[cmbBoxItemsOnPage.SelectedIndex].ToString();
                    if (!string.IsNullOrEmpty(value))
                    {
                        try
                        {
                            _pageModel.ItemsOnPage = Convert.ToInt32(value);
                        }
                        catch
                        {
                            _pageModel.ItemsOnPage = 10;
                        }
                    }
                }

                if (paginationSource.Count != 0)
                    _pageModel.CurentPage = 1 + (int)paginationSource.Current;
                else
                    _pageModel.CurentPage = 1;

                if (_pageModel.ItemsOnPage == 0)
                    _pageModel.ItemsOnPage = 10;


                return _pageModel;
            }
            set
            {
                //if (_pageModel == value) return;

                if (_pageModel.TotalPages != value.TotalPages 
                    || paginationSource.Count == 0 
                    || paginationSource.Count != value.TotalPages)
                {
                    List<int> pages = new List<int>();
                    for (int i = 0; i < value.TotalPages; i++)
                    {
                        pages.Add(i);
                    }
                    paginationSource = new BindingSource();
                    paginationSource.DataSource = new BindingList<int>(pages);
                    paginatior.BindingSource = paginationSource;

                    paginationSource.CurrentChanged += PaginationSource_CurrentChanged;
                }

                _pageModel = value;
            }
        }

        public void SetData<T>(List<T> list)
        {

            dataTable.DataSource = new BindingList<T>(list);

            dataTable.Update();

            dataTable.Refresh();

            foreach(DataGridViewColumn column in dataTable.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public event Action UpdateTable;
        
        public CustomTable()
        {
            InitializeComponent();

            _orderTypes = new Dictionary<string, OrderType>();

            _orderTypes.Add("Возростание", OrderType.ASC);
            _orderTypes.Add("Убывание", OrderType.DESC);
            
            cmbOrderType.SelectedIndex = 0;
            cmbBoxItemsOnPage.SelectedIndex = 0;
            paginatior.BindingSource = paginationSource;

            cmbBoxItemsOnPage.SelectedIndexChanged += (s, e) =>
            {
                paginationSource = new BindingSource();
                _pageModel.CurentPage = 1;
                UpdateTable();
            };
            
            dataTable.MultiSelect = false;

            dataTable.EditingControlShowing += DataTable_EditingControlShowing;
            dataTable.DataBindingComplete += DataTable_DataBindingComplete;

            dataTable.CellValueChanged += DataTable_CellValueChanged;

            paginationSource.CurrentChanged += PaginationSource_CurrentChanged;

            dataTable.SelectionChanged += DataTable_SelectionChanged;

            dataTable.DoubleClick += (s,e) => {
                if (DoubleClick != null) DoubleClick();
            };
        }

        private void DataTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (OnCellDataChanged != null) OnCellDataChanged(dataTable.Columns[e.ColumnIndex].HeaderText, dataTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value, dataTable.Rows[e.RowIndex]);
        }

        private void DataTable_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for(int i = 0; i < dataTable.RowCount; i++)
            {
                if (OnRowWasCreted != null) OnRowWasCreted(dataTable.Rows[i]);
            }
        }
        
        private void DataTable_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if((e.Control) is ComboBox)
            {
                ComboBox combo = (ComboBox)(e.Control);
                combo.DropDownStyle = ComboBoxStyle.DropDownList;
                
                combo.SelectedIndexChanged += Combo_SelectedIndexChanged;
            }
        }
        public event EventHandler SelectedIndexChanged;
        private void Combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SelectedIndexChanged != null) SelectedIndexChanged(sender, e);
        }

        private void DataTable_SelectionChanged(object sender, EventArgs e)
        {
            if(dataTable.SelectedCells.Count != 0) {

                int row = dataTable.SelectedCells[0].RowIndex;
                
                dataTable.Rows[row].Selected = true;

                if (SelectedItemChange != null) SelectedItemChange(row);
            }
        }

        public void AddComboBox(List<string> values,string headName = null, string valueMember = null)
        {
            DataGridViewComboBoxColumn comboBox = new DataGridViewComboBoxColumn();
            
            comboBox.DataSource = values;
            
            if (headName != null) comboBox.HeaderText = headName;

            comboBox.Name = valueMember;

            dataTable.Columns.Add(comboBox);
        }

        private bool _wasUpdated = false;
        private void PaginationSource_CurrentChanged(object sender, EventArgs e)
        {
            _pageModel.CurentPage = 1 + (int)paginationSource.Current;
            if (UpdateTable != null)
                UpdateTable();
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {
            //if (_pageModel.CurentPage < _pageModel.TotalPages)
            //{
            //    _pageModel.CurentPage = (int)paginationSource.Current;
            //    UpdateTable();
            //}
        }

        private void bindingNavigatorMoveLastItem_Click(object sender, EventArgs e)
        {
            //if (_pageModel.CurentPage != _pageModel.TotalPages)
            //{
            //    _pageModel.CurentPage = (int)paginationSource.Current;
            //    UpdateTable();
            //}
        }

        private void bindingNavigatorMovePreviousItem_Click(object sender, EventArgs e)
        {
            //if(_pageModel.CurentPage - 1 > 1)
            //{
            //    _pageModel.CurentPage = (int)paginationSource.Current;
            //    UpdateTable();
            //}
        }

        private void bindingNavigatorMoveFirstItem_Click(object sender, EventArgs e)
        {
            //if (_pageModel.CurentPage != 1)
            //{
            //    _pageModel.CurentPage = (int)paginationSource.Current;
            //    UpdateTable();
            //}
        }

        private void cmbOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UpdateTable != null)
                UpdateTable();
        }

        private void cmbOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (UpdateTable != null)
                UpdateTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach(var a in dataTable.SelectedCells)
            {
                MessageBox.Show(a.ToString());
            }
        }
    }
}
