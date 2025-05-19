using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject fireEffectPrefab;
    public Transform firePoint;
    public Transform effectPoint;

    public AudioClip fireSound;
    public AudioClip relodingSound;
    private AudioSource audioSource;

    private float fireCooldown = 0f;
    private bool isReloading = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        var weapon = PlayerWeaponManager.Instance.currentWeaponSO;
        if (weapon == null) return;

        fireCooldown -= Time.deltaTime;

        // ���� ���̸� �ƹ� �͵� ����
        if (isReloading) return;

        // �߻�
        if (Input.GetMouseButton(0) && fireCooldown <= 0f && weapon.currentAmmo > 0)
        {
            Shoot(weapon);
            fireCooldown = 1f / weapon.fireRate;
        }

        GameManager.Instance?.UpdateAmmoUI(weapon);

        // ���� Ű �Է� (R)
        if (Input.GetKeyDown(KeyCode.R) && weapon.currentAmmo < weapon.maxAmmo)
        {
            audioSource.PlayOneShot(relodingSound, 0.5f);
            StartCoroutine(Reload(weapon));
        }
    }

    void Shoot(WeaponSO weapon)
    {
        weapon.currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject fireEffect = Instantiate(fireEffectPrefab, firePoint.position, effectPoint.rotation);
        audioSource.PlayOneShot(fireSound, 0.2f);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = weapon.damage;
            bulletScript.moveDirection = firePoint.forward;
        }

        Debug.Log($"{weapon.weaponName} �߻�! ���� ź��: {weapon.currentAmmo}");
    }



    System.Collections.IEnumerator Reload(WeaponSO weapon)
    {
        isReloading = true;
        GameManager.Instance?.SetReloadingUI(true);

        Debug.Log($" {weapon.weaponName} ���� ��...");

        yield return new WaitForSeconds(weapon.reloadTime);

        weapon.currentAmmo = weapon.maxAmmo;
        isReloading = false;

        Debug.Log($"{weapon.weaponName} ���� �Ϸ�! ź��: {weapon.currentAmmo}");

        GameManager.Instance?.SetReloadingUI(false);

        GameManager.Instance?.UpdateAmmoUI(weapon);
    }
}
