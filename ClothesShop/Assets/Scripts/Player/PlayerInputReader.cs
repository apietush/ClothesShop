using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        player.MoveVertical(Input.GetAxis("Vertical"));
        player.MoveHorizontal(Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.X))
        {
            player.TryInteract();
        }
    }
}
