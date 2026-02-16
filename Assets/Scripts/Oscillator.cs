using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _movementVector;
    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private float _movementFactor;

    private void Start()
    {
        _startPosition = transform.position;
        _endPosition = _startPosition + _movementVector;
    }

    private void Update()
    {
        _movementFactor = Mathf.PingPong(Time.time * _speed, 1f);
        transform.position = Vector3.Lerp(_startPosition, _endPosition, _movementFactor);
    }

}
