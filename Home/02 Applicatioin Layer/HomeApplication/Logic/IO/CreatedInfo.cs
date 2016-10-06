using Library.ComponentModel.Model;
using System;

namespace HomeApplication.Logic.IO
{

    public class CreatedInfo : ICreatedInfo
    {
        public DateTime Created
        {
            get
            {
                return DateTime.Now;
            }


        }

        public string CreatedBy { get; protected set; }

        static CreatedInfo()
        {

            ScanderPhysical = new CreatedInfo() { CreatedBy = "ScanderPhysical" };
            BuildFingerprint = new CreatedInfo() { CreatedBy = "BuildFingerprint" };
            PhotoFileAnalysis = new CreatedInfo { CreatedBy = "PhotoFileAnalysis" };
            PhotoSimilar = new CreatedInfo { CreatedBy = "PhotoSimilar" };
        }
        public static CreatedInfo BuildFingerprint { get; private set; }
        public static CreatedInfo ScanderPhysical { get; private set; }
        public static CreatedInfo PhotoFileAnalysis { get; private set; }
        public static CreatedInfo PhotoSimilar { get; private set; }
    }
}