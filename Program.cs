/*
1. You need to create a Math game containing the 4 basic operations

2. The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100. Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.

3. Users should be presented with a menu to choose an operation

4. You should record previous games in a List and there should be an option in the menu for the user to visualize a history of previous games.

5. You don't need to record results on a database. Once the program is closed the results will be deleted.
*/

using System.Diagnostics;
using MathGame;

MathLogic mathGame = new MathLogic();
Random random = new Random();

int firstNum;
int secondNum;
int score = 0;
int userMenuSelection;

bool gameOver = false;

DifficultyLevel difficultyLevel = DifficultyLevel.Easy;

while (!gameOver)
{
    userMenuSelection = GetUserMenuSelection(mathGame);

    firstNum = random.Next(1, 101);
    secondNum = random.Next(1, 101);

    switch (userMenuSelection)
    {
        case 1:
            score += await PerformOperation(mathGame, firstNum, secondNum, score, '+', difficultyLevel);
            break;
        case 2:
            score += await PerformOperation(mathGame, firstNum, secondNum, score, '-', difficultyLevel);
            break;
        case 3:
            score += await PerformOperation(mathGame, firstNum, secondNum, score, '*', difficultyLevel);
            break;
        case 4:
            while (firstNum % secondNum != 0)
            {
                firstNum = random.Next(1, 101);
                secondNum = random.Next(1, 101);
            }
            score += await PerformOperation(mathGame, firstNum, secondNum, score, '+', difficultyLevel);
            break;
        case 5:
            int numberOfQuestions = 99;
            Console.WriteLine("Please enter the number of questions you want to attempt");
            while (!int.TryParse(Console.ReadLine(), out numberOfQuestions))
            {
                Console.WriteLine("Please enter the number of questions you want to attempt as an integer");
            }

            while (numberOfQuestions > 0)
            {
                int randomOperation = random.Next(1, 5);

                if (randomOperation == 1)
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, '+', difficultyLevel);
                }
                else if (randomOperation == 2)
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, '-', difficultyLevel);
                }
                else if (randomOperation == 3)
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, '*', difficultyLevel);
                }
                else
                {
                    firstNum = random.Next(1, 101);
                    secondNum = random.Next(1, 101);

                    while (firstNum % secondNum != 0)
                    {
                        firstNum = random.Next(1, 101);
                        secondNum = random.Next(1, 101);
                    }
                    score += await PerformOperation(mathGame, firstNum, secondNum, score, '+', difficultyLevel);
                }
                numberOfQuestions--;
            }
            break;
        case 6:
            Console.WriteLine("GAME HISTORY: \n");
            foreach (var operation in mathGame.gameHistoryList)
            {
                Console.WriteLine($"{operation}");
            }
            break;
        case 7:
            difficultyLevel = changeDifficulty();
            DifficultyLevel difficultyEnum = (DifficultyLevel)difficultyLevel;
            Enum.IsDefined(typeof(DifficultyLevel), difficultyEnum);
            Console.WriteLine($"Your new dificulty level is {difficultyLevel}");
            break;
        case 8:
            gameOver = true;
            Console.WriteLine($"Your final score is: {score}");
            break;
    }
    ;
}


static DifficultyLevel changeDifficulty()
{
    int userSelection = 0;

    Console.WriteLine("Please enter a difficulty level");
    Console.WriteLine("1. Easy");
    Console.WriteLine("2. Medium");
    Console.WriteLine("3. Hard");

    while (!int.TryParse(Console.ReadLine(), out userSelection) || (userSelection < 0 || userSelection > 3))
    {
        Console.WriteLine("Please enter a valid value 1-3");
    }

    switch (userSelection)
    {
        case 1:
            return DifficultyLevel.Easy;
        case 2:
            return DifficultyLevel.Medium;
        case 3:
            return DifficultyLevel.Hard;
    }

    return DifficultyLevel.Easy;
}

static void DisplayMathGameQuestion(int firstNum, int secondNum, char operation)
{
    Console.WriteLine($"{firstNum} {operation} {secondNum} = ??");
}

static int GetUserMenuSelection(MathLogic mathGame)
{
    int selection = -1;
    mathGame.ShowMenu();
    while (selection < 1 || selection > 8)
    {
        while (!int.TryParse(Console.ReadLine(), out selection))
        {
            Console.WriteLine("Please enter a valid option 1-8");
        }
    }
    return selection;
}

static async Task<int?> GetUserResponse(DifficultyLevel difficulty)
{
    int response = 0;
    int timeout = (int)difficulty;

    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    Task<string?> GetUserInputTask = Task.Run(() => Console.ReadLine());

    try
    {
        string? result = await Task.WhenAny(GetUserInputTask, Task.Delay(timeout * 1000)) == GetUserInputTask ? GetUserInputTask.Result : null;

        stopwatch.Stop();

        if (result != null && int.TryParse(result, out response))
        {
            Console.WriteLine($"Time taken to answer: {stopwatch.Elapsed.ToString(@"m\::ss\.fff")}");
            return response;
        }
        else
        {
            throw new OperationCanceledException();
        }
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Time is up!");
        return null;
    }
}

static int ValidateResponse(int result, int? userResponse, int score)
{
    if (result == userResponse)
    {
        Console.WriteLine("You answered correctly; You earned 5 points");
        score += 5;
    }
    else
    {
        Console.WriteLine("Try again!");
        Console.WriteLine($"Correct answer is: {result}");
    }
    return score;
}

static async Task<int> PerformOperation(MathLogic mathGame, int firstNum, int secondNum, int score, char operation, DifficultyLevel difficulty)
{
    int result;
    int? userResponse = null;

    DisplayMathGameQuestion(firstNum, secondNum, operation);

    result = mathGame.MathOperations(firstNum, secondNum, operation);

    userResponse = await GetUserResponse(difficulty);

    score += ValidateResponse(result, userResponse, score);

    return score;
}
public enum DifficultyLevel
{
    Easy = 45,
    Medium = 30,
    Hard = 25,
}
