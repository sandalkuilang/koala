using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using System;
using System.Threading;
using System.Threading.Tasks;
using Krokot.Database;

namespace Koala.ViewModels.Master
{
    public class MasterCollaborator : BaseBindableModel, ISynchronize
    {
        private bool run;
        private MasterType masterTypeSync;

        private MaterialCollectionViewSource material;
        public MaterialCollectionViewSource Material
        {
            get
            {
                return material;
            }
            set
            {
                NotifyIfChanged(ref material, value);
            }
        }

        private FinishingCollectionViewSource finishing;
        public FinishingCollectionViewSource Finishing
        {
            get
            {
                return finishing;
            }
            set
            {
                NotifyIfChanged(ref finishing, value);
            }
        }

        private QualityCollectionViewSource quality;
        public QualityCollectionViewSource Quality
        {
            get
            {
                return quality;
            }
            set
            {
                NotifyIfChanged(ref quality, value);
            }
        }

        private SizeCollectionViewSource size;
        public SizeCollectionViewSource Size
        {
            get
            {
                return size;
            }
            set
            {
                NotifyIfChanged(ref size, value);
            }
        }

        private SupplierCollectionViewSource supplier;
        public SupplierCollectionViewSource Supplier
        {
            get
            {
                return supplier;
            }
            set
            {
                NotifyIfChanged(ref supplier, value);
            }
        }

        public MasterCollaborator()
        {
            Size = new SizeCollectionViewSource();
            Size.SourceChanged += Size_SourceChanged;
            Quality = new QualityCollectionViewSource();
            Quality.SourceChanged += Quality_SourceChanged;
            Material = new MaterialCollectionViewSource();
            material.SourceChanged += material_SourceChanged;
            Finishing = new FinishingCollectionViewSource();
            Finishing.SourceChanged += Finishing_SourceChanged;
            
            Supplier = new SupplierCollectionViewSource();
            supplier.SourceChanged += Supplier_SourceChanged;

            masterTypeSync = MasterType.All;
            Pull();
        }

        private void Supplier_SourceChanged(object sender, EventArgs e)
        {
            masterTypeSync = MasterType.Supplier;
            Pull();
        }

        void Finishing_SourceChanged(object sender, EventArgs e)
        {
            masterTypeSync = MasterType.Finishing;
            Pull();
        }

        void material_SourceChanged(object sender, EventArgs e)
        {
            masterTypeSync = MasterType.Material;
            Pull(); 
        }

        void Quality_SourceChanged(object sender, EventArgs e)
        {
            masterTypeSync = MasterType.Quality;
            Pull();
        }

        void Size_SourceChanged(object sender, EventArgs e)
        {
            masterTypeSync = MasterType.Size;
            Pull();
        }

        public void Pull()
        { 
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name);
            int mstrType = -1;
            if (masterTypeSync == MasterType.All)
                mstrType = 100;

            if (mstrType == 100 || masterTypeSync == MasterType.Finishing)
                Finishing.Source = db.Query<KeyValueOption>("GetFinishing").Convert<KeyValueOption>();

            if (mstrType == 100 || masterTypeSync == MasterType.Material)
                Material.Source = db.Query<MaterialType>("GetMaterial").Convert<MaterialType>();

            if (mstrType == 100 || masterTypeSync == MasterType.Quality)
                Quality.Source = db.Query<KeyValueOption>("GetQuality").Convert<KeyValueOption>();

            if (mstrType == 100 || masterTypeSync == MasterType.Size)
                Size.Source = db.Query<KeyValueOption>("GetSize").Convert<KeyValueOption>();

            //if (mstrType == 100 || masterTypeSync == MasterType.Supplier)
            //    Supplier.Source = db.Query<Supplier>("GetSupplier").Convert<Supplier>();

            db.Close(); 
        }

        public void Push()
        {
            // insert and update all master

        } 
        public void Start()
        {
            run = true;
            Task.Factory.StartNew(() =>
            {
                while (run)
                {
                    Pull();
                    Thread.Sleep(5000);
                }   
            });
        }

        public void Stop()
        {
            run = false;
        }
    }
}
