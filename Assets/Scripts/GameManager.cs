using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FieldHandler fieldHandler;
    [SerializeField] private UIHandler uiHandler;
    [SerializeField] private GameObject uiCanvas;
    [SerializeField] private GameObject gameCanvas;
    [SerializeField] private List<Sprite> images;

    public List<Sprite> Images => images;

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

    private void Awake()
    {
        uiHandler.Initialize(this);
        uiHandler.UpdateContent();
        uiHandler.RefreshSelect();
    }

    public void OpenMenu()
    {
        fieldHandler.ClearGame();
        gameCanvas.SetActive(false);
        uiCanvas.SetActive(true);
    }

    public void StartGame()
    {
        
        uiCanvas.SetActive(false);
        gameCanvas.SetActive(true);
        fieldHandler.StartGame(uiHandler.GetSelectedImage(), uiHandler.GetSelectedDifficulty());
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
