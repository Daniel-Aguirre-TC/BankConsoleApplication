using System;
using System.Collections.Generic;
using System.Text;

namespace BankEncapsulation
{
    public class BankAccount
    {
        public string Username { get; set; }
        private string password;
        private bool canAccess;

        public int LoginAttempts { get; private set; } = 0;
        private double balance = 0;

        public BankAccount(string username, string password, double startingBalance)
        {
            Username = username;
            this.password = password;
            balance = startingBalance;
        }

        public BankAccount(string username, string password)
        {
            Username = username;
            this.password = password;
        }

        public bool OpenAccount(string accountPassword)
        {
                if (accountPassword == password)
                {
                    canAccess = true;
                    return true;
                }
                LoginAttempts++;
                return false;                    
        }

        public void CloseAccount()
        {
            canAccess = false;
            ConsoleHandler.CenterMidScreenAndPrint(new string[]
            {
                "You have been logged out.", "",
                "Press any key to return to login page."
            },true);
            ConsoleHandler.ClearAfterKeyPress();
            RequestHandler.SelectUser();
        }

        public void Withdraw()
        {
            if(canAccess)
            {
                ConsoleHandler.CenterMidScreenAndPrint(new string[]
                {
                    $"Your account balance is currently: {balance}.", "",
                    "Please enter how much you would like to Withdraw : "
                }, true);
                double withdrawAmount = 0;
                string input = Console.ReadLine();
                if(double.TryParse(input, out withdrawAmount))
                {
                    if (withdrawAmount == 0)
                    {
                        ConsoleHandler.CenterMidScreenAndPrint(new string[]{
                            $"Amount Requested was {withdrawAmount}.", "",
                            "No change was made to your balance.", "",
                            $"Your balance is still : {balance}.", "",
                            "Press any key to return to account options."
                        }, true);
                        ConsoleHandler.ClearAfterKeyPress();
                        RequestHandler.AccessAccount(this);

                    }
                    if(withdrawAmount > 0 && withdrawAmount < balance)
                    {
                        balance -= withdrawAmount;
                        ConsoleHandler.CenterMidScreenAndPrint(new string[]{
                            $"You have withdrawn {withdrawAmount} from your account.", "",
                            $"Your new balance is : {balance}.", "",
                            "Press any key to return to account options."
                        }, true);
                        ConsoleHandler.ClearAfterKeyPress();
                        RequestHandler.AccessAccount(this);
                    }
                    else if( withdrawAmount < 0)
                    {

                        ConsoleHandler.CenterMidScreenAndPrint(new string[]{
                            $"I'm sorry, you cannot enter a negative number.", "",
                            "Please enter a number greater than zero.", "",
                            "Press any key to return and enter a new amount."
                        }, true);
                        ConsoleHandler.ClearAfterKeyPress();
                        Withdraw();
                    }
                    else if (withdrawAmount > balance)
                    {
                        ConsoleHandler.CenterMidScreenAndPrint(new string[]{
                            $"I'm sorry, you do not have enough funds.", "",
                            "You must enter a number less than your current balance.", "",
                            "Press any key to return and enter a new amount."
                        }, true);
                        ConsoleHandler.ClearAfterKeyPress();
                        Withdraw();
                    }

                }
                else
                {
                    ConsoleHandler.CenterMidScreenAndPrint(new string[]
                    {
                        $"I'm sorry, {input} is not a valid amount.", "",
                        "Please enter an amount greater than zero and less than your current balance.", "",
                        "Press any key to return."
                    }, true);
                    ConsoleHandler.ClearAfterKeyPress();
                    Withdraw();                   
                }           
            }
        }

        public void Deposit(double amountToDeposit)
        {
            if(canAccess)
            {
                balance += amountToDeposit;
            }
            ConsoleHandler.CenterMidScreenAndPrint(new string[]
            {
                $"You have deposited {amountToDeposit} into your account.", "",
                $"Your new balance is : {balance}", "",
                "Press any key to return to account options."
            }, true);
            ConsoleHandler.ClearAfterKeyPress();
            RequestHandler.AccessAccount(this);
        }

        public double GetBalance()
        {
            if (canAccess)
            {
                return balance;
            }
            else return double.NaN;
        }


    }
}
