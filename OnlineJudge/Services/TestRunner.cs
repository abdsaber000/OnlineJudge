using Microsoft.AspNetCore.Components.Forms;

namespace OnlineJudge.Services
{
    public class TestRunner
    {
        private string code;
        private string inputTest;
        private string expectedOutput;
        private string executionFolder = Directory.GetCurrentDirectory() + "\\test";
        public string Vredict {  get; set; }
        public TestRunner(string code , string inputTest , string expectedOutput)
        {
            this.code = code;
            this.inputTest = inputTest;
            this.expectedOutput = expectedOutput;
            AppendRandomNumber();
        }
        private void AppendRandomNumber()
        {
            Random random = new Random();
            executionFolder += random.Next(1, 10).ToString();
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
            string MakeOutputExpectedCommand = "touch outputExp.txt";
            ProcessHandler.Run(PrepareCommand(MakeSourceCommand));
            ProcessHandler.Run(PrepareCommand(MakeInputCommand));
            ProcessHandler.Run(PrepareCommand(MakeOutputExpectedCommand));
        }

        public void WriteData()
        {
            string sourcePath = executionFolder + "\\temp.cpp";
            string inputPath = executionFolder + "\\input.txt";
            string outputPath = executionFolder + "\\outputExp.txt";
            File.WriteAllText(sourcePath, code);
            File.WriteAllText(inputPath, inputTest);
            File.WriteAllText(outputPath, expectedOutput);
        }
        
        public void RunCode()
        {
            CodeRunner codeRunner = new CodeRunner("temp", executionFolder);
            codeRunner.BuildAndRun();
            if(codeRunner.GetError().Length > 0)
            {
                Vredict = "Compilation Error";

            }
            else
            {
                CompareOutput();
            }

        }
        private void CompareOutput()
        {
            string filePath1 = executionFolder + "\\output.txt";
            string filePath2 = executionFolder + "\\outputExp.txt";
            if(FileCompare(filePath1 , filePath2))
            {
                Vredict = "Accepted";
            }
            else
            {
                Vredict = "Wrong Answer";
            }

        }

        private bool FileCompare(string file1 , string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            fs1 = new FileStream(file1, FileMode.Open);
            fs2 = new FileStream(file2, FileMode.Open);

            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();

                return false;
            }

            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            fs1.Close();
            fs2.Close();

            return ((file1byte - file2byte) == 0);
        }
        public void Clean()
        {
            string removeDirecotryCommand = "rm -r " + Directory.GetCurrentDirectory();
            ProcessHandler.Run(PrepareCommand(removeDirecotryCommand));
        }
    }
}
