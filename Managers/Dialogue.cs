using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class Dialogue : MonoBehaviour
{
    WeaponInventorySlot weaponInventorySlot;
    InputHandler inputHandler;

    public TextMeshProUGUI textComponet;
    public string[] lines;
    public AudioSource[] Joline;
    public float textSpeed;
    public bool hasSword;
    public bool HasTriggered_Start;

    private int index;

    public AudioSource audioSource;


    [SerializeField]
    private InputActionReference next;


    private void Awake()
    {
        weaponInventorySlot = FindObjectOfType<WeaponInventorySlot>();
        inputHandler = FindObjectOfType<InputHandler>();
    }

    void StartDialogue() //--------------------------------------------------------------------------------------------
    {
        index = 0;
        //Joline[index].Play();
        StartCoroutine(TypeLine());
        Debug.Log("StartDialogue");

    }

    private void OnTriggerEnter(Collider collider)
    {
        HasTriggered_Start = true;
        textComponet.text = string.Empty;
        StartDialogue();
        Debug.Log("OnTriggerEnter");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    IEnumerator TypeLine() //--------------------------------------------------------------------------------------------
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponet.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        Debug.Log("TypeLine");
    }

    // Update is called once per frame
    void Update()
    {
        if (inputHandler.a_Input && index < 4 && HasTriggered_Start)
        {
            Debug.Log("space key was pressed");
            if (textComponet.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponet.text = lines[index];
            }
            //next.action.performed += PerformNext;
        }
        if (inputHandler.a_Input && hasSword && HasTriggered_Start)
        {
            Debug.Log("space key was pressed");
            if (textComponet.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponet.text = lines[index];
            }
        }

        //if (Input.GetKeyDown(KeyCode.Space) && index < 2)
        //{
        //    Debug.Log("space key was pressed");
        //    if (textComponet.text == lines[index])
        //    {
        //        NextLine();
        //    }
        //    else
        //    {
        //        StopAllCoroutines();
        //        textComponet.text = lines[index];
        //    }
        //    //next.action.performed += PerformNext;
        //}
        //if (Input.GetKeyDown(KeyCode.Space) && hasSword)
        //{
        //    Debug.Log("space key was pressed");
        //    if (textComponet.text == lines[index])
        //    {
        //        NextLine();
        //    }
        //    else
        //    {
        //        StopAllCoroutines();
        //        textComponet.text = lines[index];
        //    }
        //}

    }
    void NextLine() //--------------------------------------------------------------------------------------------
    {
        if (index < lines.Length - 1)
        {
            Debug.Log("NextLine_If");
            index++;
            textComponet.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            textComponet.gameObject.SetActive(false);
            HasTriggered_Start = false;
        }

    }

}