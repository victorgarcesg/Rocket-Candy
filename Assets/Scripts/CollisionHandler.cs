using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip deathExplosion;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem deathExplosionParticles;
    [SerializeField] ParticleSystem successParticles;
    private Movement _movement;
    private AudioSource _audioSource;
    private bool isTransitioning = true;
    void Start()
    {
        _movement = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("It's a friendly object");
                break;
            case "Finish":
                StartSequence(nameof(LoadNextLevel), success, successParticles);
                break;
            default:
                StartSequence(nameof(ReloadLevel), deathExplosion, deathExplosionParticles);
                break;
        }
    }

    private void StartSequence(string functionName, AudioClip audioClip, ParticleSystem particleSystem)
    {
        if (isTransitioning)
        {
            isTransitioning = false;
            _movement.enabled = false;
            _audioSource.Stop();
            _audioSource.PlayOneShot(audioClip);
            particleSystem.Play();
            Invoke(functionName, levelLoadDelay);
        }
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
