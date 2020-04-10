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
                    if(filepath.Contains("ECV_")){
                        filepath = filepath.Replace("ECV_", "");
                    }
                    dir = 
                    dir = "";
                    break;
            }

            File.Create(dir);
            File.WriteAllBytes(dir, content);
            return dir;
        }
    }
}