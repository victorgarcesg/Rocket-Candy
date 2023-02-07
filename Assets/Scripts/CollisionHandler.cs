using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    private Movement _movement;
    void Start()
    {
        _movement = GetComponent<Movement>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's a friendly object");
                break;
            case "Finish":
                StartSequence(nameof(LoadNextLevel));
                break;
            default:
                StartSequence(nameof(ReloadLevel));
                break;
        }
    }

    private void StartSequence(string functionName)
    {
        _movement.enabled = false;
        Invoke(functionName, levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
