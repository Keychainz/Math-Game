namespace MathGame
{
    public class MathLogic
    {
        public List<string> gameHistoryList { get; set; } = new List<string>();

        public void ShowMenu()
        {
            Console.WriteLine("Please select which game you would like to play:");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. Random");
            Console.WriteLine("6. Show History");
            Console.WriteLine("7. Change Difficulty");
            Console.WriteLine("8. Exit");
        }
    }
}