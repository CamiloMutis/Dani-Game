using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
public class EstadoJugador
{
    public string nombre;
    public string escena;
    public Vector3 posicion;
}

public class GestorGuardado : MonoBehaviour
{
    [SerializeField] private Transform personajeObjetivo; // Arrástralo en el inspector o configúralo por código

    public void GuardarEstado()
    {
        if (personajeObjetivo == null)
        {
            Debug.LogWarning("❌ No se ha asignado ningún personaje objetivo.");
            return;
        }

        EstadoJugador estado = new EstadoJugador
        {
            nombre = personajeObjetivo.name,
            escena = SceneManager.GetActiveScene().name,
            posicion = personajeObjetivo.position
        };

        string json = JsonUtility.ToJson(estado);
        PlayerPrefs.SetString("EstadoJugador_" + estado.nombre, json);
        PlayerPrefs.Save();

        Debug.Log("💾 Estado guardado para '" + estado.nombre + "': " + json);
    }

    public void CargarEstado(string nombrePersonaje)
    {
        string json = PlayerPrefs.GetString("EstadoJugador_" + nombrePersonaje, "");
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogWarning("⚠️ No hay datos guardados para '" + nombrePersonaje + "'");
            return;
        }

        EstadoJugador estado = JsonUtility.FromJson<EstadoJugador>(json);
        GameObject obj = GameObject.Find(estado.nombre);
        if (obj != null)
        {
            obj.transform.position = estado.posicion;
            Debug.Log("✅ Estado restaurado para '" + estado.nombre + "' en escena: " + estado.escena);
        }
        else
        {
            Debug.LogWarning("❌ No se encontró el personaje '" + estado.nombre + "' para restaurar posición.");
        }
    }
}
