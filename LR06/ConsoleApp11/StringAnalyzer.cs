using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class StringAnalyzer
    {
        static string numsAlphabet = "0123456789";
        static string kirillicAlphabet = "йцукенгшщзхъфывапролджэячсмитьбю";
        static string latAlphabet = "qwertyuiopasdfghjklzxcvbnm";
        static string alphabet = kirillicAlphabet + kirillicAlphabet.ToUpper() + latAlphabet + latAlphabet.ToUpper();

        string str;
        public int numCount { get; set; }
        public int letCount { get; set; }
        public double averageLength { get; set; }
        public string Str
        {
            get { return str; }
            set { str = value ?? "None"; }
        }
        public StringAnalyzer()
        {
            Str = "None";
            numCount = letCount = 0;
        }
        public StringAnalyzer(string str)
        {
            Str = str;
            NumLetCount();
            AverageLength();
        }
        private void NumLetCount()
        {
            if (str != "None")
            {
                string tempStr = str.Replace(" ", "");
                for (int i = 0; i < tempStr.Length; i++)
                {
                    if (numsAlphabet.Contains(tempStr[i]))
                        numCount++;
                    else if (alphabet.Contains(tempStr[i]))
                        letCount++;
                    else
                        continue;
                }
            }
        }
        private void AverageLength()
        {
            if (str != "None")
            {
                string tempStr = string.Empty;
                for (int i = 0; i < str.Length; i++)
                    if (alphabet.Contains(str[i]) || str[i] == ' ')
                        tempStr += str[i];

                int test1 = tempStr.Replace(" ", "").Length;
                int test2 = tempStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count();

                averageLength = ( (double) tempStr.Replace(" ", "").Length / tempStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count());
            }
        }
        public void ReplaceWith(string word, string wordReplace)
        {
            string[] tempStr = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStr.Count(); i++)
                if (tempStr[i].Substring(1).ToLower() == word.Substring(1).ToLower() && tempStr[i].Substring(2) == word.Substring(2))
                    str = str.Replace(tempStr[i], wordReplace);
        }
        public int SubstringEntries(string substring)
        {
            int entriesCount = 0;
            string temp;
            string[] tempStr = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStr.Count(); i++)
            {
                temp = tempStr[i];
                while (temp.IndexOf(substring) != -1)
                {
                    entriesCount++;
                    temp = temp.Substring(temp.IndexOf(substring) + 1);
                }
            }
            return entriesCount;
        }
        public bool PalindromCheck()
        {
            string strTrim1 = str.Substring(0, (int) Math.Ceiling(a: str.Length / 2) + 1);
            string strTrim2 = str.Substring( (int) Math.Ceiling(a: str.Length / 2));

            string trim2Reversed = new string(strTrim2.Reverse().ToArray());
            bool isTrue = String.Compare(strTrim1, trim2Reversed) == 0 ? true : false;
            return isTrue;
        }
        public bool DateCheck()
        {
            bool StrIsData;
            string[] dateComponent = str.Split('.');
            if (int.TryParse(dateComponent[0], out int d1) == true && int.TryParse(dateComponent[1], out int d2) == true && int.TryParse(dateComponent[2], out int d3) == true)
                StrIsData = (d1 > 0 && d1 < 32) && (d2 > 0 && d2 < 13);
            else
                StrIsData = false;
            return StrIsData;
        }
    }
}
