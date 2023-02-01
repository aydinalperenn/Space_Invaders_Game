using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    public float delay;
    
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);    // animator comp içerisindeki stateinfoda eriþeceðim nokta tamamlandýðý + gecikme eklendiði zaman yok et
    }


}
