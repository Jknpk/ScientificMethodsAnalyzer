using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificMethodAnalyzer
{
    public class DataSample
    {

        List<Dictionary<string, int>> wordsInAllDictionaries = new List<Dictionary<string, int>>();

        public DataSample(int languagesInput)
        {
            int languages = languagesInput;
            while(languages > 0)
            {
                wordsInAllDictionaries.Add(new Dictionary<string, int>());
                languages--;
            }
        }

        public void AddWord(string word, int language, int appearances)
        {
            wordsInAllDictionaries[language][word] = appearances;
        }



        public string GenerateFileOutput()
        {
            string returnString = "";
            foreach(Dictionary<string, int> language in wordsInAllDictionaries)
            {
                returnString += language.Sum(x => x.Value) + "\t";
            }
            return returnString;
        }

        


        public string ToMoreInformationString(string[] languages)
        {
            string returnString = "\t";

            foreach(string lang in languages){
                returnString += lang.Substring(0, 3) + "\t";
            }

            returnString += "\n\t";

            foreach (string lang in languages)
            {
                returnString += "---" + "\t";
            }
            returnString += "\n";

            for (int l = 0; l < wordsInAllDictionaries.Max(t => t.Count); l++)
            {        
            returnString += "\t";

                for (int k  = 0; k< languages.Length; k++)
                {
                    // biggest element
                    try
                    {
                        returnString += wordsInAllDictionaries[k].ElementAt(l).Key + "\t";
                    }catch(Exception e)
                    {
                        returnString += "\t";
                    }


            
                }
                returnString += "\n";
                       
            }

            return returnString;


            //int i = -1;
            //foreach(Dictionary<string, int> language in wordsInAllDictionaries)
            //{
            //    i++;
            //    returnString += "\t" + languages[i].Substring(0,3) + "\n";
            //    foreach(string word in language.Keys)
            //    {
            //        returnString += "\t\t" + word + "\n";
            //    }
                
            //}

            //return returnString;
        }

    }
}
