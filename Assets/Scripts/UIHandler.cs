using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private ImageSelectUI imageSelect;
    [SerializeField] private DifficultySelectUI difficultySelect;
    [SerializeField] private Button startButton;

    private GameManager game;
    
    public void Initialize(GameManager game)
    {
        this.game = game;
        imageSelect.SelectedPreviewChangedEvent.AddListener(RefreshSelection);
        startButton.interactable = false;
    }

    public void UpdateContent()
    {
        foreach (var image in game.Images)   
        {
            imageSelect.AddPreview(image);
        }
    }

    private void RefreshSelection()
    {
        startButton.interactable = true;
    }

    public Sprite GetSelectedImage()
    {
        return imageSelect.SelectedPreview.sprite;
    }

    public Difficulty GetSelectedDifficulty()
    {
        return difficultySelect.GetSelectedDifficulty();
    }
}
