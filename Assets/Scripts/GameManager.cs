using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FieldHandler fieldHandler;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private GameObject imageSelectCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private Image fadeImage;
    [SerializeField] private List<Sprite> images;

    public List<Sprite> Images => images;

    private Tween fadeInTween;
    private Tween fadeOutTween;

    public static Vector2 GetResolution(Difficulty difficulty)
    {
        Vector2 resolution = Vector2.one * 2;
        
        switch (difficulty)
        {
            case Difficulty.EASY:
                resolution = Vector2.one * 2;
                break;
            case Difficulty.MEDIUM:
                resolution = Vector2.one * 3;
                break;
            case Difficulty.HARD:
                resolution = Vector2.one * 4;
                break;
        }

        return resolution;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Awake()
    {
        uiHandler.Initialize(this);
        uiHandler.UpdateContent();

        fadeInTween = fadeImage.DOFade(1, 0.3f).SetAutoKill(false);
        fadeOutTween = fadeImage.DOFade(0, 0.3f).SetAutoKill(false);

        fadeOutTween.onComplete += DisableFadeRaycast;

        fadeInTween.Pause();
        fadeOutTween.Pause();
    }

    private void DisableFadeRaycast()
    {
        fadeImage.raycastTarget = false;
    }

    public void OpenMenu()
    {
        fadeImage.raycastTarget = true;
        fadeInTween.onComplete += FadeOutMenu;
        fadeInTween.Restart();
    }

    private void FadeOutMenu()
    {
        fieldHandler.ClearGame();
        gameCanvas.SetActive(false);
        imageSelectCanvas.SetActive(true);

        fadeInTween.onComplete -= FadeOutMenu;
        
        fadeOutTween.Restart();
    }

    private void FadeOutGame()
    {
        imageSelectCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        fieldHandler.StartGame(uiHandler.GetSelectedImage(), uiHandler.GetSelectedDifficulty());

        fadeInTween.onComplete -= FadeOutGame;
        
        fadeOutTween.Restart();
    }

    public void StartGame()
    {
        fadeImage.raycastTarget = true;
        fadeInTween.onComplete += FadeOutGame;
        fadeInTween.Restart();
    }
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD,
}

[System.Serializable]
public class DifficultyResolution
{
    public Difficulty difficulty;
    public Vector2 resolution;
}
