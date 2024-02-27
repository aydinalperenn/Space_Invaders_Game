using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{   // mermileri oyun baþýnda oluþturup bir havuzda tutmak için (optimizasyon için)
    private Queue<GameObject> pooledObjects;    // oyun nesnelerinin olacaðý kuyruk
    [SerializeField] private GameObject objectPrefab;    // prefablerimiz çoðaltýlacak
    [SerializeField] private int poolSize;    // Havuzumuzun bir büyüklüðü olacak

    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)  // havuz içerisindeki prefableri oluþturmak için
        {
            GameObject obj = Instantiate(objectPrefab);     // objectprefableri oluþturuyor
            obj.SetActive(false);   // mermielr görünemsin diye pasif
            pooledObjects.Enqueue(obj);     // sýraya sokuyoruz (kuyruða ekliyoruz)
        }
    }

    public GameObject GetPooledObject()  // havuzdan çapýracaðýmýz sýra için
    {
        GameObject obj  = pooledObjects.Dequeue(); // sýradan çýkarttýk
        obj.SetActive(true);    // aktif ettik
        pooledObjects.Enqueue(obj); // sýranýn sonuna ekledik
        return obj;
    }
}
