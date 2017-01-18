using AppPresentators.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppPresentators.VModels.Persons;
using AppData.Entities;
using LeoBase.Components.CustomControls.SearchPanels;

namespace LeoBase.Components.CustomControls.NewControls
{
    public class PersoneSavePanel : SavePageTemplate, ISavePersonControl
    {
        private IPersoneViewModel _persone;
        public IPersoneViewModel Persone
        {
            get
            {
                return _persone;
            }

            set
            {
                _persone = value;

                _personePanel.DataSource = _persone;

                if (_persone != null) Title = _persone.IsEmploeyr ? "Сохранить сотрудника" : "Сохранить правонарушителя";
            }
        }

        public bool ShowForResult { get; set; }

        private AutoBinderPanel _personePanel;

        public event Action SavePersone;

        public Control GetControl()
        {
            return this;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public PersoneSavePanel():base()
        {
            this.Dock = DockStyle.Fill;

            this.AutoScroll = true;

            _personePanel = new AutoBinderPanel(AutoBinderPanelType.EDIT);

            this.SaveClick += () =>
            {
                if (SavePersone != null) SavePersone();
            };

            _personePanel.Dock = DockStyle.Fill;

            _personePanel.AutoScroll = true;

            SetContent(_personePanel);
        }

        void UIComponent.Resize(int width, int height)
        {

        }
    }
}
