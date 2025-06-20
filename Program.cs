/*
1. You need to create a Math game containing the 4 basic operations

2. The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100. Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.

3. Users should be presented with a menu to choose an operation

4. You should record previous games in a List and there should be an option in the menu for the user to visualize a history of previous games.

5. You don't need to record results on a database. Once the program is closed the results will be deleted.
*/

using MathGame;

MathLogic mathGame = new MathLogic();
Random random = new Random();

int firstNum;
int secondNum;
int score = 0;
int userMenuSelection;

bool gameOver = false;



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

public enum DifficultyLevel
{
    Easy = 45,
    Medium = 30,
    Hard = 25,
}
