using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delay = 2f;
    public void LoadGameScene()
    {
        ResetScore();
        SceneManager.LoadScene("Game");
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadDelay());
    }

    public void LoadStartMenuScene()
    {
        ResetScore();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
    }

    private void ResetScore()
    {
        GameSession session = FindObjectOfType<GameSession>();
        if(session == null)
        {
            return;
        }
        else
        {
            session.SetScore(0);
        }
    }
}
