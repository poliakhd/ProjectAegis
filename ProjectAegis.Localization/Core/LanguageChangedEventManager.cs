namespace ProjectAegis.Localization.Core
{
    using System;
    using System.Windows;

    using Managers;

    public class LanguageChangedEventManager : WeakEventManager
    {
        protected override void StartListening(object source)
        {
            var manager = (TranslationManager)source;
            manager.LanguageChanged += OnLanguageChanged;
        }
        protected override void StopListening(object source)
        {
            var manager = (TranslationManager)source;
            manager.LanguageChanged -= OnLanguageChanged;
        }

        public static void AddListener(TranslationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }
        public static void RemoveListener(TranslationManager source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            DeliverEvent(sender, e);
        }

        private static LanguageChangedEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(LanguageChangedEventManager);
                var manager = (LanguageChangedEventManager)GetCurrentManager(managerType);

                if (manager == null)
                {
                    manager = new LanguageChangedEventManager();
                    SetCurrentManager(managerType, manager);
                }

                return manager;
            }
        }
    }
}