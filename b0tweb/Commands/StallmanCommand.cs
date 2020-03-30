using System.Windows.Forms;

namespace b0tweb.Commands
{
    class StallmanCommand : AbstractCommand
    {
        public override string Command => "stallman";

        protected override string ExecuteCommand(string[] args)
        {
            Application.EnableVisualStyles();

            Form form = new Form();
            form.TopMost = true;

            form.Width = 800;
            form.Height = 600;

            form.Text = "UFO's are just flying disks bro.";

            PictureBox box = new PictureBox();
            box.Width = 800;
            box.Height = 600;

            string basePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string imgPath = basePath + @"\Resources\stallman.png";

            box.ImageLocation = imgPath;
            form.Controls.Add(box);

            Application.Run(form);

            return string.Empty;
        }
    }
}
