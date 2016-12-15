using System;
using System.Drawing;

namespace Library.Draw
{
    public struct AspectRatio
    {


        public AspectRatio(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Width, Height);
        }

        public static AspectRatio FormSize(int width, int height)
        {
            var gcd = SizeUtility.GCD(width, height);
            return new AspectRatio(width / gcd, height / gcd);
        }
        public static AspectRatio FormSize(Size size)
        {
            return FormSize(size.Width, size.Height);
        }
    }

    public static class HD
    {
        public const string HD1080P = "1080P";
        public const string HD720P = "720P";
        public const string HD4K = "4K";
        public const string HD2K = "2K";
        public const string HD8K = "8K";

        //        4K UHDTV（2160p）的宽高为3840×2160。总像素数是全高清1080p的4倍。
        //8K UHDTV（4320p）的宽高为7680×4320。总像素数是全高清1080p的16倍。

        //        DCI 2K(原生分辨率)  2048 × 1080	1.90:1 (256:135, ~17:9)	2,211,840
        //DCI 2K(扁平裁切)   1998 × 1080	1.85:1	2,157,840
        //DCI 2K(宽屏幕裁切)  2048 × 858	2.39:1	1,755,136
        //PC 2K(1080p)   1920 × 1080	1.(7):1 (16:9)	2,073,600


        //分辨率有2种规格：3840×2160和4096×2160像素。
        //public static readonly Size FullAperture4K = new Size(4096 , 3112);1.32:1
        //public static readonly Size Academy4K = new Size(3656, 2664);1.37:1
        //public static readonly Size DigitalCinema4K = new Size(4096, 1714);2.39:1
        //public static readonly Size DigitalCinema4K = new Size(3996, 2160);1.85:1

        //public static readonly Size FullAperture4K = new Size(4096, 3072);4:3
        //public static readonly Size Academy4K = new Size(3656, 2664);1.37:1



    }
}

namespace Library.Draw
{

    public static class SizeUtility
    {
        public static int GetDiagonal(Size size)
        {

            var f = Math.Round(Math.Sqrt(Math.Pow(size.Height, 2) + Math.Pow(size.Width, 2)), 0);
            return (int)f;
        }
        public static int GetMillionPixels(Size size)
        {

            var f = size.Height * size.Width / 1000000;
            return f;
        }
        public static int GCD(int a, int b)
        {
            if (0 != b) while (0 != (a %= b) && 0 != (b %= a)) ;
            return a + b;
        }
        public static int LCM(int a, int b)
        {
            return a * b / GCD(a, b);
        }

        public static Size ZoomSizeByPercentage(Size size, float percentage)
        {
            return ZoomSizeFByPercentage(size, percentage).ToSize();

        }
        public static Size ZoomSizeByPixels(Size size, int pixels)
        {

            return ZoomSizeFByPixels(size, pixels).ToSize();
        }

        public static SizeF ZoomSizeFByPercentage(SizeF size, float percentage)
        {
            if (size.Width > size.Height)
            {
                return new SizeF(size.Width * percentage, size.Height * percentage);
            }
            else
            {
                return new SizeF(size.Width * percentage, size.Height * percentage);
            }

        }
        public static SizeF ZoomSizeFByPixels(SizeF size, float pixels)
        {
            float percentage = 100;
            if (size.Width > size.Height)
            {
                percentage = pixels / size.Width;
            }
            else
            {
                percentage = pixels / size.Height;

            }
            return ZoomSizeFByPercentage(size, percentage);
        }
        public static Rectangle GetSquareRectangle(Size size)
        {
            var min = Math.Min(size.Width, size.Height);
            if (min == size.Width)
            {
                var marginBottom = size.Height / 2;
                var marginTop = size.Height / 4;
                return new Rectangle(0, marginTop, size.Width, marginBottom);
            }
            else
            {
                var marginRight = size.Width / 2;
                var marginleft = size.Width / 4;
                return new Rectangle(marginleft, 0, marginRight, size.Height);
            }
        }
        public static RectangleF GetSquareRectangleF(SizeF image)
        {
            var min = Math.Min(image.Width, image.Height);
            if (min == image.Width)
            {
                var marginBottom = image.Height / 2;
                var marginTop = image.Height / 4;
                return new RectangleF(0, marginTop, image.Width, marginBottom);
            }
            else
            {
                var marginRight = image.Width / 2;
                var marginleft = image.Width / 4;
                return new RectangleF(marginleft, 0, marginRight, image.Height);
            }
        }
    }
}