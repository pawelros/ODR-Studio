using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;

namespace OdrStudio.WebApi.Models.Player
{
    public class MotEncoder : IMotEncoder
    {
        private readonly string motFifo;
        private readonly ILogger logger;

        public MotEncoder(IOptions<PlayerConfiguration> configuration, ILoggerFactory loggerFactory)
        {
            this.motFifo = configuration.Value.MotFifo;
            this.logger = loggerFactory.CreateLogger("MotEncoder");
        }

        public void Invoke(string slideshowDirPath, string dlsFilePath)
        {
            this.logger.LogVerbose($"Invoked with slideshowDirPath: {slideshowDirPath} dlsFilePath: {dlsFilePath}");
            string arguments = $"-d \"{slideshowDirPath}\" -o {motFifo} -t \"{dlsFilePath}\" -v";
            this.logger.LogInformation($"Starting mot-encoder process with arguments: {arguments}");

            new TaskFactory().StartNew(() => { StartProcess(arguments); });
        }

        private void StartProcess(string arguments)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "mot-encoder",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    Arguments = arguments
                }
            };

            process.OutputDataReceived += (sender, args) => this.logger.LogInformation(args.Data);
            //It seems that mot-encoder returns non 0 exit code if everything went well.
            //process.ErrorDataReceived += (sender, args) => this.logger.LogError(args.Data);
            process.ErrorDataReceived += (sender, args) => this.logger.LogInformation(args.Data);

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
        }
    }
}
