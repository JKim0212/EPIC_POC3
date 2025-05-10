using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    [SerializeField] GameObject ammoToSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GunManager.Instance.fireAction += SpawnAmmo;
        ResetManager.Instance.resetAction += ResetAll;
        SpawnAmmo();
    }

    void SpawnAmmo()
    {
        Instantiate(ammoToSpawn, transform);
    }

    void ResetAll()
    {
        if(transform.childCount == 0)
        {
            SpawnAmmo();
        }
    }
}
