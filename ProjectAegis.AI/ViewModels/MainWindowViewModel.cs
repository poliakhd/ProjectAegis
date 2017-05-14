using System;
using System.Linq;
using AlphaChiTech.Virtualization;

namespace ProjectAegis.AI.ViewModels
{
    using System.IO;
    using System.Windows;

    using Caliburn.Micro;

    using Microsoft.Win32;

    using Models;
    using Models.TargetParameters;
    using Shared.Library.Extensions;
    using Trigger = Models.Trigger;


    public class ControllersProvider : IPagedSourceProvider<Controller>
    {
        #region Private Members

        private readonly BindableCollection<Controller> _controllers;

        #endregion

        public ControllersProvider(BindableCollection<Controller> controllers)
        {
            _controllers = controllers;
        }

        #region Implementation of IBaseSourceProvider

        public void OnReset(int count)
        {
            
        }

        #endregion

        #region Implementation of IPagedSourceProvider<Controller>

        public PagedSourceItemsPacket<Controller> GetItemsAt(int pageoffset, int count, bool usePlaceholder)
        {
            return new PagedSourceItemsPacket<Controller>()
            {
                LoadedAt = DateTime.Now,
                Items = (from items in _controllers select items).Skip(pageoffset).Take(count)
            };
        }

        public int IndexOf(Controller item)
        {
            return _controllers.IndexOf(item);
        }
        public int Count => _controllers.Count;

        #endregion
    }

    public sealed class MainWindowViewModel : Screen
    {
        #region Private members

        private Policy _aiPolicy;

        private Trigger _trigger;
        private Procedure _procedure;
        private Controller _controller;

        private VirtualizingObservableCollection<Controller> _controllers;

        private bool _isOpenFlyout;
        private int _controllerIndex;

        #endregion

        public int ControllerIndex
        {
            get { return _controllerIndex; }
            set
            {
                _controllerIndex = value;
                NotifyOfPropertyChange();
            }
        }

        public Trigger Trigger
        {
            get { return _trigger; }
            set
            {
                if (value == null)
                    return;

                _trigger = value;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Procedures));
            }
        }
        public Procedure Procedure
        {
            get { return _procedure; }
            set
            {
                if (value == null)
                    return;

                _procedure = value;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Procedure.Parameters));
                NotifyOfPropertyChange(nameof(IsTargetParameterVisible));
            }
        }
        public Controller Controller
        {
            get { return _controller; }
            set
            {
                _controller = value;

                NotifyOfPropertyChange();
                NotifyOfPropertyChange(nameof(Triggers));
            }
        }

        public VirtualizingObservableCollection<Controller> Controllers => _controllers;
        public BindableCollection<Trigger> Triggers
        {
            get { return _controller.Triggers; }
            set
            {
                _controller.Triggers = value;
                NotifyOfPropertyChange();
            }
        }
        public BindableCollection<Procedure> Procedures
        {
            get { return _trigger.Procedures; }
            set
            {
                _trigger.Procedures = value;
                NotifyOfPropertyChange();
            }
        }

        public bool IsOpenFlyout
        {
            get { return _isOpenFlyout; }
            set
            {
                _isOpenFlyout = value;
                NotifyOfPropertyChange();
            }
        }

        public Visibility IsTargetParameterVisible => Procedure.TargetParameters is ClassComboParameters ? Visibility.Visible : Visibility.Collapsed;

        public MainWindowViewModel()
        {
            base.DisplayName = "projectaegis.ai";

            Trigger = new Trigger();
            Procedure = new Procedure();
            Controller = new Controller();        

            using (var reader = new BinaryReader(new FileStream(@"I:\Projects\data\aipolicy.data", FileMode.Open, FileAccess.Read)))
            {
                _aiPolicy = reader.ReadModel<Policy>();
            }

            _controllers = new VirtualizingObservableCollection<Controller>(new PaginationManager<Controller>(new ControllersProvider(_aiPolicy.Controllers)));

            NotifyOfPropertyChange(nameof(Controllers));
        }

        private void OpenFile()
        {
            var dialog = new OpenFileDialog() { Filter = ".data files|*.data|All files |*.*" };

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                using (var reader = new BinaryReader(new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read)))
                {
                    _aiPolicy = reader.ReadModel<Policy>();
                }

                ControllerIndex = 0;

                Trigger = new Trigger();
                Procedure = new Procedure();
                Controller = new Controller();
            }
        }
        public void SaveFile()
        {
            var dialog = new OpenFileDialog { Filter = ".data files|*.data|All files |*.*" };

            //bool? result = dialog.ShowDialog();

            //if (result == true)
            //{
                using (var writer = new BinaryWriter(new FileStream(@"I:\Projects\data\aipolicy_s.data", FileMode.Create, FileAccess.Write)))
                {
                    writer.WriteModel(_aiPolicy);
                }
            //}
        }

        public void Click()
        {
            var controller = new Controller();

            _aiPolicy.Controllers.Add(controller);
            _controllers.Add(controller);

            NotifyOfPropertyChange(nameof(Controllers));
        }
    }
}