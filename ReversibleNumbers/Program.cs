using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReversibleNumbers
{
    public class Program
    {
        static void Main(string[] args)
        {
            int outerLoopCounter = 1000;
            int innerLoopCounter = 1000;
            int processLoopCounter = 1000;
            int processCount = 0;
            int n = 0;
            int resultCounter = 0;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < outerLoopCounter; i++)
            {
                for (int y = 0; y < innerLoopCounter; y++)
                {
                    for (int p = 0; p < processLoopCounter; p++)
                    {
                        processCount++;
                        n = processCount;
                        if (isReversible(n))
                        {
                            resultCounter++;
                        }
                    }
                }
            }
            sw.Stop();

            Console.WriteLine("Number of reversible numbers below {0}: {1}", processCount.ToString(), resultCounter.ToString());
            Console.WriteLine("Total Seconds: {0}", ((double)(sw.Elapsed.TotalMilliseconds) / 1000).ToString("0.00 ms"));
            Console.ReadKey();
        }

        public static bool isReversible(int value)
        {
            bool result = false;
            int numberToCheck = 0;
            int carryOverDigit = 0;
            int[] sum;
            int[] num1 = GetDigitArray(value);
            int[] num2 = GetDigitArray(value, true);

            if (num1.Last() != 0)
            {
                // only proceed when first digit is even and last digit is odd, 
                // or first digit is odd and last digit is even
                if ((num1[0] % 2 != 0 && num1[num1.Length - 1] % 2 == 0)
                        || (num1[0] % 2 == 0 && num1[num1.Length - 1] % 2 != 0))
                {
                    // perform elementary style addition starting with adding the numbers on the right side of equation moving left
                    for (int i = 0; i < num1.Length; i++)
                    {
                        int index = num1.Length - (i + 1);
                        int testNum = num1[index] + num2[index] + carryOverDigit;

                        // do not extract the carry over digit for the last two numbers added
                        if (testNum > 10 && index != 0)
                        {
                            // convert sum to array
                            sum = GetDigitArray(testNum);

                            // check number in the ones position
                            numberToCheck = sum[1];

                            // extract the carry over digit to add into the next sum
                            carryOverDigit = 1;
                        }
                        else
                        {
                            // no carry over digit
                            carryOverDigit = 0;
                            numberToCheck = testNum;
                        }

                        // continue if odd
                        if (numberToCheck % 2 != 0)
                        {
                            result = true;
                            continue;
                        }
                        else
                        {
                            // break and return false if even found
                            result = false;
                            break;
                        }

                    }
                }
            }

            return result;
        }

        // converts number to array of digits
        public static int[] GetDigitArray(int n, bool reverse = false)
        {
            if (n == 0) return new int[1] { 0 };

            var digits = new List<int>();

            for (; n != 0; n /= 10)
                digits.Add(n % 10);

            var arr = digits.ToArray();

            if (!reverse)
            {
                Array.Reverse(arr);
            }

            return arr;
        }

    }
}
