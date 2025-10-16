using UnityEngine;

public class BotaoCode : MonoBehaviour, IInteract
{
    [SerializeField] private float number;
    [SerializeField] private OpenDoor painelCode;
    [SerializeField] private AudioSource ButtonSound;

    public void Interagir()
    {
        painelCode.AddCode(number);
        ButtonSound.Play();
    }
}
