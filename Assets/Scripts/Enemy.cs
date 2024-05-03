using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float speed;

    [Range(0f, 0.5f)]
    public float rotateSpeed;

    private Rigidbody2D enemyRb;

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get target direction
        if (!target) 
        {
            GetTarget();
        }

        // Rotate towards target
        else
        {
            RotateTowardsTarget();
        }
    }

    private void FixedUpdate()
    {
        // Move forwards
        enemyRb.velocity = transform.up * speed;
    }

    private void GetTarget()
    {
        if(GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 targetDirection = target.position- transform.position;
        float angle = Mathf.Atan2(targetDirection.y, 
            targetDirection.x) * Mathf.Rad2Deg - 90f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.localRotation = Quaternion.Slerp(transform.localRotation, 
            targetRotation, rotateSpeed);
    }

    // Destroy player after collission with enemy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            target = null;
        }
        else if(collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
