using System;
using System.IO;
using System.Web.Configuration;
using System.Configuration;

namespace Ecoporto.AgendamentoCS.Files
{
    public class Files
    {
        public string HandlerPath = "~/Images/Upload";

        public string group { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int size { get; set; }
        public string progress { get; set; }
        public string url { get; set; }
        public string thumbnail_url { get; set; }
        public string delete_url { get; set; }
        public string delete_type { get; set; }
        public string error { get; set; }

        public Files() { }

        public Files(FileInfo fileInfo) { SetValues(fileInfo.Name, (int)fileInfo.Length, fileInfo.FullName); }

        public Files(string fileName, int fileLength, string fullPath) { SetValues(fileName, fileLength, fullPath); }

        private void SetValues(string fileName, int fileLength, string fullPath)
        {
            name = fileName;
            type = "application/pdf";
            size = fileLength;
            progress = "1.0";
            url = HandlerPath + "UploadHandlerFiles.ashx?f=" + fileName;
            delete_url = HandlerPath + "UploadHandlerFiles.ashx?f=" + fileName;
            delete_type = "DELETE";

            var ext = Path.GetExtension(fullPath);
            
            var fileSize = ConvertBytesToMegabytes(new FileInfo(fullPath).Length);
            if (fileSize > 3 || !IsImage(ext))
            {
                thumbnail_url = "/images/ico-pdf.jpg";
            }
            else
            {
                thumbnail_url = @"data:image/png;base64," + EncodeFile(fullPath);
            }
        }

        private bool IsImage(string ext)
        {
            return ext == ".xml";
        }

        private string EncodeFile(string fileName)
        {
            return Convert.ToBase64String(System.IO.File.ReadAllBytes(fileName));
        }

        static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}