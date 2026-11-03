using System;
using System.Collections;
using UnityEngine;

public class VistaUserInterface : MonoBehaviour, IUIView
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelFondoDif;
    [SerializeField] private GameObject panelMenu;
    [SerializeField] private GameObject panelPerdiste;
    [SerializeField] private GameObject panelPausa;
    [SerializeField] private GameObject cartelGuardadoExitoso;

    public event Action OnStartClicked;
    public event Action OnRestartClicked;
    public event Action OnResumeClicked;
    public event Action OnContinueClicked;
    public event Action OnSaveClicked;

    private void Awake()
    {

        panelPerdiste?.SetActive(false);
        panelPausa?.SetActive(false);
        cartelGuardadoExitoso?.SetActive(false);

        ShowMenu();
    }

    private void Start()
    {
        ShowMenu();
    }

    public void ClickIniciar() => OnStartClicked?.Invoke();
    public void ClickReiniciar() => OnRestartClicked?.Invoke();
    public void ClickReanudar() => OnResumeClicked?.Invoke();
    public void ClickContinuar() => OnContinueClicked?.Invoke();
    public void ClickGuardar() => OnSaveClicked?.Invoke();

    public void ShowMenu() => ActivarPanel(panelMenu, true);
    public void HideMenu() => ActivarPanel(panelMenu, false);
    public void ShowGameOver() => ActivarPanel(panelPerdiste, true);
    public void HideGameOver() => ActivarPanel(panelPerdiste, false);
    public void ShowPause() => ActivarPanel(panelPausa, true);
    public void HidePause() => ActivarPanel(panelPausa, false);

    public void SetContinueButtonActive(bool active) { }

    public void MostrarMensajeGuardado() => StartCoroutine(RutinaCartel(2f));

    private IEnumerator RutinaCartel(float seg)
    {
        cartelGuardadoExitoso?.SetActive(true);
        yield return new WaitForSecondsRealtime(seg);
        cartelGuardadoExitoso?.SetActive(false);
    }

    private void ActivarPanel(GameObject panel, bool estado)
    {
        panel?.SetActive(estado);
        panelFondoDif?.SetActive(estado);
    }
}