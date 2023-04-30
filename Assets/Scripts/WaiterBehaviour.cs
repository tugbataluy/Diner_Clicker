using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaiterBehaviour : MonoBehaviour
{
    NavMeshAgent agent;

    public bool order;
    public bool isMerge;
    [SerializeField] Transform targetPoint;
    public List<GameObject> foods = new List<GameObject>();
    public GameObject tray;
    public Transform kitchenPoint;
    public Transform tablePoint;
    public Transform mergePoint;
    [SerializeField] int orderId;
    public Transform startPoint;
    public int mergeId;
    public bool isFree=true;
    [SerializeField] Animator waiterAnimator;

    float clickDelay = 0.5f;
    float time = 0;
    float speed;

    // waiterın leveli arttıkça değeri artacak
    public float multiplier=1;

    void Start()
    {
     
        AnimationControl("Move", false);
        tray.SetActive(false);
        agent = this.GetComponent<NavMeshAgent>();
            
        //agent.avoidancePriority+=index*5;
        for (int i = 0; i < foods.Count; i++)
        {
            foods[i].SetActive(false);
        }
    }

    void Update()
    {
        Move();

        if (Input.GetMouseButtonDown(0))
        {
            time = clickDelay;
        }

        time -= Time.deltaTime;

        if (time <= 0)
        {
            speed = 1f;
            waiterAnimator.SetFloat("Speed", speed);
            agent.speed = 2f;
        }
        else
        {
            speed = 2f;
            waiterAnimator.SetFloat("Speed", speed);
            agent.speed = 4f;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kitchen")
        {
            Debug.Log("Kitchen");
            targetPoint = tablePoint;
            tray.SetActive(true);
            FoodActive(orderId);
        }

        if (other.name == "WaiterSpawnPosition")
        {
            AnimationControl("Move", false);
        }
    }

    void Move()
    {
        if (targetPoint)
            agent.SetDestination(targetPoint.position);
    }

    public void ChoosingTarget(Transform target, int id, Transform kitchenTransform)
    {
        order = true;

        kitchenPoint = kitchenTransform;
        tablePoint = target;
        orderId = id;
    }

    public void DestinationChooser(Transform target, int id = 0)
    {
        AnimationControl("Move");
        targetPoint = target;
        orderId = id;
    }

    public void ChoosingTargetNull()
    {
        order = false;
        targetPoint = startPoint;
        orderId = 0;
    }

    public void ChooseMerge()
    {
        Debug.Log("Merge Çalişti beya");
        if (!order)
        {
            orderId = 0;

            if (mergePoint){
                isMerge = true;
                targetPoint = mergePoint;
            }
            else
            targetPoint = startPoint;
        }
    }

    public void ChooseMergeMove()
    {
        isMerge = true;
        targetPoint = mergePoint;
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

    public void AnimationControl(string animationName, bool animationState = true)
    {
        waiterAnimator.SetBool(animationName, animationState);
    }


    
}