using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    private Dictionary<ChessPiece, Vector2Int> initialPositions = new Dictionary<ChessPiece, Vector2Int>();
    public static int BOARD_SIZE = 8;
    public ChessPiece[,] pieces = new ChessPiece[BOARD_SIZE, BOARD_SIZE];
    public ChessPiece[] existingChessPieces = new ChessPiece[32];
    //private ChessPieceManager chesspiecemanager;
   

    public void Awake()
    {
        
    }


    private void Start()
    {
        //chesspiecemanager = FindObjectOfType<ChessPieceManager>();
       // Dictionary<GameObject, Vector2Int> piecePositions = chesspiecemanager.GetTheList();



       // ResetPieces();
        //Debug.Log("Hey");


        StoreInitialPositions();

        existingChessPieces = GameObject.FindObjectsOfType<ChessPiece>();
        //PrintExisting(existingChessPieces);

        PopulatePiecesArray();
        //Debug.Log(pieces);
        //PrintArray();
    }

    /*private void OnEnable()
    {
        existingChessPieces = GameObject.FindObjectsOfType<ChessPiece>();
        Debug.Log(existingChessPieces.ToString());
        PopulatePiecesArray();
    }*/
    void PopulatePiecesArray()
    {
        for (int i = 0; i < existingChessPieces.Length; i++)
        {
            ChessPiece piece = existingChessPieces[i];
            //Debug.Log(piece);
            //Debug.Log($"{piece.currentX}, {piece.currentY}");

            if (piece != null)
            {
                int x = MapCoordinateToIndex(piece.currentX);
                int y = MapCoordinateToIndex(piece.currentY);

                //Debug.Log($"{x},{y}");
                if (IsWithinBoardBounds(piece.currentX, piece.currentY))
                {
                    //Debug.Log(IsWithinBoardBounds(piece.currentX, piece.currentY));
                    pieces[x, y] = piece;
                }
                //Debug.Log(pieces[x, y]);
            }

           
        }
    }

    public void UpdatePiecesArray()
    {
        // Reset the pieces array
        for (int i = 0; i < BOARD_SIZE; i++)
        {
            for (int j = 0; j < BOARD_SIZE; j++)
            {
                pieces[i, j] = null;
            }
        }

        // Populate the pieces array with the current positions
        foreach (ChessPiece piece in existingChessPieces)
        {
            if (piece != null)
            {
                int x = MapCoordinateToIndex(piece.currentX);
                int y = MapCoordinateToIndex(piece.currentY);

                if (IsWithinBoardBounds(piece.currentX, piece.currentY))
                {
                    pieces[x, y] = piece;
                }
            }
        }
    }


    void StoreInitialPositions()
    {
        ChessPiece[] allPieces = GameObject.FindObjectsOfType<ChessPiece>();

        foreach (ChessPiece piece in allPieces)
        {
            initialPositions[piece] = new Vector2Int(piece.currentX, piece.currentY);
            //Debug.Log($"Stored initial position for {piece.name}: {piece.currentX}, {piece.currentY}");
        }
    }


    // Function to map a coordinate to a valid array index
    private int MapCoordinateToIndex(int coordinate)
    {
        // Apply the mapping for your specific case
        int[] coordinateMapping = { 0, 1, 2, 3, 4, 5, 6, 7 };

        // Ensure the coordinate is within the valid range
        int mappedCoordinate = Mathf.Clamp(coordinate, -7, 7);

        // Map the coordinate to the corresponding index
        return coordinateMapping[(mappedCoordinate + 7) / 2];
    }




    public bool IsPositionOccupiedByTeam(int x, int y, Team pieceTeam)
    {
        ChessPiece pieceAtPosition = GetPieceAtPosition(x, y);
        //Debug.Log($"Checking position ({x}, {y}) - Piece: {pieceAtPosition}");

        return pieceAtPosition != null && pieceAtPosition.team == pieceTeam;
    }

    public bool IsPositionOccupiedByOpponent(int x, int y, Team pieceTeam)
    {
        ChessPiece pieceAtPosition = GetPieceAtPosition(x, y);
        Debug.Log($"Checking position ({x}, {y}) - Piece: {pieceAtPosition}");
        

        return pieceAtPosition != null && pieceAtPosition.team != pieceTeam;
    }

    public bool IsOccupied(int x, int y, Team pieceTeam)
    {
        int ix = x;
        int iy = y;
        Team iteam = pieceTeam;

        bool result1 = IsPositionOccupiedByOpponent(x, y, iteam);
        bool result2 = IsPositionOccupiedByTeam(x,y, iteam);
        if (result1 == true && result2 == true)
        {
            return true;
        }
        return false;
    }

    public ChessPiece GetPieceAtPosition(int x, int y)
    {
        int mappedX = MapCoordinateToIndex(x);
        int mappedY = MapCoordinateToIndex(y);

        if (IsWithinBoardBounds(x, y))
        {
            //Debug.Log($"THE PIECE ARRAYS{pieces[mappedX, mappedY]}");
            return pieces[mappedX, mappedY];

        }
        else
        {
            return null;
        }
    }
    /*
     * Ancien code 
    public bool IsWithinBoardBounds(int x, int y)
    {
        int halfSize = ChessBoard.BOARD_SIZE / 2;

        return x >= -halfSize && x <= halfSize && y >= -halfSize && y <= halfSize;
    }*/

    public bool IsWithinBoardBounds(int x, int y)
    {
        // Define the valid x and y coordinates for your chessboard
        int[] validXCoordinates = { -7, -5, -3, -1, 1, 3, 5, 7 };
        int[] validYCoordinates = { -7, -5, -3, -1, 1, 3, 5, 7 };

        // Check if the provided x and y coordinates are within the valid ranges
        return Array.IndexOf(validXCoordinates, x) != -1 && Array.IndexOf(validYCoordinates, y) != -1;
    }


    public void RemovePiece(ChessPiece pieceToRemove)
    {
        if (pieceToRemove != null)
        {
            int x = pieceToRemove.currentX;
            int y = pieceToRemove.currentY;

            // Remove the piece from the existingChessPieces array
            existingChessPieces = existingChessPieces.Where(piece => piece != pieceToRemove).ToArray();

            // Update the existing pieces
            UpdatePiecesArray();

            // destroy the game object after a  delay 
            Destroy(pieceToRemove.gameObject, 5f);
        }
    }



    public void ResetPieces()
    {
        foreach (var kvp in initialPositions)
        {
            ChessPiece piece = kvp.Key;
            Vector2Int initialPosition = kvp.Value;


            piece.currentX = initialPosition.x;
            piece.currentY = initialPosition.y;
           
            Debug.Log($"Reset position for {piece.name} to: {piece.currentX}, {piece.currentY}");
            

        }
        
    }

    public void MovePiece(ChessPiece selectedPiece, int targetX, int targetY)
    {
        selectedPiece.currentX = targetX;
        selectedPiece.currentY = targetY;
    }

    public bool TryGetBoardCoordinates(Vector3 worldPoint, out int x, out int y)
    {

        Vector3 localPoint = transform.InverseTransformPoint(worldPoint);




        x = Mathf.FloorToInt(localPoint.x);
        y = Mathf.RoundToInt(localPoint.z);

        //Debug.Log($"{x},{y}");


        //Debug.Log(IsWithinBoardBounds(x, y));

        return IsWithinBoardBounds(x, y);

    }


    public void PrintArray(ChessPiece[,] anarray)
    {

        foreach (ChessPiece piece in anarray)
        {
            Debug.Log(piece);
        }
    }

    public void PrintExisting(ChessPiece[] hola)
    {
        foreach (ChessPiece piece in hola)
        {
            Debug.Log($"Piece {piece.name}, Position: ({piece.currentX}, {piece.currentY})");
        }
    }

   
    
    
    /*public string GetPieceInfoByName(string pieceName, ChessPiece[] piecesArray)
    {
        foreach (ChessPiece piece in piecesArray)
        {
            if (piece != null && piece.name == pieceName)
            {
                return $"Piece {piece.name}, Position: ({piece.currentX}, {piece.currentY})";
            }
        }

        // Return a message indicating that the piece with the specified name was not found
        return $"Piece with name '{pieceName}' not found.";
    }*/



}
