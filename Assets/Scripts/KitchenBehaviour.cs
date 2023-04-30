using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenBehaviour : MonoBehaviour
{

    public ParticleSystem particle;
    void OnEnable(){
        particle.gameObject.SetActive(true);
       particle.Play();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
