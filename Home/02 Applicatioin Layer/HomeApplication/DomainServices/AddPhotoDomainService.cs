using DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.DomainEvents;
using DomainModel.Aggregates.GalleryAgg;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using Library.Infrastructure.Application;
using Library.HelperUtility;
using System.Drawing.Drawing2D;
using DomainModel.Repositories;

namespace HomeApplication.DomainServices
{



	public class AddPhotoDomainService : PhotoDomainService, IAddPhotoDomainService
	{

		public void Handle(Photo photo, DomainModel.Aggregates.FileAgg.FileInfo file)
		{
			CurrnetPhoto = photo;
			CurrnetFile = file;
			DoAddAction();
			//    ModuleProvider.UnitOfWork.Commit();
		}
		IPhotoEnvironment PhotoEnvironment = new PhotoEnvironment();


		protected override void DoAddAction()
		{
			if (CurrnetFile == null) return;
			if (!PhotoEnvironment.Isloadconfig) PhotoEnvironment.LoadConfig(ModuleProvider.CreateSystemParameter());
			Logger.Trace("AddPhotoDomainService", CurrnetFile.FullPath);
			//  System.IO.FileInfo fileinfo = new System.IO.FileInfo(CurrnetFile.FullPath);
			if (!System.IO.File.Exists(CurrnetFile.FullPath))
			{
				Logger.WarnByContent(Resources.DomainServiceResource.FileNotExist, CurrnetFile.FullPath);
				throw new PhotoDomainServiceException(Resources.DomainServiceResource.FileNotExist, new FileNotFoundException(CurrnetFile.FullPath));

			}
			if (CurrnetPhoto == null)
			{
				Logger.TraceByContent("Create Photo Entity", CurrnetFile.FullPath);

				CurrnetPhoto = new Photo(CreatedInfo.PhotoFileAnalysis)
				{
					ID = CurrnetFile.ID,
					FileID = CurrnetFile.ID,
					//       File = CurrnetFile,
					PhotoType = DomainModel.PhotoType.Graphy,
				};
				PhotoRepository.Add(CurrnetPhoto);
				CurrnetFile.Photo = CurrnetPhoto;

			}
			if (CurrnetPhoto.Attributes != null && CurrnetPhoto.Attributes.Count > 0) return;
			//       this.FilesRepository.Attach(CurrnetFile);

			Logger.TraceByContent("Analysis", CurrnetFile.FullPath);
			Image image;

			Stream fs = null;
			try
			{
				fs = System.IO.File.Open(CurrnetFile.FullPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite);
				image = new Bitmap(fs);
			}
			catch (Exception ex)
			{

				Logger.ErrorByContent(ex, "File can not open！", CurrnetFile.FullPath);
				return;

			}

			var exifInfo = Library.Draw.ImageExif.GetExifInfo(image);
			if (CurrnetPhoto.Attributes == null)
			{
				ICollection<PhotoAttribute> attributes = new List<PhotoAttribute>();
				CurrnetPhoto.Attributes = attributes;
			}
			DoImageExif(exifInfo);


			Logger.TraceByContent("Create Thumbnail", CurrnetFile.FullPath);
			CreateImage(image, Path.Combine(PhotoEnvironment.ThumbnailPath, Guid.NewGuid().ToString() + ".jpg"), ThumbnailSize, true);
			Logger.TraceByContent("Create Effect Image ", CurrnetFile.FullPath);
			CreateImage(image, Path.Combine(PhotoEnvironment.EffectImagePath, Guid.NewGuid().ToString() + ".jpg"), EffectImageSize, false);

			fs.Dispose();
			image.Dispose();

		}
		Size ThumbnailSize = new Size(250, 250);
		Size EffectImageSize = new Size(1024, 780);


