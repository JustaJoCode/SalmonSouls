using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.WSA;

public class TutorialDialogue_Heal : MonoBehaviour
{
    //WeaponInventorySlot weaponInventorySlot;
    InputHandler inputHandler;

    public TextMeshProUGUI textComponet;
    public string[] lines_Heal;
    public AudioSource[] Joline;
    public float textSpeed;
    public bool hasSword;
    public BoxCollider collider;
    public bool Hastriggered;

    private int index;

    public AudioSource audioSource;


    [SerializeField]
    private InputActionReference next;


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
        Hastriggered = true;
        textComponet.gameObject.SetActive(true);
        textComponet.text = string.Empty;
        StartDialogue();
        Debug.Log("OnTriggerEnter_Heal");
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    IEnumerator TypeLine() //--------------------------------------------------------------------------------------------
    {
        foreach (char c in lines_Heal[index].ToCharArray())
        {
            textComponet.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        Debug.Log("TypeLine_Heal");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inputHandler.a_Input && Hastriggered)
        {
            Debug.Log("space key was pressed_Heal");
            if (textComponet.text == lines_Heal[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponet.text = lines_Heal[index];
            }
        }



    }
    void NextLine() //--------------------------------------------------------------------------------------------
    {
        if (index < lines_Heal.Length - 1)
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
            Hastriggered = false;
            textComponet.gameObject.SetActive(false);
        }
    }




}