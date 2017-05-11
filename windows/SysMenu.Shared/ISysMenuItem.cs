using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSysMenu
{
    interface ISysMenuItem
    {
        bool AddItem(int index, int id, string name);
        bool AddSeparator(int index);
        bool RemoveItem(int index);

        void RegisterHandler();
        void UnregisterHandler();
    }
}
