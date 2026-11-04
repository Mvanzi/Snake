using System.Collections.Generic;

public class DatosPartidaGuardada
{
    public IReadOnlyList<PosicionTablero> Paredes { get; }
    public IReadOnlyList<PosicionTablero> CuerpoSerpiente { get; }
    public PosicionTablero Fruta { get; }
    public string Direccion { get; }

    public DatosPartidaGuardada(IReadOnlyList<PosicionTablero> paredes, IReadOnlyList<PosicionTablero> cuerpoSerpiente, PosicionTablero fruta, string direccion)
    {
        Paredes = paredes;
        CuerpoSerpiente = cuerpoSerpiente;
        Fruta = fruta;
        Direccion = direccion;
    }
}

