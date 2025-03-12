using UnityEngine;




public class EnemigoMovimiento : MonoBehaviour
{
        UnityEngine.AI.NavMeshAgent agente;
    public Transform objetivo;
    
    void Start()
    {
        agente = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    
    void Update()
    {
        agente.destination = objetivo.position;
    }
}
