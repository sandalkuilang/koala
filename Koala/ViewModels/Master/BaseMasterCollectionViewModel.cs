using Koala.Core;
using System;
using System.Windows.Input;

namespace Koala.ViewModels.Master
{
    public abstract class BaseMasterCollectionViewModel<T> : BaseCollectionViewModel<T>, IEditGridCommand where T : BaseGridRow
    {

        public event EventHandler SourceChanged;
        public ICommand EditCommand { get; set; } 
        public ICommand DeleteCommand { get; set; }
        public ICommand CreateCommand { get; set; }

        private MutableObservableCollection<T> source;
        public override MutableObservableCollection<T> Source
        {
            get
            {
                return source;
            }
            set
            {
                NotifyIfChanged(ref source, value);
            }
        }

        public BaseMasterCollectionViewModel()
        {
            Source = new MutableObservableCollection<T>();
            EditCommand = new DelegateCommand<object>(new Action<object>(OnEdit));
            DeleteCommand = new DelegateCommand<object>(new Action<object>(OnDelete));
            CreateCommand = new DelegateCommand(new Action(OnCreate)); 
        }

        protected string GenerateId(int length)
        {
            return Guid.NewGuid().ToString().Replace("-","").Substring(0, length).ToUpper();
        }

        protected void OnSourceChanged(object sender)
        {
            if (SourceChanged != null)
                SourceChanged(sender, null);
        }

        public abstract void OnEdit(object arg);
        public abstract void OnDelete(object arg); 
        public abstract void OnCreate();
    }
}
