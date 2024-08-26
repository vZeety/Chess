using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;



public class ChessInput : MonoBehaviour
{
    private ChessPiece selectedPiece;
    private Vector3 originalPiecePosition;
    private ChessBoard chessBoard;
    private Camera mainCamera;
    private Vector3 potentialPosition;

    void Start()
    {
        chessBoard = FindAnyObjectByType<ChessBoard>();
        mainCamera = Camera.main;
        
        
        
    }

    void Update()
    {
        HandleTouchInput();
        
    }

    void HandleTouchInput()
    {
        //Debug.Log(Input.touchCount);
        if (Input.touchCount > 0)
        {
            
            Touch touch = Input.GetTouch(0);
            Debug.Log("Found It");

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    HandleTouchBegan(touch.position);
                    Debug.Log("Found It 1");
                    break;

                case TouchPhase.Moved:
                    HandleTouchMoved(touch.position);
                    Debug.Log("Found It 2");
                    break;

                case TouchPhase.Ended:
                    HandleTouchEnded(touch.position);
                    Debug.Log("Found It 3");
                    break;
            }
        }
    }

    void HandleTouchBegan(Vector2 touchPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            selectedPiece = hit.collider.GetComponent<ChessPiece>();

            if (selectedPiece != null)
            {
                originalPiecePosition = selectedPiece.transform.position;

                Debug.Log($"Selected piece: {selectedPiece.name}, Original Position: {originalPiecePosition}");
            }
        }
    }

    void HandleTouchMoved(Vector2 touchPosition)
    {

        if (selectedPiece != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(touchPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                int targetX, targetY;

                // Try to get board coordinates from the hit point
                if (chessBoard.TryGetBoardCoordinates(hit.point, out targetX, out targetY))
                {
                    Debug.Log($"{targetX},{targetY}");

                    // Update the potential position based on valid board coordinates
                    potentialPosition = new Vector3(targetX, selectedPiece.transform.position.y, targetY);
                    selectedPiece.transform.position = potentialPosition;
                    Debug.Log($"Dragging piece: {selectedPiece.name}, Potential Coordinates: ({targetX}, {targetY})");
                }
            }
        }
    }

    void HandleTouchEnded(Vector2 touchPosition)
    {
        if (selectedPiece != null)
        {
            //int originalX = Mathf.RoundToInt(originalPiecePosition.x);
            //int originalY = Mathf.RoundToInt(originalPiecePosition.z);
            //Debug.Log($"Originals : {originalX},{originalY}");

            // Check if the snapped position is a valid move
            if (selectedPiece.ValidateMove((int)potentialPosition.x, (int)potentialPosition.z))
            {
                // Valid move, update the chessboard and piece position
                chessBoard.MovePiece(selectedPiece, (int)potentialPosition.x, (int)potentialPosition.z);
                Debug.Log($"Moved piece to: ({potentialPosition.x}, {potentialPosition.z})");
            }
            else
            {
                // Invalid move, snap the piece back to its original position
                selectedPiece.transform.position = originalPiecePosition;
                Debug.Log("Invalid move. Resetting position.");
            }

            // Reset selected piece and potential position
            selectedPiece = null;
            potentialPosition = Vector3.zero;
        }
    }
}

