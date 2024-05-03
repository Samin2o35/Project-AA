using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Gun variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate;

    private float fireTimer;

    // Player variables
    [SerializeField] private float speed;

    private Rigidbody2D playerRb;

    private float mx;
    private float my;

    private Vector2 mousePos;

    #endregion

    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Initialize player and mouse movement
        Movement();

        // Handle Shooting
        if(Input.GetMouseButton(0) & fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        // Handle player movement speed
        playerRb.velocity = new Vector2 (mx, my).normalized * speed;
    }

    #region Functions
    private void Movement()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y,
            mousePos.x - transform.position.y) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }
    #endregion
}
