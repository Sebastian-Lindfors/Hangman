namespace Hangman.Tests;


[TestFixture]
public class Tests
{
    private Game _game;

    [SetUp]
    public void Setup()
    {
        _game = new Game();
    }


    [Test]
    public void IsValidCharacter_IsLetter()
    {
        var result = _game.IsValidCharacter('a');

        Assert.That(result, Is.True, "The input must be a letter!");
    }

    [Test]
    public void IsGuessedLetter_LetterNotGuessed()
    {
        char input = 'd';
        List<char> list = ['a', 'b', 'c'];

        var result = _game.IsGuessedLetter(input, list);
        Assert.That(result, Is.False, "The input should be false!");

    }

    [TestCase('a')]
    [TestCase('b')]
    [TestCase('c')]
    public void IsGuessedLetter_LetterIsGuessed(char a)
    {
        List<char> list = ['a', 'b', 'c'];
        var result = _game.IsGuessedLetter(a, list);
        Assert.IsTrue(result, "input should be true!");
    }

    [TestCase("photosynthesis")]
    public void MaskedWord_CheckStringLength(string a)
    {
        List<char> guessedLetters = [];
        var result = _game.GetMaskedWord(a, guessedLetters);

        Assert.That(result.Length, Is.EqualTo(a.Length));
    }
}