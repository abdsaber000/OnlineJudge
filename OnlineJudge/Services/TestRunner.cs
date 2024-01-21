namespace OnlineJudge.Services
{
    public class TestRunner
    {
        private string code;
        private string inputTest;
        private string expectedOutput;
        private string executionFolder = Directory.GetCurrentDirectory() + "\\test";
        
        public TestRunner(string code , string inputTest , string expectedOutput)
        {
            this.code = code;
            this.inputTest = inputTest;
            this.expectedOutput = expectedOutput;
        }

        public void MakeDir()
        {
            Directory.CreateDirectory(executionFolder);
        }
        public string PrepareCommand(string command)
        {
            return "/C " + "cd " + executionFolder + " && " + command;

        }
        public void MakeFiles()
        {
            string MakeSourceCommand = "touch temp.cpp";
            string MakeInputCommand = "touch input.txt";
            string MakeOutputCommand = "touch output.txt";
            ProcessHandler.Run(PrepareCommand(MakeSourceCommand));
            ProcessHandler.Run(PrepareCommand(MakeInputCommand));
            ProcessHandler.Run(PrepareCommand(MakeOutputCommand));
        }
    }
}
