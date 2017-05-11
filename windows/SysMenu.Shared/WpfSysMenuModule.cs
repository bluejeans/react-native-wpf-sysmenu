using ReactNative.Bridge;

namespace WpfSysMenu
{
    public sealed class WpfSysMenuModule : ReactContextNativeModuleBase, ILifecycleEventListener
    {
        SysMenuHelper _menu = new SysMenuHelper();
        public WpfSysMenuModule(ReactContext reactContext)
            : base(reactContext)
        {
        }

        #region INativeModule Members
        /// <summary>
        /// The name of the module.
        /// </summary>
        /// <remarks>
        /// This will be the name used to <code>require()</code> this module
        /// from JavaScript.
        /// </remarks>
        public override string Name => "WpfSysMenuComponent";

        /// <summary>
        /// Called when a <see cref="IReactInstance"/> is initializing.
        /// </summary>
        public override void Initialize()
        {
            Context.AddLifecycleEventListener(this);
        }
        #endregion

        #region ILifecycleEventListener Members
        public void OnDestroy()
        {
            //here will remove items
            _menu.RemoveMenu();
        }

        public void OnResume()
        {
            //here will add menu items
            _menu.AddMenu();
        }

        public void OnSuspend()
        {
        }
        #endregion
    }
}
