using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LB3_BankLogin
{
    public partial class frmWeylandBank : Form
    {
        public frmWeylandBank()
        {
            InitializeComponent();
        }
        //  Constant
        const int NUMCUSTOMERS = 6;      //  Assume Bank has 5 total customers

        //  Global variables
        List<Account> accounts = new List<Account>();

        int validAcctNumLoc = -1;
        int validPinNumLoc = -1;
        bool youreLoggedIn = false;

        //  Set up the bank with 5 initial customer accounts
        private void FormWeylandBank_Load(object sender, EventArgs e)
        {
            accounts.Add(new Account("Ms.", "Sandy", "Scott", "123-456-789", "3456", 10000M));
            accounts.Add(new Account("Mr.", "Evan", "Gudmestad", "234-567-890", "8754", 2500.00M));
            accounts.Add(new Account("Mr.", "Paul", "Smith", "345-678-901", "6543", 13456.78M));
            accounts.Add(new Account("Ms.", "Shannon", "Brueggeman", "456-789-012", "2049", 5678.90M));
            accounts.Add(new Account("Mr.", "Charles", "Corrigan", "567-890-123", "4190", 100456.21M));
            accounts.Add(new Account("Mr.", "Jeff",    "Scott",    "111-111-111", "1111", 200.00M));
            btnDeposit.Enabled      = false;
            btnWithdrawal.Enabled   = false;
            btnLogout.Enabled       = false;
        }


        //*************************************************
        //
        //  This method is called when the user
        //  clicks the Login Application button
        //  to try to login to the prototype.
        //
        //*************************************************
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        //  Attempt to login
        private void Login()
        {
            //  Create LOCAL variables
            bool missingInfo = true;        //  Program continue variable 1
            bool badAcctNumber = true;      //  Program continue variable 2 
            bool badPinNumber = true;       //  Program continue variable 3

            missingInfo = isAnyLoginInforMissing();

            if (missingInfo)
            {
                return;
            }

            //  If I get to this point, the Account Number field AND
            //  the Pin Number Field have values in them. Now, I must
            //  validate that the Account Number and Pin Number are
            //  actually in my customer "database".
            badAcctNumber = validateAccountNumber(txtAccountNumber.Text);

            badPinNumber = validatePinNumber(txtPinNumber.Text);

            //  Check for a bad account number and/or a bad pin number
            //  i.e., not in our "mini-database" of customer records.
            if ((badAcctNumber) || (badPinNumber))
            {
                displayBadAccountNumberOrPinNumberMessage();
                return;
            }

            //  User logged in with a valid account number AND
            //  a valid pin number. Make sure they both come
            //  from the same customer.
            if (validAcctNumLoc != validPinNumLoc)
            {
                //  Both account number and pin number are valid
                //  but they don't collectively match an existing
                //  customer
                displayBadAccountNumberOrPinNumberMessage();
                return;
            }

            //  User logged in with a valid account number AND
            //  a valid pin number AND the account number  AND
            //  pin number match an existing customer
            rewriteWelcomeScreen();
            makeScreenActive();
        }

        //*************************************************
        //
        //  This method is called when the user
        //  clicks the Login Application button
        //  to check for nothing entered into
        //  either the Account # or the Pin #.
        //
        //*************************************************
        public bool isAnyLoginInforMissing()
        {
            txtWelcomeInfo.Text = "";

            string outputStr = "";
            outputStr = "Account Number and Pin Number Are BOTH Mandatory For Login!";
            outputStr += "\r\n\r\nThe account number is in format nnn-nnn-nnn, where n is a number";
            outputStr += "\r\n\r\nAnd the pin number is in format nnnn, again where n is a number";
            outputStr += "\r\n\r\nPlease Try Again And Enter Both Values";

            if ((txtAccountNumber.Text.Trim() == "") ||
                (txtPinNumber.Text.Trim() == ""))
            {
                txtWelcomeInfo.Text = "";
                txtWelcomeInfo.Text = outputStr;
                return true;
            }

            return false;
        }

        public void makeScreenActive()
        {
            //  User has successfully logged in
            youreLoggedIn           = true;
            btnLogin.Enabled        = false;
            btnLogout.Enabled       = true;
            btnDeposit.Enabled      = true;
            btnWithdrawal.Enabled   = true;
        }

        //*************************************************
        //
        //  This method is called to check for 
        //  an inputted Account # that does NOT
        //  exist in the Account # array.
        //
        //*************************************************
        private bool validateAccountNumber(string an)
        {
            validAcctNumLoc = -1;

            //  Iterate through all account numbers and
            //  see whether or not there is a match.
            for (int lcv = 0; lcv < accounts.Count; ++lcv)
            {
                if (an == accounts[lcv].GetAccountNumber())
                {
                    validAcctNumLoc = lcv;
                    return false;
                }
            }

            return true;
        }

        //*************************************************
        //
        //  This method is called to check for 
        //  an inputted Pin # that does NOT
        //  exist in the Pin # array.
        //
        //*************************************************
        private bool validatePinNumber(string pn)
        {
            validPinNumLoc = -1;

            //  Iterate through all account numbers and
            //  see whether or not there is a match.
            for (int lcv = 0; lcv < accounts.Count; ++lcv)
            {
                if (pn == accounts[lcv].GetPin())
                {
                    validPinNumLoc = lcv;
                    return false;
                }
            }

            return true;
        }

        //*************************************************
        //
        //  This method is called if either the Account #
        //  inputted was not in the associated Account #
        //  array or the Pin # inputted was not in the
        //  Pin # array.
        //
        //*************************************************
        private void displayBadAccountNumberOrPinNumberMessage()
        {
            string outputStr = "";
            outputStr += "Bad Account Number And/Or Bad Pin Number Inputted!";
            outputStr += "\r\n\r\nPlease re-enter your info below";

            txtWelcomeInfo.Text = outputStr;

            clearLoginFields();

            return;
        }

        //*************************************************
        //
        //  This method is called to clear out both the
        //  Account # textbox and the Pin # textbox.
        //
        //*************************************************
        private void clearLoginFields()
        {
            txtAccountNumber.Text = "";
            txtPinNumber.Text     = "";
            txtAccountNumber.Focus();
        }

        //*************************************************
        //
        //  If we get to this place, the user entered
        //  both a valid Account Number AND a valid
        //  Pin Number.
        //
        //  So, show a welcome message, along with the
        //  valid user's current balance.
        //
        //*************************************************
        private void rewriteWelcomeScreen()
        {
            youreLoggedIn = true;

            string outputStr = "Welcome ";
            outputStr += accounts[validPinNumLoc].GetTitle() + "  ";
            outputStr += accounts[validPinNumLoc].GetFirstName() + "  ";
            outputStr += accounts[validPinNumLoc].GetLastName();
            outputStr += "\r\n\r\n";
            outputStr += "Your Current Account Balance: ";
            outputStr += accounts[validPinNumLoc].GetBalance().ToString("c");

            txtWelcomeInfo.Text = outputStr;
        }

        //*************************************************
        //
        //  This method will allow the person running
        //  the program to attempt to deposit into
        //  their account.  It is called when the
        //  Deposit button is clicked.
        //
        //*************************************************
        private void buttonDeposit_Click(object sender, EventArgs e)
        {
            bool keepGoing = loggedInOrNot();

            if (!keepGoing)
            {
                return;
            }

            //  User was logged in.
            //  Allow them to attempt to make their deposit
            attemptTheDeposit();
        }

        //*************************************************
        //
        //  This method actually attempts the deposit
        //  into the person's bank account.  It is 
        //  called by the Deposit button.
        //
        //*************************************************
        private void attemptTheDeposit()
        {
            decimal deposit;

            try
            {
                deposit = Convert.ToDecimal(txtDeposit.Text);
                accounts[validPinNumLoc].MakeDeposit(deposit);
                rewriteWelcomeScreen();
                txtDeposit.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDeposit.Text = "";
                return;
            }
        }

        //*************************************************
        //
        //  This method will allow the person running
        //  the program to attempt to withdraw from
        //  their account.  It is called when the
        //  Withdrawal button is clicked.
        //
        //*************************************************
        private void buttonWithdrawal_Click(object sender, EventArgs e)
        {
            bool keepGoing = loggedInOrNot();

            if (!keepGoing)
            {
                return;
            }

            attemptTheWithdrawal();
        }

        //*************************************************
        //
        //  This method actually attempts the with-
        //  drawal from the person's bank account.
        //  It is called by the Withdrawal button.
        //
        //*************************************************
        private void attemptTheWithdrawal()
        {
            decimal withdrawal; ;

            try
            {
                withdrawal = Convert.ToDecimal(txtWithdrawal.Text);
                accounts[validPinNumLoc].MakeWithdrawal(withdrawal);
                txtWithdrawal.Text = "";
                rewriteWelcomeScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtWithdrawal.Text = "";
                return;
            }
        }

        //*************************************************
        //
        //  If user is not logged in, s/he cannot
        //  perform a deposit or a withdrawal transaction.
        //
        //*************************************************
        private bool loggedInOrNot()
        {
            if (!youreLoggedIn)
            {
                txtWelcomeInfo.Text = "You Are NOT Currently Logged In " +
                                      "So You CANNOT Make A Transaction!";
                return false;
            }

           return true;
        }

        //*************************************************
        //
        //  This method is called when the user
        //  clicks the Logout Application button
        //  to possibly logout of the prototype.
        //
        //*************************************************
        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Logout();
        }

        //*************************************************
        //
        //  This method will allow the person running
        //  the program to end the transaction run if
        //  desired.  It is called by either the
        //  Exit button or (later by the) Application
        //  menu item.
        //
        //*************************************************
        private void Logout()
        {
            if (MessageBox.Show("Do You Really Want To Logout Now???",
                                "LOGOUT NOW?!?!?",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                blankOutAllThingsOnScreen();

                //  Log out current user
                youreLoggedIn           = false;
                btnLogin.Enabled        = true;
                btnLogout.Enabled       = false;
                btnDeposit.Enabled      = false;
                btnWithdrawal.Enabled   = false;
            }
        }

        //*************************************************
        //
        //  This method will "blank out" the screen.
        //
        //*************************************************
        private void blankOutAllThingsOnScreen()
        {
            txtWelcomeInfo.Text     = "";
            txtDeposit.Text         = "";
            txtWithdrawal.Text      = "";
            txtAccountNumber.Text   = "";
            txtPinNumber.Text       = "";
            txtAccountNumber.Focus();
        }
    }
}
