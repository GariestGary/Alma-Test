using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Piece : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
{
    [SerializeField] private float solveThreshold;
    [SerializeField] private Image imageRenderer;
    
    private static readonly int PatternResolution = Shader.PropertyToID("_PatternResolution");
    private static readonly int PieceIndex = Shader.PropertyToID("_PieceIndex");
    private static readonly int PatternTex = Shader.PropertyToID("_PatternTex");
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    private Vector2 solvePoint;
    private bool dragging;

    public void Initialize(Sprite image, Sprite pattern, Vector2 index, Vector2 patternResolution)
    {
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

    public void OnPointerDown(PointerEventData eventData)
    {
        dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if(!dragging) return;

        transform.position += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }
}
