using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : ChessPiece
{
    public override bool ValidateMove(int targetX, int targetY)
    {
        int deltaX = targetX - currentX;
        int deltaY = targetY - currentY;

        if (!chessBoard.IsWithinBoardBounds(targetX, targetY))
        {
            return false;
        }

        // Check for the L-shaped movement
        if ((Mathf.Abs(deltaY) == 4 && Mathf.Abs(deltaX) == 2) ||
            (Mathf.Abs(deltaX) == 4 && Mathf.Abs(deltaY) == 2))
        {
            // Check if the target position is not occupied by a piece of the same team
            return !chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team);
        }

        
        return false;
    }

}
