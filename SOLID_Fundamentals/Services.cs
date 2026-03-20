namespace SOLID_Fundamentals;

public interface IEmailService
{
    void SendEmail(string to, string subject, string body);
}

public interface ISmsService
{
    void SendSms(string phoneNumber, string message);
}

public class EmailService : IEmailService
{
    public void SendEmail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to}: {subject}");
    }
}

public class SmsService : ISmsService
{
    public void SendSms(string phoneNumber, string message)
    {
        Console.WriteLine($"Sending SMS to {phoneNumber}: {message}");
    }
}

public class OrderService
{
    private readonly IEmailService emailService;
    private readonly ISmsService smsService;

    public OrderService(IEmailService emailService, ISmsService smsService)
    {
        this.emailService = emailService;
        this.smsService = smsService;
    }

    public void PlaceOrder(Order order)
    {
        emailService.SendEmail(order.CustomerEmail, "Order Confirmation", "Your order has been placed");
        smsService.SendSms(order.CustomerPhone, "Your order has been placed");
    }
}

public class NotificationService
{
    private readonly IEmailService emailService;

    public NotificationService(IEmailService emailService)
    {
        this.emailService = emailService;
    }

    public void SendPromotion(string email, string promotion)
    {
        emailService.SendEmail(email, "Special Promotion", promotion);
    }
}
