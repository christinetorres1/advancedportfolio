/***********************************************************************/
// Name: Christine Joy Torres
// Date: 2021/04/16
// Purpose: Make a bank account
/**********************************************************************/

using System;
using System.Collections.Generic;
using System.IO;

namespace AdvPortfolio2_ChristineTorres
{
    class Program
    {
        const string FILE = @"C:\Users\ChristineJoy\source\repos\AdvPortfolio2_ChristineTorres\AdvPortfolio2_ChristineTorres\";
        const int INDEX = 5;

        static void Main(string[] args)
         {
            //variables
            int option = 0;
            double deposit;
            double withdraw = 0;
            char type = 'D';
            double amount=0;
            int accountID=0;
            double endingBalance = 0;
            
            //make a bank account object 
            BankAccount newBankAccount = new BankAccount();

            //make a list
            List<Transactions> newTransactionsList = new List<Transactions>();
            
            do
            {
                Transactions newT = new Transactions(accountID, type, amount);
                //Bank account main menu and user input for options
                BankAccountMenu();
                option = PromptForIntergerValue("Option: ");

                switch (option)
                {
                    //Load Account & Transactions from File
                    case 1:
                        ReadToAFile(newT, newTransactionsList, newBankAccount);
                        break;

                    //Create Bank Account
                    case 2:
                        newBankAccount = CreateAccount();
                        accountID = newBankAccount.GetAccountID();
                        //Setting accountID to the list
                        newT.SetAccountID(accountID);
                        break;

                    //Display Account Information
                    case 3:
                        DisplayBankAccountInfo(newBankAccount);
                        break;

                    //Withdraw Funds
                    case 4:
                        withdraw = PromptForDoubleValue("Enter withdrawal amount: ");
                        newBankAccount.Withdraw(withdraw);

                        //Setting type, amount withdrawn and ending to the list
                        type = 'W';
                        newT.SetType(type);

                        newT.SetAmount(withdraw);
                        
                        endingBalance = newBankAccount.GetBalance();
                        newT.SetEndingBalance(endingBalance);
                        break;

                    //Deposit Funds
                    case 5:
                        deposit = PromptForDoubleValue("Enter deposit amount: ");
                        newBankAccount.Deposit(deposit);

                        //Setting type, amount deposited and ending to the list
                        type = 'D';
                        newT.SetType(type);

                        newT.SetAmount(deposit);
                        
                        endingBalance = newBankAccount.GetBalance();
                        newT.SetEndingBalance(endingBalance);
                        break;

                    //Add Interest
                    case 6:
                        
                        Console.WriteLine("Added ${0:0.00} in interest", newBankAccount.CalculateMonthlyInterest());
                        
                        //Setting type, amount added interest and ending to the list
                        type = 'I';
                        newT.SetType(type);

                        amount = newBankAccount.CalculateMonthlyInterest();
                        newT.SetAmount(amount);
                        
                        endingBalance = newBankAccount.GetBalance() + newBankAccount.CalculateMonthlyInterest();
                        newBankAccount.SetBalance(endingBalance);

                        newT.SetEndingBalance(endingBalance);

                        break;

                    //Display Transactions
                    case 7:
                        //title of transactions
                        Console.WriteLine("{0,-15}{1,-15}{2,-15}{3,-15}{4,-15}", "Account", "Type", "Amount", "Date","Ending Balance");

                        //displaying transaction lists
                        for (int index = 0; index < newTransactionsList.Count; index++)
                        {
                            Console.WriteLine("{0,-15}{1,-15}${2,-15:0.00}{3,-15:yyyy-MM-dd}${4,-15:0.00}", newTransactionsList[index].GetAccountID(), newTransactionsList[index].GetType(), newTransactionsList[index].GetAmount(), newTransactionsList[index].GetDateTime(), newTransactionsList[index].GetEndingBalance());
                        }
                        break;

                    //Save Account & Transactions to File and Exit Program
                    case 8:
                        Console.WriteLine("Saving File... Goodbye");
                        WriteTheBankAccountToAFile(newBankAccount);
                        WriteTheTransactionsToAFile(newTransactionsList, newT);
                        break;

                    default:
                        Console.WriteLine("Sorry invalid option.");
                        break;
                }
                //adding to the list if it's withdraw, deposit or add interest 
                if (option == 4 || option == 5 || option == 6)
                {
                    newTransactionsList.Add(newT);
                }

            } while (option != 8);

            Console.ReadLine();
            Console.Clear();
        }
        
