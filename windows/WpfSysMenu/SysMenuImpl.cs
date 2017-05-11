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
        private static extern bool DeleteMenu(IntPtr hMenu, Int32 wPosition, Int32 wFlags);

        /// Define our Constants we will use
        public const Int32 WM_SYSCOMMAND = 0x112;
        public const Int32 MF_SEPARATOR = 0x800;
        public const Int32 MF_BYPOSITION = 0x400;
        public const Int32 MF_STRING = 0x0;
        #endregion

        public SysMenuItem()
        {
            _systemMenuHandle = GetSystemMenu(this.Handle, false);
        }

        IntPtr Handle => new WindowInteropHelper(Application.Current.MainWindow).Handle;
        HwndSourceHook _hook;
        IntPtr _systemMenuHandle;


        public void RegisterHandler()
        {
            // Attach our WndProc handler to this Window
            var source = HwndSource.FromHwnd(this.Handle);
            if (_hook != null)
            {
                source.RemoveHook(_hook);
            }
            _hook = new HwndSourceHook(WndProc);
            source.AddHook(_hook);

        }

        public void UnregisterHandler()
        {
            if (_hook == null)
            {
                throw new InvalidOperationException("Menu has not been created yet");
            }

            var source = HwndSource.FromHwnd(this.Handle);
            source.RemoveHook(_hook);
            _hook = null;
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Check if a System Command has been executed
            if (msg == WM_SYSCOMMAND)
            {
                // Execute the appropriate code for the System Menu item that was clicked
                switch (wParam.ToInt32())
                {
                    case 10001:
                        MessageBox.Show("\"Settings\" was clicked");
                        handled = true;
                        break;
                    case 10002:
                        MessageBox.Show("\"About\" was clicked");
                        handled = true;
                        break;
                }
            }
            return IntPtr.Zero;
        }

        public bool AddItem(int index, int id, string name)
        {
            return InsertMenu(_systemMenuHandle, index, MF_BYPOSITION, id, name);
        }

        public bool AddSeparator(int index)
        {
            return InsertMenu(_systemMenuHandle, index, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty);
        }

        public bool RemoveItem(int index)
        {
            return DeleteMenu(_systemMenuHandle, index, MF_BYPOSITION);
        }
    }
}
