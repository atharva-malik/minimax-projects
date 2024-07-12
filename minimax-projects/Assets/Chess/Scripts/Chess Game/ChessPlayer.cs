using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPlayer
{
    public TeamColour team { get; set; }
    public Board board { get; set; }
    public List<Piece> activePieces { get; private set; }

    public ChessPlayer(TeamColour team, Board board){
        this.team = team;
        this.board = board;
        activePieces = new List<Piece>();
    }

    public void AddPiece(Piece piece){
        if (!activePieces.Contains(piece))
            activePieces.Add(piece);
    }

    public void RemovePiece(Piece piece){
        if (activePieces.Contains(piece))
            activePieces.Remove(piece);
    }

    public void GenerateAllPossibleMoves(){
        foreach(var piece in activePieces){
            if(board.HasPiece(piece))
                piece.SelectAvailableSquares();
        }
    }
}
