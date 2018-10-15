using System;
using System.Collections.Generic;
using System.Text;

namespace SEO.Business
{
    public class Ngrams
    {
        public Ngrams()
        { }

        public IEnumerable<string> GetNgramsWords(string text, int ngramSize)
        {
            var result = new List<string>();
            string cleanText = string.Empty;

            foreach (char i in text)
            {
                if (i == '\n' || Environment.NewLine.Equals(i))
                    cleanText += ' ';
                if (char.IsLetter(i) || i == ' ')
                    cleanText += i;
            }
            string[] words = cleanText.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (i > words.Length - ngramSize)
                    break;
                string temp = string.Empty;

                for (int j = 0; j < ngramSize; j++)
                    temp += $"{words[i + j]} ";
                result.Add(temp);
            }
            return result;
        }
    }
}
