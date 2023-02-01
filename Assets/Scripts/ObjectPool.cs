using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{   // mermileri oyun ba��nda olu�turup bir havuzda tutmak i�in (optimizasyon i�in)
    private Queue<GameObject> pooledObjects;    // oyun nesnelerinin olaca�� kuyruk
    [SerializeField] private GameObject objectPrefab;    // prefablerimiz �o�alt�lacak
    [SerializeField] private int poolSize;    // Havuzumuzun bir b�y�kl��� olacak

    private void Awake()
    {
        pooledObjects = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)  // havuz i�erisindeki prefableri olu�turmak i�in
        {
            GameObject obj = Instantiate(objectPrefab);     // objectprefableri olu�turuyor
            obj.SetActive(false);   // mermielr g�r�nemsin diye pasif
            pooledObjects.Enqueue(obj);     // s�raya sokuyoruz (kuyru�a ekliyoruz)
        }
    }

    public GameObject GetPooledObject()  // havuzdan �ap�raca��m�z s�ra i�in
    {
        GameObject obj  = pooledObjects.Dequeue(); // s�radan ��kartt�k
        obj.SetActive(true);    // aktif ettik
        pooledObjects.Enqueue(obj); // s�ran�n sonuna ekledik
        return obj;
    }
}
