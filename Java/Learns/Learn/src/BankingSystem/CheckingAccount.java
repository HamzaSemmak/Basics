package BankingSystem;

public class CheckingAccount extends Account {

    private double overdraftLimit;

    public double getOverdraftLimit() {
        return overdraftLimit;
    }

    public void setOverdraftLimit(double overdraftLimit) {
        this.overdraftLimit = overdraftLimit;
    }

    public CheckingAccount(int numberAccount, double balance, String ownerName, double overdraftLimit)
    {
        super(numberAccount, balance, ownerName);
        this.overdraftLimit = overdraftLimit;
    }

    @Override
    public void withdraw(double amount)
    {
        if(amount > this.overdraftLimit)
        {
            super.withdraw(amount);
        }
        else {
            throw new Error("Error : Please check the Amount");
        }
    }
}
