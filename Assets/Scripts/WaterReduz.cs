using UnityEngine;

public class WaterReduz : MonoBehaviour, IInteract
{
    [SerializeField] private WaterLevel waterLevel;
    private float reductionAmount = 0.2f;
    private float cooldownTime = 20f; 

    private bool canInteract = true;

    public void Interagir()
    {
        if (!canInteract) return;

        if (waterLevel != null)
        {
            waterLevel.ReduzirAgua(reductionAmount);
            StartCoroutine(Cooldown());
        }
    }

    private System.Collections.IEnumerator Cooldown()
    {
        canInteract = false;
        yield return new WaitForSeconds(cooldownTime);
        canInteract = true;
    }
}
