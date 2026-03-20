namespace SOLID_Fundamentals;

public interface IDiscountStrategy
{
    string CustomerType { get; }
    decimal CalculateDiscount(decimal orderAmount);
}

public class PercentageDiscountStrategy : IDiscountStrategy
{
    public string CustomerType { get; }
    private readonly decimal percentage;

    public PercentageDiscountStrategy(string customerType, decimal percentage)
    {
        CustomerType = customerType;
        this.percentage = percentage;
    }

    public decimal CalculateDiscount(decimal orderAmount)
    {
        return orderAmount * percentage;
    }
}

public interface IShippingCostStrategy
{
    string ShippingMethod { get; }
    decimal Calculate(decimal weight, string destination);
}

public class StandardShippingStrategy : IShippingCostStrategy
{
    public string ShippingMethod => "Standard";

    public decimal Calculate(decimal weight, string destination)
    {
        return 5.00m + (weight * 0.5m);
    }
}

public class ExpressShippingStrategy : IShippingCostStrategy
{
    public string ShippingMethod => "Express";

    public decimal Calculate(decimal weight, string destination)
    {
        return 15.00m + (weight * 1.0m);
    }
}

public class OvernightShippingStrategy : IShippingCostStrategy
{
    public string ShippingMethod => "Overnight";

    public decimal Calculate(decimal weight, string destination)
    {
        return 25.00m + (weight * 2.0m);
    }
}

public class InternationalShippingStrategy : IShippingCostStrategy
{
    public string ShippingMethod => "International";

    public decimal Calculate(decimal weight, string destination)
    {
        return destination switch
        {
            "USA" => 30.00m,
            "Europe" => 35.00m,
            "Asia" => 40.00m,
            _ => 50.00m
        };
    }
}

public class DiscountCalculator
{
    private readonly Dictionary<string, IDiscountStrategy> discounts;
    private readonly Dictionary<string, IShippingCostStrategy> shipping;

    public DiscountCalculator(IEnumerable<IDiscountStrategy> discountStrategies, IEnumerable<IShippingCostStrategy> shippingStrategies)
    {
        discounts = discountStrategies.ToDictionary(s => s.CustomerType, StringComparer.OrdinalIgnoreCase);
        shipping = shippingStrategies.ToDictionary(s => s.ShippingMethod, StringComparer.OrdinalIgnoreCase);
    }

    public decimal CalculateDiscount(string customerType, decimal orderAmount)
    {
        if (discounts.TryGetValue(customerType, out var strategy))
        {
            return strategy.CalculateDiscount(orderAmount);
        }

        return 0m;
    }

    public decimal CalculateShippingCost(string shippingMethod, decimal weight, string destination)
    {
        if (shipping.TryGetValue(shippingMethod, out var strategy))
        {
            return strategy.Calculate(weight, destination);
        }

        return 0m;
    }
}
