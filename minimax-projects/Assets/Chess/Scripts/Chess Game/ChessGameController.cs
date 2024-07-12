using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PiecesCreator))]
public class ChessGameController : MonoBehaviour
{

    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] private Board board;

    private PiecesCreator piecesCreator;
    private ChessPlayer whitePlayer;
    private ChessPlayer blackPlayer;
    private ChessPlayer activePlayer;

    void Awake() {
        SetDependencies();
        CreatePlayers();
    }

    private void CreatePlayers()
    {
        whitePlayer = new ChessPlayer(TeamColour.White, board);
        blackPlayer = new ChessPlayer(TeamColour.Black, board);
    }

    private void SetDependencies()
    {
        piecesCreator = GetComponent<PiecesCreator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartNewGame();
    }

    private void StartNewGame()
    {
        board.SetDependencies(this);
        CreatePieceFromLayout(startingBoardLayout);
        activePlayer = whitePlayer;
        GenerateAllPossiblePlayerMoves(activePlayer);
    }

    private void GenerateAllPossiblePlayerMoves(ChessPlayer activePlayer)
    {
        activePlayer.GenerateAllPossibleMoves();
    }

    private void CreatePieceFromLayout(BoardLayout layout)
    {
        for (int i = 0; i < layout.GetPiecesCount(); i++){
            Vector2Int squareCoords = layout.GetSquareCoordsAtIndex(i);
            TeamColour team = layout.GetSquareTeamColourAtIndex(i);
            string typeName = layout.GetSquarePieceNameAtIndex(i);

            Type type = Type.GetType(typeName);
            CreatePieceAndInitialize(squareCoords, team, type);
        }
    }

    private void CreatePieceAndInitialize(Vector2Int squareCoords, TeamColour team, Type type)
    {
        Piece newPiece = piecesCreator.CreatePiece(type).GetComponent<Piece>();
        newPiece.SetData(squareCoords, team, board);

        Material teamMaterial = piecesCreator.GetTeamMaterial(team);
        newPiece.SetMaterial(teamMaterial);

        board.SetPieceOnBoard(squareCoords, newPiece);

        ChessPlayer currentPlayer = team == TeamColour.White? whitePlayer : blackPlayer;
        currentPlayer.AddPiece(newPiece);
    }

    public bool IsTeamTurnActive(TeamColour team)
    {
        return activePlayer.team == team;
    }

    public void EndTurn()
    {
        GenerateAllPossiblePlayerMoves(activePlayer);
        GenerateAllPossiblePlayerMoves(GetOpponentToPlayer(activePlayer));
        ChangeActiveTeam();
    }

    private void ChangeActiveTeam()
    {
        activePlayer = activePlayer == whitePlayer? blackPlayer : whitePlayer;
    }

    private ChessPlayer GetOpponentToPlayer(ChessPlayer player)
    {
        return player == whitePlayer? blackPlayer : whitePlayer;
    }
}
