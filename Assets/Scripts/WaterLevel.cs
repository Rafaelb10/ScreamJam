using UnityEngine;
using System.Collections;

public class WaterLevel : MonoBehaviour
{
    [SerializeField] private Player player;
     private float moveSpeedNormal = 3f;
     private float waterRiseSpeed = 0.007f;
     private float waterLowerSpeed = 0.2f;
     private float targetY = -0.47f;
     private float startY = -1.30f;

    private float lastY;

    void Start()
    {
        transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        lastY = transform.position.y;
    }

    void Update()
    {
        MoverAgua();
        AtualizarVelocidade();

        if (transform.position.y < lastY)
            player.SetMoveSpeed(moveSpeedNormal);

        lastY = transform.position.y;
    }

    private void MoverAgua()
    {
        float currentY = transform.position.y;
        float step = 0f;

        if (currentY < targetY)
        {
            step = waterRiseSpeed * Time.deltaTime;
        }
        else if (currentY > targetY)
        {
            step = -waterLowerSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(
            transform.position.x,
            Mathf.MoveTowards(currentY, targetY, Mathf.Abs(step)),
            transform.position.z
        );
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
        targetY -= valor;

        float limiteMinimo = -2.0f;
        if (targetY < limiteMinimo)
            targetY = limiteMinimo;
    }

    public void SetTargetY(float novoValor)
    {
        targetY = novoValor;
    }
}
