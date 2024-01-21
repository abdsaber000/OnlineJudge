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
        private readonly string _filePath;
        private string stdError = "";
        public CodeRunner(string fileName  , string filePath)
        {
            _fileName = fileName;
            _filePath = filePath;
            InitializeProcessInfo();
        }
        private void InitializeProcessInfo()
        {
            psi.FileName = "cmd";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
        }
        
        
        private string PrepareCommand(string Command)
        {
            var ChangeDirectoryCommand = "cd " + _filePath;
            var MergedCommand = ChangeDirectoryCommand + " && " + Command;
            return "/C " + MergedCommand;
        }
        private void Build() {
            var BuildCommand = $"g++ {_fileName}.cpp -o {_fileName}";
            BuildCommand = PrepareCommand(BuildCommand);
            ProcessHandler.Run(BuildCommand);
            if(ProcessHandler.LastError != "")
            {
                stdError = ProcessHandler.LastError;
            }
            
        }

        private void Run() {
            var RunCommand = $"{_fileName}.exe";
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
