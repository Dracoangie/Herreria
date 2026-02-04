using UnityEngine;

public class SharpenTool : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private int direction = 1;

    void Update()
    {
        float movement = direction * speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);
    }

    public void OnHold()
    {
        direction *= -1;
    }
}
