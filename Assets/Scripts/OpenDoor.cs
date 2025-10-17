using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private float code;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource _doorSound;

    private List<float> currentCodeList = new List<float>();
    private bool isOpen = false;

    private float distance = 2f;
    private float speed = 2f;

    private bool isMoving = false;

    //Variaveis da luz indicadora
    [SerializeField] private GameObject lamp;
    [SerializeField] private Material lightIncorrect;
    [SerializeField] private Material lightCorrect;
    [SerializeField] private Material lightNeutra;

    private void Start()
    {
        lamp.GetComponent<MeshRenderer>().sharedMaterial = lightNeutra;
    }

    public void AddCode(float number)
    {
        if (isOpen) return;

        currentCodeList.Add(number);

        if (currentCodeList.Count == 4)
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
            lamp.GetComponent<MeshRenderer>().sharedMaterial = lightCorrect;
            StartCoroutine(MoveZSmooth());
        }
        else
        {
            currentCodeList.Clear();
            lamp.GetComponent<MeshRenderer>().sharedMaterial = lightIncorrect;
            StartCoroutine(lightRevert());
        }
    }

    private IEnumerator MoveZSmooth()
    {
        isMoving = true;
        _doorSound.Play();
        Vector3 startPos = door.transform.position;
        Vector3 targetPos = startPos + new Vector3(0, 0, distance);

        float elapsed = 0f;
        float duration = distance / speed;

        while (elapsed < duration)
        {
            door.transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        door.transform.position = targetPos;
        isMoving = false;
        _doorSound.Pause();
    }

    private IEnumerator lightRevert()
    {
        yield return new WaitForSeconds(2.5f);
        lamp.GetComponent<MeshRenderer>().sharedMaterial = lightNeutra;
    }
}
