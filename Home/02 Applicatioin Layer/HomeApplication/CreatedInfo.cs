using Library.ComponentModel.Model;
using System;

namespace HomeApplication
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
            ScheduleJob = new CreatedInfo { CreatedBy = "ScheduleJob" };
        }
        public static CreatedInfo BuildFingerprint { get; private set; }
        public static CreatedInfo ScanderPhysical { get; private set; }
        public static CreatedInfo ScheduleJob { get; private set; }
        public static CreatedInfo PhotoFileAnalysis { get; private set; }
        public static CreatedInfo PhotoSimilar { get; private set; }
    }
}