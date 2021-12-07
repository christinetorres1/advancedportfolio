using System;
using System.Collections.Generic;
using System.Text;

namespace AdvPortfolio2_ChristineTorres
{
    class Transactions
    {
        private int _accountID;
        private char _type;
        private double _amount;
        private double _endingBalance;
        public DateTime _transactionDate;

        //constructor
        public Transactions ()
        {
            _accountID = 0;
            _type = 'D';
            _amount = 0;
            _transactionDate = DateTime.Now;
        }
        //overloaded constructor 
        public Transactions (int accountID, char type, double amount)
        {
            _accountID = accountID;
            _type = type;
            _amount = amount;
            _transactionDate = DateTime.Now;
        }
        //Accessors 
        public int GetAccountID()
        {
            return _accountID;
        }
        public char GetType()
        {
            return _type;
        }
        public double GetAmount ()
        {
            return _amount;
        }
        public double GetEndingBalance ()
        {
            return _endingBalance;
        }
        public DateTime GetDateTime ()
        {
            return _transactionDate;
        }
        //Mutators 
        public void SetAccountID(int accountID)
        {
            if (accountID > 0)
            {
                _accountID = accountID;
            }
            else
            {
                throw new Exception("ERROR: AccountID must be above 0.");
            }
        }
        public void SetType(char type)
        {

            //check for empty string value
            if ( type == null)
            {
                throw new Exception("Error: Name is empty or null");
            }
            else
            {
                _type = type;
            }
        }
        public void SetAmount(double amount)
        {
            if (amount > 0)
            {
                _amount = amount;
            }
            else
            {
                throw new Exception("ERROR: Amount must be above 0.");
            }
        }
        public void SetDateTime (DateTime date)
        {
            _transactionDate = date;
        }
        public void SetEndingBalance(double endingBalance)
        {
            if (endingBalance > 0)
            {
                _endingBalance = endingBalance;
            }
            else
            {
                throw new Exception("ERROR: Ending Balance must be above 0.");
            }
        }
    }
}
