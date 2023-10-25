## Pranay Malusare - A00475492
##### ASP.net Assingment. 

For the given assignment I have created 3 classes in C#. 1. GetSet 2. DirWalker 3. ReadWrite 
###### 1. Get Set
  - This is a getter setter of the code, where I am getting the details from the CSV in the format provided and passing it along.
###### 2. DirWalker
  - This is my directory walker class where I am walking all the directories till I find a .csv file
  - I will stop and read the CSV file if found.
  - Along, the way I am calling the ReadWrite method from my Main class which is also located in this file.
  - I am closing the log file after writing in it. 
###### 3. ReadWrite
  - After calling this method, the CSV in the directory will be read and written at the same time.
  - I am also using variables to count the number of rows that are skipped and that are valid.
  - I have also written the **exception handling** in this ReadWrite, **Try and catch** code for which I am printing the error messages and **incrementing the skipped rows by 1**.
  - After last, I have written the method for closing the log file which is being called from the main method, and while closing I am writing the **total execution time, total skipped rows, and total valid rows** in the log file. 

###### How to Run: 
1. Clone the repo and open the files using a solution file of the project.
2. Make sure all three files are opened and running DirWalker, GetSet, And ReadWrite.
3. Run the project, and a console will open asking you to enter the path. Give a valid path (The program will end if the path is not valid and you have to rerun it.)
4. You will see a message "Processing of all files started" stating that the processing has started. It will also print the **Root Path** which is given by you. **Output Path** where output.csv file will be created. **Log Path** where the log file will be created.
5. All exceptions caught will be printed on the console.
6. At the very end the message "Processing of files ended. The log file has been created and can be found in the logs directory" will be printed on the console stating that the program has finished running.
7. You can access the log file and CSV file in the path mentioned. You do not need to provide an extra path for CSV or log file, it will be created by code.
8. At the start of the log you will find all the skipped rows, that have been logged into the file. 
9. At the end of the log file you will be able to find the "Total execution time", "Total skipped rows", "Total valid rows"

For example :
If the path given is ProjAssign1/ 
Output.csv file path will be ProjAssign1/Output
The log_file.txt file  path will be ProjAssign1/logs
