using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections;
public class Dialogue : MonoBehaviour
{

    [Header("Script Comms")]

    public PlayerTestScript PlayerScript;
    public AudioManager AudioScript;

    public GameObject DialogueBox;
    public TextMeshProUGUI dialogueText;


    //NPC Dialogue
    public string[] NPC_lines;



    //Displayed Dialogue (taken from each array)
    public string[] lines;



    public float TextPrintTime;

    private int index;

    void Start()
    {
        dialogueText.text = string.Empty;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = lines[index];
            }
        }
    }

    public void StartDialogue()
    {
        index = 0;
        NPC_Check(); //checks which NPC is being spoken to and loads lines
        StartCoroutine(TypeLine()); //starts typing text from lines array

    }

    IEnumerator TypeLine()
    {

        dialogueText.text = string.Empty; // Clear text at the start of typing


        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(TextPrintTime);
        }

    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    void NPC_Check()
    {
        if (PlayerScript.currentNPC != null) //if npc line is empty, don't run any of this
        {


            //Kardians
            if (PlayerScript.currentNPC == "NPC")
            {
                lines = NPC_lines;

            }
            else
            {
                Debug.Log("NPC Tag is empty/not loaded correctly.");
            }
        }
    }
}


    

