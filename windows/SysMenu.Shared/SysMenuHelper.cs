using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSysMenu
{
    class SysMenuHelper
    {
        const int _SettingsSysMenuID = 10001;
        const int _AboutSysMenuID = 10002;

        ISysMenuItem _menuItem = new SysMenuItem();

        internal void AddMenu()
        {
            /// Create our new System Menu items just before the Close menu item

            _menuItem.AddSeparator(5);
            _menuItem.AddItem(6, _SettingsSysMenuID, "Settings...");
            _menuItem.AddItem(7, _AboutSysMenuID, "About...");

            _menuItem.RegisterHandler();
        }

        internal void RemoveMenu()
        {
            _menuItem.UnregisterHandler();

            _menuItem.RemoveItem(7);
            _menuItem.RemoveItem(6);
            _menuItem.RemoveItem(5);
        }

    }
}
