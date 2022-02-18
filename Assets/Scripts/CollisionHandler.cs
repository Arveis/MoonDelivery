using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip victorySFX;
    [SerializeField] AudioClip explosionSFX;

    [SerializeField] ParticleSystem victoryVFX;
    [SerializeField] ParticleSystem explosionVFX;

    AudioSource audioSource;
    ParticleSystem particle;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start() {
        {
             audioSource = GetComponent<AudioSource>();
        }
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    private void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning || collisionDisabled) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
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
        victoryVFX.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(victorySFX);

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    private void StartCrashSequence()
    {
        explosionVFX.Play();
        isTransitioning = true;
        audioSource.Stop();
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
