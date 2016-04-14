using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Master;
using Koala.Views.Dialogs;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Texaco.Database;

using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Diagnostics;
using System.ComponentModel;

namespace Koala.ViewModels.Order
{
    public class CreateOrderDetailModel : BaseGridRow, ICloneable, IDataErrorInfo
    {
       
        public delegate void OrderDetailEventHandler(object sender, CreateOrderDetailModel e);

        public event OrderDetailEventHandler AddingOrderDetail;
        public event OrderDetailEventHandler UpdatingOrderDetail;

        private string orderId;
        public string OrderId
        {
            get
            {
                return orderId;
            }
            set
            {
                NotifyIfChanged(ref orderId, value);
            }
        }

        private int seqNbr;
        public int SeqNbr
        {
            get
            {
                return seqNbr;
            }
            set
            {
                NotifyIfChanged(ref seqNbr, value);
            }
        }

        private DateTime createdDate;
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                NotifyIfChanged(ref createdDate, value);
            }
        }


        private string finishingId;
        public string FinishingId
        {
            get
            {
                return finishingId;
            }
            set
            {
                NotifyIfChanged(ref finishingId, value); 
            }
        }
         
        private string materialId;
        public string MaterialId
        {
            get
            {
                return materialId;
            }
            set
            {
                NotifyIfChanged(ref materialId, value); 
            }
        }

        private string sizeId;
        public string SizeId
        {
            get
            {
                return sizeId;
            }
            set
            {
                NotifyIfChanged(ref sizeId, value); 
            }
        }

        private string qualityId;
        public string QualityId
        {
            get
            {
                return qualityId;
            }
            set
            {
                NotifyIfChanged(ref qualityId, value); 
            }
        }


        private KeyValueOption selectedQuality;
        public KeyValueOption SelectedQuality
        {
            get
            {
                return selectedQuality;
            }
            set
            {
                NotifyIfChanged(ref selectedQuality, value);
                if (selectedMaterial != null && value != null && materialMaster != null)
                {
                    selectedMaterial = MaterialMaster.Where(s => s.Id == selectedMaterial.Id && s.QualityId == value.Id).SingleOrDefault();
                    if (selectedMaterial != null)
                        Price = selectedMaterial.Price * qty;
                }
                else
                    Price = 0;
            }
        }
         

        private KeyValueOption selectedFinishing;
        public KeyValueOption SelectedFinishing
        {
            get
            {
                return selectedFinishing;
            }
            set
            {
                NotifyIfChanged(ref selectedFinishing, value);
            }
        }


        private MaterialType selectedMaterial;
        public MaterialType SelectedMaterial
        {
            get
            {
                return selectedMaterial;
            }
            set
            {
                NotifyIfChanged(ref selectedMaterial, value);
                if (selectedMaterial != null && materialMaster != null)
                {
                    IEnumerable<MaterialType> filteringMaterial = MaterialMaster.Where(s => s.Id == selectedMaterial.Id).Select(c => c);
                    List<QualityType> filterQuality = new List<QualityType>();
                    foreach(MaterialType material in filteringMaterial)
                    {
                        filterQuality.Add(new QualityType() 
                        { 
                            Id = material.QualityId,
                            Description = material.QualityName
                        });
                    }

                    Quality = filterQuality;
                    if (qualityId != null)
                        SelectedQuality = Quality.Where(x => x.Id == qualityId).SingleOrDefault();
                    else
                        SelectedQuality = Quality.FirstOrDefault();

                    //if (selectedQuality != null)
                        //Price = selectedMaterial.Price * qty;
                    //Price = materialMaster.Where(x => x.Id == selectedMaterial.Id && x.QualityId == selectedQuality.Id).SingleOrDefault().Price * qty;
                   // else
                       // Price = 0;
                }
            }
        }

        private KeyValueOption selectedSize;
        public KeyValueOption SelectedSize
        {
            get
            {
                return selectedSize;
            }
            set
            {
                NotifyIfChanged(ref selectedSize, value);
            }
        }


        private List<QualityType> quality;
        public List<QualityType> Quality
        {
            get
            {
                return quality;
            }
            set
            {
                NotifyIfChanged(ref quality, value); 

                if (!string.IsNullOrEmpty(qualityId))
                    SelectedQuality = Quality.Where(x => x.Id == qualityId).SingleOrDefault();
            }
        }

        private List<KeyValueOption> finishing;
        public List<KeyValueOption> Finishing
        {
            get
            {
                return finishing;
            }
            set
            {
                NotifyIfChanged(ref finishing, value);

                if (!string.IsNullOrEmpty(finishingId))
                    SelectedFinishing = Finishing.Where(x => x.Id == finishingId).SingleOrDefault();
            }
        }


        private List<KeyValueOption> size;
        public List<KeyValueOption> Size
        {
            get
            {
                return size;
            }
            set
            {
                NotifyIfChanged(ref size, value);

                if (!string.IsNullOrEmpty(sizeId))
                    SelectedSize = Size.Where(x => x.Id == sizeId).SingleOrDefault();
            }
        }

        private List<MaterialType> materialMaster;
        public List<MaterialType> MaterialMaster
        {
            get
            {
                return materialMaster;
            }
            set
            {
                NotifyIfChanged(ref materialMaster, value); 
            }
        }

        private List<MaterialType> material;
        public List<MaterialType> Material
        {
            get
            {
                return material;
            }
            set
            {
                NotifyIfChanged(ref material, value);

                if (!string.IsNullOrEmpty(materialId))
                    SelectedMaterial = Material.Where(x => x.Id == materialId).SingleOrDefault();
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                NotifyIfChanged(ref title, value);
            }
        }

        private int width;
        public int Width
        {
            get
            {
                return width;
            }
            set
            {
                NotifyIfChanged(ref width, value);
            }
        }

        private int height;
        public int Height
        {
            get
            {
                return height;
            }
            set
            {
                NotifyIfChanged(ref height, value);
            }
        }

        private int qty;
        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                NotifyIfChanged(ref qty, value);
                if (selectedMaterial != null)
                    Price = selectedMaterial.Price * value;

            }
        }
        private string filename;
        public string Filename
        {
            get
            {
                return filename;
            }
            set
            {
                NotifyIfChanged(ref filename, value);
            }
        }

        private ImageSource thumbnailImage;
        public ImageSource ThumbnailImage
        {
            get
            {
                return thumbnailImage;
            }
            set
            {
                NotifyIfChanged(ref thumbnailImage, value);
            }
        }

        private byte[] stream;
        public byte[] Stream
        {
            get
            {
                return stream;
            }
            set
            {
                NotifyIfChanged(ref stream, value);
                if (value != null)
                { 
                    ThumbnailImage = CreateThumbnail(new MemoryStream(value));
                }      
            }
        }

        private decimal price;
        public decimal Price
        {
            get
            { 
                return price;
            }
            set
            {
                NotifyIfChanged(ref price, value);
            }
        }

        private string queueStatus;
        public string QueueStatus
        {
            get
            {
                queueStatus = GetQueueStatus();
                return queueStatus;
            }
            set
            { 
                NotifyIfChanged(ref queueStatus, GetQueueStatus());
            }
        }

        private string GetQueueStatus()
        {
            string stat;
            if (queue == 1)
                stat = "Uncompleted";
            else if (queue == 2)
                stat = "Completed";
            else
                stat = "Initial";
            return stat;
        }

        private bool canUpdateDetailStatus;
        public bool CanUpdateDetailStatus
        {
            get
            {
                canUpdateDetailStatus = queue == 2;
                return canUpdateDetailStatus;
            }
            set
            {
                NotifyIfChanged(ref canUpdateDetailStatus, value);
            }
        }
         
        private int queue;
        public int Queue
        {
            get
            {
                return queue;
            }
            set
            {
                NotifyIfChanged(ref queue, value);
            }
        }

        private DateTime deadline;
        public DateTime Deadline
        {
            get
            {
                return deadline;
            }
            set
            {
                NotifyIfChanged(ref deadline, value);
            }
        }

        private string description;
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                NotifyIfChanged(ref description, value);
            }
        }
         
        private bool checkedHeader;
        public bool CheckedHeader
        {
            get
            {
                return checkedHeader;
            }
            set
            {
                foreach (CreateOrderDetailModel item in SelectableRow.ToList())
                {
                    item.IsSelected = checkedHeader;
                }
                if (selectableRow.Count == 0)
                    value = false;
                NotifyIfChanged(ref checkedHeader, value);
            }
        }

        private MutableObservableCollection<BaseGridRow> selectableRow;

        public MutableObservableCollection<BaseGridRow> SelectableRow
        {
            get
            {
                return selectableRow;
            }
            set
            {
                NotifyIfChanged(ref selectableRow, value);
            }
        }
        public event EventHandler ItemChanged;

        private bool isReadOnly;
        public bool IsReadOnly
        {
            get
            {
                return isReadOnly;
            }
            set
            {
                NotifyIfChanged(ref isReadOnly, value);
            }
        }

        public Visibility UpdateVisibility
        {
            get
            {
                return isReadOnly ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility AddVisibility
        {
            get
            {
                return !isReadOnly ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public string Status { get; set; }
        public System.Windows.Input.ICommand UpdateDetailCommand { get; set; }
        public System.Windows.Input.ICommand UploadStreamCommand { get; set; }
        public System.Windows.Input.ICommand RemoveStreamCommand { get; set; }
        public System.Windows.Input.ICommand ViewStreamCommand { get; set; }
        public ReactiveCommand<Object> AddDetailCommand { get; set; }
        public ReactiveCommand<Object> SaveStreamCommand { get; set; }

        public CreateOrderDetailModel()
        {
            SaveStreamCommand = ReactiveCommand.Create();
            SaveStreamCommand.Subscribe(x => 
            {
                SaveFileDialog fileDialog = new SaveFileDialog();
                fileDialog.FileName = this.filename;
                if (fileDialog.ShowDialog().HasValue == true)
                {
                    if (string.IsNullOrEmpty(fileDialog.FileName))
                        return;
                    FileStream stream = System.IO.File.Create(fileDialog.FileName, this.stream.Length, FileOptions.RandomAccess);
                    stream.Write(this.stream, 0, this.stream.Length);
                    stream.Close();
                }
            });

            //AddDetailCommand = new DelegateCommand<object>(new Action<object>(OnAddDetail));
            AddDetailCommand = ReactiveCommand.Create();
            AddDetailCommand.Subscribe(x => 
            {
                OnAddDetail(x);
            });

            UpdateDetailCommand = new DelegateCommand<object>(new Action<object>(OnUpdateDetail));

            UploadStreamCommand = new DelegateCommand(new Action(OnUploadStream));
            RemoveStreamCommand = new DelegateCommand(new Action(OnRemoveStream));
            ViewStreamCommand = new DelegateCommand<string>(new Action<string>(OnViewStream));

            InitializeModel();
        } 

        private void OnItemChanged()
        {
            if (ItemChanged != null)
                ItemChanged(this, null);
        }

        private bool Verify()
        {
            if (string.IsNullOrEmpty(title) && selectedMaterial == null && selectedFinishing == null && (width == 0 && height == 0))
            {
                return false;
            }
            return true;
        }

        private void OnAddDetail(object arg)
        {
            if (!Verify())
            {
                WarningModel warning = new WarningModel()
                {
                    Message = "Please make sure all item is filled"
                };
                IDialogService dialog = ObjectPool.Instance.Resolve<IDialogService>();
                dialog.ShowDialog<Warning>(warning); 
            }
            else
            { 
                Task.Factory.StartNew(() =>
                {
                    IsBusy = true;
                    if (AddingOrderDetail != null)
                        AddingOrderDetail(this, (CreateOrderDetailModel)this.Clone());
                    InitializeModel();
                    IsBusy = false;
                });
            } 
        }

        private void OnUpdateDetail(object arg)
        {
            if (UpdatingOrderDetail != null)
                UpdatingOrderDetail(this, this);

            Task.Run(() => {
                IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);

                CreateOrderDetailModel detail = (CreateOrderDetailModel)arg;
                db.Execute("UpdateOrderDetail", new
                {
                    MaterialTypeId = detail.SelectedMaterial.Id,
                    QualityId = detail.SelectedQuality.Id,
                    FinishingId = detail.SelectedFinishing.Id,
                    Title = detail.title,
                    Width = detail.width,
                    Height = detail.height,
                    Qty = detail.qty,
                    Queue = detail.Queue,
                    Deadline = detail.deadline,
                    Description = detail.description,
                    Total = detail.Price,
                    OrderId = detail.orderId,
                    SeqNbr = detail.SeqNbr
                });

                db.Close();
            }); 
            
        }
         
        private void OnViewStream(string arg)
        {
            string filepath = string.Empty;
            if (arg.ToLower().Equals("autocad"))
            {
                filepath = ApplicationSettings.Instance.OpenWith.Autocad;  
            }
            else if (arg.ToLower().Equals("photoshop"))
            {
                filepath = ApplicationSettings.Instance.OpenWith.Photoshop;

            }

            if (string.IsNullOrEmpty(filepath))
                return;

            //FileStream stream = System.IO.File.Create(fileDialog.FileName, this.stream.Length, FileOptions.RandomAccess);
            //stream.Write(this.stream, 0, this.stream.Length);
            //stream.Close();
            //ProcessStartInfo psi = new ProcessStartInfo();
            //psi.Arguments = "";
            //psi.FileName = filepath;
        }

        private void OnRemoveStream()
        {
            Stream = null;
            ThumbnailImage = null;
        }

        private BitmapImage CreateThumbnail(Stream fileStream)
        {
            try
            {
                fileStream.Position = 0;
                BitmapImage bitmap = new BitmapImage();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.BeginInit();
                bitmap.StreamSource = fileStream;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
            catch (NotSupportedException) { }
            catch (FileFormatException) { }
            return null;
        }

        private void OnUploadStream()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var uiContext = TaskScheduler.FromCurrentSynchronizationContext();
                Task.Factory.StartNew(() => 
                { 
                    IsBusy = true;
                    IsEnabled = false;

                    System.IO.File.SetAttributes(openFileDialog.FileName, FileAttributes.Normal);
                    Stream fileStream = openFileDialog.OpenFile();

                    byte[] imageData = new byte[fileStream.Length - 1];
                    fileStream.Read(imageData, 0, imageData.Length); 
                    ThumbnailImage = CreateThumbnail(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();

                    Stream = imageData;
                    Filename = openFileDialog.SafeFileName;

                    IsBusy = false;
                    IsEnabled = true;
                }, CancellationToken.None, TaskCreationOptions.None, uiContext);
            }
        }

        private void InitializeModel()
        {
            //await Task.Run(() => 
            //{
                this.Title = string.Empty;
                this.SelectedFinishing = null;
                this.SelectedMaterial = null;
                this.SelectedQuality = null;
                this.SelectedSize = null;
                this.QualityId = null;
                this.FinishingId = null;
                this.SizeId = null;
                this.MaterialId = null;
                this.Width = 0;
                this.Height = 0;
                this.Qty = 0;
                this.Price = 0;
                this.Stream = null;
                this.Description = string.Empty;
                this.Deadline = DateTime.Now;
                this.Queue = 0;
                this.ThumbnailImage = null;
                this.Stream = null;
                this.CanUpdateDetailStatus = true;

                Qty = 1;
                //Task.Factory.StartNew(() =>
                //{
                    IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
                    IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.Name);
                    Finishing = db.Query<KeyValueOption>("GetFinishing");
                    Size = db.Query<KeyValueOption>("GetSize");
                    MaterialMaster = db.Query<MaterialType>("GetMaterial");
                    Material = MaterialMaster.GroupBy(x => x.Id).Select(d => d.First()).ToList();
                    db.Close();
                //});
            //}); 
        }


        public object Clone()
        {  
            return BeginCloneAsync().Result;
        }

        private async Task<CreateOrderDetailModel> BeginCloneAsync()
        {
            CreateOrderDetailModel detail = new CreateOrderDetailModel();
             
            await Task.Run(() =>
            {
                if ((this.SelectedFinishing == null) || (this.SelectedMaterial == null) || (this.selectedQuality == null))
                    return;

                detail.title = this.title;
               
                detail.SelectedFinishing = new KeyValueOption();

                detail.SelectedFinishing.Id = this.SelectedFinishing.Id;
                detail.SelectedFinishing.Description = this.SelectedFinishing.Description;

                detail.SelectedQuality = new KeyValueOption();
                detail.SelectedQuality.Id = this.SelectedQuality.Id;
                detail.SelectedQuality.Description = this.SelectedQuality.Description;

                detail.SelectedMaterial = new MaterialType();
                detail.SelectedMaterial.Id = this.SelectedMaterial.Id;
                detail.SelectedMaterial.Description = this.SelectedMaterial.Description;
                detail.SelectedMaterial.QualityId = this.SelectedMaterial.QualityId;
                detail.SelectedMaterial.Price = this.SelectedMaterial.Price;

                detail.QualityId = this.QualityId;
                detail.MaterialId = this.MaterialId;
                detail.FinishingId = this.FinishingId;
                //detail.SizeId = this.SizeId; 

                detail.Width = this.width;
                detail.Height = this.height;
                detail.Qty = this.qty;
                detail.Price = this.price;
                detail.Stream = this.stream;
                detail.Filename = this.filename;
                detail.ThumbnailImage = this.thumbnailImage;
                detail.Description = this.description;
                detail.Deadline = this.deadline;
                detail.Queue = this.queue;

            });

            return detail;
        }

        public string this[string columnName]
        {
            get
            {
                String errorMessage = String.Empty;
                string messageFormat = "{0} cannot be empty";
                switch (columnName)
                {
                    case "SelectedMaterial":
                        if (this.SelectedMaterial == null)
                        {
                            errorMessage = String.Format(messageFormat, "Material");
                        }
                        break;
                    case "SelectedQuality":
                        if (this.SelectedMaterial == null)
                        {
                            errorMessage = String.Format(messageFormat, "Quality");
                        }
                        break;
                    case "SelectedFinishing":
                        if (this.SelectedFinishing == null)
                        {
                            errorMessage = String.Format(messageFormat, "Finishing");
                        }
                        break;
                }
                return errorMessage;
            }
        }
    }
}
