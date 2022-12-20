


// read input file
//var text = Utils.ReadFile("input.test.txt");
var text = Utils.ReadFile("input.txt");


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

    var gameScore = Game.CalculateGameScore(movementPlayer1, movementPlayer2);
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

    var gameScore = Game.CalculateGameScore((ScoresPerShape)movementPlayer1, finalMovementPlayer2);
    Console.WriteLine($"player1  {player1Str}={movementPlayer1}, player2 {player2Str}={movementPlayer2}; score {gameScore}");

    score += gameScore;

});

Console.WriteLine($"Total games score: {score}");
