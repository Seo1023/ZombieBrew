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

    private SkillController skillController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        skillController = GetComponent<SkillController>();
    }

    void Update()
    {
        if(GameManager.Instance == null || GameManager.Instance.isGameOver || GameManager.Instance.isGameClear || PauseManager.isPaused || PassiveSkillSelectorUI.IsSelectingSkill)
            return;

        var weapon = PlayerWeaponManager.Instance.currentWeaponSO;
        if (weapon == null) return;

        fireCooldown -= Time.deltaTime;

        if (isReloading) return;

        if (Input.GetMouseButton(0) && fireCooldown <= 0f && weapon.currentAmmo > 0)
        {
            Shoot(weapon);
            fireCooldown = 1f / weapon.fireRate;
        }
        

        GameManager.Instance?.UpdateAmmoUI(weapon);

        // 장전 키 입력 (R)
        if (Input.GetKeyDown(KeyCode.R) && weapon.currentAmmo < weapon.maxAmmo)
        {
            //audioSource.PlayOneShot(relodingSound, 0.5f);
            AudioManager.instance.PlaySFX("relodingSound");
            StartCoroutine(Reload(weapon));
        }
    }

    void Shoot(WeaponSO weapon)
    {
        weapon.currentAmmo--;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject fireEffect = Instantiate(fireEffectPrefab, firePoint.position, effectPoint.rotation);
        //audioSource.PlayOneShot(fireSound, 0.2f);
        AudioManager.instance.PlaySFX("fireSound");

        //audioSource.PlayOneShot(clip, 0.2f);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = weapon.damage;
            bulletScript.moveDirection = firePoint.forward;
        }

        
        Debug.Log($"{weapon.weaponName} 발사! 남은 탄약: {weapon.currentAmmo}");
    }



    System.Collections.IEnumerator Reload(WeaponSO weapon)
    {
        isReloading = true;
        GameManager.Instance?.SetReloadingUI(true);

        Debug.Log($" {weapon.weaponName} 장전 중...");

        yield return new WaitForSeconds(weapon.reloadTime);

        weapon.currentAmmo = weapon.maxAmmo;
        isReloading = false;

        Debug.Log($"{weapon.weaponName} 장전 완료! 탄약: {weapon.currentAmmo}");

        GameManager.Instance?.SetReloadingUI(false);

        GameManager.Instance?.UpdateAmmoUI(weapon);
    }
}
