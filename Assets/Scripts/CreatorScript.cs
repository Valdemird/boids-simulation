using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatorScript : MonoBehaviour {

    public int cantidad;
    public GameObject boidTemplate;
    public GameObject depredadorTemplate;
    private SphereCollider sc;
    public List<GameObject> boids;
    public GameObject meta;
    // Use this for initialization
    void Start()
    {


    }

    public void StartSimulation() {
        boids = new List<GameObject>();
        Vector3 posicion = Vector3.zero;
        GameObject tmp = null;
        sc = gameObject.GetComponent<SphereCollider>();
        for (int i = 0; i < cantidad; i++)
        {

            posicion = new Vector3(transform.position.x + Random.Range(-sc.radius, sc.radius), transform.position.y + Random.Range(-sc.radius, sc.radius), transform.position.z + Random.Range(-sc.radius, sc.radius));
            tmp = Instantiate(boidTemplate, posicion, Random.rotation) as GameObject;
            boids.Add(tmp);
            boidScript bScript = tmp.GetComponent<boidScript>();
            bScript.boids = boids;
        }

       
    }


    public void ResetSimulation() {
        foreach (GameObject boid in boids) {
            Destroy(boid);
        }
        boids.Clear();
        StartSimulation();
    }

    public void setVelocity(float velocity) {
        foreach (GameObject boid in boids)
        {
            if (boid.tag.Equals("boid")) {
                boid.GetComponent<boidScript>().velocidad = velocity;
            }
            
        }
    }

    public void setPerception(float nuevoRango)
    {
        foreach (GameObject boid in boids)
        {
            if (boid.tag.Equals("boid"))
            {
                boid.GetComponent<boidScript>().rangoSentido = nuevoRango;
            }

        }
    }


    public void setRadioColision(float nuevoRadio)
    {
        foreach (GameObject boid in boids)
        {
            if (boid.tag.Equals("boid"))
            {
                boid.GetComponent<boidScript>().distanciaCritica = nuevoRadio;
            }

        }
    }

    public void setCantidad(float nuevaCantidad)
    {

        foreach (GameObject boid in boids)
        {
            if (boid.tag.Equals("boid"))
            {
                boid.GetComponent<boidScript>().distanciaCritica = nuevaCantidad;
            }

        }
    }


    public void crearDepredador() {
        StartCoroutine(cicloDepredador());
    }

    public IEnumerator cicloDepredador() {
        Vector3 posicion = new Vector3(transform.position.x + Random.Range(-sc.radius, sc.radius), transform.position.y + Random.Range(-sc.radius, sc.radius), transform.position.z + Random.Range(-sc.radius, sc.radius));
        Vector3 direccion = PosicionPromedio() - posicion;
        GameObject tmp = Instantiate(depredadorTemplate, posicion, Quaternion.LookRotation(direccion)) as GameObject;
        boids.Add(tmp);
        depredadorScript dScript = tmp.GetComponent<depredadorScript>();
        dScript.boids = boids;
        yield return new WaitForSeconds(10f);
        boids.Remove(tmp);
        Destroy(tmp);
    }

    public Vector3 PosicionPromedio() {
        Vector3 posicionPromedio = Vector3.zero;
        int cantidadBoids = 0;
        foreach (GameObject boid in boids) {
            if (boid.tag.Equals("boid")) {
                cantidadBoids++;
                posicionPromedio = posicionPromedio + boid.transform.position;
            }
        }

        return posicionPromedio/cantidadBoids;
    }

    // Update is called once per frame
    void Update()
    {


    }
}
