using HomeApplication.ComponentModel.IO;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Library.Storage;
using Library.Storage.Image;

namespace HomeApplication.Test.ComponentModelTest
{
    [TestFixture(Category = "圖像")]
    public class PhotoStorageBuilderTest
    {
        private string GalleryPath = AppDomain.CurrentDomain.BaseDirectory + "GalleryPath";

        [Test, TestCaseSource(typeof(PhotoStorageBuilderTest), "TestCases3", Category = "圖片生成")]
        public void BuildTest(TestPhotoObj obj)
        {
            FileStoryHelper.InitPath(GalleryPath);
            PhotoStorageBuilder builder = new PhotoStorageBuilder()
            {
                Storage = new ImageStorage(new PhysicalImageStorageProvider(GalleryPath), obj.ID),
                SourceImage = obj.Image,
                IsPanoramic = obj.IsPanoramic,
            };
            builder.Build();
        }

        public static IEnumerable TestCases3
        {
            get
            {
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t1, ID = Guid.Parse("b0e7943a-b683-42e6-9b3c-0e2ed2078f8e") }).SetName("T1");
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t2, ID = Guid.Parse("b1e7943a-b683-42e6-9b3c-0e2ed2078f8e"), IsPanoramic = true }).SetName("T2");
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t3, ID = Guid.Parse("b2e7943a-b683-42e6-9b3c-0e2ed2078f8e") }).SetName("T3");
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t4, ID = Guid.Parse("b3e7943a-b683-42e6-9b3c-0e2ed2078f8e") }).SetName("T4");
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t5, ID = Guid.Parse("b4e7943a-b683-42e6-9b3c-0e2ed2078f8e") }).SetName("T5");
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t6, ID = Guid.Parse("b5e7943a-b683-42e6-9b3c-0e2ed2078f8e") }).SetName("T6");
                yield return new TestCaseData(new TestPhotoObj { Image = HomeApplication.Test.Properties.Resources.t7, ID = Guid.Parse("b6e7943a-b683-42e6-9b3c-0e2ed2078f8e") }).SetName("T7");
            }
        }
    }

    public class TestPhotoObj
    {
        public Guid ID { get; internal set; }
        public Image Image { get; internal set; }
        public bool IsPanoramic { get; internal set; }
    }
}