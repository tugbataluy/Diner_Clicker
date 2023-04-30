using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientCustomizer : MonoBehaviour
{
    [SerializeField] GameObject[] hairs;
    [SerializeField] GameObject[] upperBody;
    [SerializeField] GameObject[] lowerBody;
    [SerializeField] GameObject[] shoesL;
    [SerializeField] GameObject[] shoesR;
    void Start()
    {
        Picker(hairs);
        Picker(upperBody);
        Picker(lowerBody);
    }

    void Picker( GameObject[] objects){
        foreach( GameObject go in objects){
            go.SetActive(false);
        }
        objects[Random.Range(0,objects.GetLength(0))].SetActive(true);
    }
    void ShoePicker(GameObject[] shoe){

        for(int i=0;i<shoe.Length;i++)
        {
            shoesL[i].SetActive(false);
            shoesR[i].SetActive(false);
        }

            int pickIndex=Random.Range(0,shoe.GetLength(0));
            shoesL[pickIndex].SetActive(true);
            shoesR[pickIndex].SetActive(true);
    }
    

}
