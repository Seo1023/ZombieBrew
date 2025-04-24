using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public int damage = 10;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // Bullet lifetime
        if (transform.position.magnitude > 100f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        ZombieStats zombie = other.GetComponent<ZombieStats>();
        if (zombie != null)
        {
            zombie.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
