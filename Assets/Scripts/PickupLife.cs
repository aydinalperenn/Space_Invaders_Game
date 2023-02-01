using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLife : Pickup
{
    public override void pickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddLife();
        gameObject.SetActive(false);
    }
}
