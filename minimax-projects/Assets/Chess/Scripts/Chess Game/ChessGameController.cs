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

    void Awake() {
        SetDependencies();
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
        CreatePieceFromLayout(startingBoardLayout);
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
    }
}