        //Read to a file
        static void ReadToAFile(Transactions newT, List<Transactions> newTransactionsList, BankAccount newBankAccount)
        {
            int accountID = 0;
            int counter = 0;
            int add = 0;
            int[] accountID1 = new int[INDEX];
            double[] interest = new double[INDEX];
            char[] type = new char[INDEX];
            double[] amount = new double[INDEX];
            DateTime[] date = new DateTime[INDEX];
            double[] endAmount = new double[INDEX];
           
            Console.WriteLine("Enter Account ID: ");
            accountID = int.Parse(Console.ReadLine());

            try
            {
                using (StreamReader file = new StreamReader(FILE + accountID + "_transaction.csv"))
                {
                    string ln;
                    while ((ln = file.ReadLine()) != null)
                    {
                        string[] parts = ln.Split(',');

                        accountID1[counter] = int.Parse(parts[0]);
                        type[counter] = char.Parse(parts[1]);
                        amount[counter] = double.Parse(parts[2]);
                        date[counter] = DateTime.Parse(parts[3]);
                        endAmount[counter] = double.Parse(parts[4]);

                        newT.SetAccountID(accountID1[counter]);
                        newT.SetType(type[counter]);
                        newT.SetAmount(amount[counter]);
                        newT.SetDateTime(date[counter]);
                        newT.SetEndingBalance(endAmount[counter]);
   
                        counter++;
                        if(newTransactionsList.Count > counter)
                        {
                            newTransactionsList.Add(newT);
                        }
                        
                    }
                    file.Close();                
                }
                    
                Console.WriteLine("{0} transactions read from file", counter);

                using (StreamReader file = new StreamReader(FILE + accountID + ".csv"))
                {
                      string ln;
                      while ((ln = file.ReadLine()) != null)
                      {
                             string[] parts = ln.Split(',');

                             accountID1[add] = int.Parse(parts[0]);
                             date[add] = DateTime.Parse(parts[1]);
                             endAmount[add] = double.Parse(parts[2]);
                             interest[add] = double.Parse(parts[3]);

                             newBankAccount.SetAccountID(accountID1[add]);
                             newBankAccount.SetAnnualInterestRate(interest[add]);
                             newBankAccount.SetBalance(endAmount[add]);

                             add++;
                      }
                      file.Close();
                }
            }
            catch
            {
                Console.WriteLine("The file " + accountID + "_transaction.csv" + " Does not exist \n0 transactions read from file\n");
            }
        }

