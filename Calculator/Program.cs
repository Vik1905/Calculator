namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"This is one line calculator\n" + "Enter full math expresion using this allowed operation: \"+\", \"-\", \"*\", \"/\" \"(\", \")\" and unary minus\n");
            var calculator = new Calculator();
            string input = Console.ReadLine();
            input = input.Replace(" ", "");
            Console.WriteLine($"Answer: {calculator.Calculate(calculator.SplitByOperation(input))}");
        }
    }
}