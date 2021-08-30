using System;

namespace BankEncapsulation
{
    class Program
    {

        public enum InputRequest { Deposit, Withdraw }
        public InputRequest request;

        static void Main(string[] args)
        {
            RequestHandler.OpenApplication();
            RequestHandler.SelectUser();
        }




    }
}
