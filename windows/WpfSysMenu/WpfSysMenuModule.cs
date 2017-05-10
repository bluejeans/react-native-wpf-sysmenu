using ReactNative.Bridge;

namespace WpfSysMenu
{
    public sealed class WpfSysMenuModule : ReactContextNativeModuleBase, ILifecycleEventListener
    {
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
        }

        public void OnResume()
        {
        }

        public void OnSuspend()
        {
        }
        #endregion
    }
}
