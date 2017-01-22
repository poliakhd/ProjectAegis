using System;
using System.Windows;
using System.Windows.Threading;
using AlphaChiTech.Virtualization;

namespace ProjectAegis.AI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            if (!VirtualizationManager.IsInitialized)
            {
                VirtualizationManager.Instance.UIThreadExcecuteAction =
                    (a) => Dispatcher.Invoke(a);
                new DispatcherTimer(
                    TimeSpan.FromSeconds(1),
                    DispatcherPriority.Background,
                    (s, a) => VirtualizationManager.Instance.ProcessActions(),
                    this.Dispatcher).Start();
            }
        }
    }
}
