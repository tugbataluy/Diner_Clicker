using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaiterSpawnControl : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Level0" || other.tag == "Level1")
        {
           other.GetComponent<WaiterBehaviour>().isFree = true;
           //balladali
        }
    }
}
