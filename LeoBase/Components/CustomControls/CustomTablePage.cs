using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LeoBase.Components.CustomControls.Interfaces;
using System.Threading;

namespace LeoBase.Components.CustomControls
{
    public partial class CustomTablePage : UserControl
    {
        private OpenSearchPanelButton openSearchPanelButton;

        private ISearchPanel _searchPanel;

        private System.Windows.Forms.Timer _timer;

        private BackgroundWorker _animatorSP;

        private bool _animateOpen;

        public bool VisibleLoadPanel
        {
            get
            {
                return loadPanel.Visible;
            }
            set
            {
                loadPanel.Visible = value;
            }
        }

        public string Title
        {
            get
            {
                return lbTitle.Text;
            }
            set
            {
                lbTitle.Text = value;
            }
        }

        public CustomTablePage()
        {
            InitializeComponent();

            this.DoubleBuffered = true;

            this.Dock = DockStyle.Fill;

            this.Margin = new Padding(0, 0, 0, 0);

            openSearchPanelButton = new OpenSearchPanelButton();

            mainTableContainer.Controls.Add(openSearchPanelButton, 0, 1);
            
            SearchPanelContainer.Visible = false;

            openSearchPanelButton.OnClickButton += () =>
            {
                //_animateOpen = true;

                SearchPanelContainer.Visible = true;

                //foreach (var control in SearchPanelContainer.Controls)
                //{
                //    ((Control)control).Visible = false;
                //}

                //SearchPanelContainer.BackgroundImage = _saveState;

                openSearchPanelButton.Visible = false;

                //_animationStarted = true;

                //_animatorSP.RunWorkerAsync();

                //_timer.Start();
            };

            SearchPanelContainer.BackgroundImageLayout = ImageLayout.None;
            
            _timer = new System.Windows.Forms.Timer();

            _timer.Tick += AnimatePanel;

            _timer.Interval = 1;

            _animatorSP = new BackgroundWorker();

            _animatorSP.WorkerReportsProgress = true;

            _animatorSP.ProgressChanged += ProgressAnimationChange;
            _animatorSP.DoWork += TimerTick;
            _animatorSP.RunWorkerCompleted += AnimationComplete;
            _animatorSP.WorkerSupportsCancellation = true;
        }

        private static bool _animationStarted = false;

        private void AnimationComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }

        private void TimerTick(object sender, DoWorkEventArgs e)
        {
            while (_animationStarted)
            {
                _animatorSP.ReportProgress(0);
                Thread.Sleep(42);
            }
        }

        private void ProgressAnimationChange(object sender, ProgressChangedEventArgs e)
        {
            if (_animateOpen)
            {
                int dw = 15;// (450 - SearchPanelContainer.Width) / 20;

                if (dw < 5) dw = 5;

                if (SearchPanelContainer.Width + dw >= 450)
                {
                    SearchPanelContainer.Width = 450;

                    SearchPanelContainer.BackgroundImage = null;

                    foreach (var control in SearchPanelContainer.Controls)
                    {
                        ((Control)control).Visible = true;
                    }

                    _searchPanel.Control.Refresh();

                    _searchPanel.Control.Update();

                    _animationStarted = false;

                    _animatorSP.CancelAsync();

                    //_timer.Stop();
                }
                else
                {
                    SearchPanelContainer.Width += dw;
                }
            }
            else
            {
                int dw = -15;// (SearchPanelContainer.Width) / 20;

                if (dw > -5) dw = -5;

                if (SearchPanelContainer.Width + dw <= 0)
                {
                    SearchPanelContainer.Width = 0;

                    openSearchPanelButton.Visible = true;

                    _animationStarted = false;

                    _animatorSP.CancelAsync();

                    //_timer.Stop();
                }
                else
                {
                    SearchPanelContainer.Width += dw;
                }
            }
        }

        private void AnimatePanel(object sender, EventArgs e)
        {
            // width 450px
            if (_animateOpen)
            {
                int dw =  (450 - SearchPanelContainer.Width) / 5;
                if(SearchPanelContainer.Width + dw >= 450)
                {
                    SearchPanelContainer.Width = 450;

                    SearchPanelContainer.BackgroundImage = null;

                    foreach (var control in SearchPanelContainer.Controls)
                    {
                        ((Control)control).Visible = true;
                    }

                    _searchPanel.Control.Refresh();
                    _searchPanel.Control.Update();

                    _timer.Stop();
                }else
                {
                    SearchPanelContainer.Width += dw;
                }
            }else
            {
                int dw =  (SearchPanelContainer.Width - 450)/ 5;

                if(SearchPanelContainer.Width + dw <= 0)
                {
                    SearchPanelContainer.Width = 0;

                    openSearchPanelButton.Visible = true;

                    _timer.Stop();
                }else
                {
                    SearchPanelContainer.Width += dw;
                }
            }
        }

        private Bitmap _saveState;

        public void AddSearchPanel(ISearchPanel searchPanel)
        {
            _searchPanel = searchPanel;
            
            SearchPanelContainer.Width = 450;

            SearchPanelContainer.Controls.Add(searchPanel.Control);
            
            searchPanel.OnHideClick += HideSearchPanel;
            
            //searchPanel.Control.Refresh();

            //searchPanel.Control.Update();

            //SearchPanelContainer.Refresh();

            //SearchPanelContainer.Update();

            //_saveState = new Bitmap(SearchPanelContainer.Width, SearchPanelContainer.Height);

            //SearchPanelContainer.DrawToBitmap(_saveState, new Rectangle(0, 0, SearchPanelContainer.Width, SearchPanelContainer.Height));
            
            //SearchPanelContainer.Visible = true;

            //SearchPanelContainer.Width = 0;
        }

        public void HideSearchPanel()
        {
            SearchPanelContainer.Visible = false;

            openSearchPanelButton.Visible = true;

            //_animateOpen = false;

            //foreach(var control in SearchPanelContainer.Controls)
            //{
            //    ((Control)control).Visible = false;
            //}

            //SearchPanelContainer.BackgroundImage = _saveState;

            //SearchPanelContainer.Controls[0].Visible = false;
            //_animationStarted = true;

            //_animatorSP.RunWorkerAsync();

            //_timer.Start();
            //SearchPanelContainer.Visible = false;


            //SearchPanelContainer.Visible = true;

            //openSearchPanelButton.Visible = false;
        }

        public void SetContent(Control control)
        {
            control.Dock = DockStyle.Fill;
            mainTableContainer.Controls.Add(control, 0, 2);
        }


    }
}
