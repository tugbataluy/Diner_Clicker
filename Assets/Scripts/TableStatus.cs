using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TableStatus : MonoBehaviour
{
    [SerializeField] WaiterManager waiterManager;
    [SerializeField] KitchenManager kitchenManager;
    [SerializeField] WaiterBehaviour waiterBehaviour;
    

    UpgradeManager upgradeManager;


    public GameObject client;
    public GameObject waiter;
    public bool isMerge;

    public ClientBehaviour clientBehaviour;
    public List<GameObject> foods = new List<GameObject>();
    public List<GameObject> orderUI=new List<GameObject>();
    [SerializeField] float generalMultiplier=1.0f;
    
    [SerializeField] GameObject clientPrefab;
    [SerializeField] Transform clientStartPoint;
    [SerializeField] Transform clientEndPoint;
    [SerializeField] Transform waiterPoint;

    [SerializeField] bool order;
    [SerializeField] int orderId;

    float waiterDelay = 2f;
    float LeaveDelay = 1f;
    public bool hasWaiter=false;
    bool orderTriggerBool=false;
    [SerializeField] GameObject chair;
    float duration=2.0f;

    void Start()
    {
        upgradeManager=UpgradeManager.Instance;
        for (int i = 0; i < foods.Count; i++)
        {
            foods[i].SetActive(false);
        }
    }

    void OnEnable()
    {
        Vector3 objectPosition = new Vector3(this.transform.position.x,0,this.transform.position.z);
        this.transform.DOMove(new Vector3(objectPosition.x, Random.Range(-0.3f, -0.7f), objectPosition.z), 0.5f).SetEase(Ease.InOutSine);

        objectPosition = this.transform.position;
        this.transform.DOMove(new Vector3(objectPosition.x, 0.0f, objectPosition.z), 0.5f).SetEase(Ease.InOutSine).OnComplete(()=>{ClientSpawner();});
    }

    void Update()
    {
       
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == client)
        {
            StartCoroutine(FindFreeWaiter());
            StartCoroutine(WaitForOrder());
            
        }
        if (other.gameObject == waiter)
        {
            Debug.Log("G,rd,");
            if (order)
            {
                WaiterMove();
            }
            else
            {
                upgradeManager.MoneyIncrease(waiterBehaviour.multiplier, kitchenManager.foodPrices[orderId]);
                Animate();
                waiterBehaviour.ChoosingTargetNull();
                StartCoroutine(ClientLeave());

                FoodActive(orderId);
                waiterBehaviour.FoodFalse();
                waiterBehaviour.tray.SetActive(false);
                waiter = null;
                waiterBehaviour = null;
                hasWaiter = false;
            }
        }
    }

    private IEnumerator WaiterMoveDelay()
    {
        yield return new WaitForSeconds(4f);

        //WaiterMove();
    }

    private void WaiterMove()
    {
        waiterBehaviour.DestinationChooser(kitchenManager.kitchens[orderId].transform, orderId);
        order = false;
    }

    /*private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == waiter && !orderTriggerBool)
        {
            Debug.Log("G,rd,");
            if (order)
            {
                waiterBehaviour.DestinationChooser(kitchenManager.kitchens[orderId].transform, orderId);
                order = false;
            }
            else
            {
                upgradeManager.MoneyIncrease(waiterBehaviour.multiplier, kitchenManager.foodPrices[orderId]);

                waiterBehaviour.isFree = true;
                waiterBehaviour.ChoosingTargetNull();
                StartCoroutine(ClientLeave());

                FoodActive(orderId);
                waiterBehaviour.FoodFalse();

                waiter = null;
                waiterBehaviour = null;
                hasWaiter = false;
            }
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == waiter && orderTriggerBool)
        {
            orderTriggerBool = false;
        }
    }

    void ClientSpawner(bool isFirst=true)
    {
        if (!isMerge)
        {
            client = Instantiate(clientPrefab, clientStartPoint);
            GameObject ancestor= client.transform.GetChild(0).gameObject;

            ancestor.transform.GetChild(Random.Range(0,2)).gameObject.SetActive(true);
            clientBehaviour = client.GetComponent<ClientBehaviour>();
            client.GetComponent<ClientBehaviour>().targetTable = chair;
            client.GetComponent<ClientBehaviour>().clientEndPoint = clientEndPoint;
            orderId = Random.Range(0, kitchenManager.kitchensAmount);
            Debug.Log("Spawnlandi" + client);

            WaiterMoveDelay();
        }
    }

     IEnumerator WaiterMove(Transform target)
    {
        
        yield return new WaitForSeconds(waiterDelay);
        // StartCoroutine(WaiterMove());
    }

    public IEnumerator FindFreeWaiter(){

        if (!hasWaiter && !isMerge) 
        {
           for(int i=0;i<waiterManager.waiters.Count;i++)
           {
                if(waiterManager.waiters[i].isFree)
                {
                    Debug.Log("Boşta garson var");
                    order = true;
                    hasWaiter = true;
                    waiterBehaviour = waiterManager.waiters[i];
                    waiterBehaviour.isFree=false;
                    waiter=waiterBehaviour.gameObject;
                    waiterBehaviour.tablePoint=waiterPoint;
                    waiterBehaviour.DestinationChooser(waiterPoint);
                    break;
                }
           }

            yield return new WaitForSeconds(0.5f);
            StartCoroutine(FindFreeWaiter());
        }
    }

    IEnumerator ClientLeave()
    {
        hasWaiter = false;
        foreach(GameObject order in orderUI)
        {order.SetActive(false);}
        yield return new WaitForSeconds(LeaveDelay);

        if (clientBehaviour)
        {
            
            FoodFalse();
            clientBehaviour.Leave();
            client = null;
            clientBehaviour = null;
            yield return new WaitForSeconds(1f);
            ClientSpawner();
        }
    }

    public void ClearTable()
    {
        if (waiterBehaviour){
            waiterBehaviour.isFree = true;
            waiterBehaviour.tablePoint = null;
            waiterBehaviour.order = false;
            waiterBehaviour.ChooseMerge();
            waiterBehaviour.FoodFalse();
            waiterBehaviour = null;
            waiter = null;
            hasWaiter=false;
        }

        if (clientBehaviour){
            clientBehaviour.Leave();
            client = null;
            clientBehaviour = null;
        }

        FoodFalse();
    }

    public void ActiveTable()
    {
        if (this.enabled)
            ClientSpawner();
    }

    public void FoodActive(int foodId)
    {
        foreach (GameObject item in foods)
        {
            item.SetActive(false);
        }

        foods[foodId].SetActive(true);
    }

    public void FoodFalse()
    {
        foreach (GameObject item in foods)
        {
            item.SetActive(false);
        }
    }

    void Animate(){
        GameObject coin=this.transform.GetChild(this.transform.childCount-1).gameObject;
       Color color= coin.GetComponent<SpriteRenderer>().color;
        color.a=1f;
        coin.GetComponent<SpriteRenderer>().color=color;
        coin.SetActive(true);
        coin.transform.DOLocalMoveY(3.2f,2f).OnComplete(()=>coin.transform.DOLocalMoveY(1.6f,2f));
        
        coin.GetComponent<SpriteRenderer>().DOFade(0,2f).OnComplete(()=>coin.SetActive(false));
        
    }

    IEnumerator WaitForOrder(){
        yield return new WaitForSeconds(1f);
        orderUI[orderId].SetActive(true);
    }
}