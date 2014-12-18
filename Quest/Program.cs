using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Quest
{
    class Program
    {
       
        static void Main(string[] args)
        {            
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";
            var fileStream = new StreamReader("..\\..\\lyrics.result");
            var text = fileStream.ReadToEnd();
            var filtered = Regex.Matches(text, "(Yo|Nice)");
            var result = new StringBuilder();
            var index = 0;
            var nice = false;
            for (var i = 0; i < filtered.Count; i++)
            {
                switch (filtered[i].Value)
                {
                    case "Yo":
                        index++;
                        nice = false;
                        break;
                    case "Nice":
                        //was the previous "Nice"
                        if (nice)
                        {
                            //the end of word
                            result.Append(" ");
                            nice = false;
                        }
                        else
                        {
                            //append the letter to the word
                            result.Append(alphabet[index - 1]);
                            index = 0;
                            nice = true;
                        }
                        break;
                }
            }

            Console.WriteLine(result);
        }
    }
}
