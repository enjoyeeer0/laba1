namespace SOLID_Fundamentals;

public interface IOrderPaymentProcessor
{
    void Process(string paymentMethod, decimal amount);
}

public interface IInventoryManager
{
    void UpdateInventory(List<string> items);
}

public interface ICustomerNotifier
{
    void SendEmail(string to, string message);
}

public interface IAuditLogger
{
    void Log(string message);
}

public interface IReceiptService
{
    void Generate(Order order);
}

public interface IOrderReportService
{
    void PrintMonthlyReport(int totalOrders, decimal totalRevenue);
}

public class OrderProcessor
{
    private readonly List<Order> orders = new();
    private readonly IOrderPaymentProcessor paymentProcessor;
    private readonly IInventoryManager inventoryManager;
    private readonly ICustomerNotifier customerNotifier;
    private readonly IAuditLogger auditLogger;
    private readonly IReceiptService receiptService;
    private readonly IOrderReportService reportService;

    public OrderProcessor(
        IOrderPaymentProcessor paymentProcessor,
        IInventoryManager inventoryManager,
        ICustomerNotifier customerNotifier,
        IAuditLogger auditLogger,
        IReceiptService receiptService,
        IOrderReportService reportService)
    {
        this.paymentProcessor = paymentProcessor;
        this.inventoryManager = inventoryManager;
        this.customerNotifier = customerNotifier;
        this.auditLogger = auditLogger;
        this.receiptService = receiptService;
        this.reportService = reportService;
    }

    public void AddOrder(Order order)
    {
        orders.Add(order);
        Console.WriteLine($"Order {order.Id} added");
    }

    public void ProcessOrder(int orderId)
    {
        var order = orders.FirstOrDefault(o => o.Id == orderId);
        if (order is null)
        {
            Console.WriteLine($"Order {orderId} not found");
            return;
        }

        if (order.TotalAmount <= 0)
        {
            throw new InvalidOperationException("Invalid order amount");
        }

        paymentProcessor.Process(order.PaymentMethod, order.TotalAmount);
        inventoryManager.UpdateInventory(order.Items);
        customerNotifier.SendEmail(order.CustomerEmail, $"Order {orderId} processed");
        auditLogger.Log($"Order {orderId} processed at {DateTime.Now}");
        receiptService.Generate(order);
    }

    public void GenerateMonthlyReport()
    {
        decimal totalRevenue = orders.Sum(o => o.TotalAmount);
        int totalOrders = orders.Count;
        reportService.PrintMonthlyReport(totalOrders, totalRevenue);
    }
}

public class ConsolePaymentProcessor : IOrderPaymentProcessor
{
    public void Process(string paymentMethod, decimal amount)
    {
        Console.WriteLine($"Payment processed via {paymentMethod}: {amount:C}");
    }
}

public class ConsoleInventoryManager : IInventoryManager
{
    public void UpdateInventory(List<string> items)
    {
        Console.WriteLine($"Inventory updated for {items.Count} items");
    }
}

public class ConsoleCustomerNotifier : ICustomerNotifier
{
    public void SendEmail(string to, string message)
    {
        Console.WriteLine($"Email to {to}: {message}");
    }
}

public class ConsoleAuditLogger : IAuditLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"AUDIT: {message}");
    }
}

public class ConsoleReceiptService : IReceiptService
{
    public void Generate(Order order)
    {
        Console.WriteLine($"Receipt generated for order {order.Id}");
    }
}

public class ConsoleOrderReportService : IOrderReportService
{
    public void PrintMonthlyReport(int totalOrders, decimal totalRevenue)
    {
        Console.WriteLine($"Monthly Report: {totalOrders} orders, Revenue: {totalRevenue:C}");
    }
}
