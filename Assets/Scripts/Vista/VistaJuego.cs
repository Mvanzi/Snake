using System.Collections.Generic;
using UnityEngine;

public class VistaJuego : MonoBehaviour, IGameView
{
    [Header("Referencias de Escena")]
    public GameObject camarita;
    public GameObject plano;

    [Header("Prefabs Opcionales (si están vacíos, usa primitivas)")]
    [SerializeField] private GameObject prefabCuerpoSerpiente;
    [SerializeField] private GameObject prefabPared;
    [SerializeField] private GameObject prefabFruta;

    private readonly List<GameObject> partesGraficasSerpiente = new List<GameObject>();
    private readonly List<GameObject> partesGraficasPared = new List<GameObject>();
    private GameObject laFrutita;

    private void Awake()
    {
        if (plano != null)
        {
            plano.transform.localScale = new Vector3(4f, 1f, 4f);
        }
    }

    public void RenderWalls(IEnumerable<BoardCoordinate> coordenadasParedes)
    {
        foreach (BoardCoordinate pos in coordenadasParedes)
        {
            GameObject pared = CrearInstancia(prefabPared, PrimitiveType.Cube, Color.black);
            pared.transform.position = new Vector3(pos.X, 0f, pos.Z);
            pared.transform.SetParent(this.transform);
            partesGraficasPared.Add(pared);
        }
    }

    public void RenderFruit(BoardCoordinate posicionFruta)
    {
        Vector3 pos = new Vector3(posicionFruta.X, 0f, posicionFruta.Z);

        if (laFrutita == null)
        {
            laFrutita = CrearInstancia(prefabFruta, PrimitiveType.Cube, Color.yellow);
            laFrutita.transform.SetParent(this.transform);
        }

        laFrutita.transform.position = pos;
    }

    public void RenderSnake(IReadOnlyList<BoardCoordinate> posicionesSerpiente)
    {
        // 1. Instanciamos nuevos elementos si la serpiente creció
        while (partesGraficasSerpiente.Count < posicionesSerpiente.Count)
        {
            GameObject nuevoSegmento = CrearInstancia(prefabCuerpoSerpiente, PrimitiveType.Cube, Color.red);
            nuevoSegmento.transform.SetParent(this.transform);
            partesGraficasSerpiente.Add(nuevoSegmento);
        }

        // 2. Destruimos excedentes si se achicó o reinició
        while (partesGraficasSerpiente.Count > posicionesSerpiente.Count)
        {
            int ultimoIndice = partesGraficasSerpiente.Count - 1;
            Destroy(partesGraficasSerpiente[ultimoIndice]);
            partesGraficasSerpiente.RemoveAt(ultimoIndice);
        }

        // 3. Posicionamos cada segmento en (x, 0, z)
        for (int i = 0; i < posicionesSerpiente.Count; i++)
        {
            partesGraficasSerpiente[i].transform.position = new Vector3(
                posicionesSerpiente[i].X,
                0f,
                posicionesSerpiente[i].Z
            );
        }

        // 4. Seguimiento de cámara a la cabeza
        if (posicionesSerpiente.Count > 0 && camarita != null)
        {
            BoardCoordinate cabeza = posicionesSerpiente[posicionesSerpiente.Count - 1];
            Vector3 destinoCamara = new Vector3(cabeza.X, 20f, cabeza.Z - 15f);
            camarita.transform.position = destinoCamara;
        }
    }

    public void ClearAll()
    {
        foreach (GameObject parte in partesGraficasSerpiente)
        {
            Destroy(parte);
        }
        partesGraficasSerpiente.Clear();

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

    // Método auxiliar para desacoplar el origen del objeto gráfico
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