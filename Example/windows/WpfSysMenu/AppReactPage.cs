using ReactNative;
using ReactNative.Modules.Core;
using ReactNative.Shell;
using System.Collections.Generic;
using WpfSysMenu;

namespace WpfSysMenuDemo
{
    internal class AppReactPage : ReactPage
    {
        public override string MainComponentName => "WpfSysMenu";

        public override string JavaScriptMainModuleName => "index.windows";

#if BUNDLE
        public override string JavaScriptBundleFile => AppDomain.CurrentDomain.BaseDirectory + "ReactAssets/index.windows.bundle";
#endif

        public override List<IReactPackage> Packages => new List<IReactPackage>
        {
            new WpfSusMenuPackage(),
            new MainReactPackage(),
        };

        public override bool UseDeveloperSupport
        {
            get
            {
#if !BUNDLE || DEBUG
                return true;
#else
                return false;
#endif
            }
        }
    }
}
