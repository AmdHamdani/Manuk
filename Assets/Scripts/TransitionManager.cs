using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public GameObject startTransition;
    public GameObject endTransition;
    public float startTransitionTime;
    public float endTransitionTime;
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(StartTransitionPlay(startTransitionTime));
    }
    IEnumerator StartTransitionPlay(float transitionTime){
        startTransition.SetActive(true);
        yield return new WaitForSeconds(transitionTime);
        startTransition.SetActive(false);
    }

    public void LoadScene(int levelIndex){
        StartCoroutine(EndTransitionPlay(levelIndex));
    }
    IEnumerator EndTransitionPlay(int levelIndex){
        endTransition.SetActive(true);
        yield return new WaitForSeconds(endTransitionTime);
        SceneManager.LoadScene(levelIndex); 
    }
}
