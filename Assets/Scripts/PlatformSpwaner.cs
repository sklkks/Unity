using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public int initCount = 5;
    public Transform spawnPoint;
    private GameObject[] platforms;
    private int currentIndex = 0;
    public float interval = 1f;
    public float intervalRange = 0.1f;
    private float currentInterval;
    private float spwanTimer = 0;
    public GameManager gameManager;
    public float yRange = 2f;

    private void Awake()
    {
        platforms = new GameObject[initCount];
        for(int i = 0; i < platforms.Length; ++i)
        {
            platforms[i] = Instantiate(platformPrefab);
            platforms[i].SetActive(false);
        }        
    }

    private void Start()
    {
        currentInterval = Random.Range(-intervalRange, intervalRange);
        spwanTimer = currentInterval;
    } 
    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }
        spwanTimer += Time.deltaTime;
        if(spwanTimer > currentInterval)
        {
            spwanTimer = 0f;
            currentInterval = interval + Random.Range(-intervalRange, intervalRange);
            Spawn();
        }
    }
    private void Spawn()
    {
        platforms[currentIndex].SetActive(false);
        Vector3 point = spawnPoint.position;
        point.y += Random.Range(-yRange, yRange);
        
        platforms[currentIndex].transform.position = point;
        platforms[currentIndex].SetActive(true);

        currentIndex = (currentIndex + 1 ) % platforms.Length;
    }
}
