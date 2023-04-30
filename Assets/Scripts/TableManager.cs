using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    public WaiterManager waiterManager;
    public List<GameObject> tables = new List<GameObject>();
    public int tableAmount = 0;

    public static TableManager Instance;
    UpgradeManager upgradeManager;
    
    [SerializeField] int tableUpgradeButtonId=0;

    void Awake(){
        Instance=this;
    }
    void Start()
    {
        upgradeManager=UpgradeManager.Instance;
        foreach (GameObject go in tables)
        {
            go.SetActive(false);
        }
        SpawnTable();
    }

    public void SpawnTable(bool isFirst=true)
    {
        if (tableAmount < tables.Count)
        {
            tables[tableAmount].SetActive(true);
            tableAmount++;
        }

        if(!isFirst)
        {
            upgradeManager.ButtonValueUpdate(tableUpgradeButtonId);
            upgradeManager.tableLevel++;
            if(upgradeManager.tableLevel>4){
                upgradeManager.generalMultiplier+=0.05f;
                Illuminate();
            }
        }
    }
    
    public void MergeDisable()
    {
        for (int i = 0; i < tables.Count; i++)
        {
            tables[i].GetComponent<TableStatus>().isMerge = true;
            tables[i].GetComponent<TableStatus>().ClearTable();
        }
    }

    public void MergeEnable()
    {
        for (int i = 0; i < tables.Count; i++)
        {
            if(tables[i].activeSelf)
            {tables[i].GetComponent<TableStatus>().isMerge = false;
            tables[i].GetComponent<TableStatus>().ActiveTable();}
        }
    }
     void Illuminate(){
      foreach(GameObject tab in tables){
        tab.transform.GetChild(tab.transform.childCount-4).gameObject.SetActive(true);
        tab.transform.GetChild(tab.transform.childCount-4).gameObject.GetComponent<ParticleSystem>().Play();
      }
    }
}