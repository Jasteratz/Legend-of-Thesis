using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public Button Play;
    public float transitionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Play.onClick.AddListener(LoadNextLevel);
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // transition.SetTrigger("Start");
        SceneManager.LoadScene(levelIndex);
        yield return new WaitForSeconds(transitionTime);

        
    }
}
