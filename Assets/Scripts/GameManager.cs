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

    private Player currentPlayer;

    private void AddEvents()
    {
        Cell.OnClickHandler += OnClickCell;

        pieceManager.PieceActionCompleteHandler += OnPieceActionComplete;
    }

    private void RemoveEvents()
    {
        Cell.OnClickHandler -= OnClickCell;

        pieceManager.PieceActionCompleteHandler -= OnPieceActionComplete;
    }

    private void OnClickCell(Cell cell)
    {
        if (cell == null)
        {
            return;
        }

        Debug.Log($"Click Cell: {cell.name}");
        // Check if cell has any piece on it
        if (cell.Piece != null)
        {
            Debug.Log($"Move Piece: {cell.name}");
            pieceManager.OnMovePiece(cell);
        }
    }

    private void OnPieceActionComplete(Piece piece)
    {
        if (currentPlayer == null || piece.PlayerId != currentPlayer.Id)
        {
            return;
        }

        if (currentPlayer == northPlayer)
        {
            SetPlayerTurn(southPlayer);
        }
        else
        {
            SetPlayerTurn(northPlayer);
        }
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
        SetPlayerTurn(southPlayer);
        AddEvents();
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void SetPlayerTurn(Player player)
    {
        if (player == null)
        {
            return;
        }

        currentPlayer = player;
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
    }
}

