using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(ImageSlicer))]
public class PiecesHandler : MonoBehaviour
{
    [SerializeField] private RectTransform field;
    
    public UnityEvent SolvedEvent;

    private List<Piece> pieces;
    private Piece currentDraggingPiece;

    public List<Piece> Pieces => pieces;

    public void Initialize(List<Piece> pieces)
    {
        this.pieces = pieces;
    }

    public void DisablePieces()
    {
        pieces.ForEach(p => p.gameObject.SetActive(false));
    }

    public void EnablePieces()
    {
        pieces.ForEach(p => p.gameObject.SetActive(true));
    }

    public void ClearPieces()
    {
        pieces.Clear();
        pieces = null;
        currentDraggingPiece = null;
    }

    public void SetDraggable(Piece piece)
    {
        currentDraggingPiece = piece;
        
        pieces.ForEach(p =>
        {
            if (p != currentDraggingPiece)
            {
                p.ImageRenderer.raycastTarget = false;
            }
        });
    }

    public void UnsetDraggable()
    {
        currentDraggingPiece = null;
        pieces.ForEach(p => p.ImageRenderer.raycastTarget = true);
    }

    public void CheckSolve()
    {
        bool solved = true;

        foreach (var piece in pieces)
        {
            if (!piece.Solved)
            {
                solved = false;
                break;
            }
        }

        if (solved)
        {
            SolvedEvent?.Invoke();
        }
    }

    public void HandlePieceMove(PointerEventData eventData)
    {
        if(!currentDraggingPiece) return;
        
        float maxXValue = field.rect.width / 2;
        float minXValue = -maxXValue;
        float maxYValue = field.rect.height / 2;
        float minYValue = -maxYValue;

        currentDraggingPiece.transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);

        RectTransform pieceRect = currentDraggingPiece.transform as RectTransform;
        
        Vector2 pos = pieceRect.anchoredPosition;

        float xPos = Mathf.Clamp(pos.x, minXValue, maxXValue);
        float yPos = Mathf.Clamp(pos.y, minYValue, maxYValue);

        pieceRect.anchoredPosition = new Vector2(xPos, yPos);

    }
}
