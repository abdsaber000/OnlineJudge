using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Diagnostics;

namespace OnlineJudge.Services
{
    public abstract class ProcessHandler
    {
        static ProcessStartInfo psi = new ProcessStartInfo()
        {
            FileName = "cmd",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
        };
        public static string LastError { get; set; } = "";
        public ProcessHandler() {
            psi.FileName = "cmd";
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;
        }

        public static string Run(string command)
        {
            AddCommand(command);
            return ExecuteProcess();
        }
        private static void AddCommand(string command)
        {
            psi.Arguments = command;
        }
        private static string ExecuteProcess()
        {
            Process p = Process.Start(psi);
            string strOutput = p.StandardOutput.ReadToEnd();
            string strError = p.StandardError.ReadToEnd();
            LastError = strError;
            p.WaitForExit();
            return strError;
        }
    }
}
