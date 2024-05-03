using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private KeyCode pullBeamInput;
    [SerializeField] private KeyCode deathBeamInput;

    private Rigidbody2D playerRb;

    private float mx;
    private float my;

    private Vector2 mousePos;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Initialize player and mouse movement
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y, 
            mousePos.x - transform.position.y) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    private void FixedUpdate()
    {
        // Handle player movement speed
        playerRb.velocity = new Vector2 (mx, my).normalized * speed;
    }
}
