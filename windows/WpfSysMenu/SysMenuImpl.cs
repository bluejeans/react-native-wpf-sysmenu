using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace WpfSysMenu
{
    class SysMenuItem : ISysMenuItem
    {
        #region Win32 API Stuff
        // Define the Win32 API methods we are going to use
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        private static extern bool ModifyMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags, Int32 wIDNewItem, string lpNewItem);

        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, Int32 wIDNewItem, Int32 wFlags);

        [DllImport("user32.dll")]
        private static extern bool DeleteMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags);

        /// Define our Constants we will use
        public const Int32 WM_SYSCOMMAND = 0x112;
        public const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_BYPOSITION = 0x400;
        public const Int32 MF_BYCOMMAND = 0x000;
        public const Int32 MF_ENABLED = 0x000;
        public const Int32 MF_GRAYED = 0x001;
        public const Int32 MF_DISABLED = 0x002;
        public const Int32 MF_STRING = 0x0;
        public const Int32 SC_SIZE = 0xF000;
        #endregion

        public SysMenuItem()
        {
            _systemMenuHandle = GetSystemMenu(this.Handle, false);
        }

        IntPtr Handle => new WindowInteropHelper(Application.Current.MainWindow).Handle;
        HwndSourceHook _hook;
        IntPtr _systemMenuHandle;

        #region ISysMenuItem Implementation
        public bool AddSeparator(int index)
        {
            return InsertMenu(_systemMenuHandle, index, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
        }

        public bool AddItem(int index, int id, string name)
        {
            if (id >= SC_SIZE)
            {
                throw new ArgumentException($"id should be between 1 and 61000, given {id} : {name}");
            }
            return InsertMenu(_systemMenuHandle, index, MF_BYPOSITION, id, name);
        }

        public bool EnableItem(int id, bool enable)
        {
            var flag = enable ? MF_ENABLED : MF_DISABLED;
            return EnableMenuItem(_systemMenuHandle, id, MF_BYCOMMAND | flag);
        }

        public bool RemoveItem(int index)
        {
            return DeleteMenu(_systemMenuHandle, index, MF_BYPOSITION);
        }

        public void RegisterHandler()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                // Attach WndProc handler to this Window
                var source = HwndSource.FromHwnd(this.Handle);
                if (_hook != null)
                {

                    UnregisterHandler();
                }
                WndProcFired += SysMenuItem_WndProcFired;
                _hook = new HwndSourceHook(WndProc);
                source.AddHook(_hook);
            });

        }


        public void UnregisterHandler()
        {
            if (_hook == null)
            {
                throw new InvalidOperationException("Menu has not been created yet");
            }

            Application.Current.Dispatcher.Invoke(() =>
            {
                WndProcFired -= SysMenuItem_WndProcFired;
                var source = HwndSource.FromHwnd(this.Handle);
                source.RemoveHook(_hook);
                _hook = null;
            });
        }

        public event EventHandler<int> ItemClicked;

        #endregion

        private void SysMenuItem_WndProcFired(object sender, int id)
        {
            ItemClicked?.Invoke(this, id);
        }

        #region Static Stuff
        static event EventHandler<int> WndProcFired;

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_SYSCOMMAND)
            {
                var id = wParam.ToInt32();
                if (id < SC_SIZE)
                {
                    WndProcFired?.Invoke(null, id);
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }
        #endregion
    }
}
