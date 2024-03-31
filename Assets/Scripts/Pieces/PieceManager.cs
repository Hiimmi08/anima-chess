using System.Collections.Generic;
using Players;
using UnityEngine;

namespace Pieces
{
    public class PieceManager : MonoBehaviour
    {
        [Header("Piece Prefabs")]
        [SerializeField] private Piece elephantPrefab;
        [SerializeField] private Piece lionPrefab;
        [SerializeField] private Piece tigerPrefab;
        [SerializeField] private Piece leopardPrefab;
        [SerializeField] private Piece dogPrefab;
        [SerializeField] private Piece wolfPrefab;
        [SerializeField] private Piece catPrefab;
        [SerializeField] private Piece ratPrefab;

        // North player
        private static readonly List<(Rank rank, int cell)> NorthStartPositions = new()
        {
            (Rank.Elephant, 60),
            (Rank.Lion, 8),
            (Rank.Tiger, 62),
            (Rank.Leopard, 24),
            (Rank.Dog, 16),
            (Rank.Wolf, 42),
            (Rank.Cat, 52),
            (Rank.Rat, 6),
        };
        private List<Piece> _northPieces = new();

        // South player
        private static readonly List<(Rank rank, int cell)> SouthStartPositions = new()
        {
            (Rank.Elephant, 2),
            (Rank.Lion, 54),
            (Rank.Tiger, 0),
            (Rank.Leopard, 38),
            (Rank.Dog, 46),
            (Rank.Wolf, 20),
            (Rank.Cat, 10),
            (Rank.Rat, 56),
        };
        private List<Piece> _southPieces = new();

        private void SpawnPieces()
        {
            
        }

        private void SpawnPieceForPlayer(PlayerPosition playerPosition)
        {
            
        }
    }
}