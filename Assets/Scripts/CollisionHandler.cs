using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  private void OnCollisionEnter(Collision other)
  {
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("This thing is friendly");
        break;
      case "Finish":
        Debug.Log("This thing is the finish");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        break;
      case "Fuel":
        Debug.Log("You have collected fuel");
        break;
      default:
        ReloadLevel();
        break;
    }

  }
  void ReloadLevel()
  {
    int currentScneneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentScneneIndex);
  }

}
