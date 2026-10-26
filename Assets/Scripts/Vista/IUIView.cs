using System;

public interface IUIView
{
    event Action OnStartClicked;
    event Action OnRestartClicked;
    event Action OnResumeClicked;

    void ShowMenu();
    void HideMenu();
    void ShowGameOver();
    void HideGameOver();
    void ShowPause();
    void HidePause();
}