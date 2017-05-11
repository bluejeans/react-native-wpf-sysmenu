using System;

namespace WpfSysMenu
{
    class SysMenuItem : ISysMenuItem
    {
        #pragma warning disable CS0067 //Never used
        public event EventHandler<int> ItemClicked;
        #pragma warning restore CS0067

        public bool AddItem(int index, int id, string name)
        {
            return true;
        }

        public bool EnableItem(int id, bool enable)
        {
            return true;
        }

        public bool AddSeparator(int index)
        {
            return true;
        }

        public bool RemoveItem(int index)
        {
            return true;
        }

        public void RegisterHandler()
        {
        }

        public void UnregisterHandler()
        {
        }

    }
}
