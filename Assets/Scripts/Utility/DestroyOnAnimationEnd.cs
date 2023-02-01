using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    public float delay;
    
    void Start()
    {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);    // animator comp i�erisindeki stateinfoda eri�ece�im nokta tamamland��� + gecikme eklendi�i zaman yok et
    }


}
