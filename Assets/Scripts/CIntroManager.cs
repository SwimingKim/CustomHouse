using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CIntroManager : MonoBehaviour
{

    AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void OnStartButtonClick()
    {
        source.Play();

        SceneManager.LoadScene("Game");
        SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Additive);
    }

}
