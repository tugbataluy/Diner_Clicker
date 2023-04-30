using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class ClientBehaviour : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject targetTable;
    Vector3 targetPoint;
    public Transform clientEndPoint;
    [SerializeField] Animator clientAnimatorF;
    [SerializeField] Animator clientAnimatorM;


    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        //Vector3 tempPos = targetTable.transform.position + new Vector3(-1, 0, -1f);
        Vector3 tempPos = targetTable.transform.position;
        targetPoint = new Vector3(tempPos.x, tempPos.y, tempPos.z);

        AnimationControl("Move");
    }

    void Update()
    {
        Walk();
    }

    void Walk()
    {
        if (targetTable)
        {
            agent.SetDestination(targetPoint);

            if (Vector3.Distance(transform.position, targetPoint) < 1f)
            {
                AnimationControl("Sit");
                
               
                transform.DORotate(targetTable.transform.eulerAngles, 0.5f);
                transform.DOMove(targetTable.transform.position, 0.5f);
            }
        }
    }

    public void Leave()
    {
        AnimationControl("Sit", false);
        targetPoint = clientEndPoint.position;
    }
    void OnTriggerEnter(Collider other){
        if(other.name=="ClientEndPosition"){
            
            Destroy(this.gameObject,0.3f);
        }
    }

    public void AnimationControl(string animationName, bool animationState = true)
    {
        clientAnimatorF.SetBool(animationName, animationState);
        clientAnimatorM.SetBool(animationName, animationState);
    }
}
