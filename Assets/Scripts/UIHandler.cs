using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private ImageSelectUI imageSelect;
    [SerializeField] private DifficultySelectUI difficultySelect;

    private GameManager game;
    
    public void Initialize(GameManager game)
    {
        this.game = game;
    }

    public void UpdateContent()
    {
        foreach (var image in game.Images)   
        {
            imageSelect.AddPreview(image);
        }
    }

    public void RefreshSelect()
    {
        //select first image at start
        imageSelect.SelectImage(imageSelect.Previews[0]);
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
