using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject forge;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject sharp;

    public int points;

    void Start()
    {
        StartCoroutine(ForgeS());
    }

    IEnumerator ForgeS()
    {
        yield return new WaitForSeconds(.2f);
        //StartForge();
        yield return null;
    }

    #region ActiveGames
    public void StartForge()
    {
        forge.SetActive(true);
    }

    public void stopForge(bool condition)
    {
        forge.SetActive(false);
    }

    public void StartHammer()
    {
        hammer.SetActive(true);
    }

    public void stopHammer(bool condition)
    {
        hammer.SetActive(false);
    }
    public void StartSharp()
    {
        sharp.SetActive(true);
    }

    public void stopSharp(bool condition)
    {
        sharp.SetActive(false);
    }
    #endregion
}
