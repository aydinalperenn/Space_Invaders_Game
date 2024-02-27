using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterPos : MonoBehaviour
{
    public float bulletDeactivatePos;

    void Update()
    {
        if(transform.position.y > bulletDeactivatePos || transform.position.y < -bulletDeactivatePos)   // hem d��man mermilerinin a�a��da deaktive olams� hem de oyuncu mermilerinin yukar�da deaktive olmas� i�in
        {
            gameObject.SetActive(false);
        }
    }
}
