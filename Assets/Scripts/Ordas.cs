using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Ordas : MonoBehaviour
{
    public ValoresEnemigos[] olaEnemigos;

    private ValoresEnemigos olaActual;
    float tiempoEspera = 0f;
    int numeroOlaActual = 0;
    int enemigosPorCrear = 0;
    int enemigosVivos = 0;

    void Start()
    {
        NextOla();
    }

    void Update()
    {
        if (enemigosPorCrear > 0 && Time.time > tiempoEspera)
        {
            enemigosPorCrear--;
            tiempoEspera = Time.time + olaActual.tiempoEnemigos;

            float xAleatorio = Random.Range(100f, 120f); // Ajusta el rango según necesites
            Vector3 posicionDeseada = new Vector3(xAleatorio, 6.006249f, 445.68f);
            GameObject enemigoGO = Instantiate(olaActual.tipoEnemigo, posicionDeseada, Quaternion.identity);
            enemigoGO.SetActive(true);


            // Contar enemigo vivo
            enemigosVivos++;

            // Obtener componente y suscribirse al evento de muerte
            LivingEntity enemigo = enemigoGO.GetComponent<LivingEntity>();
            if (enemigo != null)
            {
                enemigo.OnDeath += EnemigoMuerto;
            }
        }




    }

    void EnemigoMuerto()
    {
        enemigosVivos--;

        if (enemigosVivos <= 0 && enemigosPorCrear <= 0)
        {
            // Todos los enemigos han muerto, siguiente ola
            NextOla();
        }
    }

    void NextOla()
    {
        Debug.Log(SceneManager.GetActiveScene().name);

        // Obtener la escena actual
        string escenaActual = SceneManager.GetActiveScene().name;

        // Si estamos en "Perdiste" y no quedan más oleadas, ir a "HasGanado"
        if (escenaActual == "Perdiste" && numeroOlaActual >= olaEnemigos.Length)
        {
            SceneManager.LoadScene("HasGanado");
            return;
        }

        // Si estamos en "Ganar" y no quedan más oleadas, ir a "Perdiste"
        if (escenaActual == "Ganar" && numeroOlaActual >= olaEnemigos.Length)
        {
            SceneManager.LoadScene("Perdiste");
            return;
        }

        // Si estamos en otra escena y terminan las oleadas, no hacer nada
        if (numeroOlaActual >= olaEnemigos.Length)
        {
            Debug.Log("¡Todas las olas completadas!");
            return;
        }

        // Iniciar la siguiente ola
        Debug.Log("Iniciando ola " + (numeroOlaActual + 1));
        olaActual = olaEnemigos[numeroOlaActual];
        enemigosPorCrear = olaActual.numEnemigos;
        enemigosVivos = 0;
        numeroOlaActual++;
    }

}
