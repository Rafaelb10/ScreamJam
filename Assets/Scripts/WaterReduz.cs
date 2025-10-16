using UnityEngine;

public class WaterReduz : MonoBehaviour, IInteract
{
    [SerializeField] private WaterLevel waterLevel;
    private float reductionAmount = 0.3f;

    public void Interagir()
    {
        if (waterLevel != null)
        {
            waterLevel.ReduzirAgua(reductionAmount);
        }
    }
}
