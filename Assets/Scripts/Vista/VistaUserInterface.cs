using System;
using UnityEngine;
using UnityEngine.UI;

public class VistaUserInterface : MonoBehaviour, IUIView
{
    public GameObject panelPerdiste;
    public GameObject panelPausa;
    public Button restartButton;
    public Button resumeButton;

    public event Action OnRestartClicked;
    public event Action OnResumeClicked;

    private void Awake()
    {
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        }
    }

    public void ShowGameOver()
    {
        if (panelPerdiste != null)
        {
            panelPerdiste.SetActive(true);
        }
    }

    public void HideGameOver()
    {
        if (panelPerdiste != null)
        {
            panelPerdiste.SetActive(false);
        }
    }

    public void ShowPause()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }
    }

    public void HidePause()
    {
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }
}