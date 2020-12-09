using System;
using System.Collections.Generic;

public class ATM
    {
        private string noUserLoggeInErrorMessage = "No user is currently logged in. Cannot compelte action";
        private int? currentUserIndex = null;
        List<Account> accounts;

        public ATM()
        {
            accounts = new List<Account>();
            bool ranApp = true;

            while (true)
            {
                Console.WriteLine("Welcome to the ATM. How can I help you today");

                Console.Write("Would you like to (1)login or (2)register: ");
                var userSigninPreference = Console.ReadLine();

                if (userSigninPreference == "1")
                {
                    Console.WriteLine("Please enter your User Name: ");
                    var username = Console.ReadLine();
                    Console.WriteLine("Please enter your Passwoard: ");
                    var password = Console.ReadLine();
                    LogIn(username, password);
                }
                else if (userSigninPreference == "2")
                {
                    Console.WriteLine("Welcome new user!");
                    Console.WriteLine("Please enter your desired User Name: ");
                    var username = Console.ReadLine();
                    Console.WriteLine("Please enter your Passwoard: ");
                    var password = Console.ReadLine();
                    Register(username, password);
                    continue;
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please Try Again");
                    continue;
                }

                while (currentUserIndex != null)
                {
                    Console.WriteLine($"Welcome {accounts[currentUserIndex.Value].Name }, What would you like to do today");
                    Console.WriteLine("1. Check Balance 2.Deposit 3.Withdraw 4.Logout ");
                    var userAction = Console.ReadLine();
                    if (userAction == "1")
                    {
                        CheckBalance();
                    }
                    else if (userAction == "2")
                    {
                        Console.WriteLine("What amount would you like to deposit?: ");
                        int depositAmount = 0;
                        int.TryParse(Console.ReadLine(), out depositAmount);
                        Deposit(depositAmount);
                    }
                    else if (userAction == "3")
                    {
                        Console.WriteLine("What amount would you like to withdraw?: ");
                        int withdrawAmount = 0;
                        int.TryParse(Console.ReadLine(), out withdrawAmount);
                        Withdraw(withdrawAmount);
                    }
                    else if (userAction == "4")
                    {
                        Logout();
                        Console.WriteLine("Thanks for using the ATM. Have a great day!");
                    }
                }

            }


        }

        private void Register(string name, string password)
        {
            Account existingAccount = accounts.Find(x => (x.Name == name && x.Password == password));
            if (existingAccount != null)
            {
                Console.WriteLine("User already exist. Please sign in.");
                return;
            }
            Console.WriteLine("User created. Please log in");
            accounts.Add(new Account { Name = name, Password = password, Balance = 0 });
        }

        private void LogIn(string userName, string password)
        {

            if (currentUserIndex != null)
            {
                Console.WriteLine("Cannot log in, due to another account already being signed in");
                return;
            }

            int existingAccount = accounts.FindIndex(x => (x.Name == userName && x.Password == password));
            //if (currentUserIndex <= accounts.Count && (accounts[currentUserIndex.Value].Name != userName || accounts[currentUserIndex.Value].Password != password))
            if (existingAccount == -1)
            {
                Console.WriteLine("Could not find that username/passwaord combo. Please try again or create an account");
                currentUserIndex = null;
                return;
            }

            currentUserIndex = existingAccount;
        }


        private void Logout()
        {
            if (currentUserIndex == null)
            {
                Console.WriteLine(noUserLoggeInErrorMessage);
                return;
            }

            currentUserIndex = null;
        }

        private void CheckBalance()
        {
            if (currentUserIndex == null)
            {
                Console.WriteLine(noUserLoggeInErrorMessage);
                return;
            }

            Console.WriteLine($"Current balance for {accounts[currentUserIndex.Value].Name }: {accounts[currentUserIndex.Value].Balance}");
        }

        private void Deposit(int depositAmount)
        {
            if (currentUserIndex == null)
            {
                Console.WriteLine(noUserLoggeInErrorMessage);
                return;
            }

            accounts[currentUserIndex.Value].Balance = depositAmount + accounts[currentUserIndex.Value].Balance;
            Console.WriteLine($"New balance for {accounts[currentUserIndex.Value].Name}: {accounts[currentUserIndex.Value].Balance}");
        }

        private void Withdraw(int withdrawAmount)
        {
            if (currentUserIndex == null)
            {
                Console.WriteLine(noUserLoggeInErrorMessage);
                return;
            }

            if (withdrawAmount > accounts[currentUserIndex.Value].Balance)
            {
                Console.WriteLine("Cannot complete action, withdraw amount is greater than current balance");
                return;
            }

            accounts[currentUserIndex.Value].Balance = accounts[currentUserIndex.Value].Balance - withdrawAmount;
            Console.WriteLine($"New balance for {accounts[currentUserIndex.Value].Name}: {accounts[currentUserIndex.Value].Balance}");
        }

    }
