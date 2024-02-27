using UnityEngine;
// sadece gemiye dair deðiþkenlerin tutulmasý için bir script


[System.Serializable]   // eriþebilmek için
public class ShipStats
{

    [Range(1, 5)]    // aralýk tanýmý
    public int maxHealth;
    [HideInInspector]   // yukarýdakini görmek istemediðimiz için bu satýrla gizledik
    public int currentHealth;
    [HideInInspector]
    public int maxLifes = 3;
    [HideInInspector]
    public int currentLifes = 3;

    public float shipSpeed;
    public float fireRate;
}
