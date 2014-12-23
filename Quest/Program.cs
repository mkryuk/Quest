using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Quest
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileStream = new StreamReader("..\\..\\lyrics.data");
            const string myPart = "xyzw";
            const string starPart = "abcd";
            //read all song to end (it`s in one string file)
            var wholeSong = fileStream.ReadLine();
            //cut all noise like back vocal ets.
            var songWithoutBack = Regex.Replace(wholeSong, "[^abcdxyzw]", "");
            //filter all my parties
            var myParts = Regex.Matches(songWithoutBack, "[^abcd]+");
            //filter all star parties
            var starParts = Regex.Matches(songWithoutBack, "[^xyzw]+");
            //evaluate my longest party
            var myMaxLength = myParts.Cast<Match>().Select(match=>match.Value).ToList().Max((item)=>item.Length);
            //evaluate star longest party
            var starMaxLength = starParts.Cast<Match>().Select(match => match.Value).ToList().Max((item) => item.Length);
            //calculate in persentage
            var myResult = myMaxLength * 100 / (myMaxLength + starMaxLength);
            var starResult = starMaxLength * 100 / (myMaxLength + starMaxLength);
            Console.WriteLine("my party longest length: {0}% star party longest length: {1}%", myResult, starResult);
        }
    }
}
