using UnityEngine;

public class InteractionHint : MonoBehaviour
{
    private readonly Vector2 PlayerOffset = new Vector2(20, 20);

    [SerializeField] private Transform _playerPosition;

    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        gameObject.transform.position = _camera.WorldToScreenPoint(_playerPosition.position) + (Vector3)PlayerOffset;
    }
}