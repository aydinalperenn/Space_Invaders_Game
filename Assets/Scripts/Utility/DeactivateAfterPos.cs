using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterPos : MonoBehaviour
{
    public float bulletDeactivatePos;

    void Update()
    {
        if(transform.position.y > bulletDeactivatePos || transform.position.y < -bulletDeactivatePos)   // hem düþman mermilerinin aþaðýda deaktive olamsý hem de oyuncu mermilerinin yukarýda deaktive olmasý için
        {
            gameObject.SetActive(false);
        }
    }
}
