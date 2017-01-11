using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace Grade_Scores
{
    public class SortProgram
    {
        static void Main(string[] args)
        {

            //if (args.Length != 1)
            //{
            //    Console.WriteLine("Invalid number of arguments ! Enter the path of text file to sort..");
            //    return;
            //}
            // string path = args[0];

            string path = @"C:\James\names.txt";
            SortProgram sortProgram = new SortProgram();
            sortProgram.sortTextFile(path);
        }

//---------------------------------------------------------------------------------------------------------
        //This function splits each line of the text file and store the values in an array.
        //Returns true on success.
        public bool splitAndValidateLines(string[] lines, string[,] dataArray)
        {

            for (int i = 0; i < lines.Count(); i++)
            {
                string[] words = lines[i].Split(',');

                if (words.Length != 3)
                {
                    Console.WriteLine("Please check the file format. Format should  be  Last Name, First Name, Score");
                    return false;
                }

                for (int j = 0; j < 3; j++)
                {
                    dataArray[i, j] = words[j].TrimEnd().TrimStart();
                }
            }

            return true;
        }
//--------------------------------------------------------------------------------------------------------------

        //Function to sort the contents of the text file
        public bool  sortTextFile(string path)
        {
            bool success = false;

            try
            {
                var lines = File.ReadAllLines(path);
                int linescount = lines.Count();
                string[,] dataArray = new string[linescount, 3];

                if (!splitAndValidateLines(lines, dataArray))
                {
                    return success;
                }


                bool didSwap;
                do
                {
                    didSwap = false;
                    for (int i = 0; i < linescount - 1; i++)
                    {
                        if (Convert.ToInt32(dataArray[i, 2]) < Convert.ToInt32(dataArray[i + 1, 2]))
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                string temp = dataArray[i + 1, j];
                                dataArray[i + 1, j] = dataArray[i, j];
                                dataArray[i, j] = temp;
                            }
                            didSwap = true;
                        }
                        else if (Convert.ToInt32(dataArray[i, 2]) == Convert.ToInt32(dataArray[i + 1, 2]))
                        {
                            if (string.Compare(dataArray[i, 0], dataArray[i + 1, 0]) == 1)
                            {
                                for (int j = 0; j < 3; j++)
                                {
                                    string temp = dataArray[i + 1, j];
                                    dataArray[i + 1, j] = dataArray[i, j];
                                    dataArray[i, j] = temp;
                                }

                                didSwap = true;
                            }
                            else if (string.Compare(dataArray[i, 0], dataArray[i + 1, 0]) == 0)
                            {
                                if (string.Compare(dataArray[i, 1], dataArray[i + 1, 1]) == 1)
                                {
                                    for (int j = 0; j < 3; j++)
                                    {
                                        string temp = dataArray[i + 1, j];
                                        dataArray[i + 1, j] = dataArray[i, j];
                                        dataArray[i, j] = temp;
                                    }
                                }
                            }
                        }
                    }
                } while (didSwap);

                Console.WriteLine("graded-scores " + path);

                for (int i = 0; i < linescount; i++)
                {
                    lines[i] = String.Concat(dataArray[i, 0], ", ", dataArray[i, 1], ", ", dataArray[i, 2]);
                    Console.WriteLine(lines[i]);
                }

                Console.WriteLine("Finished created names-graded.txt");

                System.IO.File.WriteAllLines(@"C:\James\graded.txt", lines);
                success = true;

            }



            catch (FormatException)
            { Console.WriteLine("The entries in the text file are not in the proper format, please check and retry"); return success; }
            catch (ArgumentException)
            { Console.WriteLine("File path is a zero-length string, contains only white space, or contains one or more invalid characters"); return success; }
            catch (PathTooLongException)
            { Console.WriteLine("The specified path, file name, or both exceed the system-defined maximum length"); return success; }
            catch (DirectoryNotFoundException)
            { Console.WriteLine("The specified path is invalid "); return success; }
            catch (FileNotFoundException)
            { Console.WriteLine("The file specified in path was not found."); return success; }
            catch (IOException)
            { Console.WriteLine("An I/O error occurred while opening the file. "); return success; }
            catch (NotSupportedException)
            { Console.WriteLine("File path is an invalid format "); return success; }

            return success;
        }
             
      
    }
}
