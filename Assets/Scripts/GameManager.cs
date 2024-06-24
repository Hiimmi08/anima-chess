using System;
using Boards;
using Cells;
using Pieces;
using Players;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Board board;
    [SerializeField] private PieceManager pieceManager;
    [SerializeField] private Player southPlayer;
    [SerializeField] private Player northPlayer;
    [SerializeField] private TMP_Text playerNameLabel;

    private Player _currentPlayer;

    private void AddEvents()
    {
        Piece.OnClickHandler += OnClickPiece;
        Piece.OnMovingComplete += OnPieceMovingComplete;
        Cell.OnClickHandler += OnClickCell;

        pieceManager.PieceActionCompleteHandler += OnPieceActionComplete;
    }

    private void RemoveEvents()
    {
        Piece.OnClickHandler -= OnClickPiece;
        Piece.OnMovingComplete -= OnPieceMovingComplete;
        Cell.OnClickHandler -= OnClickCell;

        pieceManager.PieceActionCompleteHandler -= OnPieceActionComplete;
    }

    private void OnClickPiece(Piece piece)
    {
        if (piece.PlayerId != _currentPlayer.Id)
        {
            Debug.LogError("This piece is not playable in this turn!");
            return;
        }

        pieceManager.OnClickPieceHandler(piece);
    }

    private void OnPieceMovingComplete()
    {
        board.ResetHighlightedCells();
        pieceManager.OnPieceMovingComplete();
    }

    private void OnClickCell(Cell cell)
    {
        pieceManager.OnClickCell(cell);
        board.OnClickCell(cell);
    }

    private void OnPieceActionComplete(Piece piece)
    {
        if (_currentPlayer == null || piece.PlayerId != _currentPlayer.Id)
        {
            return;
        }

        SetPlayerTurn(_currentPlayer == northPlayer ? PlayerPosition.South : PlayerPosition.North);
    }

    private void Start()
    {
        pieceManager.Init();
        board.Init();
        pieceManager.SpawnPieces();
        northPlayer.Init((int)PlayerPosition.North);
        southPlayer.Init((int)PlayerPosition.South);

        Init();
    }

    private void Init()
    {
        SetPlayerTurn(PlayerPosition.South);
        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void SetPlayerTurn(PlayerPosition position)
    {
        var player = position == PlayerPosition.North ? northPlayer : southPlayer;
        if (player == null)
        {
            return;
        }

        _currentPlayer = player;
        playerNameLabel.text = player.name;
        player.EnablePlay();

        if (player == southPlayer)
        {
            northPlayer.DisablePlay();
        }
        else
        {
            southPlayer.DisablePlay();
        }
        
        pieceManager.OnSetPlayerTurn(position);
    }

    private void Reset()
    {
        pieceManager.OnReset();
    }
}