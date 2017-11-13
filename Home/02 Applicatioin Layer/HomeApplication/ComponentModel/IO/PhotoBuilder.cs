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
        internal void SetInfo(PhotoStorageBuilder photoStorageBuilder)
        {
            SourceImage = photoStorageBuilder.SourceImage;
            Orientation = photoStorageBuilder.Orientation;
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

        public abstract Stream Create();
    }
    public class PhotoPanoramicBuilder : PhotoBuilder
    {




        public override Stream Create()
        {
            Image image = SourceImage;
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
            Size = new Size(250, 250);
        }


        public bool ThumbnailHasBorder { get; set; }





        public Size Size { get; set; }

        public override Stream Create()
        {
            Image image = SourceImage;
            int min;
            int max;
            bool minIsWidth = false;
            if (image.Width > image.Height)
            {
                min = image.Height;
                max = image.Width;
            }
            else
            {
                max = image.Height;
                min = image.Width;
                minIsWidth = true;
            }
            var thumbnailPixels = Size.Width - 16;
            Bitmap tempImage = new Bitmap(Size.Width, Size.Height, PixelFormat.Format64bppArgb);
            var thumbnailFillRectangle = new RectangleF(0, 0, Size.Width, Size.Height);
            thumbnailFillRectangle.Inflate(-10, -10);
            Graphics g = Graphics.FromImage(tempImage);
            g.Clear(Color.White);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;


            if (ThumbnailHasBorder)
            {
                g.DrawRectangle(new Pen(Color.Gray, 4), 8, 8, 234, 234);
                g.DrawRectangle(new Pen(Color.LightGray, 2), 2, 2, 246, 246);
            }
            if (min >= Size.Width)
            {
                g.DrawImage(image, thumbnailFillRectangle, SizeUtility.GetSquareRectangle(image.Size),
                    GraphicsUnit.Pixel);
            }
            else
            {
                int comparevalue;
                int tempWidth = 226, tempHeight = 226, paddingleft = 12, paddingTop = 12;

                if (minIsWidth)
                {

                    comparevalue = max;
                    var per = (decimal)thumbnailPixels / image.Height;
                    tempWidth = (int)(image.Width * per);
                    paddingleft = (thumbnailPixels - tempWidth) / 2;

                }
                else
                {

                    comparevalue = min;
                    var per = (decimal)thumbnailPixels / image.Width;

                    tempHeight = (int)(image.Height * per);
                    paddingTop = (thumbnailPixels - tempHeight) / 2;


                }
                if (comparevalue > thumbnailPixels)
                    g.DrawImage(image, thumbnailFillRectangle, SourceRectangle, GraphicsUnit.Pixel);
                else
                    g.DrawImage(image, new Rectangle(paddingleft, paddingTop, tempWidth, tempHeight), SourceRectangle, GraphicsUnit.Pixel);
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





        public override Stream Create()
        {
            Image image = SourceImage;
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