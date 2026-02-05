using System.Collections.Generic;
using UnityEngine;

public class Anvil : MonoBehaviour
{
    private List<GameObject> onTrigger = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
            onTrigger.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            if(onTrigger.Contains(other.gameObject))
            {
                GameController controller = Object.FindFirstObjectByType<GameController>();
                if (controller != null)
                    controller.stopHammer(false);
            }
        }
    }

    public void OnClick()
    {
        if (onTrigger.Count > 0)
        {
            var aux = onTrigger[0];
            onTrigger.RemoveAt(0);
            aux.SetActive(false);
        }
    }

    void OnDisable()
    {
        onTrigger.Clear();
    }
}
