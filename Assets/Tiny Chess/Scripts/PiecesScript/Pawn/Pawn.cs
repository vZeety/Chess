using System;
using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
using UnityEngine;

public class Pawn : ChessPiece
{

    public override bool ValidateMove(int targetX, int targetY)
    {
        int deltaX = targetX - currentX;
        int deltaY = targetY - currentY;

        if (!chessBoard.IsWithinBoardBounds(targetX, targetY))
        {
            return false;
        }

        // Check for moving forward
        if (deltaX == 0 && Mathf.Abs(deltaY) == 2)
        {
            // Moving forward by 2 squares (for white pawns)
            if (team == Team.White && deltaY == 2)
            {
                return !chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team);
            }
            // Moving backward by 2 squares (for black pawns)
            else if (team == Team.Black && deltaY == -2)
            {
                return !chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team);
            }
        }

        // Check for moving forward by 2 squares (initial move)
        if (deltaX == 0 && Mathf.Abs(deltaY) == 4)
        {
            if (team == Team.White && currentY == -5)
            {
                return !chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team);
            }
            else if (team == Team.Black && currentY == 5)
            {
                return !chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team);
            }
        }

        // Check for capturing diagonally
        if (Mathf.Abs(deltaX) == 2 && Mathf.Abs(deltaY) == 2)
        {
            
            if (team == Team.White && deltaY == 2 && chessBoard.IsPositionOccupiedByOpponent(targetX, targetY, team))
            {
                Debug.Log("Hey");
                return true;
            }
            else if (team == Team.Black && deltaY == -2 && chessBoard.IsPositionOccupiedByOpponent(targetX, targetY, team))
            {
                return true;
            }
        }

        return false;
    }




    /* public override void CapturePiece(int targetX, int targetY)
     {
         // Custom logic for capturing a piece with a pawn
         base.CapturePiece(targetX, targetY);
         // Additional actions specific to pawn capturing...
     }*/
}
