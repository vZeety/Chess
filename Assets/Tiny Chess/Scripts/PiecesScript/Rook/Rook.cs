using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : ChessPiece
{
    public override bool ValidateMove(int targetX, int targetY)
    {

        
        int deltaX = targetX - currentX;
        int deltaY = targetY - currentY;


        if (!chessBoard.IsWithinBoardBounds(targetX, targetY))
        {
            return false;
        }

        if (chessBoard.IsPositionOccupiedByTeam(targetX, targetY, team))
        {
            return false;
        }

        if (deltaX!= 0 && deltaY!= 0) { 
            return false; }

        //Debug.Log($"{currentX}, {currentY}");
        //Debug.Log($"{targetX}, {targetY}");
        //Debug.Log($"{deltaX}, {deltaY


        int stepX = deltaX == 0 ? 0 : 2 * (deltaX / Mathf.Abs(deltaX));
        int stepY = deltaY == 0 ? 0 : 2 * (deltaY / Mathf.Abs(deltaY));






        int x = currentX + stepX;
        int y = currentY + stepY;


        while (x != targetX || y != targetY)
        {
            if (chessBoard.IsPositionOccupiedByTeam(x, y, team))
            {
                return false;
            }

            Debug.Log($" Voila meme: {x},{y}");

            x += stepX;
            y += stepY;
        }

        return true;
    }
}

