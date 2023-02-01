using UnityEngine;
// sadece gemiye dair de�i�kenlerin tutulmas� i�in bir script


[System.Serializable]   // eri�ebilmek i�in
public class ShipStats
{

    [Range(1, 5)]    // aral�k tan�m�
    public int maxHealth;
    [HideInInspector]   // yukar�dakini g�rmek istemedi�imiz i�in bu sat�rla gizledik
    public int currentHealth;
    [HideInInspector]
    public int maxLifes = 3;
    [HideInInspector]
    public int currentLifes = 3;

    public float shipSpeed;
    public float fireRate;
}
