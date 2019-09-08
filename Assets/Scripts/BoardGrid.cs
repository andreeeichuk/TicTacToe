public class BoardGrid
{
    public int[,] cells = new int[3,3];    

    public void ClearBoardGrid()
    {
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                cells[i, j] = 0;
            }
        }
    }

    public int CheckWinner()
    {
        for (int row = 0; row < 3; row++)
        {
            if (cells[0,row] == cells[1,row] && cells[1,row] == cells[2,row])
            {
                if (cells[0,row] == 1)
                    return 1;
                else if (cells[0,row] == 2)
                    return 2;
            }
        }

        for (int col = 0; col < 3; col++)
        {
            if (cells[col,0] == cells[col,1] && cells[col,1] == cells[col,2])
            {
                if (cells[col,0] == 1)
                    return 1;
                else if (cells[col,0] == 2)
                    return 2;
            }
        }

        if (cells[0,0] == cells[1,1] && cells[1,1] == cells[2,2])
        {
            if (cells[0,0] == 1)
                return 1;
            else if (cells[0,0] == 2)
                return 2;
        }
        if (cells[2,0] == cells[1,1] && cells[1,1] == cells[0,2])
        {
            if (cells[2,0] == 1)
                return 1;
            else if (cells[2,0] == 2)
                return 2;
        }

        if(AnyMovesLeft())
        {
            return -1;
        }
        else
        {
            return 0;
        }        
    }

    private bool AnyMovesLeft()
    {
        for (int i = 0; i < cells.GetLength(0); i++)
        {
            for (int j = 0; j < cells.GetLength(1); j++)
            {
                if (cells[i, j] == 0)
                    return true;
            }
        }

        return false;
    }
}
