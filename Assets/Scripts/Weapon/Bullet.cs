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
        // 5초 후 자동 제거 (실수로 땅에 떨어진 총알 방지)
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[총알 충돌] 오브젝트 이름: {other.name}, 태그: {other.tag}");

        if (!other.CompareTag("Zombie")) return;

        ZombieStats stats = other.GetComponentInParent<ZombieStats>();
        if (stats != null)
        {
            Debug.Log("[총알] 데미지 적용!");
            stats.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
