using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class CollisionHandler : MonoBehaviour
{
  [SerializeField] float levelDelay = 1f;
  [SerializeField] AudioClip success;
  [SerializeField] AudioClip crash;

  [SerializeField] ParticleSystem successParticles;
  [SerializeField] ParticleSystem crashParticles;



  AudioSource audioSource;

  bool isTransistioning = false;
  bool CollisionDisabled = false;

  void Start()
  {
    audioSource = GetComponent<AudioSource>();
  }

  void Update()
  {
    RespondToDebugKeys();
  }

  void RespondToDebugKeys()
  {
    if (Input.GetKeyDown(KeyCode.L))
    {
      LoadNextLevel();
    }
    else if (Input.GetKeyDown(KeyCode.C))
    {
      CollisionDisabled = !CollisionDisabled;   //toggle the collision
    }
  }

  private void OnCollisionEnter(Collision other)
  {
    if (isTransistioning || CollisionDisabled) return;

    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("This thing is friendly");
        break;
      case "Finish":
        Debug.Log("This thing is the finish");
        StartSuccessSequence();
        break;
      default:
        StartCrashSequence();
        break;
    }

  }

  void StartSuccessSequence()
  {
    successParticles.Play();
    isTransistioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelDelay);
  }

  void StartCrashSequence()
  {
    crashParticles.Play();
    isTransistioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(crash);
    // todo add particle effect upon failure
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelDelay);
  }


  void LoadNextLevel()
  {
    int currentScneneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = ++currentScneneIndex;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) nextSceneIndex = 0;
    SceneManager.LoadScene(nextSceneIndex);
  }

  void ReloadLevel()
  {
    int currentScneneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScneneIndex);
  }

}
