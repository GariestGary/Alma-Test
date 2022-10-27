using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ImagePreview : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image image;

    private UIHandler handler;
    private ImageSelectUI select;

    public Sprite sprite => image.sprite;

    public void Initialize(Sprite sprite, ImageSelectUI select)
    {
        image.sprite = sprite;
        this.select = select;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        select.SelectImage(this);
    }
}
