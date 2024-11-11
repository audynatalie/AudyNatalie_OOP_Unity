using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Awake()
    {
        if (animator== null)
        {
            animator.enabled=false;
        }
    }

    // Starts the scene transition process
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    // Handles the transition animation and loads the new scene
    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Optional trigger for starting the transition animation
        yield return new WaitForSeconds(1f);

        // Sets the trigger to end the transition animation
        animator.SetTrigger("EndTransition");

        SceneManager.LoadScene(sceneName);
    }
}
