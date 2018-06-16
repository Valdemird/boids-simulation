using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerScript : MonoBehaviour {
    CreatorScript creatorScript;
    float velocidad;
    float distanciaPercepcion;
    public Slider velocitySlider;
    public Slider radioSlider;
    public Toggle perceptionToggle;
    public Text cantidadLabel;
    public Text velocidadLabel;
    public Text radioLabel;
    public Slider cantidadSlider;



    // Use this for initialization
    void Start () {
        velocidad = 2;
        distanciaPercepcion = 3;
        creatorScript = gameObject.GetComponent<CreatorScript>();
        creatorScript.StartSimulation();


    }

    public void SetVelocitySlider() {
        creatorScript.setVelocity(velocitySlider.value);
        velocidadLabel.text = "VELOCIDAD: " + velocitySlider.value;
    }

    public void setDistanciaCritica()
    {
        creatorScript.setRadioColision(radioSlider.value);
        radioLabel.text = "REPELENCIA: " + radioSlider.value;
    }

    public void SetPerceptionSlider()
    {
        if (perceptionToggle.isOn)
        {
            creatorScript.setPerception(4);
        }
        else {
            creatorScript.setPerception(0);
        }
        
    }

    public void setCantidad() {

        creatorScript.cantidad = (int)cantidadSlider.value;
        cantidadLabel.text = "CANTIDAD: " + cantidadSlider.value;
    }


    public void resetSimulation() {
        creatorScript.ResetSimulation();
        SetPerceptionSlider();
        setDistanciaCritica();
        SetVelocitySlider();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
