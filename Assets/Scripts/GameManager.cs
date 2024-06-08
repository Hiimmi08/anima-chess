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
        Piece.OnClickHandler += OnClickPiece;
        Cell.OnClickHandler += OnClickCell;

        pieceManager.PieceActionCompleteHandler += OnPieceActionComplete;
    }

    private void RemoveEvents()
    {
        Piece.OnClickHandler -= OnClickPiece;
        Cell.OnClickHandler -= OnClickCell;

        pieceManager.PieceActionCompleteHandler -= OnPieceActionComplete;
    }

    private void OnClickPiece(Piece piece)
    {
        if (piece.PlayerId != currentPlayer.Id)
        {
            Debug.LogError("This piece is not playable in this turn!");
            return;
        }

        pieceManager.OnClickPieceHandler(piece);
    }

    private void OnClickCell(Cell cell)
    {
        // Not yet needed
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

