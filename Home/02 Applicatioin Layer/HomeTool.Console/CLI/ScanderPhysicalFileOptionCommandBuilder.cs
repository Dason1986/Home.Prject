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
    public class ScanderFTPOptionCommandBuilder : OptionCommandBuilder, IOptionCommandBuilder<ScanderFTPOption>
	{
		public ScanderFTPOption GetOption()
		{
			return _option;
		}
        ScanderFTPOption _option;

        protected override IOption GetOptionImpl()
        {
            return _option;
        }
        public override void RumCommandLine()
		{
			_option = new ScanderFTPOption();
    
            _option.Path = "";
            LabCmd:
            Out.Write("輸入FTP Service：");
            var server = In.ReadLine();
            if (string.IsNullOrEmpty(server))
            {
                Out.WriteLine("不能爲空");
                goto LabCmd;
            }
            _option.Server = server;
            LabUser:
            Out.Write("輸入FTP user：");
            var user = In.ReadLine();
            if (string.IsNullOrEmpty(user))
            {
                Out.WriteLine("不能爲空");
                goto LabUser;
            }
            _option.User = user;
            LabPassword:
            Out.Write("輸入FTP password：");
            var password = In.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                Out.WriteLine("不能爲空");
                goto LabPassword;
            }

            _option.Password =password;
        }
	}
}