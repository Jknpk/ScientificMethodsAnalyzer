using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScientificMethodAnalyzer
{
    class Output
    {
        string outputString = "";
        string dictnames = "";
        string additionalInformation = "AdditionalInformation:\n";

        public Output()
        {

        }

        public string OutputString { get => outputString; }

        public void Add(string toAdd)
        {
            outputString += toAdd;

        }

        public void AddAdditionalInformation(int dataNumber, string toAdd)
        {
            additionalInformation += "Sample: " + dataNumber + "\n" + toAdd; 
        }


        public void AddDictname(string dictname)
        {
            dictnames += dictname + "\t";
        }

        public string ToFileString()
        {
            return dictnames + "\n" + outputString;
        }

        public string ToAdditionalInformationString()
        {
            
            return additionalInformation;
        }

    }
}
