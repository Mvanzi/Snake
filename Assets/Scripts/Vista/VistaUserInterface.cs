using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class VistaUserInterface : MonoBehaviour, IUIView
{
    [Header("Paneles")]
    public GameObject panelMenu;
    public GameObject panelPerdiste;
    public GameObject panelPausa;

    [Header("Botones")]
    public Button startButton;
    public Button continueButton;
    public Button restartButton;
    public Button resumeButton;
    public Button saveButton;

    public event Action OnStartClicked;
    public event Action OnRestartClicked;
    public event Action OnResumeClicked;
    public event Action OnContinueClicked;
    public event Action OnSaveClicked;

    private bool _menuVisible;
    private bool _gameOverVisible;
    private bool _pauseVisible;
    private bool _continueButtonActive;
    private bool _mensajeGuardadoVisible;
    private float _mensajeGuardadoTimer;

    private void Awake()
    {
        if (panelPerdiste == null)
        {
            panelPerdiste = GameObject.Find("Panel");
        }

        if (panelPerdiste != null && restartButton == null)
        {
            restartButton = panelPerdiste.GetComponentInChildren<Button>();
        }

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
        if (_mensajeGuardadoVisible)
        {
            _mensajeGuardadoTimer -= Time.unscaledDeltaTime;
            if (_mensajeGuardadoTimer <= 0f)
            {
                _mensajeGuardadoVisible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R) && _gameOverVisible)
        {
            OnRestartClicked?.Invoke();
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && _menuVisible)
        {
            OnStartClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.C) && _menuVisible && _continueButtonActive)
        {
            OnContinueClicked?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.G) && _pauseVisible)
        {
            OnSaveClicked?.Invoke();
        }
    }

    public void ShowMenu()
    {
        _menuVisible = true;
        if (panelMenu != null)
        {
            panelMenu.SetActive(true);
        }
    }

    public void HideMenu()
    {
        _menuVisible = false;
        if (panelMenu != null)
        {
            panelMenu.SetActive(false);
        }
    }

    public void ShowGameOver()
    {
        _gameOverVisible = true;
        if (panelPerdiste != null)
        {
            panelPerdiste.SetActive(true);
        }
    }

    public void HideGameOver()
    {
        _gameOverVisible = false;
        if (panelPerdiste != null)
        {
            panelPerdiste.SetActive(false);
        }
    }

    public void ShowPause()
    {
        _pauseVisible = true;
        if (panelPausa != null)
        {
            panelPausa.SetActive(true);
        }
    }

    public void HidePause()
    {
        _pauseVisible = false;
        if (panelPausa != null)
        {
            panelPausa.SetActive(false);
        }
    }

    public void SetContinueButtonActive(bool active)
    {
        _continueButtonActive = active;
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(active);
        }
    }

    public void MostrarMensajeGuardado()
    {
        _mensajeGuardadoVisible = true;
        _mensajeGuardadoTimer = 2f;
    }

    private void OnGUI()
    {
        if (_menuVisible && panelMenu == null)
        {
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.fontSize = 15;
            boxStyle.alignment = TextAnchor.MiddleCenter;

            float boxHeight = _continueButtonActive ? 150f : 100f;
            Rect rect = new Rect(Screen.width / 2f - 220f, 20f, 440f, boxHeight);
            GUI.Box(rect, "<b>SNAKE MVP</b>\nPresiona W, A, S, D o Espacio para comenzar\n<i>(Haz click en la ventana Game para enfocar el teclado)</i>", boxStyle);

            if (GUI.Button(new Rect(Screen.width / 2f - 110f, 75f, 220f, 30f), "Comenzar desde el principio"))
            {
                OnStartClicked?.Invoke();
            }

            if (_continueButtonActive)
            {
                if (GUI.Button(new Rect(Screen.width / 2f - 110f, 115f, 220f, 30f), "Continuar guardada (C)"))
                {
                    OnContinueClicked?.Invoke();
                }
            }
        }
        else if (_gameOverVisible && panelPerdiste == null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.fontSize = 16;
            style.alignment = TextAnchor.MiddleCenter;

            Rect rect = new Rect(Screen.width / 2f - 180f, Screen.height / 2f - 60f, 360f, 120f);
            GUI.Box(rect, "<b>¡GAME OVER!</b>\n\nPresiona W, A, S, D, R o el botón para reiniciar", style);

            if (GUI.Button(new Rect(Screen.width / 2f - 60f, Screen.height / 2f + 15f, 120f, 35f), "Reiniciar"))
            {
                OnRestartClicked?.Invoke();
            }
        }
        else if (_pauseVisible && panelPausa == null)
        {
            GUIStyle style = new GUIStyle(GUI.skin.box);
            style.fontSize = 16;
            style.alignment = TextAnchor.MiddleCenter;

            Rect rect = new Rect(Screen.width / 2f - 140f, Screen.height / 2f - 85f, 280f, 170f);
            GUI.Box(rect, "<b>PAUSA</b>\nPresiona Escape para continuar", style);

            if (GUI.Button(new Rect(Screen.width / 2f - 100f, Screen.height / 2f - 25f, 200f, 30f), "Reanudar"))
            {
                OnResumeClicked?.Invoke();
            }

            if (GUI.Button(new Rect(Screen.width / 2f - 100f, Screen.height / 2f + 15f, 200f, 30f), "Guardar Partida (G)"))
            {
                OnSaveClicked?.Invoke();
            }

            if (_mensajeGuardadoVisible)
            {
                GUIStyle msgStyle = new GUIStyle(GUI.skin.label);
                msgStyle.fontSize = 13;
                msgStyle.fontStyle = FontStyle.Bold;
                msgStyle.alignment = TextAnchor.MiddleCenter;
                msgStyle.normal.textColor = Color.green;
                GUI.Label(new Rect(Screen.width / 2f - 130f, Screen.height / 2f + 50f, 260f, 25f), "¡Partida guardada con éxito!", msgStyle);
            }
        }
    }
}