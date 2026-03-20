namespace SOLID_Fundamentals;

public interface IAccount
{
    decimal Balance { get; }
    void Deposit(decimal amount);
    decimal CalculateInterest();
}

public interface IWithdrawableAccount : IAccount
{
    void Withdraw(decimal amount);
}

public abstract class AccountBase : IAccount
{
    public decimal Balance { get; protected set; }

    public virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit amount must be positive");
        }

        Balance += amount;
    }

    public virtual decimal CalculateInterest()
    {
        return Balance * 0.01m;
    }
}

public class SavingsAccount : AccountBase, IWithdrawableAccount
{
    public decimal MinimumBalance { get; } = 100m;

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
        }

        if (Balance - amount < MinimumBalance)
        {
            throw new InvalidOperationException("Cannot go below minimum balance");
        }

        Balance -= amount;
    }
}

public class CheckingAccount : AccountBase, IWithdrawableAccount
{
    public decimal OverdraftLimit { get; } = 500m;

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal amount must be positive");
        }

        if (Balance - amount < -OverdraftLimit)
        {
            throw new InvalidOperationException("Overdraft limit exceeded");
        }

        Balance -= amount;
    }
}

public class FixedDepositAccount : AccountBase
{
    public DateTime MaturityDate { get; }

    public FixedDepositAccount(DateTime maturityDate)
    {
        MaturityDate = maturityDate;
    }

    public void Redeem(decimal amount)
    {
        if (DateTime.Now < MaturityDate)
        {
            throw new InvalidOperationException("Cannot withdraw before maturity date");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds");
        }

        Balance -= amount;
    }

    public override decimal CalculateInterest()
    {
        return Balance * 0.05m;
    }
}

public class BankService
{
    public void ProcessWithdrawal(IWithdrawableAccount account, decimal amount)
    {
        try
        {
            account.Withdraw(amount);
            Console.WriteLine($"Successfully withdrew {amount}");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Withdrawal failed: {ex.Message}");
        }
    }

    public void Transfer(IWithdrawableAccount from, IAccount to, decimal amount)
    {
        from.Withdraw(amount);
        to.Deposit(amount);
    }
}
