using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

/*=================================================================================================================== 
This Console application Takes as a parameter a string that represents a text file containing a list of names, and their scores.
Orders the names by their score. If scores are the same, order by their last name followed by first name.
Creates a new text file called <input-file- name>-graded.txt with the list of sorted score and names.

For example, if the input file contains:

BUNDY, TERESSA, 88

SMITH, ALLAN, 70

KING, MADISON, 88

SMITH, FRANCIS, 85


Then the output file would be:

BUNDY, TERESSA, 88

KING, MADISON, 88

SMITH, FRANCIS, 85

SMITH, ALLAN, 70
=====================================================================================================================*/

namespace Grade_Scores
{
    public class SortProgram
    {
        static void Main(string[] args)
        {

            if (args.Length != 1)
            {
                Console.WriteLine("Invalid number of arguments ! Enter the path of text file to sort..");
                return;
            }
            string path = args[0];
            SortProgram sortProgram = new SortProgram();
            sortProgram.sortTextFile(path);
        }

/*---------------------------------------------------------------------------------------------------------
        This function splits each line of the text file and store the values in a two dimensional array.
        Paramentes: String array with lines copied from text file | two dimensional array to store all parsed values.
        Returns true on success.
----------------------------------------------------------------------------------------------------------*/
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
/*--------------------------------------------------------------------------------------------------------------
        Function to sort the contents of the text file
        Parameters: File path which is recieved as a command line argument.
        Returns true on success
--------------------------------------------------------------------------------------------------------------*/
        public bool  sortTextFile(string path)
        {
            bool result = false;

            try
            {
             string ext = Path.GetExtension(path);
            //file extension should be ".txt"
            if (ext != ".txt")
            {
                Console.WriteLine("Invalid file...Provide the data in a text file(.txt)");
                return result;
            }
            var filesize = new FileInfo(path).Length;
            if (filesize == 0)
            {
                Console.WriteLine("The data file is empty!, Provide a valid datafile");
                return result;
            }

         
                var lines = File.ReadAllLines(path);
                int linescount = lines.Count();
                string[,] dataArray = new string[linescount, 3];

                if (!splitAndValidateLines(lines, dataArray))
                {
                    return result;
                }


                bool didSwap;
                do //Start of while loop.
                {
                    didSwap = false;
                    for (int i = 0; i < linescount - 1; i++)
                    {
                        //Compare the scores and sort the array elements accordingly.
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
                        //If the scores are equal, sort according to the last name.
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
                            //If last names are same, sort according to the first name.
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

                                    didSwap = true;
                                }
                            }
                        }
                    }
                } while (didSwap); //End of while loop.

                Console.WriteLine("graded-scores " + path);

                for (int i = 0; i < linescount; i++)
                {
                    lines[i] = String.Concat(dataArray[i, 0], ", ", dataArray[i, 1], ", ", dataArray[i, 2]);
                    //Display the sorted lines on Console.
                    Console.WriteLine(lines[i]);
                }
                //Write the sorted lines to the file <input-file- name>-graded.txt
                var newpath = Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path)+"-graded.txt";
                System.IO.File.WriteAllLines(newpath, lines);
                Console.WriteLine("Finished: created {0} ", Path.GetFileNameWithoutExtension(path) + "-graded.txt");

                result = true;

            }

            catch (FormatException)
            { Console.WriteLine("The entries in the text file are not in the proper format, please check and retry"); return result; }
            catch (ArgumentException)
            { Console.WriteLine("File path is a zero-length string, contains only white space, or contains one or more invalid characters"); return result; }
            catch (PathTooLongException)
            { Console.WriteLine("The specified path, file name, or both exceed the system-defined maximum length"); return result; }
            catch (DirectoryNotFoundException)
            { Console.WriteLine("The specified path is invalid, give full path of the text file "); return result; }
            catch (FileNotFoundException)
            { Console.WriteLine("The file specified in path was not found, give full path of the text file."); return result; }
            catch (IOException)
            { Console.WriteLine("An I/O error occurred while opening the file. "); return result; }
            catch (NotSupportedException)
            { Console.WriteLine("File path is in invalid format "); return result; }
            catch (UnauthorizedAccessException)
            { Console.WriteLine("No permission to access the file for Read/Write, give full path of the text file "); return result; }

            return result;
        }
             
      
    }
}
