
string ReadFile(string input = "input.txt")
{

    var enviroment = Environment.CurrentDirectory;
    string? projectDirectory = Directory.GetParent(enviroment)?.Parent?.FullName;
    string text = null;
    try
    {
        text = File.ReadAllText($"{projectDirectory}/../{input}");
    }
    catch (FileNotFoundException ex)
    {
        // when running from cli dotnet run
        text = File.ReadAllText($"./{input}");
    }
    return text;
}



/*
 Calculate the total score for each line summing the outcome + shape choosed.
 The score is referred to player2 (you)

  
 NOTE: player1 is the opponent
 NOTE: player2 is you

 */
int CalculateGameScore(ScoresPerShape player1, ScoresPerShape player2)
{

    var rulesForWinning = new Dictionary<ScoresPerShape, ScoresPerShape>
    {
        [ScoresPerShape.Rock] = ScoresPerShape.Scissors,
        [ScoresPerShape.Scissors] = ScoresPerShape.Paper,
        [ScoresPerShape.Paper] = ScoresPerShape.Rock,

    };

    var outcome = 0;
    var scorePerShape = 0;

    // check draw
    if (player1.Equals(player2))
    {
        outcome = (int)ScoresPerOutcome.Draw;
        scorePerShape = (int)player2;
    }

    // check win
    else if (rulesForWinning[player2] == player1)
    {
        outcome = (int)ScoresPerOutcome.Win;
        scorePerShape = (int)player2;
    }
    else
    {
        outcome = (int)ScoresPerOutcome.Lose;
        scorePerShape = (int)player2;
    }


    return outcome + scorePerShape;
}



// read input file
//var text = ReadFile("input.test.txt");
var text = ReadFile("input.txt");


// get all games line by lines
var score = 0;
var games = new List<string>(text.Split("\n"));
Console.WriteLine($"lines in file: {games.Count}");


// PART 1 
games.ForEach(game =>
{
    var players = game.Split(" ");
    var player1Str = players[0];
    var player2Str = players[1];

    var movementsEncodingPart1 = new Dictionary<string, ScoresPerShape>
    {
        ["A"] = ScoresPerShape.Rock,
        ["B"] = ScoresPerShape.Paper,
        ["C"] = ScoresPerShape.Scissors,

        ["X"] = ScoresPerShape.Rock,
        ["Y"] = ScoresPerShape.Paper,
        ["Z"] = ScoresPerShape.Scissors,
    };

    var movementPlayer1 = movementsEncodingPart1[player1Str];
    var movementPlayer2 = movementsEncodingPart1[player2Str];
    //Console.WriteLine($"player1  {player1Str}={movementPlayer1}, player2 {player2Str}={movementPlayer2}");

    var gameScore = CalculateGameScore(movementPlayer1, movementPlayer2);
    Console.WriteLine($"player1  {player1Str}={movementPlayer1}, player2 {player2Str}={movementPlayer2}; score {gameScore}");

    score += gameScore;

});

Console.WriteLine($"Total games score: {score}");


// PART 2: the columns change meaning
score = 0;
games.ForEach(game =>
{
    var players = game.Split(" ");
    var player1Str = players[0];
    var player2Str = players[1];

    // in PART 2 the 2 column changes the action taken by our player
    var movementsEncodingPart2 = new Dictionary<string, object>
    {
        ["A"] = ScoresPerShape.Rock,
        ["B"] = ScoresPerShape.Paper,
        ["C"] = ScoresPerShape.Scissors,

        ["X"] = ScoresPerOutcome.Lose,
        ["Y"] = ScoresPerOutcome.Draw,
        ["Z"] = ScoresPerOutcome.Win,
    };


    var movementPlayer1 = movementsEncodingPart2[player1Str];
    var movementPlayer2 = (ScoresPerOutcome)movementsEncodingPart2[player2Str];
    var finalMovementPlayer2 = ScoresPerShape.Rock;

    var rulesForLosing = new Dictionary<ScoresPerShape, ScoresPerShape>
    {
        [ScoresPerShape.Rock] = ScoresPerShape.Scissors,
        [ScoresPerShape.Scissors] = ScoresPerShape.Paper,
        [ScoresPerShape.Paper] = ScoresPerShape.Rock,

    };
    var rulesForWinningPart2 = new Dictionary<ScoresPerShape, ScoresPerShape>
    {
        [ScoresPerShape.Rock] = ScoresPerShape.Paper,
        [ScoresPerShape.Scissors] = ScoresPerShape.Rock,
        [ScoresPerShape.Paper] = ScoresPerShape.Scissors,

    };

    if (movementPlayer2 == ScoresPerOutcome.Draw)
    {
        finalMovementPlayer2 = (ScoresPerShape)movementPlayer1;
    }
    else if (movementPlayer2 == ScoresPerOutcome.Win)
    {
        finalMovementPlayer2 = rulesForWinningPart2[(ScoresPerShape)movementPlayer1];
    }
    else
    {
        finalMovementPlayer2 = rulesForLosing[(ScoresPerShape)movementPlayer1];
    }

    var gameScore = CalculateGameScore((ScoresPerShape)movementPlayer1, finalMovementPlayer2);
    Console.WriteLine($"player1  {player1Str}={movementPlayer1}, player2 {player2Str}={movementPlayer2}; score {gameScore}");

    score += gameScore;

});

Console.WriteLine($"Total games score: {score}");
