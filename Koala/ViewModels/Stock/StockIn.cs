using Koala.Core;
using Koala.ViewModels.Configuration.Client;
using Koala.ViewModels.Master;
using Krokot.Database;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Koala.ViewModels.Stock
{
    public class StockIn : TransactionStock, ICloneable, IDataErrorInfo
    { 

        public ReactiveCommand<Unit> SaveCommand { get; set; }

        public object Clone()
        {
            return null;
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
                    foreach (MaterialType material in filteringMaterial)
                    {
                        filterQuality.Add(new QualityType()
                        {
                            Id = material.QualityId,
                            Description = material.QualityName
                        });
                    }

                    Quality = filterQuality;
                    if (QualityId != null)
                    {
                        SelectedQuality = Quality.Where(x => x.Id == QualityId).SingleOrDefault();
                    }
                    else
                    {
                        SelectedQuality = Quality.FirstOrDefault();
                    } 
                }
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

                if (!string.IsNullOrEmpty(QualityId))
                    SelectedQuality = Quality.Where(x => x.Id == QualityId).SingleOrDefault();
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

                if (!string.IsNullOrEmpty(MaterialId))
                    SelectedMaterial = Material.Where(x => x.Id == MaterialId).SingleOrDefault();
            }
        }

        public StockIn()
        {
            IDbManager dbManager = ObjectPool.Instance.Resolve<IDbManager>();
            IDataCommand db = dbManager.GetDatabase(ApplicationSettings.Instance.Database.DefaultConnection.Name); 
            MaterialMaster = db.Query<MaterialType>("GetMaterial");
            Material = MaterialMaster.GroupBy(x => x.Id).Select(d => d.First()).ToList();
            db.Close();
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
                        if (this.SelectedQuality == null)
                        {
                            errorMessage = String.Format(messageFormat, "Quality");
                        }
                        break;
                    case "Qty":
                        if (this.Qty == 0)
                        {
                            errorMessage = String.Format(messageFormat, "Qty");
                        }
                        break;
                }
                return errorMessage;
            }
        }
    }
}
