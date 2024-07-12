using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PiecesCreator))]
public class ChessGameController : MonoBehaviour
{

    [SerializeField]
    private BoardLayout startingBoardLayout;

    private PiecesCreator piecesCreator;

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


    // Update is called once per frame
    void Update()
    {
        CreatePieceFromLayout(startingBoardLayout);
    }

    private void StartNewGame()
    {
        throw new NotImplementedException();
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
    }
}
