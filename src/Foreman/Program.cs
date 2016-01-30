using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace OdrStudio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            string procfile = File.ReadAllText("Procfile");
            
            string[] commands = procfile.Split('\n');
            
            foreach (var command in commands)
            {
                string[] commandParts = command.Split(' ');
                string exec = commandParts.First();
                string arguments = string.Join(" ", commandParts.Skip(1));
                
                
                System.Console.WriteLine($"Starting exec {exec} with arguments {arguments}...");
                Process.Start(new ProcessStartInfo (exec, arguments) { UseShellExecute = false });
            }

            Console.Read();
        }
    }
}
