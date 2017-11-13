
using Home.DomainModel.ModuleProviders;
using Library.ComponentModel.Logic;
using Library.Infrastructure.Application;
using Library;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using Home.DomainModel.Aggregates.FileAgg;
using Home.DomainModel.DomainServices;
using Home.DomainModel.Repositories;
using HomeApplication.DomainServices;

namespace HomeApplication.Logic.IO
{
    public struct ScanderPhysicalFileOption : IOption
    {
        public string Path { get; set; }
    }
   
    public class ScanderPhysicalFile : BaseLogicService
    {
        public ScanderPhysicalFileOption Option { get; set; }

        protected override IOption ServiceOption
        {
            get
            {
                return Option;
            }

            set
            {
                Option = (ScanderPhysicalFileOption)value;
            }
        }



        protected override bool OnVerification()
        {

            return base.OnVerification();
        }







        protected override void OnDowrok()
        {


        }
    }
}