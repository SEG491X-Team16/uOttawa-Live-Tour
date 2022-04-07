using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{   
    private Queue<string> informations;
    public Text buildingNameText;
    public Text informationText;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        informations = new Queue<string>();

    }
    public void StartDialogue (Dialogue dialogue) {
        Debug.Log("Display information about "+ dialogue.buildingName);

        animator.SetBool("IsOpen",true);

        buildingNameText.text = dialogue.buildingName; 

        informations.Clear();

        foreach (string information in dialogue.informations)
        {
            informations.Enqueue(information);
            
        }
        DisplayNext();
    
    }
    public void DisplayNext(){
        if (informations.Count==0){
            EndDialogue();
            return;
        }

        string information = informations.Dequeue();
        StopAllCoroutines();
        Debug.Log(information);
        StartCoroutine(TypeSentence(information)); 
    }

    IEnumerator TypeSentence (string information)
    {
        informationText.text ="";
        foreach(char letter  in information.ToCharArray()){
            informationText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue(){
        Debug.Log("end");
        animator.SetBool("IsOpen",false);

    }
}
