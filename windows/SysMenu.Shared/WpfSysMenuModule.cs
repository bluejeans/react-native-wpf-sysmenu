using ReactNative.Bridge;
using ReactNative.Modules.Core;
using System;
using System.Threading.Tasks;

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
            _menu.RemoveAll();
        }

        public void OnResume()
        {
            _menu.ItemClicked += _menu_ItemClicked;
        }

        public void OnSuspend()
        {
            _menu.ItemClicked -= _menu_ItemClicked;
        }
        #endregion

        #region React Methods
        [ReactMethod]
        public async void addSeparator(IPromise promise)
        {
            await BackgroundRun(() => _menu.AddSeparator(), promise).ConfigureAwait(false);
        }

        [ReactMethod]
        public async void addItem(int id, string name, IPromise promise)
        {
            await BackgroundRun(() => _menu.AddItem(id, name), promise).ConfigureAwait(false);
        }

        [ReactMethod]
        public async void enableItem(int id, bool enable, IPromise promise)
        {
            await BackgroundRun(() => _menu.EnableItem(id, enable), promise).ConfigureAwait(false);
        }

        [ReactMethod]
        public async void removeAll(IPromise promise)
        {
            await BackgroundRun(() => _menu.RemoveAll(), promise).ConfigureAwait(false);
        }

        #endregion

        #region Private Methods
        private void _menu_ItemClicked(object sender, int id)
        {
            SendEvent("SystemMenuItemClicked", id);
        }

        private void SendEvent(string eventName, int id)
        {
            Context.GetJavaScriptModule<RCTDeviceEventEmitter>().emit(eventName, id);
        }

        async Task BackgroundRun(Action a, IPromise promise)
        {
            await Task.Run(() =>
            {
                try
                {
                    a();
                    promise.Resolve("");
                }
                catch (Exception e)
                {
                    promise.Reject(e);
                }
            }).ConfigureAwait(false);
        }
        #endregion
    }
}
