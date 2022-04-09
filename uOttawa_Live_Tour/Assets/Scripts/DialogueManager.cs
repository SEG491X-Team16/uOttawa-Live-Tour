using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class DialogueManager : MonoBehaviour
{   
    private Queue<string> informations;
    // private Queue<Sprite> sprites;
    //private Queue<Image> images;
    public Text buildingNameText;
    public Text informationText;
    // public Image image;
    // public Sprite sprite;

    public Animator animator;

    public UnityEvent dialogueFinished;

    // Start is called before the first frame update
    void Start()
    {
        informations = new Queue<string>();
        // sprites = new Queue<Sprite>();
       // images = new Queue<Image>();
    }
    public void StartDialogue (Dialogue dialogue) {
        Debug.Log("Display information about "+ dialogue.buildingName);

        animator.SetBool("IsOpen",true);

        buildingNameText.text = dialogue.buildingName; 

        informations.Clear();
      //  image.Clear();

        foreach (string information in dialogue.informations)
        {
            informations.Enqueue(information);
            
        }
        // foreach (Sprite s in dialogue.image.sprite)
        // {
        //     image.sprite.Enqueue(s);
        // }
        DisplayNext();
    
    }
    public void DisplayNext(){
        Debug.Log("Display next1");
        if (informations.Count==0){
            EndDialogue();
            return;
        }
        Debug.Log("Display next2");
        //maybe remove
        // if (images.Count==0){
        //     return;
        // }

        string information = informations.Dequeue();
        // Image image= images.Dequeue();
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

        dialogueFinished.Invoke();
    }
}
