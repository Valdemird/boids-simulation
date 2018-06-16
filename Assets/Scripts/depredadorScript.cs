using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class depredadorScript : MonoBehaviour
{
    public List<GameObject> boids;
    public float velocidad;
    private Vector3 direccion;
    public float rangoSentido;
    public float velocidadAngular;
    public float eficiencia;
    // Use this for initialization
    void Update()
    {

        
        transform.Translate(0, 0, Time.deltaTime * velocidad);
    }

    public void aplicarReglas()
    {
        direccion = Vector3.zero;
        int boidsCercanos = 0;
        if (boids.Count != 0)
        {
            Vector3 objective = Vector3.zero;
            float lastDistance = Mathf.Infinity;
            foreach (GameObject boid in boids)
            {
                float distancia = Vector3.Distance(boid.transform.position, transform.position);
                    if (boid.tag.Equals("boid") && distancia < rangoSentido && !boid.Equals(gameObject))
                    {
                        objective = objective + boid.transform.position;
                    boidsCercanos++;
                    }

            }

            objective = (objective / boidsCercanos) - transform.position;
            direccion = objective;
            eficiencia = Vector3.Distance(direccion, transform.position);
            Debug.DrawRay(transform.position, direccion, Color.green);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), velocidadAngular * Time.deltaTime);

        }
    }
}
