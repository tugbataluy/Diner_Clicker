using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public static UpgradeManager Instance;

    public List<Button> upgradeButtons= new List<Button>();

    public List<float> requiredMoney= new List<float>();

    public List<float> buttonMultiplier= new List<float>();

    public Queue<GameObject> coins= new Queue<GameObject>();
    int maxCoins=200;
    [SerializeField] GameObject coinPrefab;


    [Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	public Vector3 targetMoneyPosition;
    

    
     public float duration = 0.2f;
    void Awake()
    {
        Instance=this;
       

    }
    [SerializeField] public TextMeshProUGUI score;
    public float generalMultiplier=1.0f;
    public int tableLevel=1;
    public int foodLevel=1;

   [SerializeField] float value=0;

   [SerializeField] float shakeT = 1.5f;
   [SerializeField] float shakeV = 7.0f;

    void Start()
    {
        for(int i=0;i<upgradeButtons.Count;i++){
            upgradeButtons[i].interactable=false;
            upgradeButtons[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=requiredMoney[i].ToString();
           
        }
        score.text=value.ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoneyIncrease( float waiterLevel=1, float foodPrice=1)
    {
        float startValue=value;
        value+=waiterLevel*foodPrice*generalMultiplier;
        float endValue=Mathf.CeilToInt(value);
        
         Tween tween = DOTween.To(() => startValue, x => startValue = x, endValue, duration)
            .OnUpdate(() => score.text = startValue.ToString("0"))
            .Play();


       // score.text=Mathf.CeilToInt(value).ToString();
       //WaiterManager.waiterManager.MergeChecker();
       WaiterManager.waiterManager.MergeCheckList();
        CheckButtons();
        

    }

    public void MoneyDecrease(float amount){

        float startValue=value;
        value-=amount;
        float endValue=Mathf.CeilToInt(value);

        Tween tween = DOTween.To(() => startValue, x => startValue = x, endValue, duration)
        .OnUpdate(() => score.text = startValue.ToString("0"))
        .Play();
        
        //score.text=Mathf.CeilToInt(value).ToString();
        CheckButtons();
    }

    void CheckButtons(){
        for (int i=0;i<upgradeButtons.Count;i++)
        {
            if(requiredMoney[i]<=value)
            {
                if(i==1)
                {
                    
                    if(WaiterManager.waiterManager.waiters.Count<TableManager.Instance.tableAmount)
                        upgradeButtons[i].interactable=true;
                    
                }
                else if(i==3 )
                {
                    if (WaiterManager.waiterManager.mergeIsOkay)
                    upgradeButtons[i].interactable=true;
                    else
                    upgradeButtons[i].interactable=false;

                }
                else
                    upgradeButtons[i].interactable=true;
               Debug.Log("genel buton kontrol "+i);

                upgradeButtons[i].transform.DOShakePosition(shakeT, shakeV);

            }
            else
            {
                upgradeButtons[i].interactable=false;
            }
           
        }
        

    }
    public void ButtonValueUpdate(int buttonId){
        MoneyDecrease(requiredMoney[buttonId]);
        requiredMoney[buttonId]*=buttonMultiplier[buttonId];
        upgradeButtons[buttonId].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text= requiredMoney[buttonId].ToString();
        CheckButtons();
    }
     
   /* public void CreateCoins(){
        for(int i=0;i<maxCoins;i++)
        {
            GameObject coin=Instantiate(coinPrefab);
            coin.transform.parent = transform;
			coin.SetActive (false);
			coins.Enqueue (coin);
        }

    }*/
  
    /*public void Animate (Vector3 collectedCoinPosition, int amount, Vector3 targetPos)
	{

		for (int i = 0; i < amount; i++) {
			//check if there's coins in the pool
			if (coins.Count > 0) {
				//extract a coin from the pool
				GameObject coin = coins.Dequeue ();
				coin.SetActive (true);

				//move coin to the collected coin pos
				coin.transform.position = collectedCoinPosition + new Vector3 (Random.Range (-spread, spread), 0f, 0f);
				//coin.transform.position = Camera.main.ScreenToWorldPoint(collectedCoinPosition + new Vector3 (Random.Range (-spread, spread), 0f, 0f)) ;
				//animate coin to target position
				float duration = Random.Range (minAnimDuration, maxAnimDuration);
				//coin.transform.DOMove (Camera.main.ScreenToWorldPoint(targetPos), duration)
				coin.transform.DOMove (targetPos, duration)
				.SetEase (easeType)
				.OnComplete (() => {
					//executes whenever coin reach target position
					coin.SetActive (false);
					coins.Enqueue (coin);

					
				});
			}
		}
	}*/
    
}
