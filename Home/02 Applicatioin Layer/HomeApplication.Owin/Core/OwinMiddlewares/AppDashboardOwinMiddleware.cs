using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Owin;
using Microsoft.Owin.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApplication
{
    public static class WebHelper
    {
        public static Stream FindAndReplace(Encoding encoding, Stream stream, IDictionary<string, string> replacements)
        {
            var tempencoding = encoding ?? Encoding.UTF8;

            var originalText = new StreamReader(stream, tempencoding).ReadToEnd();
            var outputBuilder = new StringBuilder(originalText);

            foreach (var replacement in replacements)
            {
                outputBuilder.Replace(replacement.Key, replacement.Value);
            }

            return new MemoryStream(tempencoding.GetBytes(outputBuilder.ToString()));
        }

        public static string InferMediaTypeFrom(string path)
        {
            var extension = path.Split('.').Last();


            switch (extension)
            {
                case "css":
                    return "text/css";
                case "js":
                    return "text/javascript";
                case "gif":
                    return "image/gif";
                case "png":
                    return "image/png";
                case "eot":
                    return "application/vnd.ms-fontobject";
                case "woff":
                    return "application/font-woff";
                case "woff2":
                    return "application/font-woff2";
                case "otf":
                    return "application/font-sfnt"; // formerly "font/opentype" 
                case "ttf":
                    return "application/font-sfnt"; // formerly "font/truetype" 
                case "svg":
                    return "image/svg+xml";
                default:
                    return "text/html";
            }
        }

    }
    abstract class ZipSiteOwinMiddleware : OwinMiddleware
    {
        public ZipSiteOwinMiddleware(OwinMiddleware next)
            : base(next)
        {


        }
        public abstract string ZipFile { get; }
        public abstract string VirtualPathRoot { get; }
        public bool IsAuthorization { get; set; }
        protected virtual string segment { get; private set; }
        IDictionary<string, byte[]> sitefile;
        void OpenZipFile()
        {
            var filepath = ZipFile;
            if (File.Exists(filepath))
            {
                sitefile = new Dictionary<string, byte[]>(StringComparer.OrdinalIgnoreCase);
                var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                var zf = new ZipFile(fs);
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;
                    }
                    String entryFileName = string.Format("/{0}/{1}", VirtualPathRoot, zipEntry.Name);


                    Stream zipStream = zf.GetInputStream(zipEntry);
                    var buffer = new byte[zipEntry.Size];
                    zipStream.Read(buffer, 0, (int)zipEntry.Size);
                    sitefile.Add(entryFileName, buffer);
                    if ("Index.html".Equals(zipEntry.Name, StringComparison.OrdinalIgnoreCase))
                    {

                        sitefile.Add(string.Format("/{0}/", VirtualPathRoot), buffer);
                        sitefile.Add(string.Format("/{0}", VirtualPathRoot), buffer);
                        sitefile.Add("", buffer);
                    }
                    zipStream.Dispose();
                }
                fs.Dispose();
            }
            else
            {
                throw new Exception("");
            }

        }
        // DateTime? lasttime = null;
        public async override Task Invoke(IOwinContext context)
        {
            var url = context.Request.Uri;
            if (
                url.Segments.Length > 1 && url.Segments[1].StartsWith(segment, StringComparison.OrdinalIgnoreCase))
            {

                if (sitefile == null) { OpenZipFile(); }

                if (sitefile.ContainsKey(context.Request.Uri.LocalPath))
                {
                    var req = context.Request;
                    if (IsAuthorization)
                    {
                        var auth = req.Headers["Authorization"];
                        if (String.IsNullOrEmpty(auth))
                        {
                            SetAuthorization(context);
                            return;


                        }
                        else
                        {
                            try
                            {
                                var cred = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                                if (cred[0] != "admin" && cred[1] != "admin")
                                {
                                    SetAuthorization(context);
                                    return;
                                }
                            }
                            catch (Exception)
                            {

                                SetAuthorization(context);
                                return;
                            }

                            //  lasttime = DateTime.Now;
                        }
                    }

                    var extension = Path.GetExtension(context.Request.Uri.LocalPath);
                    if (extension != null)
                    {
                        var buffer = sitefile[context.Request.Uri.LocalPath];
                        context.Response.ContentType = WebHelper.InferMediaTypeFrom(context.Request.Uri.LocalPath);

                        await context.Response.WriteAsync(buffer);

                        return;
                    }

                }
                else if (!context.Request.Uri.IsFile)
                {

                }

            }
            await Next.Invoke(context);

        }

        private static void SetAuthorization(IOwinContext context)
        {
            var response = context.Response;
            response.StatusCode = 401;

            response.Headers.Add("WWW-Authenticate", new[] { String.Format("Basic realm=\"IAS \"") });
        }
    }

    sealed class AppDashboardOwinMiddleware : ZipSiteOwinMiddleware
    {

        public AppDashboardOwinMiddleware(OwinMiddleware next)
            : base(next)
        {
            virtualPathRoot = "sites";
            _segment = "sites";
            zipfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sites.zip");
            IsAuthorization = false;
        }
        string _segment;
        protected override string segment { get { return _segment; } }
        private string virtualPathRoot;
        private string zipfile;
        public override string VirtualPathRoot
        {
            get { return virtualPathRoot; }

        }

        public override string ZipFile
        {
            get { return zipfile; }

        }
    }
}