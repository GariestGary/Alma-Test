using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject imagePreviewPrefab;
    [SerializeField] private RectTransform selectFrame;

    private ImagePreview selectedPreview;
    private List<ImagePreview> previews = new List<ImagePreview>();

    public ImagePreview SelectedPreview => selectedPreview;
    public List<ImagePreview> Previews => previews;

    public void AddPreview(Sprite sprite)
    {
        ImagePreview preview = Instantiate(imagePreviewPrefab, Vector3.zero, Quaternion.identity, transform)
            .GetComponent<ImagePreview>();
        
        preview.Initialize(sprite, this);
        
        previews.Add(preview);
    }
    
    public void SelectImage(ImagePreview preview)
    {
        selectedPreview = preview;
        selectFrame.gameObject.SetActive(true);
        selectFrame.position = preview.transform.position;
    }
}
