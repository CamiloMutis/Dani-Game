using System.Collections.Generic;
using UnityEngine;

public class FondoRepetible : MonoBehaviour
{
    [Header("Prefabs y configuración")]
    [SerializeField] private GameObject fondoPrefab;
    [SerializeField] private int cantidadInicial = 3;
    [SerializeField] private float anchoFondo = 20f; // ancho del fondo en unidades
    [SerializeField] private Transform objetivo; // puede ser Sonic o la cámara

    private Queue<GameObject> fondos = new Queue<GameObject>();

    private void Start()
    {
        Vector3 posicionInicial = transform.position;

        // Crear copias iniciales alineadas horizontalmente
        for (int i = 0; i < cantidadInicial; i++)
        {
            GameObject fondo = Instantiate(fondoPrefab, posicionInicial + Vector3.right * anchoFondo * i, Quaternion.identity, transform);
            fondos.Enqueue(fondo);
        }
    }

    private void Update()
    {
        GameObject fondoFrontal = fondos.Peek();

        // Si el objetivo ha avanzado más allá del centro del fondo frontal
        if (objetivo.position.x > fondoFrontal.transform.position.x + anchoFondo)
        {
            // Sacar el fondo más viejo
            GameObject fondoAntiguo = fondos.Dequeue();

            // Moverlo al frente
            GameObject fondoUltimo = fondos.Peek();
            Vector3 nuevaPosicion = fondoUltimo.transform.position + Vector3.right * anchoFondo;
            fondoAntiguo.transform.position = nuevaPosicion;

            fondos.Enqueue(fondoAntiguo);
        }
    }
}
