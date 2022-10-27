using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(PiecesHandler), typeof(ImageSlicer))]
public class FieldHandler : MonoBehaviour
{
    [SerializeField] private Image solvedImageView;
    [SerializeField] private RectTransform field;
    [SerializeField] private GameObject solvedUI;
    
    private PiecesHandler piecesHandler;
    private ImageSlicer slicer;

    private Difficulty currentDifficulty;
    private Sprite currentImage;
    

    private void Awake()
    {
        piecesHandler = GetComponent<PiecesHandler>();
        slicer = GetComponent<ImageSlicer>();
        piecesHandler.SolvedEvent.AddListener(SolvedScreen);
        solvedUI.SetActive(true);
    }

    private void SolvedScreen()
    {
        piecesHandler.DisablePieces();
        solvedImageView.gameObject.SetActive(true);
        solvedUI.SetActive(true);
    }

    public void RestartGame()
    {
        StartGame(currentImage, currentDifficulty);
    }

    public void ClearGame()
    {
        piecesHandler.ClearPieces();
    }

    public void StartGame(Sprite image, Difficulty difficulty)
    {
        currentDifficulty = difficulty;
        currentImage = image;
        solvedImageView.sprite = image;
        solvedImageView.gameObject.SetActive(false);
        solvedUI.SetActive(false);

        List<Piece> pieces;

        if (piecesHandler.Pieces == null)
        {
            pieces = slicer.SliceImage(image, difficulty);
            piecesHandler.Initialize(pieces);
        }
        else
        {
            pieces = piecesHandler.Pieces;
        }
        
        PrepareField(difficulty, pieces);
    }
    
    public void PrepareField(Difficulty difficulty, List<Piece> pieces)
    {
        foreach (var piece in pieces)
        {

            float width = field.rect.width / GameManager.GetResolution(difficulty).x;
            float height = field.rect.height / GameManager.GetResolution(difficulty).y;

            RectTransform rect = piece.transform as RectTransform;
            
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width * 2);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height * 2);

            var fieldRect = field.rect;
            
            float xPos = Random.Range(-fieldRect.width / 2 + width / 2, fieldRect.width / 2 - width / 2);
            float yPos = Random.Range(-fieldRect.height / 2 + height / 2, fieldRect.height / 2 - height / 2);

            rect.anchoredPosition = new Vector2(xPos, yPos);

            float xPadding = width / 2;
            float yPadding = height / 2;
            
            piece.ImageRenderer.raycastPadding = new Vector4(xPadding, yPadding, xPadding, yPadding);

            float xSolve = width / 2 + width * piece.Index.x - fieldRect.width / 2;
            float ySolve = height / 2 + width * piece.Index.y - fieldRect.height / 2;
            
            piece.SetSolvedPoint(new Vector2(xSolve, ySolve));
            piece.SetHandler(piecesHandler);
            piecesHandler.ResetPieces();
        }
    }
}