        //Save to a file the new bank account
        static void WriteTheBankAccountToAFile(BankAccount newBankAccount)
        {

            StreamWriter writer = new StreamWriter(FILE + newBankAccount.GetAccountID() + ".csv");

            writer.WriteLine("{0},{1:yyyy-MM-dd},{2:0.00},{3:0.00}", newBankAccount.GetAccountID(), newBankAccount.GetDateCreated(), newBankAccount.GetBalance(), newBankAccount.GetAnnualInterestRate());

            writer.Close();
        }
        //Save to file the bank Transactions
        static void WriteTheTransactionsToAFile(List<Transactions> newTransactionsList, Transactions newT)
        {
            StreamWriter writer = new StreamWriter(FILE + newT.GetAccountID() + "_transaction.csv");
            for (int index = 0; index < newTransactionsList.Count; index++)
            {
                writer.WriteLine("{0},{1},{2:0.00},{3:yyyy-MM-dd},{4:0.00}", newTransactionsList[index].GetAccountID(), newTransactionsList[index].GetType(), newTransactionsList[index].GetAmount(), newTransactionsList[index].GetDateTime(), newTransactionsList[index].GetEndingBalance());
            }

            writer.Close();
        }
        //Display Bank Account info
        static void DisplayBankAccountInfo(BankAccount newBankAccount)
        {
                Console.WriteLine("______________________________");
                Console.WriteLine("      ACCOUNT INFORMATION      ");
                Console.WriteLine("______________________________\n");
                Console.WriteLine(" Account ID: {0}\n Created: {1:yyyy-MM-dd}\n Balance: ${2:0.00}\n Annual Interest Rate: {3:0.00}%", newBankAccount.GetAccountID(), newBankAccount.GetDateCreated(), newBankAccount.GetBalance(), newBankAccount.GetAnnualInterestRate());
                Console.WriteLine("______________________________\n");
        }
        //create a bank account
        static BankAccount CreateAccount()
        {
            //variables
            int accountID = 0;
            double initialBalance = 0;
            double annualInterestRate = 0;
            int ans = 0;

            //make a new bankaccount object
            BankAccount newBankAccount = new BankAccount();
            
            do
            {
                CreateBankAccountMenu();
                ans = PromptForIntergerValue("Option: ");
                   switch (ans)
                   {
                        //Enter account ID
                        case 1:
                            accountID = PromptForIntergerValue("Enter account ID: ");
                            newBankAccount.SetAccountID(accountID);
                            break;

                        //Initial account balance
                        case 2:
                            initialBalance = PromptForDoubleValue("Enter initial account balance: ");
                            newBankAccount.SetBalance(initialBalance);
                            break;

                        //Set annual interest rate
                        case 3:
                            annualInterestRate = PromptForDoubleValue("Enter annual interest rate: ");
                            newBankAccount.SetAnnualInterestRate(annualInterestRate);
                            break;

                        //exit
                        case 4:
                            Console.WriteLine("");
                            break;

                        default:
                            Console.WriteLine("Sorry invalid option.");
                            break;
                   } 

            } while (ans != 4);
            
            return newBankAccount;
        }
        //Main bank account menu
        static void BankAccountMenu()
        {
            Console.WriteLine("Bank Account Menu: ");
            Console.WriteLine("\t1. Load Account & Transactions from File");
            Console.WriteLine("\t2. Create Bank Account");
            Console.WriteLine("\t3. Display Account Information");
            Console.WriteLine("\t4. Withdraw Funds");
            Console.WriteLine("\t5. Deposit Funds");
            Console.WriteLine("\t6. Add Interest");
            Console.WriteLine("\t7. Display Transactions");
            Console.WriteLine("\t8. Save Account & Transactions to File and Exit Program");
        }
        //Create a bank account menu
        static void CreateBankAccountMenu()
        {
            Console.WriteLine("______________________________");
            Console.WriteLine("Create a bank account menu: ");
            Console.WriteLine("\t1. Enter Account ID");
            Console.WriteLine("\t2. Enter Initial Balance");
            Console.WriteLine("\t3. Set Annual Interest Rate");
            Console.WriteLine("\t4. Exit Account Creation");
        }
        //Get a safe int
        static int PromptForIntergerValue(string prompt)
        {
            int number = 0;
            bool isValid = false;

            do
            {
                try
                {
                    Console.Write(prompt);
                    number = int.Parse(Console.ReadLine());
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValid = false;
                }
            }
            while (isValid == false);

            return number;
        }
        //Get a safe double
        static double PromptForDoubleValue(string prompt)
        {
            double number = 0;
            bool isValid = false;

            do
            {
                try
                {
                    Console.Write(prompt);
                    number = double.Parse(Console.ReadLine());
                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    isValid = false;
                }
            }
            while (isValid == false);

            return number;
        }
    }
}
