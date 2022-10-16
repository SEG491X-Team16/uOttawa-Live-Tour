using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class DialogueManager : MonoBehaviour
{    
    private AudioClip[] audioClips;
    private string[] dialogueInformation;
    private int currentIndex;

    public AudioSource source;
    public Text buildingNameText;
    public Text informationText;
    public Text nextButtonText;
    public Text playButtonText;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update(){
        if (source.isPlaying){
            playButtonText.text = "Pause II";
        } else {
            playButtonText.text = "Play ►";
        }
    }
    public void StartDialogue (Dialogue dialogue) {
        Debug.Log("Display information about "+ dialogue.buildingName);

        animator.SetBool("IsOpen",true);

        buildingNameText.text = dialogue.buildingName; 

        audioClips = dialogue.audioClips;
        dialogueInformation = dialogue.informations;
        currentIndex = -1;
        nextButtonText.text = "Next";

        // foreach (Sprite s in dialogue.image.sprite)
        // {
        //     image.sprite.Enqueue(s);
        // }
        DisplayNext();
    
    }
    
    public void DisplayNext(){
        source.Stop();
        currentIndex ++;
        if (currentIndex == dialogueInformation.Length){
            EndDialogue();
            return;
        }
        if (currentIndex == dialogueInformation.Length -1){
            nextButtonText.text = "Finish";
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogueInformation[currentIndex])); 
        Debug.Log("Current Index: " + currentIndex);
        
    }

    public void DisplayPrevious(){
        if (currentIndex == dialogueInformation.Length -1){
            nextButtonText.text = "Next";
        }
        if (currentIndex > 0){
            source.Stop();
            currentIndex --;
        
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogueInformation[currentIndex])); 
        } 
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

    public void PlayAudio(){
        source.clip = audioClips[currentIndex];
        if (source.isPlaying){
            source.Pause();
        } else {
            source.Play();
        }
    }

}
