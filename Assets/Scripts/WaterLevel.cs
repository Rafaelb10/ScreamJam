using UnityEngine;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] private Player player;
    private float moveSpeedNormal = 3f;
    private float waterRiseSpeed = 0.1f;
    private float targetY = -0.47f;
    private float startY = -2.01f;

    private float lastY; 

    void Start()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        lastY = transform.position.y;
    }

    void Update()
    {
        if (transform.position.y < targetY)
        {
            transform.position = new Vector3(
                transform.position.x,
                Mathf.MoveTowards(transform.position.y, targetY, waterRiseSpeed * Time.deltaTime),
                transform.position.z
            );
        }

        AtualizarVelocidade();

        if (transform.position.y < lastY)
        {
            player.SetMoveSpeed(moveSpeedNormal);
        }

        lastY = transform.position.y;
    }

    private void AtualizarVelocidade()
    {
        float y = transform.position.y;

        if (y >= -0.70f && y < -0.55f)
            player.SetMoveSpeed(2.5f);
        else if (y >= -0.55f && y < -0.47f)
            player.SetMoveSpeed(2f);
        else if (y >= -0.47f)
            player.SetMoveSpeed(1.5f);
        else
            player.SetMoveSpeed(moveSpeedNormal);
    }

    public void ReduzirAgua(float valor)
    {
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y - valor,
            transform.position.z
        );
    }
}
