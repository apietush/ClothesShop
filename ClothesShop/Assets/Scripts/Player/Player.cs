using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject menu;

    [SerializeField] public SpriteRenderer ShirtSprite;
    [SerializeField] public SpriteRenderer BootsSprite;
    [SerializeField] public SpriteRenderer PantsSprite;
    
    private float _xVelocity;
    private float _yVelocity;
    private Collider2D _interactable;
    private Rigidbody2D rigidbody;

    public PlayerState PlayerState { get; set; } = new PlayerState();

    // Start is called before the first frame update
    void Start()
    {

        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = new Vector2(_xVelocity, _yVelocity) * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        _interactable = collider;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        _interactable = null;
    }

    public void TryInteract()
    {
        if (_interactable?.tag == "ShopTrigger")
        {
            menu.SetActive(true);
        }
    }

    public void MoveHorizontal(float direction) => _xVelocity = direction;

    public void MoveVertical(float direction) => _yVelocity = direction;

}
