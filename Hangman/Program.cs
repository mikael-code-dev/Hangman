using System.Text;

Random randNum = new Random();
string[] words = new string[]
{
    "Reinforcement Learning",
    "state space equation",
    "cern researcher",
    "Gene-environment interaction",
    "polar inertia",
    "discrete fourier transform",
    "quantum mechanics",
    "Neil deGrasse Tyson",
    "Kas:st",
    "amplitude",
    "eigenvalue"
};

var numberOfGuessesLeft = 10;
var isAlive = true;

Start();
RunProgram();
End();

void RunProgram()
{
    var theWord = ReadWord();
    //var currentCorrect = Enumerable.Repeat('_', theWord.Length).ToArray();
    char[] currentCorrect = new char[theWord.Length];
    var guessedLetters = new StringBuilder();
    var rightGuess = false;
    
    for (int i = 0; i < theWord.Length; i++)
    {
        if (theWord[i] == '-')
        {
            currentCorrect[i] = '-';
        }
        else if (theWord[i] == ':')
        {
            currentCorrect[i] = ':';
        }
        else if (theWord[i] == ' ')
        {
            currentCorrect[i] = ' ';
        }
        else
        {
            currentCorrect[i] = '_';
        }
    }

    while (isAlive)
    {
        if(numberOfGuessesLeft <= 0)
        {
            Console.Clear();
            Console.WriteLine("\n\n");
            Console.Write("    ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(" You DIE! ");
            Console.ResetColor();
            break;
        }

        Console.WriteLine("\n\n Guess a letter or guess the whole word");
        Console.Write(" :> ");
        string? theGuess = Console.ReadLine();

        if (!string.IsNullOrEmpty(theGuess) && !isInGuessedList(theGuess, guessedLetters))
        {
            if (theGuess.Length > 1)
            {
                if (checkIfSuccess(theWord, theGuess.ToCharArray()))
                {
                    printWinUI();
                    break;
                }
            }
            else
            {
                guessedLetters.Append(theGuess);

                foreach (var (letter, index) in theWord.Select((letter, index) => (letter, index)))
                {
                    if (letter.ToString().ToLower() == theGuess)
                    {
                        currentCorrect[index] = theWord[index];
                        rightGuess = true; 
                    }
                }

                if (!rightGuess)
                {
                    numberOfGuessesLeft--;
                }

                rightGuess = false;
            }
        }

        printUI(theWord, currentCorrect, numberOfGuessesLeft, guessedLetters, theGuess);

        if (checkIfSuccess(theWord, currentCorrect))
        {
            printWinUI();
            isAlive = false;
        }
    }
}

void printWinUI()
{
    Thread.Sleep(2000);
    Console.Clear();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\n\n\n You guessed the right word. You win!");
    Console.ResetColor();
}

bool checkIfSuccess(string theWord, char[] currentCorrect)
{
    return theWord == new string(currentCorrect);
}

void printUI(string theWord, char[] currentCorrect, int numberOfGuessesLeft, StringBuilder guessedLetters, string? theGuess)
{
    Console.Clear();

    Console.Write($" You have");
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write($" {numberOfGuessesLeft} ");
    Console.ResetColor();
    Console.Write("number of guesses left before you die!");

    Console.WriteLine($"\n You guessed: {theGuess}");
    Console.WriteLine("\n");

    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($" Letters you have guessed: {guessedLetters}");
    Console.ResetColor();

    Console.WriteLine("\n");

    foreach (var letter in currentCorrect)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($" {letter}");
        Console.ResetColor();
        
    }
}

bool isInGuessedList(string theGuess, StringBuilder guessedLetters)
{
    foreach (var guessedLetter in guessedLetters.ToString())
    {
        if (guessedLetter.ToString() == theGuess)
        {
            return true;
        }
    }

    return false;
}

string ReadWord()
{
    return words[randNum.Next(0, words.Length)];
}

void End()
{
    Console.WriteLine("\n\n Thanks for playing the game!");
    Console.WriteLine("\n\n\n\n");
}

void Start()
{
    Console.WriteLine(" ********   HANGMAN   ********");
    Console.WriteLine("\n Gues the right word/sentence");
    Console.WriteLine(" You have 10 tries before you're hanged!");
    
    for (int i = 0; i > 0; i--)
    {
        Thread.Sleep(500);
        Console.WriteLine($"  {i}");
        Console.SetCursorPosition(0, Console.CursorTop - 1);
    }

    Console.Clear();
}