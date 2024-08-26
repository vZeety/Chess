using UnityEngine;

public class MouseInput : MonoBehaviour
{
    private ChessPiece selectedPiece;
    private Vector3 originalPiecePosition;
    private Vector3 potentialPosition;  // New variable to track potential position during drag
    private ChessBoard chessBoard;
    private Camera mainCamera1;

    void Start()
    {
        chessBoard = FindObjectOfType<ChessBoard>();
        mainCamera1 = Camera.main;
    }

    void Update()
    {
        HandleMouseInput();
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            HandleMouseDrag(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            HandleMouseRelease(Input.mousePosition);
        }
    }

    void HandleMouseClick(Vector3 mousePosition)
    {
        Ray ray = mainCamera1.ScreenPointToRay(mousePosition);
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

    void HandleMouseDrag(Vector3 mousePosition)
    {
        if (selectedPiece != null)
        {
            Ray ray = mainCamera1.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                int targetX, targetY;

                // Try to get board coordinates from the hit point
                if (chessBoard.TryGetBoardCoordinates(hit.point, out targetX, out targetY))
                {
                    //Debug.Log($"{targetX},{targetY}");

                    // Update the potential position based on valid board coordinates
                    potentialPosition = new Vector3(targetX, selectedPiece.transform.position.y, targetY);
                    selectedPiece.transform.position = potentialPosition;
                    Debug.Log($"Dragging piece: {selectedPiece.name}, Potential Coordinates: ({targetX}, {targetY})");
                }
            }
        }
    }


    void HandleMouseRelease(Vector3 mousePosition)
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
                chessBoard.UpdatePiecesArray();
                
                
                Debug.Log($"Moved piece to: ({potentialPosition.x}, {potentialPosition.z})");
                //Debug.Log($"{chessBoard.GetPieceInfoByName(selectedPiece.name,chessBoard.existingChessPieces)}");
                //Debug.Log(chessBoard.pieces);
                //Debug.Log(chessBoard.GetPieceAtPosition((int)potentialPosition.x, (int)potentialPosition.z));
               
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
