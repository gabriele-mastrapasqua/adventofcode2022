
static class Game
{
    /*
  Calculate the total score for each line summing the outcome + shape choosed.
  The score is referred to player2 (you)


  NOTE: player1 is the opponent
  NOTE: player2 is you

  */
    public static int CalculateGameScore(ScoresPerShape player1, ScoresPerShape player2)
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


}
