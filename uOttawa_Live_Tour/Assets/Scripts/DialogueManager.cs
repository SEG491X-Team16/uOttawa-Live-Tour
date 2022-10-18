using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class DialogueManager : MonoBehaviour
{
    private List<string> informations;
    private List<Sprite> images;
    public Text buildingNameText;
    public Text informationText;
    public Image inforamtionImage; 

    public Animator animator;
    public UnityEvent dialogueFinished;
    
    public int currentIndex { get; private set; }


    void Start(){
        informations = new List<string>();
        images = new List<Sprite>();

    }

    public void StartDialogue (Dialogue dialogue){
        animator.SetBool("IsOpen",true);

        buildingNameText.text = dialogue.buildingName; 

        informations.Clear();
        images.Clear();
        currentIndex =0;

        foreach (string information in dialogue.informations)
        {
            informations.Add(information);
            
        }

        foreach (Sprite image in dialogue.images)
        {
            images.Add(image);
        }

    DisplayNext();
    }
    
    public void DisplayNext(){
        
       Debug.Log("Before display next index "+currentIndex +informations.Count);
        if (informations.Count==0 || currentIndex >= informations.Count){
            EndDialogue();
            return ;
        }
        
    
        StopAllCoroutines();
      //Debug.Log(information);
        StartCoroutine(TypeSentence(informations[currentIndex++])); 
        inforamtionImage.sprite=images[currentIndex];
        Debug.Log("after display next index "+currentIndex);
    }

    public void DisplayBack(){
        Debug.Log("before display back index "+currentIndex);
        
        
        if (currentIndex<=0 || currentIndex >= informations.Count){
            EndDialogue();
            return;
        }
        //Debug.Log ( $"DisplayBack: currentIndex = {currentIndex}" );
       // --currentIndex;
        Debug.Log("before display back coroutine 1 "+currentIndex);
      
        StopAllCoroutines();
        StartCoroutine(TypeSentence(informations[--currentIndex]));
        inforamtionImage.sprite=images[currentIndex];
        Debug.Log("afrer display back coroutinee 2 "+currentIndex);
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