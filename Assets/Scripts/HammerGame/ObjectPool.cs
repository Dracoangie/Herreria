using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    [SerializeField] private float minWaitTime = 0.2f;
    [SerializeField] private float maxWaitTime = 8.0f;

    [SerializeField] private float lifetime = 4f;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            float wait = Random.Range(minWaitTime, maxWaitTime);
            yield return new WaitForSeconds(wait);

            if (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();

                obj.transform.position = new Vector3(0, transform.position.y, 0);
                obj.SetActive(true);

                StartCoroutine(Deactive(obj));
            }
        }
    }

    IEnumerator Deactive(GameObject obj)
    {
        yield return new WaitForSeconds(lifetime);

        if (obj.activeSelf)
        {
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}