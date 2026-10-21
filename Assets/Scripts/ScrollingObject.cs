using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 10f;
    private GameManager gameManager;

    private void Start()
    {
        GameObject findGo = GameObject.FindWithTag("GameController");
        gameManager = findGo.GetComponent<GameManager>();
    }
    private void Update()
    {
        if (gameManager.IsGameOver)
        {
            return;
        }
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
    }
}
