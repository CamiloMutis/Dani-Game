using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [Header("Velocidad de desplazamiento constante")]
    [SerializeField] private float velocidadScroll = 0.1f;

    private Material fondoMaterial;
    private Vector2 offset;

    private void Start()
    {
        fondoMaterial = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        // Mueve el offset todo el tiempo hacia la izquierda
        offset = new Vector2(Time.time * velocidadScroll, 0);
        fondoMaterial.mainTextureOffset = offset;
    }
}
