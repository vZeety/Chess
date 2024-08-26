
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : ChessPiece
{
    public override bool ValidateMove(int targetX, int targetY)
    {
        int deltaX = Mathf.Abs(targetX - currentX);
        int deltaY = Mathf.Abs(targetY - currentY);

       
        if (!chessBoard.IsWithinBoardBounds(targetX, targetY))
        {
            return false;
        }

        
        if (chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team))
        {
            return false;
        }

        
        if (deltaX <= 2 && deltaY <= 2)
        {
            return true;
        }

        return false;
    }
}


