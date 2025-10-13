using UnityEngine;

public class OpenDoor : MonoBehaviour, IInteract
{
    public void Interagir()
    {
        Debug.Log("A porta foi aberta!");
    }
}
