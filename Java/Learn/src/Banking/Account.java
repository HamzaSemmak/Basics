package Banking;

public class Account {
    private int numberAccount;
    private double balance;
    private String ownerName;

    public int getNumberAccount()
    {
        return numberAccount;
    }

    public void setNumberAccount(int numberAccount)
    {
        this.numberAccount = numberAccount;
    }

    public double getBalance()
    {
        return balance;
    }

    public void setBalance(double balance)
    {
        this.balance = balance;
    }

    public String getOwnerName() {
        return ownerName;
    }
    public void setOwnerName(String ownerName) {
        this.ownerName = ownerName;
    }

    public Account(int numberAccount, double balance, String ownerName)
    {
        this.numberAccount = numberAccount;
        this.balance = balance;
        this.ownerName = ownerName;
    }

    public void deposit(double amount)
    {
        this.balance += amount;
    }

    public void withdraw(double amount)
    {
        this.balance -= amount;
    }

    public double getAccountBalance()
    {
        return this.balance;
    }

    public String toString()
    {
        return  "N°Account : " + this.numberAccount + ", Name : " + this.ownerName + ", Balance : " + this.balance;
    }
}
