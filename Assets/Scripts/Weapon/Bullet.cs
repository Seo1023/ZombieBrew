using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 50f;
    public int damage = 10;

    private void Start()
    {
        // 5�� �� �ڵ� ���� (�Ǽ��� ���� ������ �Ѿ� ����)
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[�Ѿ� �浹] ������Ʈ �̸�: {other.name}, �±�: {other.tag}");

        if (!other.CompareTag("Zombie")) return;

        ZombieStats stats = other.GetComponentInParent<ZombieStats>();
        if (stats != null)
        {
            Debug.Log("[�Ѿ�] ������ ����!");
            stats.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
