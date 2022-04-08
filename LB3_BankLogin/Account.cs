using System;
using System.Windows.Forms;

namespace LB3_BankLogin
{
    class Account
    {
        //  Class constant
        public const decimal MINIMUMBALANCE = 25.00m;

        //  Instance variables
        private readonly string _title;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _accountNumber;
        private readonly string _pin;
        private decimal _balance;

        //  Full-Arg Constructor
        public Account(string  title,
                        string firstName,
                        string lastName,
                        string accountNumber,
                        string pin,
                        decimal balance)
        {
            _title          = title;
            _firstName      = firstName;
            _lastName       = lastName;
            _accountNumber  = accountNumber;
            _pin            = pin;
            _balance        = balance;
        }

        //  Getters
        public string GetTitle()
        {
            return _title;
        }

        public string GetFirstName()
        {
            return _firstName;
        }

        public string GetLastName()
        {
            return _lastName;
        }

        public string GetAccountNumber()
        {
            return _accountNumber;
        }

        public string GetPin()
        {
            return _pin;
        }

        public decimal GetBalance()
        {
            return _balance;
        }

        //  MakeDeposit(decimal amount) Method
        public void MakeDeposit(decimal amount)
        {
            decimal theDeposit = -1M;

            try
            {
                //  Attempt to make a deposit
                theDeposit = amount;

                //  The deposit was numeric.
                //  Verify deposit was NOT negative
                if (theDeposit <= 0)
                {
                    MessageBox.Show("Illegal deposit attempt (0 or negative deposit)",
                                "ILLEGAL DEPOSIT",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                    return;
                }

                //  A postive amount was entered to deposit.
                //  So, deposit it.
                _balance += amount;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                                "An Error Occurred. Please Try Again Later",
                                "INTERNAL ERROR OCCURRED",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        //  MakeWithdrawal(decimal amount) Method
        public void MakeWithdrawal(decimal amount)
        {
            decimal temp;

            try
            {
                //  Check for 0 or negative withdrawal amount
                if (amount <= 0)
                {
                    MessageBox.Show("Illegal withdrawal attempt (0 or negative withdrawal)",
                                    "ILLEGAL WITHDRAWAL",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }

                //  A postive amount was entered to withdrawal.
                //  So, withdraw it into our temp variable.
                temp = _balance - amount;

                //  Have to assure that the attempted
                //  withdrawal leave the balance at or
                //  above the minimum balance of $25.00.
                if (temp >= MINIMUMBALANCE)
                {   //  OK to do the withdrawal
                    _balance -= amount;
                }
                else
                {   //  Illegal withdrawal attempt
                    MessageBox.Show("Illegal withdrawal attempt.\n" +
                                    "The attempted withdrawal\n" +
                                    "would leave the account\n" +
                                    "with less than the mimimum balance",
                                    "ILLEGAL WITHDRAWAL",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    return;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n\n" +
                                "An Error Occurred. Please Try Again Later",
                                "INTERNAL ERROR OCCURRED",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }
    }
}
