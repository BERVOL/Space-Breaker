using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioClip _successSFX;
    [SerializeField] AudioClip _crashSFX;
    [SerializeField] ParticleSystem _successParticles;
    [SerializeField] ParticleSystem _crashParticles;
    [SerializeField] InputAction _nextLevelButton;

    [Header("Settings")]
    [SerializeField] private float _levelLoadDelay = 2f;

    AudioSource _audioSource;
    bool isControllable = true;
    bool isCollidable = true;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You Here To Friendly Point!");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    private void Update()
    {
        RespondToDebugKeys();
    }
    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("you went next level");
            LoadNextScene();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("you off the collisions");
            isCollidable = !isCollidable;
        }
    }
    private void ReloadScene()
    {
        int _currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(_currentScene);
    }
    private void LoadNextScene()
    {
        int _currentScene = SceneManager.GetActiveScene().buildIndex;
        int _nextScene = _currentScene + 1;

        if (_nextScene == SceneManager.sceneCountInBuildSettings)
        {
            _nextScene = 0;
        }

        SceneManager.LoadScene(_nextScene);
    }
    private void StartCrashSequence()
    {
        isControllable = false;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashSFX, 0.2f);

        _crashParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", _levelLoadDelay);
    }
    private void StartFinishSequence()
    {
        isControllable = false;

        _audioSource.Stop();
        _audioSource.PlayOneShot(_successSFX, 0.2f);

        _successParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextScene", _levelLoadDelay);
    }
}
