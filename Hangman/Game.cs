/// <summary>
/// Main Game Class.
/// </summary>
public class Game
{
    List<char> guessedLetters = [];
    string word;
    int lives;

    // Main game loop. Method calls and game logic goes here.
    public void GameLoop()
    {
        Initialize();

        while (true)
        {
            if (HasWon(word, guessedLetters))
            {
                if (ContinueOnGameOver(lives)) continue;
                else break;
            }

            Console.WriteLine(GetMaskedWord(word, guessedLetters));
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"Current lives: {lives}");

            Console.Write("Please enter a letter: ");
            char input = Console.ReadKey().KeyChar;


            if (IsValidCharacter(input) == false)
            {
                WarningMessage("\nInvalid character.");
                continue;
            }

            if (IsGuessedLetter(input, guessedLetters))
            {
                WarningMessage("\nThis letter has already been guessed");
                lives--;
                continue;
            }
            else
            {
                guessedLetters.Add(input);
            }

            if (CheckIfCorrect(input, word))
            {
                SuccessMessage("\nYou guessed right!");
                continue;
            }
            else
            {
                FailMessage("\nYou guessed wrong!");
            }

            lives -= 2;

            if (lives <= 0)
            {
                if (ContinueOnGameOver(lives)) continue;
                else break;
            }
        }
    }

    // Asks if the player wants to continue after a game over.
    public bool ContinueOnGameOver(int lives)
    {
        if (lives <= 0)
        {
            FailMessage("\nYou lose 💀");
            FailMessage($"The word was {word}");
            WarningMessage("\nDo you wish to restart? y/n");
        }
        else
        {
            SuccessMessage("\nCongratulations, you won! 🎉");
            WarningMessage("\nDo you wish to restart? y/n");
        }


        while (true)
        {
            char input = Console.ReadKey().KeyChar;

            if (input == 'y')
            {


                Initialize();
                return true;
            }

            else if (input == 'n')
            {
                return false;
            }
        }

    }

    // Initializes values for the game upon startup and restart.
    void Initialize()
    {
        lives = 10;
        guessedLetters.Clear();
        word = FetchRandomWord();
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n🔥 Welcome to Hangman! 🔥");
    }

    // Checks if the input is a letter.
    public static bool IsValidCharacter(char input)
    {
        return char.IsLetter(input);
    }

    // Checks if the player has won the game.
    static bool HasWon(string word, List<char> guessedLetters)
    {
        return word.All(guessedLetters.Contains);
    }

    // Checks if the players guess is correct.
    static bool CheckIfCorrect(char input, string word)
    {
        foreach (char a in word.ToCharArray())
        {
            if (input == a)
            {
                return true;
            }
        }

        return false;
    }

    // Prints a masked version of the word.
    public static string GetMaskedWord(string word, List<char> guessedLetters)
    {
        char[] maskedWord = new char[word.Length];
        string returnableWord = string.Empty;

        for (int i = 0; i < maskedWord.Length; i++)
        {
            maskedWord[i] = '_';
        }

        for (int i = 0; i < maskedWord.Length; i++)
        {
            for (int j = 0; j < guessedLetters.Count; j++)
            {
                if (word[i] == guessedLetters[j])
                {
                    maskedWord[i] = word[i];
                }
            }
        }

        foreach (char a in maskedWord)
        {
            returnableWord += $"{a} ";
        }
        Console.Write("\n");
        return returnableWord;
    }

    // Checks if a letter has already been guessed.
    public static bool IsGuessedLetter(char input, List<char> guessedLetters)
    {
        foreach (char a in guessedLetters)
        {
            if (input == a)
            {
                return true;
            }
        }

        return false;
    }

    // Outputs a yellow line of text.
    static void WarningMessage(string text)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    // Outputs a green line of text.
    static void SuccessMessage(string text)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        // ANSI escape code for orange-ish color.
        // Console.Write("\u001b[38;5;214m");

        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    // Outputs a red line of text.
    static void FailMessage(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(text);
        Console.ForegroundColor = ConsoleColor.White;
    }

    // Fetches a random word from an API.
    public static string FetchRandomWord()
    {
        string url = "https://random-word-api.herokuapp.com/word";
        string word;

        using (HttpClient client = new HttpClient())
        {
            word = client.GetStringAsync(url).Result;
        }

        word = word.Remove(0, 2);
        word = word.Remove(word.Length - 2, 2);

        return word;
    }
}