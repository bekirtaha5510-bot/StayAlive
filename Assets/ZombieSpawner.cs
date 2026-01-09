using UnityEngine;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Spawn")]
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    [Header("Mission Settings")]
    public int totalZombiesToSpawn = 10;   // GÖREV: toplam üretilecek zombi
    public float spawnInterval = 2f;
    public int maxAliveZombies = 3;

    [Header("UI (opsiyonel)")]
    public TextMeshProUGUI taskText;

    int spawnedCount = 0;
    float timer = 0f;
    bool missionFinished = false;

    void Start()
    {
        UpdateTaskUI();
    }

    void Update()
    {
        if (missionFinished) return;

        int alive = GameObject.FindGameObjectsWithTag("Zombie").Length;

        // Spawn (toplam limit dolana kadar)
        timer += Time.deltaTime;
        if (timer >= spawnInterval &&
            alive < maxAliveZombies &&
            spawnedCount < totalZombiesToSpawn)
        {
            SpawnZombie();
            spawnedCount++;
            timer = 0f;
        }

        // Görev bitti mi? (Üretim tamam + sahnede canlý yok)
        if (spawnedCount >= totalZombiesToSpawn && alive == 0)
        {
            missionFinished = true;

            // Win ekranýný aç
            WinManager wm = FindObjectOfType<WinManager>();
            if (wm != null) wm.ShowWin();
            else Time.timeScale = 0f;
        }

        UpdateTaskUI(alive);
    }

    void SpawnZombie()
    {
        if (zombiePrefab == null || spawnPoints == null || spawnPoints.Length == 0) return;

        int i = Random.Range(0, spawnPoints.Length);
        Instantiate(zombiePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
    }

    void UpdateTaskUI(int aliveOverride = -1)
    {
        if (taskText == null) return;

        int alive = (aliveOverride == -1)
            ? GameObject.FindGameObjectsWithTag("Zombie").Length
            : aliveOverride;

        taskText.text =
            $"GÖREV\n" +
            $"Kalan: {alive}\n" +
            $"Üretilen: {spawnedCount}/{totalZombiesToSpawn}\n" +
            $"Hepsini temizle!";
    }
}
