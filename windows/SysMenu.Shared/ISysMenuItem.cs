using System;

namespace WpfSysMenu
{
    interface ISysMenuItem
    {
        bool AddItem(int index, int id, string name);
        bool EnableItem(int id, bool enable);
        bool AddSeparator(int index);
        bool RemoveItem(int index);

        void RegisterHandler();
        void UnregisterHandler();

        event EventHandler<int> ItemClicked;
    }
}
