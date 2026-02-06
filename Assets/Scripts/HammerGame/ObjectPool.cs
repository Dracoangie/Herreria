using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Hierarchy;
using Unity.VisualScripting;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 20;

    private float minWaitTime = 0.2f;
    private float maxWaitTime = 8.0f;
    [SerializeField] private float baseMinWait = 1.0f;
    [SerializeField] private float baseMaxWait = 3.0f;
    [SerializeField] private float absoluteMinLimit = 0.2f;

    public float lifetime = 4f;
    public float speed = 1f;
    private float baseSpeed = 1f;

    private Queue<GameObject> pool = new Queue<GameObject>();

	void Awake()
    {
        baseSpeed = speed;
    }

	void OnDisable()
    {
        pool.Clear();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
            Destroy(this.gameObject.transform.GetChild(i).gameObject);
    }

	void OnEnable()
	{
        GameController controller = Object.FindFirstObjectByType<GameController>();
        speed = baseSpeed + Mathf.Sqrt(controller.points);
        minWaitTime = Mathf.Max(baseMinWait / (1f + (controller.points * 0.1f)), absoluteMinLimit);
        maxWaitTime = Mathf.Max(baseMaxWait / (1f + (controller.points * 0.1f)), absoluteMinLimit + 0.5f);
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