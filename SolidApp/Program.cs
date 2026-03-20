// See https://aka.ms/new-console-template for more information
using SOLID_Fundamentals;

Console.WriteLine("SOLID lab started");

// LSP demo
var bankService = new BankService();
var checking = new CheckingAccount();
var savings = new SavingsAccount();
checking.Deposit(1000m);
savings.Deposit(300m);
bankService.Transfer(checking, savings, 200m);
bankService.ProcessWithdrawal(savings, 150m);

// OCP demo
var discountCalculator = new DiscountCalculator(
	new IDiscountStrategy[]
	{
		new PercentageDiscountStrategy("Regular", 0.05m),
		new PercentageDiscountStrategy("Premium", 0.10m),
		new PercentageDiscountStrategy("VIP", 0.15m),
		new PercentageDiscountStrategy("Student", 0.08m),
		new PercentageDiscountStrategy("Senior", 0.07m)
	},
	new IShippingCostStrategy[]
	{
		new StandardShippingStrategy(),
		new ExpressShippingStrategy(),
		new OvernightShippingStrategy(),
		new InternationalShippingStrategy()
	});

Console.WriteLine($"VIP discount: {discountCalculator.CalculateDiscount("VIP", 1000m):C}");
Console.WriteLine($"International shipping: {discountCalculator.CalculateShippingCost("International", 2m, "Europe"):C}");

// DIP demo
IEmailService emailService = new EmailService();
ISmsService smsService = new SmsService();
var orderService = new OrderService(emailService, smsService);

var order = new Order
{
	Id = 1,
	TotalAmount = 1500m,
	PaymentMethod = "Card",
	Items = new List<string> { "Laptop", "Mouse" },
	CustomerEmail = "student@example.com",
	CustomerPhone = "+79990000000"
};

orderService.PlaceOrder(order);

// SRP demo
var processor = new OrderProcessor(
	new ConsolePaymentProcessor(),
	new ConsoleInventoryManager(),
	new ConsoleCustomerNotifier(),
	new ConsoleAuditLogger(),
	new ConsoleReceiptService(),
	new ConsoleOrderReportService());

processor.AddOrder(order);
processor.ProcessOrder(1);
processor.GenerateMonthlyReport();

// ISP demo
IOrderCrudOperations customerPortal = new CustomerPortal();
customerPortal.CreateOrder(order);

Console.WriteLine("SOLID lab finished");
