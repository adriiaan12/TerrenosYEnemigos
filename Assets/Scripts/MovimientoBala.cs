using UnityEngine;

public class MovimientoBala : MonoBehaviour
{
    public float velocidad = 10f;
    
    public LayerMask collisionMask;
    void Start()
    {
        
    }

    
    void Update()
    {

        float moveDistance = velocidad * Time.deltaTime;
        transform.Translate(Vector3.forward * moveDistance);
        Checkcollision(moveDistance);



    }
    private void Checkcollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,moveDistance))
        {
            OnHitObject(hit);
        }
    }

    private void OnHitObject(RaycastHit hit)
    {
        gameObject.SetActive(false);
        if(hit.collider.gameObject.layer == 8)
        {
            MovimientoEnemigo enemigo = hit.collider.GetComponent<MovimientoEnemigo>();
            if(enemigo != null)
            {
                enemigo.takehit(1);
            }
        }



    }


    void OnBecomeInvisible()
    {
        gameObject.SetActive(false);
    }


}
