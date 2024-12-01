using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;  // Animator to control scene transition animations

    void Awake()
    {
        // Disable the animator at the start to prevent any animation from playing prematurely
        animator.enabled = false;
    }

    // Coroutine to load a new scene asynchronously with a transition animation
    IEnumerator LoadSceneAsync(string sceneName)
    {
        // Enable the animator and play the "StartTransition" animation
        animator.enabled = true;

        // Wait for the "StartTransition" animation to complete
        yield return new WaitForSeconds(1f);  // Adjust the time based on the duration of the "StartTransition" animation

        // Load the new scene asynchronously
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the scene has finished loading
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        // Once the scene is loaded, play the "EndTransition" animation
        animator.SetTrigger("EndTransition");
    }

    // Public method to trigger scene loading
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));  // Start the scene loading coroutine
    }
}
