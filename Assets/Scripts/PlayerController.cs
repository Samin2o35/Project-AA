using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    #region Variables

    // Gun variables
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private LineRenderer lineRenderer;

    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate;

    private float fireTimer;

    // Player variables
    [SerializeField] private float speed;

    private Rigidbody2D playerRb;
    private Camera cam;
    
    private float mx;
    private float my;

    private Vector2 mousePos;

    #endregion

    #region Start and Update
    private void Start()
    {
        cam = Camera.main;
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Initialize player and mouse movement
        Movement();

        // Handle Shooting bullets
        if(Input.GetMouseButton(0) & fireTimer <= 0f)
        {
            ShootBullet();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }

        // Handle Shooting beam
        if (Input.GetMouseButtonDown(1))
        {
            ShootBeam();
        }
    }
    private void FixedUpdate()
    {
        // Handle player movement speed
        playerRb.velocity = new Vector2 (mx, my).normalized * speed;
    }

    #endregion

    #region Functions
    private void Movement()
    {
        mx = Input.GetAxisRaw("Horizontal");
        my = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(mousePos.y - transform.position.y,
            mousePos.x - transform.position.y) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
    private void ShootBullet()
    {
        Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
    }

    private void ShootBeam()
    {
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if(hit.collider != null)
        {
            if(hit == GameObject.FindGameObjectWithTag("Enemy"))
            {
                Debug.Log("hit");
                lineRenderer.SetPosition(0, firingPoint.position);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
    }
    #endregion
}
