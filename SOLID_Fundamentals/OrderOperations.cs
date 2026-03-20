namespace SOLID_Fundamentals;

public interface IOrderCrudOperations
{
    void CreateOrder(Order order);
    void UpdateOrder(Order order);
    void DeleteOrder(int orderId);
}

public interface IPaymentOperations
{
    void ProcessPayment(Order order);
}

public interface IShippingOperations
{
    void ShipOrder(Order order);
}

public interface IInvoiceOperations
{
    void GenerateInvoice(Order order);
}

public interface INotificationOperations
{
    void SendNotification(Order order);
}

public interface IReportingOperations
{
    void GenerateReport(DateTime from, DateTime to);
}

public interface IExcelExportOperations
{
    void ExportToExcel(string filePath);
}

public interface IDatabaseMaintenanceOperations
{
    void BackupDatabase();
    void RestoreDatabase();
}

public class OrderManager : IOrderCrudOperations, IPaymentOperations, IShippingOperations, IInvoiceOperations,
    INotificationOperations, IReportingOperations, IExcelExportOperations, IDatabaseMaintenanceOperations
{
    public void CreateOrder(Order order)
    {
        Console.WriteLine("Order created");
    }

    public void UpdateOrder(Order order)
    {
        Console.WriteLine("Order updated");
    }

    public void DeleteOrder(int orderId)
    {
        Console.WriteLine("Order deleted");
    }

    public void ProcessPayment(Order order)
    {
        Console.WriteLine("Payment processed");
    }

    public void ShipOrder(Order order)
    {
        Console.WriteLine("Order shipped");
    }

    public void GenerateInvoice(Order order)
    {
        Console.WriteLine("Invoice generated");
    }

    public void SendNotification(Order order)
    {
        Console.WriteLine("Notification sent");
    }

    public void GenerateReport(DateTime from, DateTime to)
    {
        Console.WriteLine("Report generated");
    }

    public void ExportToExcel(string filePath)
    {
        Console.WriteLine("Exported to Excel");
    }

    public void BackupDatabase()
    {
        Console.WriteLine("Database backed up");
    }

    public void RestoreDatabase()
    {
        Console.WriteLine("Database restored");
    }
}

public class CustomerPortal : IOrderCrudOperations
{
    public void CreateOrder(Order order)
    {
        Console.WriteLine("Order created by customer");
    }

    public void UpdateOrder(Order order)
    {
        Console.WriteLine("Order updated by customer");
    }

    public void DeleteOrder(int orderId)
    {
        Console.WriteLine("Order deleted by customer");
    }
}
