using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class S_SceneManager : MonoBehaviour
{
    public string playerTag1 = "Player1";
    public string playerTag2 = "Player2";

    private void OnCollisionEnter(Collision collision)
    {
        if ((gameObject.CompareTag(playerTag1) && collision.gameObject.CompareTag(playerTag2)) ||
            (gameObject.CompareTag(playerTag2) && collision.gameObject.CompareTag(playerTag1)))
        {
            Debug.Log("Collision detected between Player1 and Player2.");
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("No next scene in build settings.");
        }
    }
}
