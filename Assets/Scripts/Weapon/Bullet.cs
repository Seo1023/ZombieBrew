using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 moveDirection;
    public float speed = 50f;
    public int damage = 10;
    public GameObject hitEffectPrefab;

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

        if (hitEffectPrefab != null)
        {
            Vector3 hitPoint = transform.position;
            Quaternion hitRotation = Quaternion.LookRotation(-moveDirection);

            GameObject effect = Instantiate(hitEffectPrefab, hitPoint, hitRotation);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();
            if(ps != null)
            {
                var main = ps.main;
                main.startSizeMultiplier *= 10f;
            }
            Destroy(effect, 1.5f);
        }

        ZombieStats stats = other.GetComponentInParent<ZombieStats>();
        if (stats != null)
        {
            Debug.Log("[�Ѿ�] ������ ����!");
            stats.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
