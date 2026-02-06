using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Note : MonoBehaviour
{
    private ObjectPool pool;
    private Rigidbody2D rb;
    private float speed;
    private float lifetime;

    void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        pool = GetComponentInParent<ObjectPool>();
        lifetime = pool.lifetime;
        speed = pool.speed;
    }



    void OnEnable() 
    {
        rb.linearVelocity = Vector2.down * speed;
        StartCoroutine(Deactive(this.gameObject));
    }

    IEnumerator Deactive(GameObject obj)
    {
        yield return new WaitForSeconds(lifetime);

        if (obj.activeSelf)
            obj.SetActive(false);
    }

    void OnDisable() 
    {
        pool.ReturnToPool(this.gameObject);
        rb.linearVelocity = Vector2.zero;
    }
}