using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] InputAction _push;
    [SerializeField] InputAction _rotation;
    [SerializeField] AudioClip _mainEngineSFX;
    [SerializeField] ParticleSystem _mainEngineParticles;
    [SerializeField] ParticleSystem _leftBoosterParticles;
    [SerializeField] ParticleSystem _rightBoosterParticles;

    [Header("Settings")]
    [SerializeField] float _pushStrength = 100f;
    [SerializeField] float _rotationStrength = 100f;

    Rigidbody _rigidbody;
    AudioSource _audioSource;

    private void OnEnable()
    {
        _push.Enable();
        _rotation.Enable();
    }
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        ProcessPush();
        ProcessRotation();
    }

    private void ProcessPush()
    {
        if (_push.IsPressed())
        {
            StartPushing();
        }
        else
        {
            StopPushing();
        }
    }
    private void StartPushing()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _pushStrength * Time.fixedDeltaTime);
        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_mainEngineSFX);
        }
        if (!_mainEngineParticles.isPlaying)
        {
            _mainEngineParticles.Play();
        }
    }
    private void StopPushing()
    {
        _audioSource.Stop();
        _mainEngineParticles.Stop();
    }
    private void ProcessRotation()
    {
        float _rotationInput = _rotation.ReadValue<float>();

        if (_rotationInput < 0)
        {
            RotateRight();
        }
        else if (_rotationInput > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }
    private void RotateRight()
    {
        ApplyRotation(_rotationStrength);
        if (!_rightBoosterParticles.isPlaying)
        {
            _rightBoosterParticles.Play();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(-_rotationStrength);
        if (!_leftBoosterParticles.isPlaying)
        {
            _leftBoosterParticles.Play();
        }
    }
    private void StopRotating()
    {
        _rightBoosterParticles.Stop();
        _leftBoosterParticles.Stop();
    }
    private void ApplyRotation(float _rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * _rotationThisFrame * Time.fixedDeltaTime);
        _rigidbody.freezeRotation = false;

    }

}
