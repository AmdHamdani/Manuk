using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float startCheckAfter = 10f;
    public float gameLength = 5f;
    public bool isRunning = true;
    public GameObject winPanel;
    public GameObject gameOverPanel;
    public List<GameObject> birds = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(CheckGameOver());
        StartCoroutine(CheckWin());
    }

    private IEnumerator CheckGameOver()
    {
        yield return new WaitForSeconds(startCheckAfter);
        while (isRunning)
        {
            yield return new WaitForSeconds(.1f);
            if (birds.Count <= 0)
            {
                gameOverPanel.SetActive(true);
                isRunning = false;
            }
        }
    }

    private IEnumerator CheckWin()
    {
        yield return new WaitForSeconds(gameLength);
        if(birds.Count > 0)
        {
            winPanel.SetActive(true);
        }
    }
}
