using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class ImageSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject imagePreviewPrefab;
    [SerializeField] private Transform previewContainer;
    [SerializeField] private RectTransform selectFrame;

    private ImagePreview selectedPreview;
    private List<ImagePreview> previews = new List<ImagePreview>();

    public ImagePreview SelectedPreview => selectedPreview;
    public List<ImagePreview> Previews => previews;
    public UnityEvent SelectedPreviewChangedEvent;

    private Tween selectionFrameTween;

    public void AddPreview(Sprite sprite)
    {
        ImagePreview preview = Instantiate(imagePreviewPrefab, Vector3.zero, Quaternion.identity, previewContainer)
            .GetComponent<ImagePreview>();
        
        preview.Initialize(sprite, this);
        
        previews.Add(preview);
    }
    
    public void SelectImage(ImagePreview preview)
    {
        if(preview == selectedPreview || preview == null) return; 
        
        selectedPreview = preview;
        selectFrame.gameObject.SetActive(true);

        if (selectionFrameTween != null)
        {
            selectionFrameTween.Kill();
        }

        selectionFrameTween = selectFrame.DOMove(preview.transform.position, 0.3f).SetEase(Ease.OutElastic);

        SelectedPreviewChangedEvent?.Invoke();
    }
}
