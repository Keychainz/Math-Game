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

        public int MathOperations(int firstNum, int secondNum, char operation)
        {
            switch (operation)
            {
                case '+':
                    gameHistoryList.Add($"{firstNum} + {secondNum} = {firstNum + secondNum}");
                    return firstNum + secondNum;
                case "-":
                    gameHistoryList.Add($"{firstNum} - {secondNum} = {firstNum - secondNum}");
                    return firstNum - secondNum;
                case "*":
                    gameHistoryList.Add($"{firstNum} * {secondNum} = {firstNum * secondNum}");
                    return firstNum * secondNum;
                case "/":
                    while (firstNum < 0 || firstNum > 100)
                    {
                        try
                        {
                            Console.WriteLine("Please enter a number between 0 and 100");
                            firstNum = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (System.Execption)
                        {
                            // Do something here
                        }
                    }
                    gameHistoryList.Add($"{firstNum} / {secondNum} = {firstNum / secondNum}");
                    return firstNum / secondNum;
            }
        }
    }
}