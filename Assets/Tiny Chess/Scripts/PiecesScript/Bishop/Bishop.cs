using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : ChessPiece
{
    public override bool ValidateMove(int targetX, int targetY)
    {
        int deltaX = targetX - currentX;
        int deltaY = targetY - currentY;

        
        if (!chessBoard.IsWithinBoardBounds(targetX, targetY))
        {
            return false;
        }

        if (chessBoard.IsPositionOccupiedByTeam(targetX, targetY,team))
        {
            return false;
        }

        
        if (Mathf.Abs(deltaX) != Mathf.Abs(deltaY))
        {
            return false;
        }

        int stepX = 2 * (deltaX / Mathf.Abs(deltaX));
        int stepY = 2 * (deltaY / Mathf.Abs(deltaY));

        int x = currentX + stepX;
        int y = currentY + stepY;

        
        while (x != targetX && y!=targetY)
        {
            if (chessBoard.IsPositionOccupiedByTeam(x, y, team))
            {
                return false; 
            }

            x += stepX;
            y += stepY;
        }

        return true; 
    }
}
