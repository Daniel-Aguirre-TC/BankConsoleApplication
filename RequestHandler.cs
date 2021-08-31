using System;
using System.Collections.Generic;
using System.Text;

namespace BankEncapsulation
{
    public static class RequestHandler
    {


        public static void OpenApplication()
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[]{
            "Thank you for using our Banking Application!", "",
            "Created by Daniel Aguirre. ", "",
            "Press any key to continue. "
            }, true);
            ConsoleHandler.ClearAfterKeyPress();
        }

        public static void SelectUser()
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[] {
            "Login Page", "",
            "Please enter your username below :", "",
            "Username : "
            }, 34);
            string response = Console.ReadLine();
            BankAccount selectedAccount = InputHandler.CheckForUserName(response);
            if(selectedAccount == null)
            {
                OfferNewAccountOrSelectUser(response);
            }
            else
            {
                GetPassword(selectedAccount);
            }
        }

        public static void OfferNewAccountOrSelectUser(string responseThatDidntMatch)
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[]{
            $"I'm sorry, we were unable to find a username for {responseThatDidntMatch}","",
            "Please select an option below:", "",
            "1) Try a different username   ",
            "2) Create a new account       ",
            ""
            }, 30);
            int selection = InputHandler.CheckInput(new string[] {
                "Try a different username   ",
                "Create a new account       "
            });
            if(selection == 1)
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[] {
                "You have selected to try a different username.",
                "Press any key to return. "
                }, true);
                ConsoleHandler.ClearAfterKeyPress();
                SelectUser();
            }
            else
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[] {
                "You have selected to create a new account.",
                "Press any key to continue. "
                }, true);
                CreateNewAccount();
            }

        }

        public static void GetPassword(BankAccount accountAccessing)
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[] {
                $"Account located for username : {accountAccessing.Username}", "",
                "Please enter your password below:", "", ""
                }, 33);
            // can adjust this to hide password as it's being entered later on?
            var password = Console.ReadLine();
            InputHandler.AttemptLogin(accountAccessing, password);
        }


        public static void AccessAccount(BankAccount accountToAccess)
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[]
            {
                $"You are currently viewing the account : {accountToAccess.Username}", "",
                "What would you like to do today?", "",
                "1) Check your balance              ",
                "2) Deposit funds into your account ",
                "3) Withdraw funds from your account",
                "4) Exit account                    ",
                ""
            }, 36);
            int selection = InputHandler.CheckInput(new string[] {
                "Check your balance              ",
                "Deposit funds into your account ",
                "Withdraw funds from your account",
                "Exit account                    "
            });
            if (selection == 1)
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[] {
                    $"Your account balance is: {accountToAccess.GetBalance()}","",
                    "Press any key to return to account options."
                }, true);
                ConsoleHandler.ClearAfterKeyPress();
                AccessAccount(accountToAccess);
            }
            else if (selection == 2)
            {
                GetDepositAmount(accountToAccess);
            }
            else if (selection == 3)
            {
                accountToAccess.Withdraw();
            }
            else accountToAccess.CloseAccount();
        }


        public static void GetDepositAmount(BankAccount accountToAccess)
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[]
                {
                    "How much would you like to deposit today?", "",
                    "Amount To Deposit : "
                }, 41);
            string input = Console.ReadLine();
            double amountToDeposit = 0;
            if (double.TryParse(input, out amountToDeposit))
            {
                if(amountToDeposit > 0)
                {
                    accountToAccess.Deposit(amountToDeposit);
                }
                else
                {
                    ConsoleHandler.CenterMidScreenAndPrint(new string[]{
                    $"I'm sorry, {amountToDeposit} is not a valid amount.", "",
                    "Please enter a number greater than 0.", "",
                    "Press any key to return and enter a new amount."
                    }, true);
                    Console.ReadKey();
                    GetDepositAmount(accountToAccess);
                }
            }
            else
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[]
                {
                    $"I'm sorry, \"{input}\" is not a valid amount.", "",
                    "Please enter a number greater than 0.", "",
                    "Press any key to return and enter a new amount."
                    }, true);
                Console.ReadKey();
                GetDepositAmount(accountToAccess);
            }
        }

        public static void CreateNewAccount()
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[] {
                "Please enter the username you would like to use.","",
                "New Username : "
            }, 47);
            string newUsername = Console.ReadLine();
            // make sure the provided username doesn't already exist
            if (InputHandler.CheckForUserName(newUsername) != null)
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[]{
                    "The username you entered is already taken.", "",
                    "Press any key to return and try a different username."
                }, true);
                ConsoleHandler.ClearAfterKeyPress();
                CreateNewAccount();
            }
            // else we returned null so we can use the username entered
            else
            {
                ConfirmNewUsername(newUsername);
            }
        }

        static void ConfirmNewUsername(string usernameSelected)
        {
            string[] confirmUsernameString = new string[] {
                $"We will use the username \"{usernameSelected}\", is this correct?","",
                "Please enter Y/N : "
                };
            if (InputHandler.YesOrNoQuestion(confirmUsernameString))
            {
                CreateNewAccountPassword(usernameSelected);
            }
            // if no, then go back to entering username by calling CreateNewAccount
            else CreateNewAccount();
        }

        static void CreateNewAccountPassword(string newUsername)
        {
            ConsoleHandler.CenterMidScreenAndPrint(new string[] {

                "Please enter the password you would like to use.","",
                "Password : "
            }, 48);
            string password = Console.ReadLine();
            string[] confirmPasswordString = new string[]{
                $"We will use the password \"{password}\", is this correct?","",
                "Please enter Y/N : "
            };
            if (InputHandler.YesOrNoQuestion(confirmPasswordString))
            {
                AccountList.allAccounts.Add(new BankAccount(newUsername, password));
                ConsoleHandler.CenterMidScreenAndPrint(new string[]
                {
                    "We have created your account!", "",
                    $"You can now log in with the username : {newUsername}", "",
                    "Press any key to return to the login page."
                }, true);
                ConsoleHandler.ClearAfterKeyPress();
                SelectUser();
            }
            // if no, then go back to entering username by calling CreateNewAccount
            else CreateNewAccountPassword(newUsername);
        }

    }
}
