using System;
using UnityEngine;

public static class AI
{
    public static Coordinates GetBestMove(Difficulty difficulty, BoardGrid grid, int aiSign)
    {
        int bestVal = -1000;
        int opponentSign = 2;

        if (aiSign==2)
        {
            bestVal = 1000;
            opponentSign = 1;
        }  

        Coordinates coordinates = new Coordinates(-1,-1);

        for (int i = 0; i < grid.cells.GetLength(0); i++)
        {
            for (int j = 0; j < grid.cells.GetLength(1); j++)
            {
                if(grid.cells[i,j]==0)
                {
                    grid.cells[i, j] = aiSign;

                    int moveVal = MiniMax(grid, 0, opponentSign);

                    grid.cells[i, j] = 0;

                    if (aiSign==1 && moveVal > bestVal)
                    {
                        coordinates.x = i;
                        coordinates.y = j;
                        bestVal = moveVal;
                    }

                    if (aiSign == 2 && moveVal < bestVal)
                    {
                        coordinates.x = i;
                        coordinates.y = j;
                        bestVal = moveVal;
                    }
                }
            }
        }

        UnityEngine.Debug.Log($"Best Move: {coordinates.x},{coordinates.y}");

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

        if(score == 0)
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
