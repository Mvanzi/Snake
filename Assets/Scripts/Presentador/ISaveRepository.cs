using System.Collections.Generic;

public class DatosPartidaGuardada
{
    public IReadOnlyList<Coordinate> Paredes { get; }
    public IReadOnlyList<Coordinate> CuerpoSerpiente { get; }
    public Coordinate Fruta { get; }
    public IDirection Direccion { get; }

    public DatosPartidaGuardada(IReadOnlyList<Coordinate> paredes, IReadOnlyList<Coordinate> cuerpoSerpiente, Coordinate fruta, IDirection direccion)
    {
        Paredes = paredes;
        CuerpoSerpiente = cuerpoSerpiente;
        Fruta = fruta;
        Direccion = direccion;
    }
}

public interface ISaveRepository
{
    bool HayPartidaGuardada();
    void Guardar(DatosPartidaGuardada datos);
    DatosPartidaGuardada Cargar();
    void BorrarPartida();
}

