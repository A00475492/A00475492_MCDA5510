using CsvHelper;
using CsvHelper.Configuration;
using ProgAssign1;
using System.Globalization;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography.X509Certificates;
//imported necessary libraries 

namespace ProgAssign1
{
    public class ReadWrite
    {
        public int RowsMissingFinal = 0; //declaring variables to be used finally 
        public int RowsFinalCount = 0;
        DateTime StartTime = DateTime.Now; //start time of execution
        bool flag = true;
        public string logPath;
        public string outputPath;

        public ReadWrite(string dirPath) {
            string outputDirectory = Path.Combine(dirPath, "output");
            string logDirectory = Path.Combine(dirPath, "logs");
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            outputPath =  Path.Combine(dirPath, "output", "output.csv");
            logPath = Path.Combine(dirPath, "logs", "Log_file.txt");

            if (!File.Exists(outputPath))
            {
                File.Create(outputPath).Close();
            }
            if (!File.Exists(logPath))
            {
                File.Create(logPath).Close();
            }
        }
        private bool ValidEmail(string email)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(email, pattern);
        }
        public void CsvReadWrite(String path)
        {
            string errormessage = null;
            using var logWriter = new StreamWriter(logPath, true);
            using var log = new CsvWriter(logWriter, new CsvConfiguration(CultureInfo.InvariantCulture));
            logWriter.WriteLine();
            if (flag && File.Exists(outputPath))
            {
                File.Delete(outputPath);
            }
            using var streamReader = new StreamReader(path);
            using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            using var writer = new StreamWriter(outputPath, true);
            using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));

            try
            {
                if (flag)
                {
                    csv.WriteHeader<GetSet>();
                    csv.WriteField("Date");
                    csv.NextRecord();
                    flag = false;

                }

                var records = csvReader.GetRecords<GetSet>().ToList();
                checkData(records, csv, path,log);
            }
            catch (FileNotFoundException)
            {
                errormessage = "The file or directory cannot be found.";
                
            }
            catch (DirectoryNotFoundException)
            {
                errormessage = "The file or directory cannot be found.";
            }
            catch (DriveNotFoundException)
            {
                errormessage = "The drive specified in 'path' is invalid.";
                
            }
            catch (PathTooLongException)
            {
                errormessage = "path' exceeds the maxium supported path length.";
               
            }
            catch (UnauthorizedAccessException)
            {
                errormessage = "You do not have permission to create this file.";
                
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 32)
            {
                errormessage = "There is a sharing violation.";
                
            }
            catch (IOException e) when ((e.HResult & 0x0000FFFF) == 80)
            {
                errormessage = "The file already exists.";
                
            }
            catch (IOException e)
            {
                errormessage = "An exception occurred:\nError code: " +
                                  $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}";
                
            }
            catch (Exception e)
            {
                errormessage = "An exception occurred:\nError code: " +
                                   $"{e.HResult & 0x0000FFFF}\nMessage: {e.Message}";
                
            }
        }
        public void checkData(List<GetSet> data, CsvWriter csv, String path, CsvWriter logWriter)
        {

            foreach (var GetSet in data)
            {
                if (string.IsNullOrEmpty(GetSet.firstname) || string.IsNullOrEmpty(GetSet.lastname)
                    || string.IsNullOrEmpty(GetSet.street) || string.IsNullOrEmpty(GetSet.city)
                    || string.IsNullOrEmpty(GetSet.province) || string.IsNullOrEmpty(GetSet.postalCode)
                    || string.IsNullOrEmpty(GetSet.country) || string.IsNullOrEmpty(GetSet.email)
                    || (GetSet.streetNumber < 1) || (GetSet .phoneNumber < 1) || !ValidEmail(GetSet.email))
                {
                    RowsMissingFinal += 1;
                    string[] pathParts = path.Split(Path.DirectorySeparatorChar);
                    int year = int.Parse(pathParts[pathParts.Length - 4]);
                    int month = int.Parse(pathParts[pathParts.Length - 3]);
                    int day = int.Parse(pathParts[pathParts.Length - 2]);
                    DateTime specificDate = new DateTime(year, month, day);
                    string dateAsString = specificDate.ToString("yyyy/MM/dd");
                    logWriter.WriteRecord(GetSet);
                    logWriter.WriteField(dateAsString);
                    logWriter.NextRecord();
                }
                else
                {
                    
                    RowsFinalCount += 1;

                    string[] pathParts = path.Split(Path.DirectorySeparatorChar);
                    int year = int.Parse(pathParts[pathParts.Length - 4]);
                    int month = int.Parse(pathParts[pathParts.Length - 3]);
                    int day = int.Parse(pathParts[pathParts.Length - 2]);
                    DateTime specificDate = new DateTime(year, month, day);
                    string dateAsString = specificDate.ToString("yyyy/MM/dd");
                    csv.WriteRecord(GetSet);
                    csv.WriteField(dateAsString);

                    csv.NextRecord();
                }
                csv.Flush();
            }
        }
        public void CloseLog()
        {
            DateTime EndTime = DateTime.Now;
            using(var logWriter = new StreamWriter(logPath, true))
            { 
                logWriter.WriteLine();
                logWriter.WriteLine("Total Number of Valid Rows: " + RowsFinalCount);
                logWriter.WriteLine("Total Number of Skipped Rows: " + RowsMissingFinal);
                logWriter.WriteLine("End of processing at " + EndTime.ToString());
                logWriter.WriteLine("Total Execution Time was: " + ((EndTime - StartTime)) + " in HH: MM:SS.mmmm");
                logWriter.Close();
            }
        }
    }
}
