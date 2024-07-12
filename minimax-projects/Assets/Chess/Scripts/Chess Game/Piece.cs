using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

[RequireComponent(typeof(IObjectTweener))]
[RequireComponent(typeof(MaterialSetter))]
public abstract class Piece : MonoBehaviour
{
    private MaterialSetter materialSetter;
    public Board board {protected get; set;}
    public Vector2Int occupiedSquare {get; set;}
    public TeamColour team {get; set;}
    public bool hasMoved {get; private set;}
    public List<Vector2Int> validMoves {get; private set;}
    public List<Vector2Int> availableMoves;
    private IObjectTweener tweener;

    public abstract List<Vector2Int> SelectAvailableSquares();

    private void Awake() {
        availableMoves = new List<Vector2Int>();
        tweener = GetComponent<IObjectTweener>();
        materialSetter = GetComponent<MaterialSetter>();
        hasMoved = false;
    }

    public void SetMaterial(Material material){
        if (materialSetter == null)
            materialSetter = GetComponent<MaterialSetter>();
        materialSetter.SetSingleMaterial(material);
    }

    public bool IsFromSameTeam(Piece piece){
        return team == piece.team;
    }

    public bool CanMoveTo(Vector2Int coords){
        return availableMoves.Contains(coords);
    }

    public virtual void MovePiece(Vector2Int coords){
        Vector3 targetPosittion = board.CalculatePositionFromCoords(coords);
        occupiedSquare = coords;
        hasMoved = true;
        tweener.MoveTo(transform, targetPosittion);
    }

    protected void TryToAddMove(Vector2Int coords){
        availableMoves.Add(coords);
    }

    public void SetData(Vector2Int coords, TeamColour team, Board board){
        this.team = team;
        occupiedSquare = coords;
        this.board = board;
        transform.position = board.CalculatePositionFromCoords(coords);
    }
}
