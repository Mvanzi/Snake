using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class VistaUserInterface : MonoBehaviour, IUIView
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelPerdiste;
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject cartelGuardadoExitoso;

    [Header("Botones")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button saveButton;

    public event Action OnStartClicked;
    public event Action OnRestartClicked;
    public event Action OnResumeClicked;
    public event Action OnContinueClicked;
    public event Action OnSaveClicked;

    private float _mensajeGuardadoTimer;

    private void Awake()
    {
        if (startButton != null)
        {
            startButton.onClick.AddListener(() => OnStartClicked?.Invoke());
        }

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(() => OnContinueClicked?.Invoke());
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(() => OnRestartClicked?.Invoke());
        }

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        }

        if (saveButton != null)
        {
            saveButton.onClick.AddListener(() => OnSaveClicked?.Invoke());
        }
    }

    private void Update()
    {
        if (cartelGuardadoExitoso != null && cartelGuardadoExitoso.activeSelf)
        {
            _mensajeGuardadoTimer -= Time.unscaledDeltaTime;
            if (_mensajeGuardadoTimer <= 0f)
            {
                cartelGuardadoExitoso.SetActive(false);
            }
        }
    }

    public void ShowMenu()
    {
        if (panelMenu != null)
        {
            panelMenu.SetActive(true);
        }
    }

    public void HideMenu()
    {
        if (panelMenu != null)
        {
            panelMenu.SetActive(false);
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

    public void SetContinueButtonActive(bool active)
    {
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(active);
        }
    }

    public void MostrarMensajeGuardado()
    {
        if (cartelGuardadoExitoso != null)
        {
            cartelGuardadoExitoso.SetActive(true);
            _mensajeGuardadoTimer = 2f;
        }
    }
}