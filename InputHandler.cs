using System;
using System.Collections.Generic;
using System.Text;

namespace BankEncapsulation
{
    public static class InputHandler
    {
       
        
        public static bool AttemptLogin(BankAccount accountAccessing, string passwordEntered)
        {
            if (accountAccessing.OpenAccount(passwordEntered))
            { 
                ConsoleHandler.CenterMidScreenAndPrint("You have logged into your account!");
                ConsoleHandler.ClearAfterKeyPress();
                return true;
            }
            else
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[] { 
                    "Incorrect Login Received", 
                    "Try again?"
                });
                ConsoleHandler.ClearAfterKeyPress();
                return false;
            }
        }

        public static bool YesOrNoQuestion(string[] questionToDisplay)
        {
            ConsoleHandler.CenterMidScreenAndPrint(questionToDisplay, true);
            char response = Console.ReadKey().KeyChar;
            switch (response)
            {
                case (char)ConsoleKey.Enter:
                case 'y':
                    return true;
                case 'n':
                    return false;
                default:                   
                    return YesOrNoQuestion(new string[] { $"'{response}' is not a valid entry." + questionToDisplay});
            }
        }

        // return an int matching the option number selected if the options start at 1 and not 0 !!!
        public static int CheckInput(string[] possibleAnswers)
        {
            var input = Console.ReadLine();
            for (int i = 0; i < possibleAnswers.Length; i++)
            {
                string currentOptionNumber = (i + 1).ToString();
                if(input == currentOptionNumber || input.ToLower() == possibleAnswers[i].ToLower())
                {
                    return ++i;
                }
            }
            // if we exit for loop without returning anything then an invalid selection was made
            List<string> invalidSelectionString = new List<string>()
            {
                $"I'm sorry, we were unable to find an option for \"{input}\".",
                "",
                "Please try again."
            };
            for (int i = 0; i < possibleAnswers.Length; i++)
            {
                invalidSelectionString.Add($"{i+1}) " + possibleAnswers[i]);
            }  
            // first clear the console then reprint options.            
            ConsoleHandler.CenterMidScreenAndPrint(invalidSelectionString.ToArray());
            Console.Write("".PadLeft((Console.WindowWidth / 2) - (invalidSelectionString[4].Length / 2)));
            // will continue to cycle through until a valid option is selected.
            return CheckInput(possibleAnswers);
        }

        public static BankAccount CheckForUserName(string usernameToSearchFor)
        {
            foreach (var bankAccount in AccountList.allAccounts)
            {
                if(usernameToSearchFor == bankAccount.Username)
                {
                    return bankAccount;
                }
            }
            // if none returned one then return null
            return null;
        }

    }
}
