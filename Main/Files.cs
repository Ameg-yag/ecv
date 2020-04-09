using System.IO;
using ecv.enums;

namespace ecv.files
{
    public static class Files
    {
        public static string StoreToDisk(string filepath, AESOps op, string content)
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
                    dir = "";
                    break;
            }

            File.Create(dir);
            File.WriteAllText(dir, content);
            return dir;
        }
    }
}