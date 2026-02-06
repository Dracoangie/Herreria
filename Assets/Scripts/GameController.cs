using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject forge;
    [SerializeField] private GameObject forgeButton;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject hammerButton;
    [SerializeField] private GameObject sharp;
    [SerializeField] private GameObject gameFail;
    [SerializeField] private TextMeshProUGUI roundText;

    public int points;
    private Animator animator;

    void Start()
    {
        roundText.text = "Round:  " + points.ToString();
        animator = GetComponent<Animator>();
        StartCoroutine(NextGoblin(2.5f));
    }

    IEnumerator NextGoblin(float waittime)
    {
        yield return new WaitForSeconds(waittime);
        forgeButton.SetActive(true);
        yield return null;
    }

    private void GameFail()
    {
        gameFail.SetActive(true);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex);
    }

    #region ActiveGames
    public void StartForge()
    {
        forgeButton.SetActive(false);
        forge.SetActive(true);
    }

    public void stopForge(bool condition)
    {
        forge.SetActive(false);
        if (condition)
            hammerButton.SetActive(true);
        else
            GameFail();
    }

    public void StartHammer()
    {
        hammer.SetActive(true);
        hammerButton.SetActive(false);
    }

    public void stopHammer(bool condition)
    {
        hammer.SetActive(false);
        if (condition)
        {
            animator.SetTrigger("RoundEnd");
            points++;
            roundText.text = "Round: " + points.ToString();
            StartCoroutine(NextGoblin(5f));
        }
        else
            GameFail();
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
