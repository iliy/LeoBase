using AppPresentators.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.VModels;
using AppPresentators.Components;
using AppPresentators.Components.MainMenu;
using MetroFramework.Forms;
using System.Threading;
using LeoBase.Properties;
using LeoBase.Components.CustomControls;
using LeoBase.Components.CustomControls.SearchPanels;
using AppPresentators.Services;
using AppPresentators;

namespace LeoBase.Forms
{
    public partial class MainView : Form, IMainView
    {
        public event Action Login;
        public event Action FastSearchGO;

        private VManager _currentManager;

        private IMainMenu _mainMenu;

        Size _oldSize = new Size(640, 480);

        private PreloadPanel _preloadPanel;

        private static MainView _instance = null;

        private BackgroundWorker _pageLoader;

        public bool ShowFastSearch { get
            {
                return panelFastSearch.Visible;
            }
            set
            {
                panelFastSearch.Visible = value;
            }
        }

        public string SearchQuery
        {
            get
            {
                return tbFastSearch.Text.Trim();
            }
            set
            {
                string oldValue = tbFastSearch.Text.Trim();
                tbFastSearch.Text = value;
            }
        }
        public static MainView GetInstance()
        {
            if (_instance == null) _instance = new MainView();

            return _instance;
        }

        public MainView()
        {
            InitializeComponent();
            this.Resize += (s, e) => MainResize();
            this.Text = string.Empty;

           
            tbFastSearch.TextChanged += (s, e) => FastSearch();

            _oldSize = this.Size;

            _instance = this;

            _pageLoader = new BackgroundWorker();

            _pageLoader.DoWork += PageLoading;

            _pageLoader.RunWorkerCompleted += PageLoaded;

            _preloadPanel = new PreloadPanel();

            _preloadPanel.Visible = false;

            centerPanel.Controls.Add(_preloadPanel);
        }

        

        AdminViolationSearchModel aa = new AdminViolationSearchModel();
        public void SetTopControls(List<Control> controls)
        {
            contentButtons.Controls.Clear();

            if (controls == null || controls.Count == 0) return;

            contentButtons.ColumnCount = controls.Count;

            contentButtons.Refresh();

            foreach (var c in controls)
            {
                contentButtons.Controls.Add(c);
            }
        }
        private void BtnFastSearch_Click(object sender, EventArgs e)
        {
        }

        private void FastSearch()
        {
            string query = tbFastSearch.Text.Trim();
            if(query.Length > 3 || query.Length == 0)
            {
                if(FastSearchGO != null)
                {
                    FastSearchGO();
                }
            }
        }
        
        private void MainView_ResizeEnd(object sender, EventArgs e)
        {
            _oldSize = this.Size;
        }
        private void MainResize()
        {
        }

        public VManager Manager
        {
            get
            {
                return _currentManager;
            }

            set
            {
                _currentManager = value;
                if (_currentManager != null)
                {
                    lbAccauntInfo.Text = _currentManager.Login.Equals("admin") ? "Администратор" : "Пользователь";
                    btnLogout.Enabled = true;
                    managerPanel.Visible = true;
                }
                else
                {
                    managerPanel.Visible = false;
                }
            }
        }


        public void ShowError(string errorMessage)
        {
            MessageBox.Show(errorMessage);
        }

        private static bool _pageLoading = false;

        Control saveControl = null;

        private void PageLoaded(object sender, RunWorkerCompletedEventArgs e)
        {

        }
        private void OnFrameChanged(object o, EventArgs e)
        {
            
        }

        private void PageLoading(object sender, DoWorkEventArgs e)
        {
            

            centerPanel.Invoke(new MethodInvoker(() =>
            {
                var preloader = new Label();

                preloader.AutoSize = true;

                preloader.BackColor = Color.Transparent;
                
                preloader.Text = "Загрузка...";

                preloader.Top = centerPanel.Height / 2 - preloader.Height / 2;

                preloader.Left = centerPanel.Width / 2 - preloader.Width / 2;

                preloader.Font = new Font("Arial", 18, FontStyle.Italic);

                Bitmap bmp = new Bitmap(preloader.Width, preloader.Height);

                Graphics g = Graphics.FromImage(bmp);
                
                preloader.Image = bmp;

                centerPanel.Controls.Add(preloader);
            }));
        }

        private Action<Control> setPictureBox = (Control control) =>
        {
            

            


            //control.Controls.Add(preloader);
        };

        public void StartTask()
        {
            Bitmap saveState = new Bitmap(centerPanel.Width, centerPanel.Height);

            centerPanel.DrawToBitmap(saveState, new Rectangle(0, 0, centerPanel.Width, centerPanel.Height));

            Graphics g = Graphics.FromImage(saveState);

            var brash = new SolidBrush(Color.FromArgb(140, 255, 255, 255));

            g.FillRectangle(brash, new Rectangle(0, 0, centerPanel.Width, centerPanel.Height));

            if (centerPanel.Controls.Count != 0) saveControl = centerPanel.Controls[0];

            centerPanel.Controls.Clear();
            
            centerPanel.BackgroundImage = saveState;

            _pageLoading = true;

            _pageLoader.RunWorkerAsync();

            //Thread.Sleep(3000);

        }

        public void EndTask(bool setOldState)
        {
            _pageLoading = false;

            centerPanel.BackgroundImage = null;

            if (setOldState && saveControl != null) {
                centerPanel.Controls.Add(saveControl);
            }
        }

        public void Show()
        {
            try {
                Application.Run(this);
            }catch(Exception e)
            {
                return;
            }
        }

        public bool RemoveComponent(Control component)
        {
            if (centerPanel.Controls.IndexOf(component) != -1)
            {
                centerPanel.Controls.Remove(component);
                return true;
            }

            return false;
        }

        private Control childrenControl;
        public void SetComponent(Control component)
        {
            if(component == null)
            {
                centerPanel.Controls.Clear();
                this.Refresh();
                return;
            }

            component.Anchor = AnchorStyles.None;

            component.Dock = DockStyle.Fill;

            centerPanel.Controls.Clear();

            centerPanel.Controls.Add(component);

            childrenControl = component;
            this.Refresh();
        }

        public void MainView_Load(object sender, EventArgs ev)
        {
            MainResize();

            if (_currentManager == null) Login();

            btnLogout.Click += (s, e) =>
            {
                SetComponent(null);
                SetTopControls(null);
                LogoutClick();
            };
        }

        private void LogoutClick()
        {
            managerPanel.Visible = false;
            if (Login != null) { 
                Login();
                tbFastSearch.Visible = false;
            }
        }

        public void SetMenu(IMainMenu control)
        {
            _mainMenu = control;
            _mainMenu.Resize(menuPanel.Width, menuPanel.Height);
            
            menuPanel.Controls.Clear();
            menuPanel.Controls.Add(_mainMenu.GetControl());
        }

        public void ClearCenter()
        {
            centerPanel.Controls.Clear();
            centerPanel.Refresh();
        }

        public event Action GoNextPage;
        public event Action GoBackPage;

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (GoNextPage != null) GoNextPage();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (GoBackPage != null) GoBackPage();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {

        }
    }
}
