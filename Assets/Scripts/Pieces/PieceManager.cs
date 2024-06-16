using Boards;
using Cells;
using Players;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pieces
{
    public class PieceManager : MonoBehaviour
    {
        public static PieceManager Instance;

        public event Action<int, bool, bool> CellHighlightHandler;
        public event Action<Piece> PieceActionCompleteHandler;

        [Header("Piece Prefabs")]
        [SerializeField] private Piece elephantPrefab;
        [SerializeField] private Piece lionPrefab;
        [SerializeField] private Piece tigerPrefab;
        [SerializeField] private Piece leopardPrefab;
        [SerializeField] private Piece dogPrefab;
        [SerializeField] private Piece wolfPrefab;
        [SerializeField] private Piece catPrefab;
        [SerializeField] private Piece ratPrefab;

        #region Variables

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
        public List<Piece> _northPieces;

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
        public List<Piece> _southPieces;

        private Piece _selectedPiece;

        [SerializeField] private float SpawnY = 0.15f;

        #endregion

        #region Spawn Pieces

        public void SpawnPieces()
        {
            SpawnPieceForPlayer(PlayerPosition.North);
            SpawnPieceForPlayer(PlayerPosition.South);
        }

        private void SpawnPieceForPlayer(PlayerPosition playerPosition)
        {
            List<(Rank rank, int cell)> startPositions;
            PieceDirection direction;
            List<Piece> pieces;
            var playerId = 0;

            switch (playerPosition)
            {
                case PlayerPosition.North:
                    {
                        startPositions = NorthStartPositions;
                        direction = PieceDirection.North;
                        pieces = _northPieces;
                        playerId = (int)playerPosition;
                    }
                    break;

                case PlayerPosition.South:
                    {
                        startPositions = SouthStartPositions;
                        direction = PieceDirection.South;
                        pieces = _southPieces;
                        playerId = (int)playerPosition;
                    }
                    break;

                default:
                    return;
            }

            if (startPositions == null)
            {
                return;
            }

            foreach (var (rank, cellId) in startPositions)
            {
                SpawnPieceToCell(rank, cellId, direction, playerId, pieces);
            }
        }

        private void SpawnPieceToCell(Rank rank, int cellId, PieceDirection direction,
            int playerId, List<Piece> pieces)
        {
            Piece piece = rank switch
            {
                Rank.Rat => SpawnPieceToCell(ratPrefab, cellId, direction, playerId),
                Rank.Cat => SpawnPieceToCell(catPrefab, cellId, direction, playerId),
                Rank.Dog => SpawnPieceToCell(dogPrefab, cellId, direction, playerId),
                Rank.Wolf => SpawnPieceToCell(wolfPrefab, cellId, direction, playerId),
                Rank.Leopard => SpawnPieceToCell(leopardPrefab, cellId, direction, playerId),
                Rank.Tiger => SpawnPieceToCell(tigerPrefab, cellId, direction, playerId),
                Rank.Lion => SpawnPieceToCell(lionPrefab, cellId, direction, playerId),
                Rank.Elephant => SpawnPieceToCell(elephantPrefab, cellId, direction, playerId),
                _ => null,

            };

            if (piece != null && pieces != null)
            {
                pieces.Add(piece);
            }
        }

        private Piece SpawnPieceToCell(Piece piece, int cellId,
            PieceDirection direction, int playerId)
        {
            var cell = Board.Instance.GetCell(cellId);
            if (cell == null)
            {
                return null;
            }

            var position = cell.Position + new Vector3(0f, SpawnY, 0f);
            var p = Instantiate(piece, position, Quaternion.identity);
            p.transform.SetParent(transform);
            p.Init(direction, playerId);
            cell.SetPiece(p);

            return p;
        }

        #endregion

        public void Init()
        {
            _southPieces = new();
            _northPieces = new();

            AddEvents();
        }

        private void AddEvents()
        {
            Cell.OnClickHandler += OnClickCell;
        }

        private void RemoveEvents()
        {
            Cell.OnClickHandler -= OnClickCell;
        }

        public void OnMovePiece(Cell cell)
        {
            if (cell == null || cell.Piece == null)
            {
                Debug.Log($"OnMovePiece: {cell.name} 1");
                return;
            }

            var piece = cell.Piece;
            if (_selectedPiece != null && _selectedPiece != piece)
            {
                Debug.Log($"OnMovePiece: {cell.name} 2");
                _selectedPiece.ResetOnClick();
            }

            Debug.Log($"OnMovePiece: {cell.name} 3");
            _selectedPiece = piece;
            piece.SetOnClick();
            var rank = piece.CurrentRank;
            CellHighlightHandler?.Invoke(cell.Id, IsSwimmable(rank), IsCrossable(rank));
        }

        private void OnClickCell(Cell cell)
        {
            if (_selectedPiece == null)
            {
                return;
            }

            _selectedPiece.MoveToCell(cell, SpawnY);
            _selectedPiece.ResetOnClick(); // need change if move has animation
            PieceActionCompleteHandler?.Invoke(_selectedPiece);
            _selectedPiece = null;
        }

        private bool IsSwimmable(Rank rank) => rank is Rank.Rat or Rank.Dog;
        private bool IsCrossable(Rank rank) => rank is Rank.Lion or Rank.Tiger;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        private void OnDestroy()
        {
            RemoveEvents();
        }
    }
}