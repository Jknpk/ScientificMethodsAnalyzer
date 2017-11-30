using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScientificMethodAnalyzer
{
    
    class Program
    {
        public static int kk = 0;
        const int minimalValidCharacters = 500;
        static Output outputData = new Output();
        static string[] languagesArray;

        static void Main(string[] args)
        {
            // Read Input Directory
            const string pathToFolder = "./Data/";
            const string pathToDictionries = "./Dictionary";
            
            // Read Input Data (optimized=only a-z allowed)
            List<String> dictionaries = readOptimizedData(pathToDictionries, true);
            List<String> rawDataSamples = readOptimizedData(pathToFolder, false);
            List<DataSample> dataSamples = new List<DataSample>();
           
            foreach (string sample in rawDataSamples)
            {
                dataSamples.Add(new DataSample(dictionaries.Count()));
            }
            
            int dictionaryCounter = 0;
            foreach (string dicitonary in dictionaries)
            {
                StringReader sr = new StringReader(dicitonary);
                string word;
                while ((word = sr.ReadLine()) != null)
                {
                    if (word.Length <= 1 || word.Distinct().Count() == 1) continue;
                    int i = 0;
                    foreach(string sample in rawDataSamples)
                    {
                        int appearances = Regex.Matches(Regex.Escape(sample), word).Count;
                        if(appearances > 0)
                        {
                            dataSamples[i].AddWord(word,dictionaryCounter, appearances);
                        }
                        i++;
                    }
                }
                dictionaryCounter++;
            }


            int l = 0;
            foreach (DataSample ds in dataSamples)
            {
                outputData.Add(ds.GenerateFileOutput() + "\t" + rawDataSamples.ElementAt(l).Count() + "\n");
                l++;
            }

            // foreach Dictonary // Same order as in DataSample
            // foreach word in Dictonary
            // foreach sampleData in dataContent
            // check if word occurs 
            // count how many times a word accures


            // what we know:
            // word, amount of occurs per sample 
            // 
            // what we want to output:

            //german  french  engish danish
            //1 2 3 4
            //4 2 8 0
            //9 2 0 1


            // Check how many words in each language are included
            // Save important information

            // Read Dictonaries
            // to lowercase
            // Remove special characters
            // Replace List: 
            // ä=a, ü=u, ö=o, é=e, è=e

            
            File.WriteAllText("./results.txt", outputData.ToFileString());

            // Generate outputfile
            int k = 0;
            foreach(DataSample data in dataSamples)
            {
                k++;
                outputData.AddAdditionalInformation(k, data.ToMoreInformationString(languagesArray));
            }
            Console.WriteLine(outputData.ToAdditionalInformationString());
            Console.ReadLine();
        }

        private static List<String> readOptimizedData(String pathToFolder, Boolean dicts)
        {
            string[] dataFiles = null;
            try
            {
                dataFiles = Directory.GetFiles(pathToFolder);
            }
            catch (DirectoryNotFoundException)
            {
                Console.Error.WriteLine("Invalid Directory! " + pathToFolder);
                Console.ReadLine();
                System.Environment.Exit(1);
            }
            List<string> dataContent = new List<string>();
            if (dicts) languagesArray = new string[dataFiles.Length];
            int i = -1;
            foreach (string dataFile in dataFiles)
            {
                // Open file, Read as string
                
                if (dicts)
                {
                    outputData.AddDictname(Path.GetFileNameWithoutExtension(dataFile).Substring(0, 3));
                    i++;
                    languagesArray[i] = Path.GetFileNameWithoutExtension(dataFile);
                }
                string content = File.ReadAllText(dataFile);
                
                // Only low character 
                content = content.ToLower();

                // replace every special character by a-z

                Regex reg1 = new Regex("[éèêë]");
                content = reg1.Replace(content, "e");

                Regex reg2 = new Regex("[äàâæå]");
                content = reg2.Replace(content, "a");

                Regex reg3 = new Regex("[üùû]");
                content = reg3.Replace(content, "u");

                Regex reg4 = new Regex("[öôœø]");
                content = reg4.Replace(content, "o");

                Regex reg5 = new Regex("[ß]");
                content = reg5.Replace(content, "ss");

                Regex reg6 = new Regex("[ç]");
                content = reg6.Replace(content, "c");
                                
                Regex reg7 = new Regex("[ïî]");
                content = reg7.Replace(content, "i");

                string regexString = "";
                
                if (dicts)
                {
                    // Remove everything that isn't a-z and new line
                    regexString = @"[^a-z\S\r\n]";
                }
                else
                {
                    // Remove everything that isn't a-z
                    regexString = @"[^a-z]";
                }

                Regex keepAlphabet = new Regex(regexString);
                content = keepAlphabet.Replace(content, String.Empty);

                // At this point we have the perfect input string!
                //Console.WriteLine(content)
                if (dicts)
                {
                    dataContent.Add(content);
                }
                else
                {

                    dataContent.Add(content);
                    // OR: take defined amount of valid letters into account
                    /*
                    if(content.Length >= minimalValidCharacters)
                    {
                        dataContent.Add(content.Substring(0, minimalValidCharacters));
                    }
                    else
                    {
                        Console.WriteLine("WARNING: The file " + dataFile + " contains "+(minimalValidCharacters - content.Length) +" charcaters less than the required amount of valid characters ("+ minimalValidCharacters + ") ! Sample won't be taken into account!");
                    }
                    // if final dataString too small: go over it and don't car
                    // that, of course, results in a smaller n!
                    */
                }
            }
            return dataContent;
        }
    }
}
