using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SEO.Helpers
{
    public static class FileManager
    {
        public static void SaveFile(string path, string content)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, content, Encoding.UTF8);
        }

        public static void SaveFile(string path, IEnumerable<string> content)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllLines(path, content, Encoding.UTF8);
        }

        public static IEnumerable<string> GetFilesContent(string folderPath)
        {
            string[] files = null;
            var result = new List<string>();

            if ((files = GetFilesList(folderPath)) != null)
            {
                foreach (string i in files)
                    result.Add(GetFileContent(i));
            }
            return result;
        }

        private static string GetFileContent(string path)
        {
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;
        }

        private static string[] GetFilesList(string folderPath)
        {
            return Directory.Exists(folderPath) ? Directory.GetFiles(folderPath) : null; 
        }
    }
}
