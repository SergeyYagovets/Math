using System;
using System.Collections.Generic;

namespace MathTest
{
    internal class Program
    {
        private const int V = 13;
        private static string[] TwoNumber = new string[2];
        private static string NumberIn13;

        static bool TryPerevod(char charDigit, out int number, int notation)
        {
            number = 0;
            if (notation < 2 || notation > 16) // Проверка корректности указанной системы счисления
                return false; // Возврат false, если некорректна
            int numb;
            if (charDigit >= '0' && charDigit <= '9') numb = charDigit - '0'; // Получение кода 0...9
            else if (charDigit >= 'A' && charDigit <= 'C') numb = charDigit - 'A' + 2; // Получение кода A...C
            else if (charDigit >= 'a' && charDigit <= 'c') numb = charDigit - 'a' + 2; // Получение кода a...c
            else return false; // Возврат false, если символ некорректен

            if (numb >= notation) return false; // Ошибка если символ больше указанной СС
            number = numb;
            return true;
        }
        public static void To13(long number)
        {
            List<string> remainderList = new List<string>();
            long remainder;
            long result = number;
            do
            {
                result = Math.DivRem(result, V, out remainder);
                remainderList.Add(Convert.ToString(remainder));
            }
            while (result != 0);
            for (int i = 0; i < remainderList.Count; i++)
            {
                switch (remainderList[i])
                {
                    case "10":
                        remainderList[i] = "A";
                        break;
                    case "11":
                        remainderList[i] = "B";
                        break;
                    case "12":
                        remainderList[i] = "C";
                        break;
                    default: break;
                }
            }
            for (int i = remainderList.Count; i < V; i++)
            {
                remainderList.Add("0");
            }
            remainderList.Reverse();
            NumberIn13 = string.Join("", remainderList);
        }

        public static bool GoodNumber()
        {
            TwoNumber[0] = NumberIn13.Substring(0, 6);
            TwoNumber[1] = NumberIn13.Substring(7);

            long firstNumber = 0;
            long secondNumber = 0;

            for (int i = 0; i < TwoNumber.Length; i++)
            {
                foreach (var item in TwoNumber[i].ToCharArray())
                {
                    int Digit = 0;
                    if (i == 0)
                    {
                        if (TryPerevod(item, out Digit, V)) // перевод в 13-ричную с проверкой на корректность
                            firstNumber += Digit;
                        else break;
                    }
                    if (i == 1)
                    {
                        if (TryPerevod(item, out Digit, V)) // перевод в 13-ричную с проверкой на корректность
                            secondNumber += Digit;
                        else break;
                    }
                }
            }
            if (firstNumber == secondNumber)
            {
                Console.WriteLine(NumberIn13);
                Console.WriteLine(TwoNumber[0] + "  " + TwoNumber[1]);
                Console.WriteLine(firstNumber + " = " + secondNumber);
                return true;
            }
            return false;
        }

        private static void Main(string[] args)
        {
            Console.WriteLine("Good Numbers =  302875106592252");
            long countOfGoodNumbers = 0;

            // i = 302 875 106 592 252 or C CCC CCC CCC CCC   
            for (long i = 0; i <= 302875106592252; i++)
            {
                To13(i);
                if (GoodNumber())
                {
                    countOfGoodNumbers++;
                    Console.WriteLine("number = " + countOfGoodNumbers);
                }
                else
                {
                    continue;
                }
            }
            Console.WriteLine("Количество хороших чисел = " + countOfGoodNumbers);
        }
    }
}

