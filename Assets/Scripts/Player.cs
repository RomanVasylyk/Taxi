using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rychlost;
    private bool plny;
    private int početKlientov; 
    private SpriteRenderer spriteRenderer; 
    private float prejdenáVzdialenosť;
    private float čas; 
    private float časZačiatok;
    void Start()
    {
        rychlost = 0.05f;
        plny = false;
        početKlientov = 0; 
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        prejdenáVzdialenosť = 0f;
        čas = 0f;
        časZačiatok = 0f;
    }

    void Update()
    {
        float rotacia = Input.GetAxis("Horizontal");
        float posun = Input.GetAxis("Vertical") * rychlost;
        
        transform.Rotate(0,0,-rotacia);
        transform.Translate(0,posun,0); 

        if (plny) {
            prejdenáVzdialenosť += Mathf.Abs(posun);
            čas += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "client") {
            if (!plny) {
                string myname = other.gameObject.name; 
                Debug.Log("mam klienta"); 
                Destroy(other.gameObject, 0.5f);
                plny = true;
                početKlientov++; 
                Debug.Log("Počet odvezených klientov: " + početKlientov); 
                spriteRenderer.color = Color.red; 

                časZačiatok = Time.time;
            } else if(plny){
                Debug.Log("uz mam klienta"); 
            }
        }
        if (other.gameObject.tag == "Stadion") {
            if (plny) {
                Debug.Log("vylozil som"); 
                plny = false;
                spriteRenderer.color = Color.white; 

                float časJazdy = Time.time - časZačiatok;
                Debug.Log("Prejdená vzdialenosť: " + prejdenáVzdialenosť + " jednotiek");
                Debug.Log("Čas jazdy: " + časJazdy + " sekúnd");
                
                prejdenáVzdialenosť = 0f;
                čas = 0f;
                časZačiatok = 0f;
            }
            
        }
        if (other.gameObject.tag == "parking") {
            rychlost = 0.01f;
        }   
        
    } 
    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "parking") {
            rychlost = 0.05f;
        }
    }
}
