using System;
using System.Collections.Generic;
using System.Text;

namespace BankEncapsulation
{
    public static class InputHandler
    {
       
        // will return true or false depending on if the 
        public static void AttemptLogin(BankAccount accountAccessing, string passwordEntered)
        {
            if (accountAccessing.LoginAttempts < 3)
            {
                if (accountAccessing.OpenAccount(passwordEntered))
                {
                    RequestHandler.AccessAccount(accountAccessing);
                }
                else
                {
                    ConsoleHandler.CenterMidScreenAndPrint(new string[] {
                    "Incorrect Login Received",
                    "Press any key to return and try again."
                });
                    ConsoleHandler.ClearAfterKeyPress();
                    if (accountAccessing.LoginAttempts < 3)
                    {
                        RequestHandler.GetPassword(accountAccessing);
                    }
                    else
                    {
                        ConsoleHandler.CenterMidScreenAndPrint(new string[]
                        {
                        "Too many incorrect attempts.", "",
                        "Your account has been locked.", "",
                        "Press any key to return to the login page. "
                        }, true);
                        ConsoleHandler.ClearAfterKeyPress();
                        RequestHandler.SelectUser();
                    }
                }
            }
            else
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[]
                {
                    "The account you are trying to access is locked.", "",
                    "Please try accessing this account another time.", "",
                    "You may contact support if you feel this is an error.", "",
                    "Press any key to return to the login page. "
                }, true);
                ConsoleHandler.ClearAfterKeyPress();
                RequestHandler.SelectUser();
            }
        }

        // will return a bool
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
                    List<string> invalidSelectionString = new List<string>()
                    {
                        $"'{response}' is not a valid entry.", ""
                    };
                    foreach (var message in questionToDisplay)
                    {
                        invalidSelectionString.Add(message);
                    }
                    return YesOrNoQuestion(invalidSelectionString.ToArray());
            }
        }

        // return an int matching the option number selected if the options start at 1 and not 0 !!!
        public static int CheckInput(string[] possibleAnswers)
        {
            var input = Console.ReadLine();
            for (int i = 0; i < possibleAnswers.Length; i++)
            {
                string currentOptionNumber = (i + 1).ToString();
                if(input == currentOptionNumber || input.ToLower() == possibleAnswers[i].ToLower().TrimEnd())
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
