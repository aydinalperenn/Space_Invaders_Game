using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{

    public float seconds;
     
    void Start()
    {
        
        Destroy(gameObject, seconds);   // belli bir saniye sonra (unity �zerinden de�er verdim) mermiyi yok ediyor
    }


}
