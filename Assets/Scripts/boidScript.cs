using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidScript : MonoBehaviour {
  
    public List<GameObject> boids;
    public float velocidad;
    private Vector3 direccion;
    public float rangoSentido;
    public float rangoSentidoDepredador;
    public float distanciaCritica;
    public float velocidadAngular;
    private Material material;

    // Use this for initialization
    void Start () {
        direccion = Vector3.forward;
        material = gameObject.GetComponentInChildren<Renderer>().material;

    }


    public void  aplicarReglas() {
        bool repulsion = false;
        bool repulsionDepredador = false;
        Vector3 puntoDeMasa = Vector3.zero;
        Vector3 puntoRepulsion = Vector3.zero;
        Vector3 direccionPromedio = Vector3.zero;
        Vector3 puntoRepulsionDepredador = Vector3.zero;

        int boidsCercanos = 0;
        if (boids.Count != 0)
        {
            direccion = Vector3.forward;
            foreach (GameObject boid in boids)
            {

                float distancia = Vector3.Distance(boid.transform.position, transform.position);
                float angulo = Vector3.Angle(boid.transform.position, transform.position);

                if (distancia <= rangoSentidoDepredador  ) {
                    if (boid.tag.Equals("Player") || boid.tag.Equals("limits"))
                    {
                        repulsionDepredador = true;
                        puntoRepulsionDepredador = puntoRepulsion + (transform.position - boid.transform.position);
                    }
                }
                
                if (distancia <= rangoSentido && !boid.Equals(gameObject) )
                {
                    if (boid.tag.Equals("boid") )
                    {
                        if (distancia <= rangoSentido) {
                            boidScript boidScript = gameObject.GetComponent<boidScript>();
                            direccionPromedio = direccion + boidScript.direccion;
                            //puntoDeMasa = puntoDeMasa + boid.transform.position;
                            boidsCercanos++;
                        }
                    }

                    if (distancia <= distanciaCritica)
                    {
                        repulsion = true;
                        puntoRepulsion = puntoRepulsion + (transform.position - boid.transform.position);
                    }
                    else
                    {

                    }
                }
            }
            direccionPromedio = (direccionPromedio / boidsCercanos) - transform.position ;
            puntoDeMasa = (puntoDeMasa / boidsCercanos - transform.position);
            puntoRepulsion = puntoRepulsion;

            if (repulsionDepredador)
            {
                direccion = puntoRepulsionDepredador;
                //material.color = Color.red;
            }
            else {
                material.color = Color.white;
                direccion =  direccionPromedio;
                if (repulsion)
                {
                    direccion = direccion + puntoRepulsion;
                }
            }

            direccion = direccion - transform.position;
            direccion = direccion.normalized;
            if (direccion != Vector3.zero) {
                Debug.DrawRay(transform.position, puntoRepulsion, Color.blue);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), velocidadAngular * Time.deltaTime);
            }
            
            if (repulsionDepredador)
            {
                transform.Translate(0, 0, Time.deltaTime * velocidad *2f);
            }
            else {
                transform.Translate(0, 0, Time.deltaTime * velocidad);
            }
            
        }

    }
	
	// Update is called once per frame
	void Update () {
       aplicarReglas();
    }
}
