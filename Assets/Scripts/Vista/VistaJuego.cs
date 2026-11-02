using System.Collections.Generic;
using UnityEngine;

public class VistaJuego : MonoBehaviour, IGameView
{
    [Header("Referencias de Escena")]
    public GameObject camarita;
    public GameObject plano;

    [Header("Prefabs Opcionales (si estan vacios, usa primitivas)")]
    [SerializeField] private GameObject prefabCuerpoSerpiente;
    [SerializeField] private GameObject prefabPared;
    [SerializeField] private GameObject prefabFruta;

    [Header("Rendimiento y Suavizado")]
    [SerializeField] private int targetFPS = 60;
    [SerializeField] private float suavizadoCamara = 8f;
    [SerializeField] private float suavizadoSerpiente = 18f;

    private readonly List<GameObject> partesGraficasSerpiente = new List<GameObject>();
    private readonly List<Vector3> _posicionesObjetivoSerpiente = new List<Vector3>();
    private readonly List<GameObject> partesGraficasPared = new List<GameObject>();
    private GameObject laFrutita;
    private Vector3 _objetivoCamara;
    private bool _camaraInicializada = false;

    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS > 0 ? targetFPS : 60;

        if (plano == null)
        {
            plano = GameObject.Find("Plane") ?? GameObject.Find("Plano");
            if (plano == null)
            {
                plano = GameObject.CreatePrimitive(PrimitiveType.Plane);
                plano.name = "Plane";
                plano.transform.position = new Vector3(20f, -0.5f, 20f);
                plano.transform.localScale = new Vector3(4f, 1f, 4f);
            }
        }

        if (camarita == null)
        {
            if (Camera.main != null)
            {
                camarita = Camera.main.gameObject;
            }
            else
            {
                camarita = GameObject.Find("Main Camera");
            }
        }

        if (plano != null)
        {
            plano.transform.localScale = new Vector3(4f, 1f, 4f);
        }
    }

    public void RenderWalls(IEnumerable<PosicionTablero> coordenadasParedes)
    {
        foreach (PosicionTablero pos in coordenadasParedes)
        {
            GameObject pared = CrearInstancia(prefabPared, PrimitiveType.Cube, Color.black);
            pared.transform.position = new Vector3(pos.X, 0f, pos.Z);
            pared.transform.SetParent(this.transform);
            partesGraficasPared.Add(pared);
        }
    }

    public void RenderFruit(PosicionTablero posicionFruta)
    {
        Vector3 pos = new Vector3(posicionFruta.X, 0f, posicionFruta.Z);

        if (laFrutita == null)
        {
            laFrutita = CrearInstancia(prefabFruta, PrimitiveType.Cube, Color.yellow);
            laFrutita.transform.SetParent(this.transform);
        }

        laFrutita.transform.position = pos;
    }

    private void Update()
    {
        // 1. Interpolacion suave de la camara a 60+ FPS
        if (camarita != null && _camaraInicializada)
        {
            camarita.transform.position = Vector3.Lerp(camarita.transform.position, _objetivoCamara, suavizadoCamara * Time.deltaTime);
            camarita.transform.rotation = Quaternion.Euler(54f, 0f, 0f);
        }

        // 2. Interpolacion suave de los cubos de la serpiente a 60+ FPS
        for (int i = 0; i < partesGraficasSerpiente.Count && i < _posicionesObjetivoSerpiente.Count; i++)
        {
            GameObject parte = partesGraficasSerpiente[i];
            if (parte != null)
            {
                parte.transform.position = Vector3.Lerp(
                    parte.transform.position,
                    _posicionesObjetivoSerpiente[i],
                    suavizadoSerpiente * Time.deltaTime
                );
            }
        }
    }

    public void RenderSnake(IReadOnlyList<PosicionTablero> posicionesSerpiente)
    {
        // 1. Instanciamos nuevos elementos si la serpiente crecio
        while (partesGraficasSerpiente.Count < posicionesSerpiente.Count)
        {
            GameObject nuevoSegmento = CrearInstancia(prefabCuerpoSerpiente, PrimitiveType.Cube, Color.red);
            nuevoSegmento.transform.SetParent(this.transform);

            // El nuevo segmento nace en la cola (indice 0), exactamente en su posicion para evitar que vuele desde la esquina (0,0,0)
            Vector3 posCola = new Vector3(posicionesSerpiente[0].X, 0f, posicionesSerpiente[0].Z);
            nuevoSegmento.transform.position = posCola;

            partesGraficasSerpiente.Insert(0, nuevoSegmento);
            _posicionesObjetivoSerpiente.Insert(0, posCola);
        }

        // 2. Destruimos elementos si la serpiente se achico (removiendo desde la cola)
        while (partesGraficasSerpiente.Count > posicionesSerpiente.Count)
        {
            Destroy(partesGraficasSerpiente[0]);
            partesGraficasSerpiente.RemoveAt(0);
            _posicionesObjetivoSerpiente.RemoveAt(0);
        }

        // 3. Asignamos los objetivos a cada segmento
        for (int i = 0; i < posicionesSerpiente.Count; i++)
        {
            Vector3 destino = new Vector3(
                posicionesSerpiente[i].X,
                0f,
                posicionesSerpiente[i].Z
            );
            _posicionesObjetivoSerpiente[i] = destino;

            // Si es la primera vez que se renderiza, posicionamos de forma instantanea
            if (!_camaraInicializada)
            {
                partesGraficasSerpiente[i].transform.position = destino;
            }
        }

        if (posicionesSerpiente.Count > 0 && camarita != null)
        {
            PosicionTablero cabeza = posicionesSerpiente[posicionesSerpiente.Count - 1];
            _objetivoCamara = new Vector3(cabeza.X, 20f, cabeza.Z - 15f);

            if (!_camaraInicializada)
            {
                camarita.transform.position = _objetivoCamara;
                camarita.transform.rotation = Quaternion.Euler(54f, 0f, 0f);
                _camaraInicializada = true;
            }
        }
    }

    public void ClearAll()
    {
        foreach (GameObject parte in partesGraficasSerpiente)
        {
            Destroy(parte);
        }
        partesGraficasSerpiente.Clear();
        _posicionesObjetivoSerpiente.Clear();
        _camaraInicializada = false;

        foreach (GameObject pared in partesGraficasPared)
        {
            Destroy(pared);
        }
        partesGraficasPared.Clear();

        if (laFrutita != null)
        {
            Destroy(laFrutita);
            laFrutita = null;
        }
    }

    private GameObject CrearInstancia(GameObject prefab, PrimitiveType primitivaPorDefecto, Color colorPorDefecto)
    {
        if (prefab != null)
        {
            return Instantiate(prefab);
        }

        GameObject primitiva = GameObject.CreatePrimitive(primitivaPorDefecto);
        Renderer render = primitiva.GetComponent<Renderer>();
        if (render != null)
        {
            render.material.color = colorPorDefecto;
        }
        return primitiva;
    }
}