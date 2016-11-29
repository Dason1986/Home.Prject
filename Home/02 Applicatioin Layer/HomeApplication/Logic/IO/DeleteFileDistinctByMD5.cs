using Home.DomainModel.ModuleProviders;
using Library;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Home.Repository;
using Home.Repository.ModuleProviders;
using System.Collections.Generic;
using System.Linq;
using HomeApplication.DomainServices;
using System.IO;

namespace HomeApplication.Logic.IO
{
	public class DeleteFileDistinctByMD5 : BaseMultiThreadingLogicService
	{

		IPhotoEnvironment PhotoEnvironment = new PhotoEnvironment();
		EmptyOption _option;
		protected override IOption ServiceOption
		{
			get
			{
				return _option;
			}

			set
			{
				_option = (EmptyOption)value;
			}
		}


		protected override int GetTotalRecord()
		{

			{
				var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
				var _photoRepository = provider.CreateFileInfo();

				md5s = _photoRepository.GetFileDistinctByMD5();
				return md5s.Count;
			}
		}
		IList<string> md5s;
		protected override void ThreadProssSize(int beginindex, int endindex)
		{

			{
				var take = endindex - beginindex;
				var provider = Bootstrap.Currnet.GetService<IGalleryModuleProvider>();
				var _fileInfoRepository = provider.CreateFileInfo();
				var _photoRepository = provider.CreatePhoto();
				PhotoEnvironment.LoadConfig(provider.CreateSystemParameter());
				// var _photoAttRepository = provider.CreatePhotoAttribute();
				var photolist = md5s.Skip(beginindex).Take(take).ToList();
				foreach (var item in photolist)
				{
					var fileDistincts = _fileInfoRepository.GetAll().Where(n => n.MD5 == item).OrderByDescending(n => n.StatusCode).ToList();
					if (fileDistincts.Count <= 1) continue;

					for (int i = 1; i < fileDistincts.Count; i++)
					{
						var fileinfo = fileDistincts[i];
						Logger.TraceByContent("Distinct", fileinfo.FileName);
						if (fileinfo.StatusCode == Library.ComponentModel.Model.StatusCode.Disabled) continue;
						var photo = fileinfo.Photo;
						if (photo == null) photo = _photoRepository.GetByFileId(fileinfo.ID);
						if (fileinfo.Photo == null) { 
							_fileInfoRepository.Remove(fileinfo);
						}

						else
						{
							Logger.Trace("删除文件！");
							DeleFile(fileinfo,"Thumbnail");
							DeleFile(fileinfo, "EffectImage");
						 
							_photoRepository.DeletePhotoAllInfoByID(fileinfo.Photo.ID); 
							_fileInfoRepository.Get(fileinfo.ID).StatusCode = Library.ComponentModel.Model.StatusCode.Disabled;

							try
							{
								System.IO.File.Delete(fileinfo.FileName);

							}
							catch (System.Exception)
							{

								Logger.WarnByContent("删除文件失败！", fileinfo.FileName);
							}
						}
						provider.UnitOfWork.Commit();
					
					}


				}
			}
		}

		void DeleFile(Home.DomainModel.Aggregates.FileAgg.FileInfo fileinfo,string key)
		{
			var Thumbnail = fileinfo.Photo.Attributes.FirstOrDefault(n => n.AttKey == "Thumbnail");
			if (Thumbnail != null)
			{
				var fileThumbnail = Path.Combine(PhotoEnvironment.ThumbnailPath, Thumbnail.AttValue);


				try
				{
					System.IO.File.Delete(fileThumbnail);

				}
				catch (System.Exception)
				{
					Logger.WarnByContent("删除文件失败！", fileThumbnail);
				}
			}
		}
}
}