using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonHandling : MonoBehaviour
{
    public Button quitButton;
    public Button playButton;

    private void Start()
    {
       // quitButton = GetComponent<Button>();
        quitButton.onClick.AddListener(QuitGame);

        //playButton = GetComponent<Button>();
        playButton.onClick.AddListener(PlayGame);
    }

    void QuitGame()
    {
        Debug.Log("Tha prepe na vgei");
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    void PlayGame()
    {
        Debug.Log("Tha prepe na mpei");
    }
}
