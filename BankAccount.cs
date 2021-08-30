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
        }


        public void Deposit(double amountToDeposit)
        {
            if(canAccess)
            {
                balance += amountToDeposit;
            }
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
