package BankingSystem;

import java.util.ArrayList;

public class Bank {
    private ArrayList<Account> accounts;

    protected double bankBalance;

    public ArrayList<Account> getAccounts() {
        return accounts;
    }

    public void setAccounts(ArrayList<Account> accounts) {
        this.accounts = accounts;
    }

    public Bank()
    {

    }

    public void newAccount(Account account)
    {
        this.accounts.add(account);
    }

    public void bankBalance()
    {
        for (Account account : this.accounts) {
            System.out.println("Account Number: " + account.getNumberAccount());
            account.toString();
            this.bankBalance += account.getBalance();
        }

        System.out.println("The balance of this bank : " + this.bankBalance);
    }
}
