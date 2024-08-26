using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum Team
{
    White,
    Black
}

public class ChessPiece : MonoBehaviour
{
    public Team team;
    public int currentX;
    public int currentY;
    public string pieceName;
    public ChessBoard chessBoard; 

    public void Awake()
    {
        
        chessBoard = FindObjectOfType<ChessBoard>();
        //Debug.Log("start");
        pieceName = gameObject.name;
        Vector3 initialPosition1 = gameObject.transform.position;
        Vector2Int initialGridPosition = SetInitialPosition(initialPosition1);
        currentX = initialGridPosition.x; 
        currentY = initialGridPosition.y;
    }

    public virtual bool ValidateMove(int targetX, int targetY)
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

        if (chessBoard.IsPositionOccupiedByOpponent(targetX, targetY, team))
        {
            return true;
        }

        return false;
    }

    public virtual void CapturePiece(int targetX, int targetY)
    {
        ChessPiece capturedPiece = chessBoard.GetPieceAtPosition(targetX, targetY);

        if (capturedPiece != null)
        {
            // Perform any necessary actions when capturing a piece (e.g., remove the piece from the board)
            chessBoard.RemovePiece(capturedPiece);
        }
    }

    private Vector2Int SetInitialPosition(Vector3 position)
    {

        // Vector3 initialPosition = gameObject.transform.position;


        //currentX = Mathf.FloorToInt(initialPosition.x);
        //currentY = Mathf.FloorToInt(initialPosition.z);

        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.z);

        return new Vector2Int(x, y);
    }

}

