using System;
using System.IO;
using System.Text;
using System.Web;

namespace DemoDienToanDamMay.MoreOptions
{
    public class FileFolder
    {
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        public static bool CreateFolder(string email, string folderName)
        {
            try
            {
                var FolderPath = $@"{path}/{"Data"}/{email}/{folderName}/";
                Directory.CreateDirectory(FolderPath);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetPathImg(string email, string folderName, string fileName)
        {
            var FolderPath = $@"{path}\\{"Data"}\\{email}\\{folderName}\\{"img"}\\{fileName}";
            try
            {
               var x= Directory.CreateDirectory(FolderPath);
                return FolderPath;
            }
            catch
            {
                return FolderPath;
            }
        }
        public static bool WirteFile(string path, string text)
        {
            try
            {
                File.WriteAllBytes(path, Encoding.ASCII.GetBytes(text));
                //var x = File.ReadAllBytes(path);
                //text = Encoding.ASCII.GetString(x);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string ReadFile(HttpPostedFileBase file)
        {
            try
            {
                BinaryReader b = new BinaryReader(file.InputStream);
                byte[] binData = b.ReadBytes(file.ContentLength);
                return Encoding.ASCII.GetString(binData);
            }
            catch
            {
                return "";
            }
            
        }
    }
}