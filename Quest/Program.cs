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
        private static Stack<int> _stack = new Stack<int>();
        private static StringBuilder _text = new StringBuilder();
        static void Main(string[] args)
        {
            //loading file in !# language
            var fileStream = new StreamReader("..\\..\\song.data");
            while (!fileStream.EndOfStream)
            {
                var letter = fileStream.Read();
                CalculateLetter(letter, ref _stack);
            }            
            fileStream.Close();

            //write down the result
            var strWriter = new StreamWriter("..\\..\\result.txt");
            strWriter.Write(_text);
            strWriter.Close();
            Console.WriteLine(_text);
        }

        private static void CalculateLetter(int letter, ref Stack<int> stack)
        {
            var symbol = Convert.ToChar(letter);
            switch (symbol)
            {
                case 'Z':
                    stack.Push(0);
                    break;
                case '+':
                case '*':
                    var first = stack.Pop();
                    var second = stack.Pop();
                    var result = symbol == '+' ? first + second : first * second;                    
                    stack.Push(result);
                    break;
                case '!':
                    //append the letter to the text
                    //'a' starts from 97 in ASCII
                    _text.Append(Convert.ToChar(stack.Peek() + 97));
                    break;
                case '#':
                    var top = stack.Pop();
                    stack.Push(++top);
                    break;
                case '~':
                    _text.Append(" ");
                    break;
            }
        }
    }
}
