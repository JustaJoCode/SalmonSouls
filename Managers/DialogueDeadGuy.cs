using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class DialogueDeadGuy : MonoBehaviour
{
    //WeaponInventorySlot weaponInventorySlot;
    InputHandler inputHandler;

    public TextMeshProUGUI textComponet;
    public string[] lines_DeadGuy;
    public AudioSource[] Joline;
    public float textSpeed;
    public bool hasSword;
    //public BoxCollider collider;
    public bool Hastriggered_DeadGuy;

    private int index;

    public AudioSource audioSource;


    [SerializeField]
    private InputActionReference next;
    private Collider collider;


    private void Awake()
    {
        //weaponInventorySlot = FindObjectOfType<WeaponInventorySlot>();
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
        Hastriggered_DeadGuy = true;
        textComponet.gameObject.SetActive(true);
        textComponet.text = string.Empty;
        StartDialogue();
        Debug.Log("OnTriggerEnter_DeadGUY");
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    IEnumerator TypeLine() //--------------------------------------------------------------------------------------------
    {
        foreach (char c in lines_DeadGuy[index].ToCharArray())
        {
            textComponet.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        Debug.Log("TypeLine_DeadGUY");
    }

    // Update is called once per frame
    void Update()
    {

        if (inputHandler.a_Input && Hastriggered_DeadGuy)
        {
            Debug.Log("space key was pressed_DeadGUY");
            if (textComponet.text == lines_DeadGuy[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponet.text = lines_DeadGuy[index];
            }
        }



    }
    void NextLine() //--------------------------------------------------------------------------------------------
    {
        if (index < lines_DeadGuy.Length - 1)
        {
            Debug.Log("NextLine_If");
            index++;
            textComponet.text = string.Empty;
            StartCoroutine(TypeLine());

        }
        else
        {
            Debug.Log("NextLine_Else");
            //SceneManager.LoadScene(2);
            Hastriggered_DeadGuy = false;
            textComponet.gameObject.SetActive(false);
        }
    }




}