using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Window : MonoBehaviour
{
    [SerializeField] protected CanvasGroup _canvasGroup;
    [SerializeField] protected Button _actionButton;

    private void OnEnable()
    {
        _actionButton.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _actionButton.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick();

    public void Open()
    {
        _canvasGroup.alpha = 1;
        _actionButton.interactable = true;
    }

    public void Close()
    {
        _canvasGroup.alpha = 0;
        _actionButton.interactable = false;
    }
}
