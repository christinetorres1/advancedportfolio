using System;
using System.Collections.Generic;
using System.Text;

namespace AdvPortfolio2_ChristineTorres
{
    class BankAccount
    {
        //members
        private int _accountID;
        private double _balance;
        private double _annualInterestRate;
        private DateTime _dateCreated;

        //constructor
        public BankAccount()
        {
            _accountID = 0;
            _balance = 0;
            _annualInterestRate = 0;
            _dateCreated = DateTime.Now;
        }

        //overloaded constructor
        public BankAccount(int accountID, double initialBalance, double annualInterestRate)
        {
            _accountID = accountID;
            _balance = initialBalance;
            _annualInterestRate = annualInterestRate;
            _dateCreated = DateTime.Now;
        }

        //Accessors
        public int GetAccountID()
        {
            return _accountID;
        }
        public double GetBalance()
        {
            return _balance;
        }
        public double GetAnnualInterestRate()
        {
            return _annualInterestRate;
            
        }
        public DateTime GetDateCreated()
        {
            return _dateCreated;
            
        }
        //Mutators
        public void SetAccountID(int accountID)
        {
            if (accountID >= 0)
            {
                _accountID = accountID;
            }
            else
            {
                throw new Exception("ERROR: AccountID must be above 0.");
            }
        }
        public void SetBalance(double initialBalance)
        {
            if (initialBalance >= 0)
            {
                _balance = initialBalance;
            }
            else
            {
                throw new Exception("ERROR: Balance must be above 0.");
            }
        }
        public void SetAnnualInterestRate(double annualInterestRate)
        {
            if (annualInterestRate >= 0)
            {
                _annualInterestRate = annualInterestRate;
            }
            else
            {
                throw new Exception("ERROR: Annual interest rate must be above 0.");
            }
        }
        
        public void SetDateCreated()
        {
            _dateCreated = DateTime.Now;
        }

        //METHODS
        //Monthly Interest Rate
        public double CalculateMonthlyInterestRate()
        {
            return _annualInterestRate / 12;
        }

        //Monthly Interest
        public double CalculateMonthlyInterest()
        {
            return (_balance * CalculateMonthlyInterestRate())/100;
        }
        //Withdraw
        public double Withdraw(double withdraw)
        {
            if (withdraw < _balance)
            {
                return _balance = _balance - withdraw;
            }
            else
            {
                throw new Exception("ERROR: The withdraw amount is greater than the remaining balance.");
            }
        }
        //Deposit
        public double Deposit(double deposit)
        {
            return _balance = _balance + deposit;

        }
    }
}
