using UnityEngine;

public class Zone : MonoBehaviour
{
    [SerializeField] private float maxX = 4f;
    [SerializeField] private float localScaleX = 5;

    private void OnEnable()
    {
        transform.localScale = new Vector3(transform.localScale.x, localScaleX, transform.localScale.z);
    }

    public void ChangePlace(float pointDir, float pointX)
    {
        float actX = (pointDir > 0) ? Random.Range(-maxX, pointX - 1) : Random.Range(pointX +1, maxX);

        transform.localPosition = new Vector3(actX, transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y -1, transform.localScale.z);
    }
}
