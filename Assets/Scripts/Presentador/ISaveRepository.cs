public interface ISaveRepository
{
    bool HayPartidaGuardada();
    void Guardar(DatosPartidaGuardada datos);
    DatosPartidaGuardada Cargar();
    void BorrarPartida();
}
