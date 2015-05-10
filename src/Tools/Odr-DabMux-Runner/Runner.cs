using System.Diagnostics;
using System.IO;
using System;

namespace Odr_DabMux_Runner
{
    public class Runner
    {
        public string[] RunDefaultConfigurationFile()
        {
            // Start the child process.
            Process p = new Process();
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.FileName = "odr-dabmux";
            p.StartInfo.Arguments = "/home/pr/open_digital_radio/ODR-Studio/src/Tools/Odr-DabMux-Runner/Configs/OutputToFile.mux";
            p.Start();
            // Do not wait for the child process to exit before
            // reading to the end of its redirected stream.
            // p.WaitForExit();
            // Read the output stream first and then wait.
            string output = p.StandardOutput.ReadToEnd();
            string errOutput = p.StandardError.ReadToEnd();
            p.WaitForExit();
            return new[]{ output, errOutput };
        }
    }
}