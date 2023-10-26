using ProgAssign1;

namespace Assignment1
{
    public class DirWalker
    {
        public void walk(String path)
        {
            List<string> list = Directory.EnumerateFiles(path, "*.csv", SearchOption.AllDirectories).ToList<String>();
            //using enumerate files fucntion to get all the csv
            ReadWrite rw = new ReadWrite(path);
            Console.WriteLine();
            Console.WriteLine("Processing of files started !!");
            Console.WriteLine();

            foreach (string filePath in list)
            {
                rw.CsvReadWrite(filePath); 
                //Calling object of CSVREADWRITE class for each file which is defined in ReadWrite Class
            }
            rw.CloseLog();
            Console.WriteLine();
            Console.WriteLine("Processing of files ended. The Log file has been created and can be found in the logs directory.");
            Console.WriteLine();
            Console.WriteLine("Root path is  :" + path);
            Console.WriteLine("Output path is  :" + rw.outputPath);
            Console.WriteLine("Log path is  :" + rw.logPath);
            Console.WriteLine();
        }

        public static void Main(String[] args)
        {
            StreamWriter logWriter;
            Console.WriteLine("Enter Path : ");
            var dirPath = @"" + Console.ReadLine();
            if (Directory.Exists(dirPath)) //checking the given path is right 
            {
                DirWalker fw = new DirWalker(); //Created a fw as an object of dirwalker class 
                fw.walk(dirPath); //calling the walk method
            }
            else
            {
                Console.WriteLine("Enter a valid path ");
            }
        }
    }
}