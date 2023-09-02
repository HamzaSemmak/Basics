package Banking;

public class SavingsAccount extends Account {

    private double interestRate;

    public double getInterestRate() {
        return interestRate;
    }

    public void setInterestRate(double interestRate) {
        this.interestRate = interestRate;
    }

    public SavingsAccount(int numberAccount, double balance, String ownerName, double interestRate)
    {
        super(numberAccount, (balance * interestRate) / 100, ownerName);
        this.interestRate = interestRate;
    }

    @Override
    public String toString()
    {
        return  super.toString() + ", interestRate of this account is " + this.interestRate;
    }

}