		protected void CreateImage(Image image, string path, Size size, bool imageType = true)
		{
			var photo = CurrnetPhoto;
			ICollection<PhotoAttribute> attributes = photo.Attributes;
			var ThumbnailWidth = 0;
			var ThumbnailHeight = 0;
			if (image.Width < size.Width && image.Height < size.Height)
			{
				ThumbnailWidth = image.Width;
				ThumbnailHeight = image.Height;
			}
			else if (image.Width > image.Height)
			{
				var per = (decimal)size.Width / image.Width;
				ThumbnailWidth = size.Width;

				ThumbnailHeight = (int)(image.Height * per);
			}
			else
			{
				var per = (decimal)size.Height / image.Height;
				ThumbnailHeight = size.Height;

				ThumbnailWidth = (int)(image.Width * per);
			}

			Bitmap thumbnailImage = new Bitmap(ThumbnailWidth, ThumbnailHeight, PixelFormat.Format64bppArgb);


			Graphics g = Graphics.FromImage(thumbnailImage);
			// 插值算法的质量
			g.InterpolationMode = InterpolationMode.HighQualityBicubic;

			g.DrawImage(image, new Rectangle(0, 0, ThumbnailWidth, ThumbnailHeight), new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
			g.Dispose();
			switch (imageOrientation)
			{
				case Library.Draw.Orientation.Rotate180:
					g.RotateTransform(-180);
					break;
				case Library.Draw.Orientation.Rotate270CW:
					g.RotateTransform(-270);
					break;
				case Library.Draw.Orientation.Rotate90CW:
					g.RotateTransform(-90);
					break;
				default:
					break;
			}
			thumbnailImage.Save(path, ImageFormat.Jpeg);
			attributes.Add(new PhotoAttribute(CreatedInfo.PhotoFileAnalysis)
			{
				PhotoID = photo.ID,
				AttKey = imageType ? "Thumbnail" : "EffectImage",
				AttValue = Path.GetFileName(path)
			});
			thumbnailImage.Dispose();
		}

		private PhotoAttribute CreateAtt(string key, string value)
		{
			var additemExif = new PhotoAttribute(CreatedInfo.PhotoFileAnalysis)
			{
				PhotoID = CurrnetPhoto.ID,
				AttKey = key,
				AttValue = value
			};
			return additemExif;
		}
		Library.Draw.Orientation imageOrientation;
		protected void DoImageExif(Library.Draw.ImageExif exif)
		{
			Logger.TraceByContent("Create exif", CurrnetFile.FullPath);

			var photo = CurrnetPhoto;
			ICollection<PhotoAttribute> attributes = photo.Attributes;
			if (exif.DateTimeDigitized != null)
				attributes.Add(CreateAtt("DateTimeDigitized", exif.DateTimeDigitized.Value.ToString("yyyy-MM-dd HH:mm:ss")));
			if (exif.DateTimeOriginal != null)
				attributes.Add(CreateAtt("DateTimeOriginal", exif.DateTimeOriginal.Value.ToString("yyyy-MM-dd HH:mm:ss")));
			if (!string.IsNullOrWhiteSpace(exif.EquipmentMake)) attributes.Add(CreateAtt("EquipmentMake", exif.EquipmentMake));
			if (!string.IsNullOrWhiteSpace(exif.EquipmentModel)) attributes.Add(CreateAtt("EquipmentModel", exif.EquipmentModel));
			if (!string.IsNullOrWhiteSpace(exif.Description)) attributes.Add(CreateAtt("Description", exif.Description.Trim()));
			if (!string.IsNullOrWhiteSpace(exif.Keyword)) attributes.Add(CreateAtt("Keyword", exif.Keyword));
			if (!string.IsNullOrWhiteSpace(exif.Title)) attributes.Add(CreateAtt("Title", exif.Title));
			if (!string.IsNullOrWhiteSpace(exif.RawFormat)) attributes.Add(CreateAtt("RawFormat", exif.RawFormat));
			if (exif.GPS != null) attributes.Add(CreateAtt("GPS", exif.GPS.ToString()));
			if (!string.IsNullOrWhiteSpace(exif.Author)) attributes.Add(CreateAtt("Author", exif.Author));
			if (!string.IsNullOrWhiteSpace(exif.Comment)) attributes.Add(CreateAtt("Comment", exif.Comment));
			attributes.Add(CreateAtt("Orientation", exif.Orientation.ToString()));
			imageOrientation = exif.Orientation;

		}
	}
}
