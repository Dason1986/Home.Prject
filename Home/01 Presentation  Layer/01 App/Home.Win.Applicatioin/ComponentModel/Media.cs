using MediaDevices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home.Win.Applicatioin.ComponentModel
{
    public class Media
    {
        public Media()
        {

        }

        public string[] GetMediaFiles()
        {
            List<string> _files = new List<string>();
            var devices = MediaDevice.GetDevices();
            foreach (var device in devices)
            {
                device.Connect();
                var canFindInfo = false;
                try
                {
                    device.GetDirectoryInfo(@"\");
                    canFindInfo = true;
                }
                catch (Exception)
                {

                }
                if (canFindInfo)
                    _files.AddRange(FindByInfo(device));
                else
                    _files.AddRange(FindByForech(device));


                device.Disconnect();
                device.Dispose();

            }
            return _files.ToArray();
        }

        private string[] FindByInfo(MediaDevice device)
        {
            List<string> _files = new List<string>();

            var dirs = device.EnumerateDirectories(@"\").
            SelectMany(n => device.EnumerateDirectories(n).Select(ff => Path.Combine("\\", n, ff)))
            .Where(n => n.EndsWith("DCIM", StringComparison.OrdinalIgnoreCase)).ToArray();

            foreach (var item in dirs)
            {
                var dirinfo = device.GetDirectoryInfo(item);
                var files = dirinfo.EnumerateFiles("*.*", SearchOption.AllDirectories).Where(n => !n.FullName.Contains(".thumbnails")).ToArray();
                _files.AddRange(files.Select(n => n.FullName));
                //Find(device, item);
            }
            return _files.ToArray();
        }

        private string[] FindByForech(MediaDevice device)
        {
            List<string> _files = new List<string>();
            var dirs = device.EnumerateDirectories(@"\").
            SelectMany(n => device.EnumerateDirectories(n).Select(ff => Path.Combine("\\", n, ff)))
            .Where(n => n.EndsWith("DCIM", StringComparison.OrdinalIgnoreCase)).ToArray();

            foreach (var item in dirs)
            {
                Find(device, item, _files);
            }
            return _files.ToArray();
        }

        private void Find(MediaDevice device, string path, IList<string> files)
        {

            foreach (var item in device.EnumerateFiles(path))
            {
                var filepath = Path.Combine(path, item);
                files.Add(filepath);
            }

            foreach (var item in device.EnumerateDirectories(path))
            {
                if (item.Contains(".thumbnails")) continue;
                Find(device, Path.Combine(path, item), files);

            }
        }
    }
}
