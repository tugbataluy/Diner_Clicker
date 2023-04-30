using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    #region Attributes
    [SerializeField] float zoomOutMin = 30;
    [SerializeField] float zoomOutMax = 90;
    private Vector3 touchStart;
    private bool isZoomIn;
    private Camera gamePlayCamera;
    private bool playerInputState = true;
    private bool OpenAlways;
    bool isTouched;
    #endregion

    #region UnityAttributes
    private void Start()
    {
        gamePlayCamera = Camera.main;
    }
    private void Update()
    {
        if (!OpenAlways)
        {
            if (!playerInputState) { return; }
        }

       /* if (Input.GetMouseButtonDown(0))
        { OnFingerDown(); }*/

        if (Input.touchCount == 2)
        { TwoFingerPinch(); }

       /* if (Input.GetMouseButton(0))
        { OnFingerPressing(); }

        if (Input.GetMouseButtonUp(0))
        { OnFingerUp(); }*/

        Zoom(Input.GetAxis("Mouse ScrollWheel") * 20);
        Vector3 viewportPoint = gamePlayCamera.WorldToViewportPoint(transform.position);
        if (viewportPoint.x < 0 || viewportPoint.x > 1)
        {
            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, gamePlayCamera.ViewportToWorldPoint(new Vector3(0, 0, gamePlayCamera.nearClipPlane)).x,
                gamePlayCamera.ViewportToWorldPoint(new Vector3(0, 1, gamePlayCamera.nearClipPlane)).x);
            transform.position = pos;
        }

       
       









    }
    #endregion

    #region Methods
    private void OpenPanZoomAlways()
    {
        OpenAlways = true;
    }
    private void OnFingerDown()
    {
        isTouched = true;
        touchStart = gamePlayCamera.ScreenToWorldPoint(GetMosePosition());
    }
    private void OnFingerPressing()
    {
        if (!isTouched) { return; }
        if (isZoomIn) { return; }
        Vector3 direction = touchStart - (gamePlayCamera.ScreenToWorldPoint(GetMosePosition()));
        direction.y = 0;
        gamePlayCamera.transform.position += direction * Time.deltaTime * 10;
    }
    private void OnFingerUp()
    {
        isTouched = false;
        isZoomIn = false;
    }
    private void TwoFingerPinch()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

        float difference = currentMagnitude - prevMagnitude;

        Zoom(difference * 0.03f);

        isZoomIn = true;
    }
    private void Zoom(float increment)
    {
        gamePlayCamera.fieldOfView = Mathf.Clamp(gamePlayCamera.fieldOfView - increment, zoomOutMin, zoomOutMax);
    }
    private Vector3 GetMosePosition()
    {
        var mousePos = Input.mousePosition;
        mousePos.z = -10;
        return mousePos;
    }
    private void InputState(bool state)
    {
        playerInputState = state;
    }
    #endregion
}
