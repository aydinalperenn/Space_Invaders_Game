using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoin : Pickup
{
    public override void pickMeUp()
    {
        Inventory.currentCoins++;
        PlayerPrefs.SetInt("Coin", Inventory.currentCoins);
        UIManager.updateCoins();
        gameObject.SetActive(false);
    }
}
