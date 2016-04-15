namespace Koala.ViewModels
{
    public class BaseGridRow : BaseBindableModel
    {
        private bool selectAll;
        public bool SelectAll
        {
            get
            {
                return selectAll;
            }
            set
            {
                NotifyIfChanged(ref this.selectAll, value);
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                NotifyIfChanged(ref this.isSelected, value);
            }
        }

    }
}
