using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class BuyManager : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] TextMeshPro sellPriceText;
    [SerializeField] GameObject[] falseObjects;
    [SerializeField] GameObject[] activeObjectsUp;
    [SerializeField] GameObject[] activeObjectsDown;
    [SerializeField] GameObject[] activeObjectsStatic;
    [SerializeField] Ease activeEaseFirst;
    [SerializeField] Ease activeEaseSecond;

    float priceDelayDefault;
    float priceDelay;
    Vector3[] path;

    private void Start()
    {
        priceDelayDefault = 0.05f;
        sellPriceText.text = price.ToString();

        for (int i = 0; i < activeObjectsUp.Length; i++)
        {
            activeObjectsUp[i].SetActive(false);
        }

        for (int i = 0; i < activeObjectsDown.Length; i++)
        {
            //activeObjectsDown[i].SetActive(false);
        }

        for (int i = 0; i < activeObjectsStatic.Length; i++)
        {
            activeObjectsStatic[i].SetActive(false);
        }
    }

    public void Buying(int id)
    {
        for (int i = 0; i < falseObjects.Length; i++)
        {
            falseObjects[i].SetActive(false);
        }

        StartCoroutine(ActiveDelay(id));
    }

    private IEnumerator ActiveDelay(int id)
    {
        Vector3 objectPosition;

        yield return new WaitForSeconds(0.01f);
        activeObjectsUp[id].SetActive(true);
        objectPosition = activeObjectsUp[id].transform.position;
        activeObjectsUp[id].transform.DOMove(new Vector3(objectPosition.x, Random.Range(0.3f, 0.7f), objectPosition.z), 0.5f).SetEase(activeEaseFirst);

        objectPosition = activeObjectsUp[id].transform.position;
        activeObjectsUp[id].transform.DOMove(new Vector3(objectPosition.x, 0.0f, objectPosition.z), 0.5f).SetEase(activeEaseSecond);
        
        activeObjectsDown[id].SetActive(true);
        objectPosition = activeObjectsDown[id].transform.position;
        activeObjectsDown[id].transform.DOMove(new Vector3(objectPosition.x, Random.Range(-0.3f, -0.7f), objectPosition.z), 0.5f).SetEase(activeEaseFirst);

        objectPosition = activeObjectsDown[id].transform.position;
        activeObjectsDown[id].transform.DOMove(new Vector3(objectPosition.x, 0.0f, objectPosition.z), 0.5f).SetEase(activeEaseSecond);

        yield return new WaitForSeconds(0.25f);

        activeObjectsStatic[id].SetActive(true);

    }
}