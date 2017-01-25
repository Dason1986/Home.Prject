using System;
using Library.ComponentModel.Logic;

namespace HomeApplication.Logic.IO
{
    public class PhotoSimilarOptionCommandBuilder : OptionCommandBuilder, IOptionCommandBuilder<PhotoSimilarOption>
    {
        public PhotoSimilarOption GetOption()
        {
            return _option;
        }

        private PhotoSimilarOption _option;

        protected override IOption GetOptionImpl()
        {
            return _option;
        }

        public override void RumCommandLine()
        {
            _option = new PhotoSimilarOption();
            Out.Write("是否使用默认条件（Y）：");
            var key = In.ReadLine();
            if (key.ToUpper() == "Y")
            {
                Out.WriteLine();
                _option.AlgorithmType = Home.DomainModel.SimilarAlgorithm.PerceptualHash;
                _option.Similarity = 5;

                return;
            }
            Out.WriteLine();
        LabCmd:
            Out.Write("輸入圖像正確率：");
            var path = In.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Out.WriteLine("不能爲空");
                goto LabCmd;
            }
            var dimilarity = Library.HelperUtility.StringUtility.TryCast<double>(path);
            if (dimilarity.HasError)
            {
                Out.WriteLine("無效輸入");
                goto LabCmd;
            }
            if (dimilarity.Value > 100 || dimilarity.Value < 50)
            {
                Out.WriteLine("正確率不能少於50並大於100");
                goto LabCmd;
            }
            _option.Similarity = dimilarity.Value;
        }
    }
}