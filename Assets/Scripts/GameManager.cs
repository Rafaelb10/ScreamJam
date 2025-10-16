using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    void Start()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GameEnded()
    {
        Debug.Log("Game Over");
    }
}
