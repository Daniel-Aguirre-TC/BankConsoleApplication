using System;
using System.Collections.Generic;
using System.Text;

namespace BankEncapsulation
{
    public static class ConsoleHandler
    {


        // this should only be used by the ConsoleHandler, to center text horizontally before printing.
        static string CenterHorizontal(string textToCenter)
        {
            return textToCenter.PadLeft((int)MathF.Round((Console.WindowWidth/2) + (textToCenter.Length / 2)));
        }

        // print the provided string or string[] centered on the current line.
        public static void CenterAndPrint(string textToPrint)
        {
            Console.WriteLine(CenterHorizontal(textToPrint));
        }
        public static void CenterAndPrint(string[] arrayOfStringsToPrint)
        {
            foreach (var str in arrayOfStringsToPrint)
            {
                CenterAndPrint(str);
            }
        }
        
        //Clear Console, then print the provided string or string[] centered vertically and horizontally
        public static void CenterMidScreenAndPrint(string stringToPrint)
        {
            Console.Clear();
            for (int i = 0; i < Console.WindowHeight / 2; i++)
            {
                Console.WriteLine();
            }
            Console.WriteLine(CenterHorizontal(stringToPrint));
        }
        public static void CenterMidScreenAndPrint(string[] arrayOfStringsToPrint)
        {
            Console.Clear();
            for (int i = 0; i < (Console.WindowHeight / 2) - (arrayOfStringsToPrint.Length); i++)
            {
                Console.WriteLine();
            }
            foreach (var textLine in arrayOfStringsToPrint)
            {
                Console.WriteLine(CenterHorizontal(textLine));
            }
        }
        // CenterMidScreenAndPrint but if bool is true then we will do a Write instead of WriteLine.
        public static void CenterMidScreenAndPrint(string stringToPrint, bool dontEndLine)
        {
            Console.Clear();
            for (int i = 0; i < Console.WindowHeight / 2; i++)
            {
                Console.WriteLine();
            }
            if (dontEndLine)
            {
                Console.Write(CenterHorizontal(stringToPrint));
            }
            else Console.WriteLine(CenterHorizontal(stringToPrint));
        }
        public static void CenterMidScreenAndPrint(string[] arrayOfStringsToPrint, bool dontEndLastLine)
        {
            Console.Clear();
            for (int i = 0; i < (Console.WindowHeight / 2) - (arrayOfStringsToPrint.Length); i++)
            {
                Console.WriteLine();
            }
            for (int i = 0; i < arrayOfStringsToPrint.Length; i++)
            {
                // if it's not on the last cycle
                if (i != arrayOfStringsToPrint.Length - 1)
                {
                    Console.WriteLine(CenterHorizontal(arrayOfStringsToPrint[i]));
                }
                else Console.Write(CenterHorizontal(arrayOfStringsToPrint[i]));
            }
        }
        public static void CenterMidScreenAndPrint(string[] arrayOfStringsToPrint, int lengthOfStringToMatchPaddingOf)
        {
            Console.Clear();
            for (int i = 0; i < (Console.WindowHeight / 2) - (arrayOfStringsToPrint.Length); i++)
            {
                Console.WriteLine();
            }
            for (int i = 0; i < arrayOfStringsToPrint.Length; i++)
            {
                // if it's not on the last cycle
                if (i != arrayOfStringsToPrint.Length - 1)
                {
                    Console.WriteLine(CenterHorizontal(arrayOfStringsToPrint[i]));
                }
                else Console.Write(arrayOfStringsToPrint[i].PadLeft((Console.WindowWidth / 2) - ((lengthOfStringToMatchPaddingOf/2) - arrayOfStringsToPrint[i].Length)));              
            }
        }


        // Clear the console after the user presses any key.
        public static void ClearAfterKeyPress()
        {
            Console.ReadKey();
            Console.Clear();
        }

    }
}
