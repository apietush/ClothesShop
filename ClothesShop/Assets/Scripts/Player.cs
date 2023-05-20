using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

    private float _xVelocity;
    private float _yVelocity;
    private Collider2D interactable;
    private Rigidbody2D rigidbody;

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
        interactable = collider;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        interactable = null;
    }

    public void TryInteract()
    {
        if (interactable?.tag == "ShopTrigger")
        {
            Debug.Log("Interact with shop");
        }
    }

    public void MoveHorizontal(float direction) => _xVelocity = direction;

    public void MoveVertical(float direction) => _yVelocity = direction;

}
