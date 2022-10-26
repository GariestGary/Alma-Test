using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private ImageSlicer slicer;
    [Tooltip("Image must be 1024x1024 resolution")]
    [SerializeField] private List<Sprite> images;

    public List<Sprite> Images => images;

    private Sprite currentSelectedImage;

    public void SelectImage(Sprite image)
    {
        if (image == null)
        {
            currentSelectedImage = images[0];
            return;
        }

        currentSelectedImage = image;
    }
}

public enum Difficulty
{
    EASY,
    MEDIUM,
    HARD,
}
