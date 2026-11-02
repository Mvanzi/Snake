using System;

public interface IUIView
{
    event Action OnStartClicked;
    event Action OnRestartClicked;
    event Action OnResumeClicked;
    event Action OnContinueClicked;
    event Action OnSaveClicked;

    void ShowMenu();
    void HideMenu();
    void ShowGameOver();
    void HideGameOver();
    void ShowPause();
    void HidePause();
    void SetContinueButtonActive(bool active);
    void MostrarMensajeGuardado();
}