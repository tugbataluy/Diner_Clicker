using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WaiterManager : MonoBehaviour
{
    public TableManager tableManager;
    public static WaiterManager waiterManager;
    UpgradeManager upgradeManager;
    public List<WaiterBehaviour> waiters = new List<WaiterBehaviour>();
    public GameObject[] waiterPrefab;
    public Transform startWaiterPoint;
    public Transform mergePoint;
    public bool result;
   
    [SerializeField] int degree=0;
    public bool mergeIsOkay=false;
    WaiterBehaviour waiterOne;
    WaiterBehaviour waiterTwo;

    [SerializeField] int waiterMergeButtonId=3;

    int min = 100;
    int pair = 0;

    int checkId = 0;

    [SerializeField] int  waitressUpgradeButtonId=1;

    void Start()
    {
       
        upgradeManager=UpgradeManager.Instance;
        waiterManager=this;
        CreateWaiter();
    }

    public void CreateWaiter(bool isFirst=true)
    {
        degree++;
        GameObject waiter = Instantiate(waiterPrefab[0], startWaiterPoint);
        waiters.Add(waiter.GetComponent<WaiterBehaviour>());
        
        waiter.GetComponent<WaiterBehaviour>().startPoint = startWaiterPoint;
       
        waiter.GetComponent<NavMeshAgent>().avoidancePriority+=degree;

        if(!isFirst){
                upgradeManager.ButtonValueUpdate(waitressUpgradeButtonId);
                
        }
        if(waiters.Count>=tableManager.tableAmount){
            upgradeManager.upgradeButtons[waitressUpgradeButtonId].interactable=false;
        }
        
        
    }

    public void CreateWaiterSecond(int id, Transform newPos)
    {
        degree++;
        GameObject waiter = Instantiate(waiterPrefab[id], newPos);
        
       
        waiters.Add(waiter.GetComponent<WaiterBehaviour>());
        waiter.GetComponent<WaiterBehaviour>().startPoint = startWaiterPoint;
        waiter.GetComponent<NavMeshAgent>().avoidancePriority+=degree;

        waiter.GetComponent<WaiterBehaviour>().multiplier=1.25f;
        tableManager.MergeEnable();
    }

    public void MergeWaiter()
    {
        Time.timeScale = 1.5f;

        for (int i = 0; i < waiters.Count; i++)
        {
            if (waiters[i].mergeId < min)
            {
                min = waiters[i].mergeId;
            }
        }
        result=FindPairs(min); 
       
        while(!result){
            min++;
            result=FindPairs(min);
             
        }
        if(result){
            Debug.Log("Merge zamani");
        }

        waiterOne.mergePoint = mergePoint;
        waiterOne.gameObject.GetComponent<NavMeshAgent>().avoidancePriority=50;
        waiterOne.ChooseMergeMove();
        waiters.Remove(waiterOne);

        waiterTwo.mergePoint = mergePoint;
        waiterTwo.gameObject.GetComponent<NavMeshAgent>().avoidancePriority=50;
        waiterTwo.ChooseMergeMove();
        waiters.Remove(waiterTwo);

        tableManager.MergeDisable();
        upgradeManager.upgradeButtons[waiterMergeButtonId].interactable=false;

        mergeIsOkay=false;
        

    }

   public bool FindPairs(int ind){
        result=false;
        int counter=-1;
        while(!result)
        {
            for(int i=0;i<waiters.Count;i++)
            {
                if(waiters[i].mergeId==ind){
                    counter++;
                    if(counter==0){
                        waiterOne=waiters[i];
                    }
                    if(counter==1){
                        waiterTwo=waiters[i];
                        Debug.Log("Pair is found " +ind);
                        result=true;
                        break;
                    }
                }
        }
        }
        return result;
    }
    /*public void MergeChecker(){
        int final = 0;
        for (int i = 0; i < waiters.Count; i++){
            for (int j = 0; j < waiters.Count; j++){
                if (waiters[i].tag == "Level" + j){
                    final++;

                    if (final == 2)
                    {
                        Debug.Log("Davsannnn");
                        mergeIsOkay=true;
                        break;
                    }
                }
            }
        }
    }*/

    public void MergeCheckList(){
        for(int i=0;i<waiterPrefab.GetLength(0);i++){
            string levelTag="Level"+i;
            int countOfWaiters=GameObject.FindGameObjectsWithTag(levelTag).GetLength(0);
            if(countOfWaiters>=2 && levelTag!= waiterPrefab[waiterPrefab.GetLength(0)-1].tag ){
                mergeIsOkay=true;
                break;
            }
        }
    }  

    
}