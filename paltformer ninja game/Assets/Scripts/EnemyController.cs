using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform pointA, pointB;
    [Range(1f,10f)]
    public float speed;

    private Vector3 currentTraget;
    private SpriteRenderer sr;

    void Start()
    {
        Init(); 
    }

    void Update()
    {
        EnemyMovement();
    }

    private void Init()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void EnemyMovement()
    {
        if (transform.position == pointA.position)
        {
            currentTraget = pointB.position;
            sr.flipX = false;
        }
        else if(transform.position == pointB.position)
        {
            currentTraget = pointA.position;
            sr.flipX = true;
        }
        transform.position = Vector3.MoveTowards(transform.position,currentTraget,speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerController.instance.DecreaseDamage(damage: 10);
        }
    }
}
