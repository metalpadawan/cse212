/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Create a queue with an invalid max size.
        // Expected Result: The max size should default to 10.
        Console.WriteLine("Test 1");
        var cs = new CustomerService(0);
        AssertTest(cs.ToString().Contains("max_size=10"), "Invalid max size should default to 10.");

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Add one customer to the queue.
        // Expected Result: The queue should contain that customer.
        Console.WriteLine("Test 2");
        cs = new CustomerService(2);
        AddCustomerForTest(cs, "Ada", "A123", "Cannot sign in");
        AssertTest(cs.ToString().Contains("Ada (A123)  : Cannot sign in"), "AddNewCustomer should add the customer to the queue.");

        // Defect(s) Found: None

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below

        // Test 3
        // Scenario: Add a customer when the queue is already full.
        // Expected Result: An error message should display and the customer should not be added.
        Console.WriteLine("Test 3");
        cs = new CustomerService(1);
        AddCustomerForTest(cs, "Ben", "B123", "Forgot password");
        var output = AddCustomerForTest(cs, "Cara", "C123", "Payment failed");
        AssertTest(output.Contains("Maximum Number of Customers in Queue."), "AddNewCustomer should display an error when the queue is full.");
        AssertTest(!cs.ToString().Contains("Cara"), "AddNewCustomer should not add a customer when the queue is full.");

        // Defect(s) Found: Queue full check allowed one extra customer.

        Console.WriteLine("=================");

        // Test 4
        // Scenario: Serve the next customer from a queue with multiple customers.
        // Expected Result: The first customer should display and be removed.
        Console.WriteLine("Test 4");
        cs = new CustomerService(3);
        AddCustomerForTest(cs, "Dina", "D123", "Reset MFA");
        AddCustomerForTest(cs, "Eli", "E123", "Update address");
        output = CaptureConsole(() => cs.ServeCustomer());
        AssertTest(output.Contains("Dina (D123)  : Reset MFA"), "ServeCustomer should display the first customer.");
        AssertTest(!cs.ToString().Contains("Dina") && cs.ToString().Contains("Eli"), "ServeCustomer should remove only the first customer.");

        // Defect(s) Found: ServeCustomer removed the first customer before displaying it.

        Console.WriteLine("=================");

        // Test 5
        // Scenario: Serve a customer from an empty queue.
        // Expected Result: An error message should display.
        Console.WriteLine("Test 5");
        cs = new CustomerService(3);
        output = CaptureConsole(() => cs.ServeCustomer());
        AssertTest(output.Contains("No Customers in Queue."), "ServeCustomer should display an error when the queue is empty.");

        // Defect(s) Found: ServeCustomer did not check for an empty queue.
    }

    private static void AssertTest(bool condition, string message) {
        if (!condition)
            throw new InvalidOperationException(message);
    }

    private static string AddCustomerForTest(CustomerService cs, string name, string accountId, string problem) {
        var input = string.Join(Environment.NewLine, name, accountId, problem) + Environment.NewLine;
        return CaptureConsole(() => cs.AddNewCustomer(), input);
    }

    private static string CaptureConsole(Action action, string input = "") {
        var originalIn = Console.In;
        var originalOut = Console.Out;

        using var reader = new StringReader(input);
        using var writer = new StringWriter();

        try {
            Console.SetIn(reader);
            Console.SetOut(writer);
            action();
            return writer.ToString();
        }
        finally {
            Console.SetIn(originalIn);
            Console.SetOut(originalOut);
        }
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        if (_queue.Count == 0) {
            Console.WriteLine("No Customers in Queue.");
            return;
        }

        var customer = _queue[0];
        _queue.RemoveAt(0);
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}
