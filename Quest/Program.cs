using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;


namespace Quest
{


    class Program
    {

        static void Main(string[] args)
        {
            var fileReader = new StreamReader("..//..//message.dat");
            var dictionary = new Dictionary<string, Dictionary<string, int>>();
            //read file and load data
            var words = fileReader.ReadToEnd().Split(' ');
            fileReader.Close();
            for (var i = 0; i < words.Length-1; i++)
            {
                //create key for inner dictionary of word pairs
                var key = words[i] +" "+ words[i + 1];
                //if dictionary contains first word
                if (dictionary.ContainsKey(words[i]))
                {
                    //if that key word contains key (pair of words words[i] and words[i + 1])
                    if (dictionary[words[i]].ContainsKey(key))
                    {
                        //increase that words pair count
                        dictionary[words[i]][key]++;
                    }
                    else
                    {
                        //set count of key pair to 1
                        dictionary[words[i]][key] = 1;
                    }                      
                }
                else
                {
                    dictionary.Add(words[i],new Dictionary<string, int>());
                    dictionary[words[i]][key] = 1;
                }             
            }

            var result = new Dictionary<string, int>();            
            //order dictionary by quantity
            var orderedDictionary = dictionary.OrderByDescending(item => item.Value.Sum(pair => pair.Value));            
            foreach (var pairs in orderedDictionary)
            {
                var orderedPairs = pairs.Value.OrderByDescending(pair => pair.Value);
                //magic 15 is how many key pairs in collection
                if (orderedPairs.First().Value <= 15) continue;
                var key = orderedPairs.First().Key;
                var value = orderedPairs.First().Value;
                result.Add(key, value);
            }            

            using (var writer = new StreamWriter("..//..//result.txt"))
            {
                result.ToList().ForEach(data=>writer.Write(data.Key+" "));
            }
        }
    }
}
