using UnityEngine;

public class SnakeBootstrapper : MonoBehaviour
{
    [SerializeField] private VistaJuego vistaJuego;
    [SerializeField] private VistaUserInterface vistaUI;
    [SerializeField] private InputKey inputKey;
    [SerializeField] private Ticks ticks;

    private SnakePresenter _presenter;

    public SnakePresenter Presenter => _presenter;

    private void Awake()
    {
        if (vistaJuego == null)
        {
            vistaJuego = FindObjectOfType<VistaJuego>();
            if (vistaJuego == null)
            {
                vistaJuego = gameObject.AddComponent<VistaJuego>();
            }
        }

        if (vistaUI == null)
        {
            vistaUI = FindObjectOfType<VistaUserInterface>();
            if (vistaUI == null)
            {
                vistaUI = gameObject.AddComponent<VistaUserInterface>();
            }
        }

        if (inputKey == null)
        {
            inputKey = FindObjectOfType<InputKey>();
            if (inputKey == null)
            {
                inputKey = gameObject.AddComponent<InputKey>();
            }
        }

        if (ticks == null)
        {
            ticks = FindObjectOfType<Ticks>();
            if (ticks == null)
            {
                ticks = gameObject.AddComponent<Ticks>();
            }
        }
    }

    private void Start()
    {
        IGameContext modelo = new GameModel();
        ISaveRepository saveRepository = new PlayerPrefsSaveRepository();
        _presenter = new SnakePresenter(modelo, vistaJuego, vistaUI, inputKey, ticks, saveRepository);
        _presenter.Iniciar();
    }

    private void OnDestroy()
    {
        if (_presenter != null)
        {
            _presenter.DesuscribirEventos();
        }
    }
}
