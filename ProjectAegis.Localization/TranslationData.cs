namespace ProjectAegis.Localization
{
    using System;
    using System.ComponentModel;
    using System.Windows;

    using Core;
    using Managers;

    public class TranslationData : IWeakEventListener, INotifyPropertyChanged
    {
        #region Private Members

        private string _key;

        #endregion

        public object Value => TranslationManager.Instance.Translate(_key);

        public TranslationData(string key)
        {
            _key = key;
            LanguageChangedEventManager.AddListener(TranslationManager.Instance, this);
        }
        ~TranslationData()
        {
            LanguageChangedEventManager.RemoveListener(TranslationManager.Instance, this);
        }

        #region Implementation of IWeakEventListener

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(LanguageChangedEventManager))
            {
                OnLanguageChanged(sender, e);
                return true;
            }
            return false;
        }

        #endregion

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
        }
    }
}