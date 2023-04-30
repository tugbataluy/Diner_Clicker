using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollAndPinch : MonoBehaviour
{/*
    public Camera Camera;
    protected Plane Plane;

    public float smoothness=0.5f;
    [SerializeField] float multiplier=3f;
    public bool Rotate;
    public void Awake(){
        if(Camera==null){
            Camera=Camera.main;
        }
    }
    
    void Update()
    {
        if(Input.touchCount>=1)
        {
            Plane.SetNormalAndPosition(transform.up,transform.position);
        }

        var Delta1=Vector3.zero;
       

        // Scroll
        if (Input.touchCount==1){
            Delta1=PlanePositionDelta(Input.GetTouch(0));
            //Debug.Log(Delta1);
            var res= Camera.ScreenToWorldPoint(Delta1 * Time.deltaTime * 100);
            Debug.Log("Res: " + res);
            float xPos = Mathf.Clamp(res.x, -20, -10);
            float zPos = Mathf.Clamp(res.z, 10, 24);
            Camera.transform.position= Vector3.Lerp(Camera.transform.position, new Vector3(xPos, Camera.transform.position.y, Camera.transform.position.z), Time.deltaTime * smoothness);
            
           // Camera.transform.Translate(Delta1,Space.World);

            //Camera.transform.position = new Vector3(xPos, Camera.transform.position.y, zPos);
            //Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(xPos, Camera.transform.position.y, zPos), Time.deltaTime * smoothness);
            //Camera.transform.position=Vector3.Lerp(Camera.transform.position,Delta1,Time.deltaTime*smoothness);
        }

        // Pinch
       /* if(Input.touchCount>=2)
        {
            var pos1= PlanePosition(Input.GetTouch(0).position);
            var pos2=PlanePosition(Input.GetTouch(1).position);
            var pos1b=PlanePosition(Input.GetTouch(0).position-Input.GetTouch(0).deltaPosition);
            var pos2b=PlanePosition(Input.GetTouch(1).position-Input.GetTouch(1).deltaPosition);
            //zoomlama
            var zoom=Vector3.Distance(pos1,pos2)/Vector3.Distance(pos1b,pos2b);

            Debug.Log(zoom);

            if(zoom==0|| zoom>10){
                return;
            }

            Camera.transform.position=Vector3.LerpUnclamped(pos1,Camera.transform.position,1/zoom);
            float yPos = Mathf.Clamp(Camera.transform.position.y, 10, 20);
            Camera.transform.position = Vector3.Lerp(Camera.transform.position, new Vector3(Camera.transform.position.x, yPos, Camera.transform.position.z), Time.deltaTime * smoothness);
        }*/
    /*}

    protected Vector3 PlanePositionDelta(Touch touch){
      
           
        // parmak hareket etmiyorsa
        if(touch.phase!=TouchPhase.Moved)
        {
                return Vector3.zero;
        }
        // değişimi hesaplama
        var rayBefore=Camera.ScreenPointToRay(touch.position-touch.deltaPosition);
        var rayNow=Camera.ScreenPointToRay(touch.position);
       

        if(Plane.Raycast(rayBefore, out var enterBefore)&&Plane.Raycast(rayNow, out var enterNow))
        {
            return rayBefore.GetPoint(enterBefore)- rayNow.GetPoint(enterNow);
        }

        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos){
        var rayNow=Camera.ScreenPointToRay(screenPos);
        if(Plane.Raycast(rayNow, out var enterNow))
        {
                return rayNow.GetPoint(enterNow);
        }
        return Vector3.zero;
    }*/

    private Vector3 touchStart;
    public Camera cam;
    public float groundY = 0;
    [SerializeField] float smoothness = 10;
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            touchStart=GetWorldPosition(groundY);
        }
        if(Input.GetMouseButton(0)){
            Vector3 direction=touchStart-GetWorldPosition(groundY);

            Vector3 newPos = cam.transform.position + direction;

            float xPos = Mathf.Clamp(newPos.x, -20, -10);
            float zPos = Mathf.Clamp(newPos.z, 16, 21);

            //cam.transform.position+=direction;

            //cam.transform.position = new Vector3(xPos,newPos.y,zPos);
            cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(xPos,newPos.y,zPos), Time.deltaTime * smoothness);

        }
    }
    private Vector3 GetWorldPosition(float smt){
        Ray mousePos=cam.ScreenPointToRay(Input.mousePosition);
        Plane ground= new Plane(Vector3.up,new Vector3(0,smt ,0));
        float distance;
        ground.Raycast(mousePos, out distance);
        return mousePos.GetPoint(distance);
        
    }
}
