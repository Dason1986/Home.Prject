using Home.DomainModel.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Domain.DomainEvents;
using Home.DomainModel.Aggregates.GalleryAgg;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using Library.Infrastructure.Application;
using Library.HelperUtility;
using System.Drawing.Drawing2D;
using Home.DomainModel.Repositories;

namespace HomeApplication.DomainServices
{

	public interface IPhotoEnvironment
	{
		void LoadConfig(ISystemParameterRepository systemParRepository);
		string EffectImagePath { get; }
		string ThumbnailPath { get; }
			bool Isloadconfig{ get; }
	}

	public class PhotoEnvironment : IPhotoEnvironment
	{
		public string ThumbnailPath { get; private set; }

		public string EffectImagePath { get; private set; }
		public bool Isloadconfig { get; private set; }
		public void LoadConfig(ISystemParameterRepository systemParRepository)
		{
			if (Isloadconfig) return;
			Isloadconfig = true;

			var configs = systemParRepository.GetAll().Where(n => n.Group == "GallerySetting").ToList();
			ThumbnailPath = configs.FirstOrDefault(n => n.Key == "ThumbnailPath").GetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThumbnailPath"));
			EffectImagePath = configs.FirstOrDefault(n => n.Key == "EffectImagePath").GetValue(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EffectImagePath"));

			if (!Directory.Exists(ThumbnailPath)) Directory.CreateDirectory(ThumbnailPath);
			if (!Directory.Exists(EffectImagePath)) Directory.CreateDirectory(EffectImagePath);
		}
	}
}
