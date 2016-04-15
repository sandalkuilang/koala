using System;
using ReactiveUI;
using System.Reactive.Linq;

namespace Koala.ViewModels
{
    public abstract class BaseCollectionViewModel<T> : BaseBindableModel, ISelectableGridCommand where T : BaseGridRow
    { 
    //    public System.Windows.Input.ICommand CheckedHeaderCommand { get; set; }

    //    public System.Windows.Input.ICommand CheckedRowCommand { get; set; }

        public ReactiveCommand<object> CheckedHeaderCommand { get; set; }

        public ReactiveCommand<dynamic> CheckedRowCommand { get; set; }


        private bool checkedHeader;
        public bool CheckedHeader
        {
            get
            {
                return checkedHeader;
            }
            set
            {
                if (this.Source.Count == 0 && !checkedHeader)
                    return;

                NotifyIfChanged(ref checkedHeader, value);  
            }
        }

        private MutableObservableCollection<BaseGridRow> selectableRows;

        public MutableObservableCollection<BaseGridRow> SelectableRows
        {
            get
            {
                return selectableRows;
            }
            set
            {
                NotifyIfChanged(ref selectableRows, value);
            }
        }

        public BaseCollectionViewModel()
        {
            SelectableRows = new MutableObservableCollection<BaseGridRow>();

            //CheckedHeaderCommand = new DelegateCommand<object>(new Action<object>(OnCheckedHeader)); 
            //CheckedRowCommand = new DelegateCommand<dynamic>(new Action<dynamic>(OnCheckedRow));

            CheckedHeaderCommand = ReactiveCommand.Create();
            CheckedHeaderCommand.Subscribe(x =>
            {
                if (this.Source.Count == 0)
                    return;

                bool check = (bool)x;

                SelectableRows.Clear();
                foreach (T item in this.Source)
                {
                    item.IsSelected = check;
                    item.SelectAll = check;

                    if (item.IsSelected)
                    {
                        SelectableRows.Add(item);
                    }
                    else
                        SelectableRows.Remove(item);
                } 
            });
            //CheckedRowCommand = new DelegateCommand<dynamic>(new Action<dynamic>(OnCheckedRow));
            CheckedRowCommand = ReactiveCommand.Create();
            CheckedRowCommand.Subscribe(x => 
            {
                SelectableRows.Clear();
                foreach (BaseGridRow item in Source)
                {
                    if (item.IsSelected)
                        SelectableRows.Add(item);
                    else
                        SelectableRows.Remove(item);
                }

                if (selectableRows.Count == Source.Count)
                    CheckedHeader = true;
                else
                    CheckedHeader = false; 
            });
        }

        //private void OnUncheckedRow(dynamic arg)
        //{
        //    SelectableRows.Clear();
        //    foreach (dynamic item in arg)
        //    {
        //        if (item.IsSelected)
        //            SelectableRows.Add(item);
        //        else
        //            SelectableRows.Remove(item);
        //    }
        //    CheckedHeader = false;  
        //}

        public abstract MutableObservableCollection<T> Source { get; set; }

        //private void OnCheckedHeader(object arg)
        //{
        //    if (this.Source.Count == 0)
        //        return;

        //    bool check = (bool)arg;
             
        //    SelectableRows.Clear(); 
        //    foreach (T item in this.Source)
        //    {
        //        item.IsSelected = check;
        //        item.SelectAll = check;

        //        if (item.IsSelected)
        //        { 
        //            SelectableRows.Add(item);
        //        }
        //        else
        //            SelectableRows.Remove(item);
        //    } 
        //}

        //private void OnCheckedRow(dynamic arg)
        //{

        //    SelectableRows.Clear();
        //    foreach (BaseGridRow item in Source)
        //    {
        //        if (item.IsSelected)
        //            SelectableRows.Add(item);
        //        else
        //            SelectableRows.Remove(item);
        //    }

        //    if (selectableRows.Count == Source.Count)
        //        CheckedHeader = true;
        //    else
        //        CheckedHeader = false; 
        //}

    }
}
