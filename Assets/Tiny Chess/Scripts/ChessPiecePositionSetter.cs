using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiecePositionSetter : MonoBehaviour
{
    [System.Serializable]
    public struct PiecePosition
    {
        public Transform pieceTransform;
        public Vector2 coordinates;
    }

    [Header("Chess Piece Positions")]
    [SerializeField] private List<PiecePosition> piecePositions = new List<PiecePosition>();

    private void Awake()
    {
        foreach (var piecePosition in piecePositions)
        {
            Transform pieceTransform = piecePosition.pieceTransform;
            Vector2 coordinates = piecePosition.coordinates;

            pieceTransform.position = new Vector3(coordinates.x, pieceTransform.position.y, coordinates.y);
        }
    }
}
