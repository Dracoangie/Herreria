using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Point : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    private float speed;
    [SerializeField] private float changeSpeed = 20f;
    private Zone zone = null;
    private float normalSpeed = 0;
    private Vector3 origen;

    private float dir = 1f;
    private float dirAux = 1f;

    private Rigidbody2D rb;

    [SerializeField] private float defaultMaxX = 6f;
    private float currentMaxX;

    void Start()
    {
        origen = transform.localPosition;
        rb = GetComponent<Rigidbody2D>();
        currentMaxX = defaultMaxX;
    }

    private void OnEnable()
    {
        dir = 1f; 
        dirAux = 1f;
        currentMaxX = defaultMaxX;
        
        speed = baseSpeed + Mathf.Sqrt(Object.FindFirstObjectByType<GameController>().points);
        normalSpeed = 0;
        StartCoroutine(StartMove());
    }

    void Update()
    {
        dirAux = Mathf.MoveTowards(dirAux, dir, changeSpeed * Time.deltaTime);
        transform.Translate(dirAux * normalSpeed * Time.deltaTime, 0, 0);

        if(transform.localPosition.x > currentMaxX || transform.localPosition.x < -currentMaxX)
        {
            currentMaxX = 200f;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 4f;
            StartCoroutine(BadEnd());
        }
    }

	private void OnDisable()
	{
        transform.localPosition = origen;
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
	}

    IEnumerator StartMove()
    {
        Vector3 originalScale = transform.parent.localScale;
        Vector3 upScale = originalScale * 1.2f;
        float duration = 0.1f;

        float elapsed = 0;
        while (elapsed < duration)
        {
            transform.parent.localScale = Vector3.Lerp(originalScale, upScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0;
        while (elapsed < duration)
        {
            transform.parent.localScale = Vector3.Lerp(upScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.parent.localScale = originalScale;

        elapsed = 0;
        duration = 1f;

        yield return new WaitForSeconds(0.5f);
        while (elapsed < duration)
        {
            normalSpeed = Mathf.Lerp(0, speed, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        normalSpeed = speed;
    }

    private IEnumerator BadEnd()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(End(transform.parent, false));
        yield return null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Zone"))
            zone = other.GetComponent<Zone>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Zone"))
            zone = null;
    }

    public void OnClick()
    {
        if(zone != null)
        {
            zone.ChangePlace(dir, transform.position.x);
            dir *= -1f;
            if (zone.transform.localScale.y <= 0)
            {
                dir = 0;

                if (zone.transform.parent != null)
                    StartCoroutine(End(zone.transform.parent, true));
            }
        }
    }

    private IEnumerator End(Transform target, bool end)
    {
        Vector3 originalScale = target.localScale;
        Vector3 upScale = originalScale * 1.2f;
        float duration = 0.1f;

        float elapsed = 0;
        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(originalScale, upScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        elapsed = 0;
        while (elapsed < duration)
        {
            target.localScale = Vector3.Lerp(upScale, originalScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        target.localScale = originalScale;
        GameController controller = Object.FindFirstObjectByType<GameController>();
        if (controller != null)
            controller.stopForge(end);
        yield return null;
    }
}