using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace WpfSysMenu
{
    class SysMenuHelper
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

        // The constants we'll use to identify our custom system menu items
        public const Int32 _SettingsSysMenuID = 1000;
        public const Int32 _AboutSysMenuID = 1001;
        /// <summary>
        /// This is the Win32 Interop Handle for this Window
        /// </summary>
        public IntPtr Handle
        {
            get
            {
                return new WindowInteropHelper(Application.Current.MainWindow).Handle;
            }
        }

        HwndSourceHook _hook;

        internal void AddMenu()
        {
            /// Get the Handle for the Forms System Menu
            IntPtr systemMenuHandle = GetSystemMenu(this.Handle, false);
            /// Create our new System Menu items just before the Close menu item
            InsertMenu(systemMenuHandle, 5, MF_BYPOSITION | MF_SEPARATOR, 0, string.Empty); // <-- Add a menu seperator
            InsertMenu(systemMenuHandle, 6, MF_BYPOSITION, _SettingsSysMenuID, "Settings...");
            InsertMenu(systemMenuHandle, 7, MF_BYPOSITION, _AboutSysMenuID, "About...");
            
            // Attach our WndProc handler to this Window
            var source = HwndSource.FromHwnd(this.Handle);
            if (_hook != null)
            {
                source.RemoveHook(_hook);
            }
            _hook = new HwndSourceHook(WndProc);
            source.AddHook(_hook);

        }

        internal void RemoveMenu()
        {
            if (_hook == null)
            {
                throw new InvalidOperationException("Menu has not been created yet");
            }

            var source = HwndSource.FromHwnd(this.Handle);
            source.RemoveHook(_hook);
            _hook = null;

            /// Get the Handle for the Forms System Menu
            IntPtr systemMenuHandle = GetSystemMenu(this.Handle, false);
            /// Create our new System Menu items just before the Close menu item
            DeleteMenu(systemMenuHandle, 7, MF_BYPOSITION);
            DeleteMenu(systemMenuHandle, 6, MF_BYPOSITION);
            DeleteMenu(systemMenuHandle, 5, MF_BYPOSITION); // <-- Add a menu seperator

        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Check if a System Command has been executed
            if (msg == WM_SYSCOMMAND)
            {
                // Execute the appropriate code for the System Menu item that was clicked
                switch (wParam.ToInt32())
                {
                    case _SettingsSysMenuID:
                        MessageBox.Show("\"Settings\" was clicked");
                        handled = true;
                        break;
                    case _AboutSysMenuID:
                        MessageBox.Show("\"About\" was clicked");
                        handled = true;
                        break;
                }
            }
            return IntPtr.Zero;
        }
    }
}
