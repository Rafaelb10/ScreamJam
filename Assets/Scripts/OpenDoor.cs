using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float code;
    private List<float> currentCodeList = new List<float>();
    private bool isOpen = false;


    public void AddCode(float number)
    {
        if (isOpen) return;

        currentCodeList.Add(number);

        if (currentCodeList.Count == 3)
        {
            CheckCode();
        }
    }

    private void CheckCode()
    {
        string enteredCodeString = string.Join("", currentCodeList);
        float enteredCode = float.Parse(enteredCodeString);

        if (Mathf.Approximately(enteredCode, code))
        {
            isOpen = true;
            Debug.Log("Porta Aberta!");
        }
        else
        {
            Debug.Log("Código incorreto! Resetando...");
            currentCodeList.Clear();
        }
    }
}
