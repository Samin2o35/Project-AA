using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float speed;

    [Range(1, 10)]
    [SerializeField] private float lifeTime;

    private Rigidbody2D bulletRb;

    private void Start()
    {
        bulletRb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate()
    {
        bulletRb.velocity = transform.up * speed;
    }
}
