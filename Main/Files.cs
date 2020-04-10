using System;
using System.IO;
using ecv.enums;

namespace ecv.files
{
    public static class Files
    {
        public static string StoreToDisk(string filepath, AESOps op, byte[] content)
        {
            string dir = "";
            switch (op)
            {
                case AESOps.Encrypt:
                    dir = Path.Combine(new string[] {
                                    filepath.Replace(Path.GetFileName(filepath), ""),
                                    "ECV_" + Path.GetFileName(filepath)
                                });
                    break;

                case AESOps.Decrypt:
                    if (filepath.Contains("ECV_"))
                    {
                        filepath = filepath.Replace("ECV_", "");
                    }
                    dir = filepath;
                    break;
            }

            try
            {
                using (var fs = new FileStream(dir, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(content, 0, content.Length);
                }
            }
            catch (Exception ex)
            {
                throw new FileReadException("Couldn't create a new file: " + dir);
            }

            return dir;
        }
    }
}