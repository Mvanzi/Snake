using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaveRepository : ISaveRepository
{
    private const string SAVE_KEY = "SNAKE_SAVED_GAME_STATE";

    [Serializable]
    private struct CoordenadaDTO
    {
        public int x;
        public int z;

        public CoordenadaDTO(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
    }

    [Serializable]
    private class PartidaDTO
    {
        public CoordenadaDTO fruta;
        public string direccion;
        public List<CoordenadaDTO> serpiente = new List<CoordenadaDTO>();
        public List<CoordenadaDTO> paredes = new List<CoordenadaDTO>();
    }

    public bool HayPartidaGuardada()
    {
        return PlayerPrefs.HasKey(SAVE_KEY);
    }

    public void Guardar(DatosPartidaGuardada datos)
    {
        if (datos == null) return;

        PartidaDTO dto = new PartidaDTO();
        dto.fruta = new CoordenadaDTO(datos.Fruta.X, datos.Fruta.Z);
        dto.direccion = datos.Direccion;

        for (int i = 0; i < datos.CuerpoSerpiente.Count; i++)
        {
            dto.serpiente.Add(new CoordenadaDTO(datos.CuerpoSerpiente[i].X, datos.CuerpoSerpiente[i].Z));
        }

        for (int i = 0; i < datos.Paredes.Count; i++)
        {
            dto.paredes.Add(new CoordenadaDTO(datos.Paredes[i].X, datos.Paredes[i].Z));
        }

        string json = JsonUtility.ToJson(dto);
        PlayerPrefs.SetString(SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public DatosPartidaGuardada Cargar()
    {
        if (!HayPartidaGuardada()) return null;

        string json = PlayerPrefs.GetString(SAVE_KEY);
        if (string.IsNullOrEmpty(json)) return null;

        PartidaDTO dto = JsonUtility.FromJson<PartidaDTO>(json);
        if (dto == null) return null;

        List<PosicionTablero> paredes = new List<PosicionTablero>(dto.paredes.Count);
        for (int i = 0; i < dto.paredes.Count; i++)
        {
            paredes.Add(new PosicionTablero(dto.paredes[i].x, dto.paredes[i].z));
        }

        List<PosicionTablero> cuerpo = new List<PosicionTablero>(dto.serpiente.Count);
        for (int i = 0; i < dto.serpiente.Count; i++)
        {
            cuerpo.Add(new PosicionTablero(dto.serpiente[i].x, dto.serpiente[i].z));
        }

        PosicionTablero fruta = new PosicionTablero(dto.fruta.x, dto.fruta.z);
        string direccion = dto.direccion;

        return new DatosPartidaGuardada(paredes, cuerpo, fruta, direccion);
    }

    public void BorrarPartida()
    {
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
    }
}
