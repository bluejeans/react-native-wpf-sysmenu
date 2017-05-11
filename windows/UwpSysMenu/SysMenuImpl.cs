namespace WpfSysMenu
{
    ///
    ///
    class SysMenuItem : ISysMenuItem
    {
        public bool AddItem(int index, int id, string name)
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
