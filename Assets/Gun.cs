using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    public int maxAmmo = 30;
    public int ammo = 30;
    public float range = 50f;

    public TextMeshProUGUI ammoText;

    [Header("VFX")]
    public ParticleSystem muzzleFlash;
    public GameObject bloodPrefab;     // BloodFX prefabýný buraya vereceksin
    public float bloodLifeTime = 1.5f;

    void Start()
    {
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            ammo--;
            UpdateAmmoUI();

            if (muzzleFlash != null) muzzleFlash.Play();

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, range))
            {
                // ? Kan: sadece Zombie vurulunca çýksýn
                if (hit.transform.CompareTag("Zombie") || hit.transform.GetComponentInParent<ZombieAI>() != null)
                {
                    if (bloodPrefab != null)
                    {
                        GameObject blood = Instantiate(
                            bloodPrefab,
                            hit.point,
                            Quaternion.LookRotation(hit.normal)
                        );
                        Destroy(blood, bloodLifeTime);
                    }
                }

                // ? Ölüm (animasyon)
                ZombieAI zombie = hit.transform.GetComponentInParent<ZombieAI>();
                if (zombie != null)
                {
                    zombie.Die();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ammo = maxAmmo;
            UpdateAmmoUI();
        }
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
            ammoText.text = $"Ammo: {ammo} / {maxAmmo}";
    }
}
