using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Point = System.Windows.Point;
using Size = System.Windows.Size;

namespace bytePassion.Lib.Utils
{
	public static class UIElementToImage
	{
		public static void Convert(UIElement element, string filename, 
								   double width, double height)
		{
			element.Measure(new Size(width, height));
			element.Arrange(new Rect(new Size(width, height)));

			var drawingVisual = new DrawingVisual();
			
			using (var drawingContext = drawingVisual.RenderOpen())
			{
				var visualBrush = new VisualBrush(element);
				drawingContext.DrawRectangle(visualBrush, 
											 null, 
											 new Rect(new Point(), new Size(width, height)));
			}

			var bmpCopied = new RenderTargetBitmap((int)Math.Floor(width), 
												   (int)Math.Floor(height), 
												   100, 100, 
												   PixelFormats.Default);
			bmpCopied.Render(drawingVisual);

			var stream = new MemoryStream();
			BitmapEncoder encoder = new BmpBitmapEncoder();
			encoder.Frames.Add(BitmapFrame.Create(bmpCopied));
			encoder.Save(stream);

			var bitmap = new Bitmap(stream);
			bitmap.Save(filename);
		}
	}
}
