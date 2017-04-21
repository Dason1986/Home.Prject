using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Library.Draw;
using Library.HelperUtility;

namespace HomeApplication.ComponentModel.IO
{
    public abstract class PhotoBuilder
    {
        private Image _sourceImage;
        public Orientation Orientation { get; set; }
        private Rectangle _sourceRectangle;

        public Image SourceImage
        {
            get { return _sourceImage; }
            set
            {
                _sourceImage = value;
                _sourceRectangle = new Rectangle(0, 0, SourceImage.Width, SourceImage.Height);
            }
        }

        protected Rectangle SourceRectangle
        {
            get { return _sourceRectangle; }
        }

        protected void Rotate(Bitmap tempImage)
        {
            switch (Orientation)
            {
                case Library.Draw.Orientation.Rotate180:
                    tempImage.RotateFlip(RotateFlipType.Rotate180FlipX);
                    break;

                case Library.Draw.Orientation.Rotate270CW:
                    tempImage.RotateFlip(RotateFlipType.Rotate270FlipX);
                    break;

                case Library.Draw.Orientation.Rotate90CW:
                    tempImage.RotateFlip(RotateFlipType.Rotate90FlipX);
                    break;

                default:
                    break;
            }
        }

        public abstract Stream Create(Image image);
    }
    public class PhotoPanoramicBuilder : PhotoBuilder
    {


         

        public override Stream Create(Image image)
        {
            const int fixHeight = 1080;
            var tempheight = image.Size.Height > fixHeight ? fixHeight : image.Size.Height;
            var per = (decimal)tempheight / image.Height;
            var tempWidth = (int)(image.Width * per);
            Bitmap tempImage = new Bitmap(tempWidth, tempheight, PixelFormat.Format64bppArgb);

            Graphics g = Graphics.FromImage(tempImage);
            g.DrawImage(image, new RectangleF(0, 0, tempWidth, tempheight), SourceRectangle, GraphicsUnit.Pixel);
            g.Dispose();
            MemoryStream ms = new MemoryStream();
            tempImage.Save(ms, ImageFormat.Jpeg);

            tempImage.Dispose();
            return ms;
        }






    }
    public class PhotoThumbnailBuilder : PhotoBuilder
    {
        public PhotoThumbnailBuilder()
        {
            ThumbnailHasBorder = true;
        }


        public bool ThumbnailHasBorder { get; set; }


 


        private const int MamarginLr = 10;
        private const int MamarginTb = 230;
        private static readonly RectangleF ThumbnailFillRectangle = new RectangleF(MamarginLr, MamarginLr, MamarginTb, MamarginTb);
        private const int ThumbnailPixels = 234;



        public override Stream Create(Image image)
        {
            var min = Math.Min(image.Width, image.Height);
            var max = Math.Max(image.Width, image.Height);
            Bitmap tempImage = new Bitmap(250, 250, PixelFormat.Format64bppArgb);

            Graphics g = Graphics.FromImage(tempImage);
            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            if (ThumbnailHasBorder)
            {
                g.DrawRectangle(new Pen(Color.Gray, 4), 8, 8, 234, 234);
                g.DrawRectangle(new Pen(Color.LightGray, 2), 2, 2, 246, 246);
            }
            if (min < 250)
            {
                if (min == image.Width)
                {
                    if (max > ThumbnailPixels)
                    {
                        g.DrawImage(image, ThumbnailFillRectangle, SourceRectangle, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var per = (decimal)ThumbnailPixels / image.Height;
                        var tempWidth = (int)(image.Width * per);
                        var paddingleft = (ThumbnailPixels - tempWidth) / 2;
                        g.DrawImage(image, new Rectangle(paddingleft, 12, tempWidth, 226), SourceRectangle, GraphicsUnit.Pixel);
                    }
                }
                else
                {
                    if (min > ThumbnailPixels)
                    {
                        g.DrawImage(image, ThumbnailFillRectangle, SourceRectangle, GraphicsUnit.Pixel);
                    }
                    else
                    {
                        var per = (decimal)ThumbnailPixels / image.Width;

                        var tempHeight = (int)(image.Height * per);
                        var paddingTop = (ThumbnailPixels - tempHeight) / 2;
                        g.DrawImage(image, new Rectangle(12, paddingTop, 226, tempHeight), SourceRectangle, GraphicsUnit.Pixel);
                    }
                }
            }
            else
            {
                g.DrawImage(image, ThumbnailFillRectangle, SizeUtility.GetSquareRectangle(image.Size), GraphicsUnit.Pixel);
            }

            g.Dispose();

            Rotate(tempImage);
            MemoryStream ms = new MemoryStream();
            tempImage.Save(ms, ImageFormat.Jpeg);

            tempImage.Dispose();
            return ms;
        }


    }
    public class PhotoZoomBuilder : PhotoBuilder
    {




        public Size ZoomSize { get; set; }

 



        public override Stream Create(Image image)
        {
            Size endPoint = image.Size;
            if ((image.Width < ZoomSize.Width && image.Height < ZoomSize.Height) == false)
            {
                endPoint = SizeUtility.ZoomSizeByPixels(endPoint, image.Width > image.Height ? ZoomSize.Height : ZoomSize.Width);
            }

            Bitmap tempImage = new Bitmap(endPoint.Width, endPoint.Height, PixelFormat.Format64bppArgb);

            Graphics g = Graphics.FromImage(tempImage);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(image, new Rectangle(Point.Empty, endPoint), SourceRectangle, GraphicsUnit.Pixel);

            g.Dispose();
            Rotate(tempImage);
            MemoryStream ms = new MemoryStream();
            tempImage.Save(ms, ImageFormat.Jpeg);

            tempImage.Dispose();
            return ms;
        }




    }
}