using UnityEngine;
using System.Collections.Generic;

public class DisparoBala : MonoBehaviour
{
    public Transform salida;
    public GameObject bala;
    public float tiempoDisparo = 0.5f;
    float proximoDisparo = 0f;

    public int tamanoPool = 20;
    private List<GameObject> poolBalas;

    //public AudioSource sonidoDisparo;
    //public AudioClip clipDisparo;  // Declarar el clip de audio como público

    public float volumen = 0.2f;

    void Start()
    {
        poolBalas = new List<GameObject>();
        for (int i = 0; i < tamanoPool; i++)
        {
            GameObject obj = Instantiate(bala);
            obj.SetActive(false);
            poolBalas.Add(obj);
        }

        /* Asignar el clip de audio manualmente
        if (clipDisparo != null)
        {
            sonidoDisparo.clip = clipDisparo;
            sonidoDisparo.volume = volumen;  // Ajustar el volumen aquí
        }
        else
        {
            Debug.LogWarning("No se ha asignado el clip de disparo.");
        }*/
    }

    public void Disparar()
    {
        if (Time.time > proximoDisparo)
        {
            Debug.Log("Intentando disparar...");
            GameObject balaDisponible = ObtenerBalaDisponible();
            if (balaDisponible != null)
            {
                Debug.Log("Disparo realizado");
                balaDisponible.transform.position = salida.position;
                balaDisponible.transform.rotation = salida.rotation;
                balaDisponible.SetActive(true);
                proximoDisparo = Time.time + tiempoDisparo;
            }
            else
            {
                Debug.Log("No hay balas disponibles en el pool.");
            }
        }
    }


    private GameObject ObtenerBalaDisponible()
    {
        for (int i = 0; i < poolBalas.Count; i++)
        {
            if (!poolBalas[i].activeInHierarchy)
            {
                return poolBalas[i];
            }
        }

        GameObject obj = Instantiate(bala);
        obj.SetActive(false);
        poolBalas.Add(obj);
        return obj;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Botón izquierdo del mouse
        {
            Disparar();
        }
    }
}
