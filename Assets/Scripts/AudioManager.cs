using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] AudioSource audioSource;
    [SerializeField] UpgradeManager upgradeManager;
    [SerializeField] List<AudioClip> sounds= new List<AudioClip>();
    int index=-1;
    [SerializeField] int musicUpgradeButtonIndex=4;

    void Awake(){
      
    }
    void Start()
    {
        upgradeManager=UpgradeManager.Instance;
        index++;
        audioSource.clip=sounds[index];
        audioSource.Play();
    }

    // Update is called once per frame
    public void changeMusic(){
        if(index<sounds.Count-1){
            index++;
            audioSource.clip=sounds[index];
            audioSource.Play();
            upgradeManager.ButtonValueUpdate(musicUpgradeButtonIndex);
        }
        else{
            index=0;
            upgradeManager.ButtonValueUpdate(musicUpgradeButtonIndex);
        }
    }
}
