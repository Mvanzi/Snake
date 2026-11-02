using UnityEngine;

public class SnakeBootstrapper : MonoBehaviour
{
    [Header("Vistas MVP (opcionales, se autodetectan si estan vacias)")]
    [SerializeField] private VistaJuego vistaJuego;
    [SerializeField] private VistaUserInterface vistaUI;
    [SerializeField] private InputKey inputKey;
    [SerializeField] private Ticks ticks;

    private SnakePresenter _presenter;

    public SnakePresenter Presenter => _presenter;

    private void Awake()
    {
        // 1. Obtener o agregar dinamicamente las vistas si no fueron asignadas
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
        // 2. Instanciar el Modelo en C# puro
        IGameContext modelo = new GameModel();

        // 3. Instanciar el Repositorio de guardado local (PlayerPrefs)
        ISaveRepository saveRepository = new PlayerPrefsSaveRepository();

        // 4. Instanciar el Presentador orquestador en C# puro con todas sus dependencias
        _presenter = new SnakePresenter(modelo, vistaJuego, vistaUI, inputKey, ticks, saveRepository);

        // 5. Iniciar el juego (MenuState)
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

