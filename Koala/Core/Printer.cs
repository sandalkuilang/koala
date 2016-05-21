using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Koala.Core
{
    /// <summary>Provides printing capabilities.</summary>
    public static class Printer
    {
        /// <summary>Prints the FrameworkElement.</summary>
        /// <param name="fe">The FrameworkElement.</param>
        public static void Print(this FrameworkElement fe)
        {
            PrintDialog pd = new PrintDialog();

            bool? result = pd.ShowDialog();

            pd.PrintTicket.PageOrientation = PageOrientation.Portrait;

            if (!result.HasValue || !result.Value) return;

            System.Printing.PrintCapabilities capabilities = pd.PrintQueue.GetPrintCapabilities(pd.PrintTicket);
            try
            {
                fe.Dispatcher.Invoke(new Action(() =>
                {
                    //fe.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                    //fe.Arrange(new Rect(fe.DesiredSize));
                    //fe.UpdateLayout();

                    // Change the layout of the UI Control to match the width of the printer page
                    fe.Width = capabilities.PageImageableArea.ExtentWidth;
                    fe.UpdateLayout();
                    fe.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    Size size = new Size(capabilities.PageImageableArea.ExtentWidth,
                                         fe.DesiredSize.Height);
                    fe.Measure(size);
                    size = new Size(capabilities.PageImageableArea.ExtentWidth,
                                    fe.DesiredSize.Height);
                    fe.Measure(size);
                    fe.Arrange(new Rect(size));

                }), System.Windows.Threading.DispatcherPriority.Render);

                int height = (int)pd.PrintTicket.PageMediaSize.Height;
                int width = (int)pd.PrintTicket.PageMediaSize.Width;
                int pages = (int)Math.Ceiling((fe.ActualHeight / height));

                FixedDocument document = new FixedDocument();

                for (int i = 0; i < pages; i++)
                {
                    FixedPage page = new FixedPage();
                    page.Height = height;
                    page.Width = width;
                    page.PrintTicket = pd.PrintTicket;

                    PageContent content = new PageContent();
                    content.Child = page;

                    document.DocumentPaginator.PageSize =
                          new Size(pd.PrintableAreaWidth + 50, pd.PrintableAreaHeight);

                    document.Pages.Add(content);

                    VisualBrush vb = new VisualBrush(fe);
                    vb.AlignmentX = AlignmentX.Left;
                    vb.AlignmentY = AlignmentY.Top;
                    vb.Stretch = Stretch.None;
                    vb.TileMode = TileMode.None;
                    vb.Viewbox = new Rect(-11, i * height, width + 10, (i + 1) * height);
                    vb.ViewboxUnits = BrushMappingMode.Absolute;

                    RenderOptions.SetBitmapScalingMode(vb, BitmapScalingMode.Fant);

                    Canvas canvas = new Canvas();
                    canvas.Background = vb;
                    canvas.Height = height;
                    canvas.Width = width;

                    //FixedPage.SetLeft(canvas, 0);
                    FixedPage.SetTop(canvas, 25);

                    page.Children.Add(canvas);
                }

                pd.PrintDocument(document.DocumentPaginator,
                ((String.IsNullOrWhiteSpace(fe.Name) ? "Temp" : fe.Name) + " PRINT"));

            } 
            finally
            { 
                //// Scale UI control back to the original so we don't effect what is on the screen 
                //fe.Width = double.NaN;
                //fe.UpdateLayout();
                //fe.LayoutTransform = new ScaleTransform(1, 1);
                //Size size = new Size(capabilities.PageImageableArea.ExtentWidth,
                //                     capabilities.PageImageableArea.ExtentHeight);
                //fe.Measure(size);
                //fe.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth,
                //                      capabilities.PageImageableArea.OriginHeight), size));
                Mouse.OverrideCursor = null;
            }
            
        }

        //public static void Print(this FrameworkElement fe)
        //{
        //    PrintDialog printDialog = new PrintDialog();
        //    if ((bool)printDialog.ShowDialog().GetValueOrDefault())
        //    {
        //        Mouse.OverrideCursor = Cursors.Wait;
        //        System.Printing.PrintCapabilities capabilities = printDialog.PrintQueue.GetPrintCapabilities(printDialog.PrintTicket);
        //        double dpiScale = 300.0 / 96.0;

        //        printDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;

        //        FixedDocument document = new FixedDocument();
        //        try
        //        {
        //            // Change the layout of the UI Control to match the width of the printer page
        //            fe.Width = capabilities.PageImageableArea.ExtentWidth;
        //            fe.UpdateLayout();
        //            fe.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
        //            Size size = new Size(capabilities.PageImageableArea.ExtentWidth,
        //                                 fe.DesiredSize.Height);
        //            fe.Measure(size);
        //            size = new Size(capabilities.PageImageableArea.ExtentWidth,
        //                            fe.DesiredSize.Height);
        //            fe.Measure(size);
        //            fe.Arrange(new Rect(size));

        //            // Convert the UI control into a bitmap at 300 dpi
        //            double dpiX = 300;
        //            double dpiY = 300;

        //            RenderTargetBitmap bmp = new RenderTargetBitmap(Convert.ToInt32(
        //              capabilities.PageImageableArea.ExtentWidth * dpiScale),
        //              Convert.ToInt32(fe.ActualHeight * dpiScale),
        //              dpiX, dpiY, PixelFormats.Rgb48);
        //            bmp.Render(fe);

        //            // Convert the RenderTargetBitmap into a bitmap we can more readily use
        //            PngBitmapEncoder png = new PngBitmapEncoder();
        //            png.Frames.Add(BitmapFrame.Create(bmp));
        //            System.Drawing.Bitmap bmp2;
        //            using (MemoryStream memoryStream = new MemoryStream())
        //            {
        //                png.Save(memoryStream);
        //                bmp2 = new System.Drawing.Bitmap(memoryStream);
        //            }
        //            document.DocumentPaginator.PageSize =
        //              new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

        //            // break the bitmap down into pages
        //            int pageBreak = 0;
        //            int previousPageBreak = 0;
        //            int pageHeight =
        //                Convert.ToInt32(capabilities.PageImageableArea.ExtentHeight * dpiScale);
        //            while (pageBreak < bmp2.Height - pageHeight)
        //            {
        //                pageBreak += pageHeight;  // Where we thing the end of the page should be

        //                // Keep moving up a row until we find a good place to break the page
        //                while (!IsRowGoodBreakingPoint(bmp2, pageBreak))
        //                    pageBreak--;

        //                PageContent pageContent = generatePageContent(bmp2, previousPageBreak,
        //                  pageBreak, document.DocumentPaginator.PageSize.Width,
        //                  document.DocumentPaginator.PageSize.Height, capabilities);
        //                document.Pages.Add(pageContent);
        //                previousPageBreak = pageBreak;
        //            }

        //            // Last Page
        //            PageContent lastPageContent = generatePageContent(bmp2, previousPageBreak,
        //              bmp2.Height, document.DocumentPaginator.PageSize.Width,
        //              document.DocumentPaginator.PageSize.Height, capabilities);
        //            document.Pages.Add(lastPageContent);
        //        }
        //        finally
        //        {
        //            // Scale UI control back to the original so we don't effect what is on the screen 
        //            fe.Width = double.NaN;
        //            fe.UpdateLayout();
        //            fe.LayoutTransform = new ScaleTransform(1, 1);
        //            Size size = new Size(capabilities.PageImageableArea.ExtentWidth,
        //                                 capabilities.PageImageableArea.ExtentHeight);
        //            fe.Measure(size);
        //            fe.Arrange(new Rect(new Point(capabilities.PageImageableArea.OriginWidth,
        //                                  capabilities.PageImageableArea.OriginHeight), size));
        //            Mouse.OverrideCursor = null;
        //        }
        //        printDialog.PrintDocument(document.DocumentPaginator, "Print Document Name");
        //    }
        //}

        private static PageContent generatePageContent(System.Drawing.Bitmap bmp, int top,
         int bottom, double pageWidth, double PageHeight,
         System.Printing.PrintCapabilities capabilities)
        {
            FixedPage printDocumentPage = new FixedPage();
            printDocumentPage.Width = pageWidth;
            printDocumentPage.Height = PageHeight;

            int newImageHeight = bottom - top;
            System.Drawing.Bitmap bmpPage = bmp.Clone(new System.Drawing.Rectangle(0, top,
                   bmp.Width, newImageHeight), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            // Create a new bitmap for the contents of this page
            Image pageImage = new Image();
            BitmapSource bmpSource =
                System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmpPage.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromWidthAndHeight(bmp.Width, newImageHeight));

            pageImage.Source = bmpSource;
            pageImage.VerticalAlignment = VerticalAlignment.Top;

            // Place the bitmap on the page
            printDocumentPage.Children.Add(pageImage);

            PageContent pageContent = new PageContent();
            ((System.Windows.Markup.IAddChild)pageContent).AddChild(printDocumentPage);

            FixedPage.SetLeft(pageImage, capabilities.PageImageableArea.OriginWidth);
            FixedPage.SetTop(pageImage, capabilities.PageImageableArea.OriginHeight);

            pageImage.Width = capabilities.PageImageableArea.ExtentWidth;
            pageImage.Height = capabilities.PageImageableArea.ExtentHeight;
            return pageContent;

        }
        private static bool IsRowGoodBreakingPoint(System.Drawing.Bitmap bmp, int row)
        {
            double maxDeviationForEmptyLine = 1627500;
            bool goodBreakingPoint = false;

            if (rowPixelDeviation(bmp, row) < maxDeviationForEmptyLine)
                goodBreakingPoint = true;

            return goodBreakingPoint;
        }

        private static double rowPixelDeviation(System.Drawing.Bitmap bmp, int row)
        {
            int count = 0;
            double total = 0;
            double totalVariance = 0;
            double standardDeviation = 0;
            System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0,
                   bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bmpData.Stride;
            IntPtr firstPixelInImage = bmpData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)firstPixelInImage;
                p += stride * row;  // find starting pixel of the specified row
                for (int column = 0; column < bmp.Width; column++)
                {
                    count++; //count the pixels
        
                    byte blue = p[0];
                    byte green = p[1];
                    byte red = p[3];

                    int pixelValue = System.Drawing.Color.FromArgb(0, red, green, blue).ToArgb();
                    total += pixelValue;
                    double average = total / count;
                    totalVariance += Math.Pow(pixelValue - average, 2);
                    standardDeviation = Math.Sqrt(totalVariance / count);

                    // go to next pixel
                    p += 3;
                }
            }
            bmp.UnlockBits(bmpData);

            return standardDeviation;
        }
        /// <summary>Prints the FrameworkElement as a 
        /// continuous (no page breaks) print.</summary>
        /// <param name="fe">The FrameworkElement.</param>
        public static void PrintContinuous(this FrameworkElement fe)
        {
            PrintDialog pd = new PrintDialog();

            bool? result = pd.ShowDialog();

            if (!result.HasValue || !result.Value) return;

            fe.Dispatcher.Invoke(new Action(() =>
            {
                fe.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                fe.Arrange(new Rect(fe.DesiredSize));
                fe.UpdateLayout();
            }), System.Windows.Threading.DispatcherPriority.Render);

            pd.PrintVisual(fe, ((String.IsNullOrWhiteSpace
            (fe.Name) ? "Temp" : fe.Name) + " PRINT"));
        }

        /// <summary>Takes a one page snapshot 
        /// the FrameworkElement.</summary>
        /// <param name="fe">The FrameworkElement.</param>
        public static void Snapshot(this FrameworkElement fe)
        {
            PrintDialog pd = new PrintDialog();

            bool? result = pd.ShowDialog();

            if (!result.HasValue || !result.Value) return;

            pd.PrintVisual(fe, ((String.IsNullOrWhiteSpace(fe.Name) ?
                "Temp" : fe.Name) + " PRINT"));
        }
    }


}
