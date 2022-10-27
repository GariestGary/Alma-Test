using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour
{
    [SerializeField] private float solveThreshold;
    [SerializeField] private Image imageRenderer;
    [SerializeField] private PointerEventsWrapper pointerEvents;
    
    private static readonly int PatternResolution = Shader.PropertyToID("_PatternResolution");
    private static readonly int PieceIndex = Shader.PropertyToID("_PieceIndex");
    private static readonly int PatternTex = Shader.PropertyToID("_PatternTex");
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    public Vector2 Index => index;
    public Image ImageRenderer => imageRenderer;
    public bool Solved => solved;

    private Vector2 solvePoint;
    private Vector2 index;
    private PiecesHandler handler;
    private bool solved;

    public void Initialize(Sprite image, Sprite pattern, Vector2 index, Vector2 patternResolution)
    {
        this.index = index;
        
        pointerEvents.PointerDownEvent.AddListener(OnPointerDown);
        pointerEvents.PointerMoveEvent.AddListener(OnPointerMove);
        pointerEvents.PointerUpEvent.AddListener(OnPointerUp);
        
        Material materialCopy = Instantiate(imageRenderer.material);
        imageRenderer.material = materialCopy;
        imageRenderer.material.SetTexture(MainTex, image.texture);
        imageRenderer.material.SetTexture(PatternTex, pattern.texture);
        imageRenderer.material.SetVector(PieceIndex, new Vector4(index.x, index.y));
        imageRenderer.material.SetVector(PatternResolution, new Vector4(patternResolution.x, patternResolution.y));
    }

    public void SetSolvedPoint(Vector2 point)
    {
        solvePoint = point;
    }

    public bool CheckSolve()
    {
        if (solved) return true;
        
        RectTransform rect = transform as RectTransform;

        float dist = Vector2.Distance(rect.anchoredPosition, solvePoint);

        if (dist <= solveThreshold)
        {
            (transform as RectTransform).anchoredPosition = solvePoint;
            transform.SetSiblingIndex(0);
            imageRenderer.raycastTarget = false;
            handler.UnsetDraggable();
            solved = true;
            return true;
        }

        return false;
    }

    public void SetHandler(PiecesHandler handler)
    {
        this.handler = handler;
    }

    private void OnPointerDown(PointerEventData eventData)
    {
        if(solved) return;
        
        handler.SetDraggable(this);
    }

    private void OnPointerMove(PointerEventData eventData)
    {
        if(solved) return;

        handler.HandlePieceMove(eventData);
        if (CheckSolve())
        {
            handler.CheckSolve();
        }
    }

    private void OnPointerUp(PointerEventData eventData)
    { 
        if(solved) return;

        handler.UnsetDraggable();
    }
}
