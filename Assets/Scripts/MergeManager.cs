using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    [SerializeField] WaiterManager waiterManager;
    public List<GameObject> mergeWaiters = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WaiterBehaviour>().isMerge)
        {
            other.gameObject.GetComponent<WaiterBehaviour>().isMerge = false;
            mergeWaiters.Add(other.gameObject);

            if (mergeWaiters.Count > 1)
            {
                for (int i = 0; i < 2; i++)
                {
                    Destroy(mergeWaiters[0]);
                    mergeWaiters.Remove(mergeWaiters[0]);
                }
                int level=other.gameObject.GetComponent<WaiterBehaviour>().mergeId;
                level++;
                waiterManager.CreateWaiterSecond(level, transform);

                Time.timeScale = 1.0f;
            }
        }
    }
}
