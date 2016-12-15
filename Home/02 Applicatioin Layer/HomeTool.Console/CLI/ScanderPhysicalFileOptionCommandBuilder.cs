using Library.ComponentModel.Logic;
using System;

namespace HomeApplication.Logic.IO
{
	public class ScanderPhysicalFileOptionCommandBuilder : OptionCommandBuilder, IOptionCommandBuilder<ScanderPhysicalFileOption>
	{
		public ScanderPhysicalFileOption GetOption()
		{
			return _option;
		}
		ScanderPhysicalFileOption _option;

        protected override IOption GetOptionImpl()
        {
            return _option;
        }
        public override void RumCommandLine()
		{
			_option = new ScanderPhysicalFileOption();
		LabCmd:
            Out.Write("輸入指定掃描目標：");
			var path = In.ReadLine();
			if (string.IsNullOrEmpty(path))
			{
                Out.WriteLine("不能爲空");
				goto LabCmd;
			}
			if (!System.IO.Directory.Exists(path))
			{
                Out.WriteLine("目錄不存在");
				goto LabCmd;
			}
			_option.Path = path;
		}
	}
}