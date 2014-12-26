using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Quest
{
    class Program
    {

        static void Main(string[] args)
        {
            //loading crypted letter
            var fileStream = new StreamReader("..\\..\\letter.data");            
            var originalText = new StringBuilder();
            var originalAlphabet = fileStream.ReadLine();
            while (!fileStream.EndOfStream)
            {
                originalText.AppendLine(fileStream.ReadLine());
            }
            //filter text from punctuation marks
            var filteredText = Regex.Replace(originalText.ToString(), "[\\W]", "");
            //group all the letters in array and order them by quantity
            var sortedChars =  filteredText.GroupBy((_char) => _char).OrderByDescending((item)=>item.Count());
            /*int inx = 0;
            foreach (var sortedChar in sortedChars)
            {
                Console.WriteLine("{0} {1} {2}",sortedChar.Key,alphabet[inx], sortedChar.Count());
                inx++;
            }*/
            //create crypted alphabet to compare with original one
            var cryptedAlphabet = sortedChars.ToDictionary((item) => item.Key).Keys.ToArray();
            for (var i = 0; i < originalText.Length; i++)
            {
                var index = Array.IndexOf(cryptedAlphabet, originalText[i]);
                if (index != -1)
                {
                    originalText[i] = originalAlphabet[index];
                }                
            }
            var sw = new StreamWriter("..\\..\\decrypted.txt");
            //write down decrypted letter
            sw.Write(originalText);
            sw.Close();
            //show it in console
            Console.WriteLine(originalText);
        }
    }
}
