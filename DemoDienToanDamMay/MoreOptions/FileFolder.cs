using System;
using System.IO;

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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}