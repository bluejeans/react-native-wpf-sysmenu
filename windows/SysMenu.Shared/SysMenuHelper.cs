using System;
using System.Collections.Generic;

namespace WpfSysMenu
{
    class SysMenuHelper
    {
        const int START_MENU_INDEX = 5;

        int _index = START_MENU_INDEX; //start index for add items in system menu
        bool _isMenuRegistered = false;

        ISysMenuItem _menuItem = new SysMenuItem();

        public event EventHandler<int> ItemClicked;

        public void AddSeparator()
        {
            _menuItem.AddSeparator(_index++);
        }

        public void AddItem(int id, string name)
        {
            if (!_menuItem.AddItem(_index, id, name))
            {
                throw new InvalidOperationException($"Menu Item {id}:[{name}] cannot be added");
            }

            ++_index;

            if (!_isMenuRegistered)
            {
                _menuItem.RegisterHandler();
                _menuItem.ItemClicked += _menuItem_ItemClicked;
                _isMenuRegistered = true;
            }
        }

        public void EnableItem(int id, bool enable)
        {
            _menuItem.EnableItem(id, false);
        }

        public void RemoveAll()
        {
            _menuItem.ItemClicked -= _menuItem_ItemClicked;
            _isMenuRegistered = false;
            _menuItem.UnregisterHandler();

            for (; _index > START_MENU_INDEX; _index--)
            {
                _menuItem.RemoveItem(_index);
            }
        }

        private void _menuItem_ItemClicked(object sender, int id)
        {
            ItemClicked?.Invoke(this, id);
        }
    }
}
