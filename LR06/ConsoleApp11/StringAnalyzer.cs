using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    public class StringAnalyzer
    {
        static readonly string numsAlphabet = "0123456789";
        static readonly string alphabet = "йцукенгшщзхъфывапролджэячсмитьбюqwertyuiopasdfghjklzxcvbnm";
        static readonly string numLet = alphabet + numsAlphabet;

        string str;

        public int LetCount
        {
            get { return LetC(); }
        }
        public double AverageLength
        {
            get { return AverageStrLength(); }
        }
        public int NumCount
        {
            get { return NumC(); }
        }
        public string Str
        {
            get { return str; }
            set { str = value.ToLower() ?? "None"; }
        }
        public StringAnalyzer()
        {
            Str = "None";
        }
        public StringAnalyzer(string str)
        {
            Str = str;
        }
        private int NumC()
        {
            int numCount = 0;
            string tempStr = Str.Replace(" ", String.Empty);
            for (int i = 0; i < tempStr.Length; i++)
            {
                if (numsAlphabet.Contains(tempStr[i]))
                    numCount++;
            }
            return numCount;
        }
        private int LetC()
        {
            int letCount = 0;
            string tempStr = Str.Replace(" ", String.Empty);
            for (int i = 0; i < tempStr.Length; i++)
            {
                if (alphabet.Contains(Convert.ToString(tempStr[i])))
                    letCount++;
            }
            return letCount;
        }
        private double AverageStrLength()
        {
            string tempStr = string.Empty;
            for (int i = 0; i < Str.Length; i++)
                if (alphabet.Contains(Convert.ToString(Str[i])) || Str[i] == ' ')
                    tempStr += Str[i];

            return ((double)tempStr.Replace(" ", String.Empty).Length / tempStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count());
        }
        public void ReplaceWith(string word, string wordReplace)
        {
            string tempStr = Str;
            string separators = ",.'\"!?()[]<>;:";
            string[] tempArr = tempStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempArr.Count(); i++)
            {
                for (int j = 0; j < separators.Length - 1; j++)
                {
                    string sepDel = tempArr[i].Replace(Convert.ToString(separators[j]), " ");
                    if (sepDel == word.ToLower())
                        Str = Str.Replace(sepDel, wordReplace);
                }
            }
        }
        public int SubstringEntries(string substring)
        {
            int entriesCount = 0;
            string temp;
            string tempSubStr = substring.ToLower();
            string[] tempStr = Str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tempStr.Count(); i++)
            {
                temp = tempStr[i];
                while (temp.IndexOf(tempSubStr) != -1)
                {
                    entriesCount++;
                    temp = temp.Substring(temp.IndexOf(substring) + 1);
                }
            }
            return entriesCount;
        }
        public bool PalindromCheck()
        {
            string strTemp = Str;
            for (int i = 0; i < strTemp.Length; i++)
                if (!numLet.Contains(Convert.ToString(strTemp[i])))
                    strTemp = strTemp.Replace(Convert.ToString(strTemp[i]), String.Empty);

            string strTrim1 = strTemp.Substring(0, (int)Math.Ceiling(a: strTemp.Length / 2) + 1);
            string strTrim2 = strTemp.Substring((int)Math.Ceiling(a: strTemp.Length / 2));

            string trim2Reversed = new string(strTrim2.Reverse().ToArray());
            bool isTrue = String.Compare(strTrim1, trim2Reversed) == 0 ? true : false;
            return isTrue;
        }
        public bool DateCheck()
        {
            return DateTime.TryParse(Str, out DateTime dt);
        }
    }
}
