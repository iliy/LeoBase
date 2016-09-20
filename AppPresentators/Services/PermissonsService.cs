using AppPresentators.Components.MainMenu;
using AppPresentators.Infrastructure;
using AppPresentators.VModels.MainMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppPresentators.Services
{
    public interface IPermissonsService
    {
        List<MenuItemModel> GetMenuItems(string role);
    }

    public class PermissonsService: IPermissonsService
    {
        private Dictionary<string, List<MenuItemModel>> _menuPermissions;
        public PermissonsService(Dictionary<string, List<MenuItemModel>> menuPermissions)
        {
            _menuPermissions = menuPermissions;
        }
        public PermissonsService()
        {
            _menuPermissions = new Dictionary<string, List<MenuItemModel>>();
            _menuPermissions.Add("admin", new List<MenuItemModel>
            {
                new MenuItemModel
                {
                    Title = "Нарушения",
                    MenuCommand = MenuCommand.Infringement
                },
                new MenuItemModel
                {
                    Title = "Нарушители",
                    MenuCommand = MenuCommand.Violators
                },
                new MenuItemModel
                {
                    Title = "Сотрудники",
                    MenuCommand = MenuCommand.Employees
                },
                new MenuItemModel
                {
                    Title = "Пропуски",
                    MenuCommand = MenuCommand.Omissions
                },
                new MenuItemModel
                {
                    Title = "Карта",
                    MenuCommand = MenuCommand.Map
                },
                new MenuItemModel
                {
                    Title = "Настройки",
                    MenuCommand = MenuCommand.Options
                }
            });

            _menuPermissions.Add("manager", new List<MenuItemModel>
            {
                new MenuItemModel
                {
                    Title = "Нарушения",
                    MenuCommand = MenuCommand.Infringement
                },
                new MenuItemModel
                {
                    Title = "Нарушители",
                    MenuCommand = MenuCommand.Violators
                },
                new MenuItemModel
                {
                    Title = "Сотрудники",
                    MenuCommand = MenuCommand.Employees
                },
                new MenuItemModel
                {
                    Title = "Пропуски",
                    MenuCommand = MenuCommand.Omissions
                },
                new MenuItemModel
                {
                    Title = "Карта",
                    MenuCommand = MenuCommand.Map
                }
            });

        }

        public List<MenuItemModel> GetMenuItems(string role)
        {
            if (_menuPermissions.ContainsKey(role))
                return _menuPermissions[role];

            return null;
        }
    }
}
