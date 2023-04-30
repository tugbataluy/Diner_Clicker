using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenManager : MonoBehaviour
{
    public List<GameObject> kitchens = new List<GameObject>();
    public List<GameObject> foods = new List<GameObject>();
    public List <float> foodPrices=new List<float>(); 
    public int kitchensAmount = 0;
    [SerializeField] int kitchenUpgradeButtonId=2;
    
    UpgradeManager upgradeManager;
    void Start()
    {
        upgradeManager=UpgradeManager.Instance;
        foreach (GameObject go in kitchens)
        {
            go.SetActive(false);
        }
        SpawnKitchen();
    }

    public void SpawnKitchen(bool isFirst=true)
    {
         
        if (kitchensAmount < kitchens.Count)
        {
            kitchens[kitchensAmount].SetActive(true);
           
            kitchensAmount++;
            
        }
        
      
       if(!isFirst){
            upgradeManager.ButtonValueUpdate(kitchenUpgradeButtonId);
            upgradeManager.foodLevel++;
            if(upgradeManager.foodLevel>4){
                upgradeManager.generalMultiplier+=0.05f;
                Illuminate();
                
            }
            
        }
    }

    void Illuminate(){
      foreach(GameObject kitchen in kitchens){
        kitchen.GetComponent<KitchenBehaviour>().particle.Play();
      }
    }
}