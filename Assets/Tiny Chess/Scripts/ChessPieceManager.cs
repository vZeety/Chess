using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPieceManager : MonoBehaviour
{
    public GameObject[] chessPieces;

    
    private Dictionary<GameObject, Vector2Int> piecePositions = new Dictionary<GameObject, Vector2Int>();

    void Awake()
    {
        
        foreach (var piece in chessPieces)
        {
            Vector3 position = piece.transform.position;
            Vector2Int positionInGrid = CalculatePositionInGrid(position);

          
            piecePositions[piece] = positionInGrid;
        }

       
        foreach (var kvp in piecePositions)
        {
            GameObject piece = kvp.Key;
            Vector2Int position = kvp.Value;

            //Debug.Log($"{piece.name} is at position ({position.x}, {position.y})");
        }
    }

    private Vector2Int CalculatePositionInGrid(Vector3 position)
    {
        
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.z);

        return new Vector2Int(x, y);
    }

    public Dictionary<GameObject, Vector2Int> GetTheList()
    {
        return piecePositions;
    }
}
