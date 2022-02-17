using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip victorySFX;
    [SerializeField] AudioClip explosionSFX;

    AudioSource audioSource;

    void Start() {
        {
             audioSource = GetComponent<AudioSource>();
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSucessSequence();
                break;
            case "Fuel":
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSucessSequence()
    {
        // add particle effect for victory
        audioSource.PlayOneShot(victorySFX);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    private void StartCrashSequence()
    {
        // add particle effect for crash
        audioSource.PlayOneShot(explosionSFX, 0.3f);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delay);
    }
    void ReloadLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentScene);
    }

    void LoadNextLevel()
    {
        int CurrentScene = SceneManager.GetActiveScene().buildIndex;
        int NextScene = CurrentScene + 1;
        // check if this is the last level
        if (NextScene == SceneManager.sceneCountInBuildSettings)
        {
            NextScene = 0;
        }
        SceneManager.LoadScene(NextScene);
    }
}
