using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace bytePassion.Lib.Utils
{
	public static class ImageLoader
	{
		public static ImageSource LoadImage (Uri imagePath)
		{
			switch (Path.GetExtension(imagePath.LocalPath).ToLower())
			{			
				case ".png":
				case ".jpg":
				case ".bmp": { return LoadBitMap(imagePath); }
			}

			throw new FileFormatException("file-extension is not supportet");
		}
		
		private static ImageSource LoadBitMap (Uri imagePath)
		{
			var imageExample = new BitmapImage(imagePath);

			var rect = new Rect()
			{
				Height = imageExample.PixelHeight,
				Width = imageExample.PixelWidth
			};

			var rectGeometry = new RectangleGeometry(rect);

			var geometryDrawing = new GeometryDrawing(new ImageBrush(imageExample),
													  new Pen() { Brush = Brushes.White }, rectGeometry);

			var drawingGroup = new DrawingGroup();
			drawingGroup.Children.Add(geometryDrawing);

			return new DrawingImage()
			{
				Drawing = drawingGroup
			};			
		}
	}
}
