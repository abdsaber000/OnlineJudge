using Microsoft.EntityFrameworkCore.Design.Internal;
using Newtonsoft.Json.Bson;
using System.Diagnostics;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace OnlineJudge.Services
{
    public class CodeRunner
    {
        ProcessStartInfo psi = new ProcessStartInfo();
        private readonly string _fileName;
        private string stdError = "";
        public CodeRunner(string fileName = "temp")
        {
            _fileName = fileName;
            InitializeProcessInfo();
        }
        private void InitializeProcessInfo()
        {
            psi.FileName = "cmd";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
        }
        
        private void ExecuteProcess()
        {
            Process p = Process.Start(psi);
            string strOutput = p.StandardOutput.ReadToEnd();
            string strError = p.StandardError.ReadToEnd();
            p.WaitForExit();
            if (strError.Length > 0)
            {
                stdError = strError;
            }
            Console.WriteLine(strError);
        }
        private string PrepareCommand(string Command)
        {
            var ChangeDirectoryCommand = "cd " + Directory.GetCurrentDirectory() + "\\Services";
            var MergedCommand = ChangeDirectoryCommand + " && " + Command;
            return "/C " + MergedCommand;
        }
        private void Build() {
            var BuildCommand = $"g++ {_fileName}.cpp -o {_fileName}";
            //psi.Arguments = PrepareCommand(BuildCommand);
            BuildCommand = PrepareCommand(BuildCommand);
            ProcessHandler.Run(BuildCommand);
            if(ProcessHandler.LastError != "")
            {
                stdError = ProcessHandler.LastError;
            }
            
        }

        private void Run() {
            var RunCommand = $"{_fileName}.exe";
            //psi.Arguments = PrepareCommand(RunCommand);
            RunCommand = PrepareCommand(RunCommand);
            ProcessHandler.Run(RunCommand);
            if (ProcessHandler.LastError != "")
            {
                stdError = ProcessHandler.LastError;
            }
        }

        public void BuildAndRun()
        {
            Build();
            Run();
        }
        
        public string GetError()
        {
            return stdError;
        }
    }
}
