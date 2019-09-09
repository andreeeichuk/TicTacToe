using System;
using System.Collections.Generic;
using System.Linq;

public static class AI
{
    private static int[] turnsToConsider = new int[3] { 1, 4, 9};
    
    private static int currentDifficulty;

    public static Coordinates GetBestMove(int difficulty, BoardGrid grid, int aiSign)
    {
        currentDifficulty = difficulty;

        int opponentSign = aiSign == 1 ? 2 : 1;

        Coordinates coordinates;

        List<Move> moves = new List<Move>();

        for (int i = 0; i < grid.cells.GetLength(0); i++)
        {
            for (int j = 0; j < grid.cells.GetLength(1); j++)
            {
                if(grid.cells[i,j]==0)
                {
                    grid.cells[i, j] = aiSign;

                    int moveVal = MiniMax(grid, 0, opponentSign);

                    grid.cells[i, j] = 0;

                    moves.Add(new Move(new Coordinates(i,j),moveVal));
                }
            }
        }        

        if (aiSign == 1)
        {
            moves = moves.OrderByDescending(m => m.score).ToList();
        }
        else
        {
            moves = moves.OrderBy(m => m.score).ToList();
        }       

        List<Move> sameMoves = moves.Where(m => m.score == moves[0].score).ToList();
               
        coordinates = sameMoves[UnityEngine.Random.Range(0,sameMoves.Count)].coordinates;        

        return coordinates;
    }

    private static int MiniMax(BoardGrid grid, int depth, int playerSign)
    {
        int score = Evaluate(grid);

        if (score == -10)
        {
            return score + depth;
        }
        
        if(score == 10)
        {
            return score - depth;
        }

        if(score == 0 || depth >= turnsToConsider[currentDifficulty])
        {
            return 0;
        }

        if(score==-1)
        {
            score = 0;
        }

        if(playerSign==1)
        {
            int best = -1000;

            for (int i = 0; i < grid.cells.GetLength(0); i++)
            {
                for (int j = 0; j < grid.cells.GetLength(1); j++)
                {
                    if(grid.cells[i,j]==0)
                    {
                        grid.cells[i, j] = playerSign;

                        best = Math.Max(best,MiniMax(grid, depth + 1, 2));

                        grid.cells[i, j] = 0;
                    }
                }
            }

            return best;
        }
        else if(playerSign==2)
        {
            int best = 1000;

            for (int i = 0; i < grid.cells.GetLength(0); i++)
            {
                for (int j = 0; j < grid.cells.GetLength(1); j++)
                {
                    if (grid.cells[i, j] == 0)
                    {
                        grid.cells[i, j] = playerSign;

                        best = Math.Min(best, MiniMax(grid, depth + 1, 1));

                        grid.cells[i, j] = 0;
                    }
                }
            }

            return best;
        }

        return score;
    }

    private static int Evaluate(BoardGrid grid)
    {
        int result = grid.CheckWinner();

        switch(result)
        {
            case 1:
                return 10;
            case 2:
                return -10;
            case 0:
                return 0;
            default:
                return -1;
        }
    }
}
