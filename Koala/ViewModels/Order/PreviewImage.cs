using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Koala.ViewModels.Order
{
    public class ThumbnailImage : BaseBindableModel
    {
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

        private ImageSource source;
        public ImageSource Source
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
                    if (Source == null)
                        Source = CreateThumbnail(new MemoryStream(value));
                }

            }
        }

        private BitmapImage CreateThumbnail(Stream fileStream)
        {
            fileStream.Position = 0;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = fileStream;
            bitmap.CacheOption = BitmapCacheOption.OnLoad;
            bitmap.EndInit();
            bitmap.Freeze();
            return bitmap;
        }


    }
}
