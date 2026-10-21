using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;
    public float obstaclesRatio = 0.3f;
    private bool stepped = false;
    private GameManager gameManager;
    public int score = 10;
    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }

    private void OnEnable()
    {
        stepped = false;
        foreach (GameObject obstacles in obstacles)
        {
            //obstacles.SetActive(Random.Range(0f, 1f) < obstaclesRatio); 밑에 코드랑 같은 코드다.
            obstacles.SetActive(Random.value < obstaclesRatio);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player")&& !stepped)
        {
            stepped = true;
            gameManager.AddScore(score);
        }
    }
}
