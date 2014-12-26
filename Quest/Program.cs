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
        private static int _index;
        private static int _mark;
        static void Main(string[] args)
        {
            //loading file in !++ language
            var fileStream = new StreamReader("..\\..\\song.data");            
            var originalText = fileStream.ReadLine();
            while (_index < originalText.Count())
            {                                
                CalculateLetter(originalText[_index], ref _stack);
                _index++;
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
                //положить ноль на верхушку стека
                case 'Z':
                    stack.Push(0);
                    break;
                //сложить два числа на верхушке стека
                case '+':
                //умножить два числа на верхушке стека
                case '*':
                    {
                        var first = stack.Pop();
                        var second = stack.Pop();
                        var result = symbol == '+' ? first + second : first * second;
                        stack.Push(result);
                        break;
                    }
                //поменять местами два верхних элемента стека
                case '$':
                    {
                        var first = stack.Pop();
                        var second = stack.Pop();
                        stack.Push(first);
                        stack.Push(second);
                        break;
                    }
                //дублировать верхний элемент стека
                case '@':
                    {
                        stack.Push(stack.Peek());
                        break;
                    }
                //вывести на печать букву, номер которой лежит на верхушке стека (a – 0, b – 1 и т.д.)
                case '!':
                    //append the letter to the text
                    //'a' starts from 97 in ASCII
                    _text.Append(Convert.ToChar(stack.Peek() + 97));
                    break;
                //увеличить верхушку стека на 1
                case '#':
                    {
                        var top = stack.Pop();
                        stack.Push(++top);
                        break;
                    }
                //уменьшить верхушку стека на 1
                case '-':
                    {
                        var top = stack.Pop();
                        stack.Push(--top);
                        break;
                    }
                //напечатать пробел
                case '~':
                    _text.Append(" ");
                    break;
                //установить метку в программе
                case '[':
                    {
                        _mark = _index;
                        break;
                    }
                //если верхушка стека не равна 0, то возврат на последнюю метку в программе, 
                //иначе – продолжение выполнения программы,
                //верхушка стека удаляется
                case '<':
                    {
                        var top = stack.Peek();
                        if (top != 0)
                        {                            
                            _index = _mark;
                        }
                        stack.Pop();
                        break;
                    }
                //удалить верхний элемент стека
                case '`':
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                    break;
            }
        }
    }
}
