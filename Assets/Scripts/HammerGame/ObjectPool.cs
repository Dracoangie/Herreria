using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using Unity.VisualScripting;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    [SerializeField] private float minWaitTime = 0.2f;
    [SerializeField] private float maxWaitTime = 8.0f;

    public float lifetime = 4f;
    public float speed = 1f;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void OnDisable()
    {
        pool.Clear();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
    }

	void OnEnable()
	{
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab, this.transform);
            obj.SetActive(false);
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
                Debug.Log("obj");
                GameObject obj = pool.Dequeue();

                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(true);
            }
        }
    }



    public void ReturnToPool(GameObject obj)
    {
        if (!pool.Contains(obj))
            pool.Enqueue(obj);
    }
}