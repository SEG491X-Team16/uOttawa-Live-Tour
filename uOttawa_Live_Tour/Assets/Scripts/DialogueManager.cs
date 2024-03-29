using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Localization.Settings;


/**
 * This manager handles displaying the dialogues to the user at a POI.
 */
public class DialogueManager : MonoBehaviour
{    
    private AudioClip[] audioClips;
    private string[] dialogueInformation;
    private int currentIndex;

    public AudioSource source;
    public Text buildingNameText;
    public Text informationText;
    public Text textSizer;
    public Text nextButtonText;
    public Text playButtonText;

    public Animator animator;

    public UnityEvent dialogueFinished;

    // Start is called before the first frame update
    void Start()
    {

    }
    void Update(){
        if (source.isPlaying){
            playButtonText.text = "Pause II";
        } else {
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
                playButtonText.text = "Jouer ►";
            } else{
                playButtonText.text = "Play ►";
            }
        }
    }
    public void StartDialogue (Dialogue dialogue) {
        Debug.Log("Display information about "+ dialogue.buildingName);

        animator.SetBool("IsOpen",true);

        buildingNameText.text = dialogue.buildingName; 

        audioClips = dialogue.audioClips;
        dialogueInformation = dialogue.informations;
        currentIndex = -1;
        if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
                nextButtonText.text = "Suivant";
         } else{
            nextButtonText.text = "Next";
        }

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
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
                nextButtonText.text = "Finir";
            } else{
                nextButtonText.text = "Finish";
            }
        }

        StopAllCoroutines();
        StartCoroutine(ResizeFont());
        StartCoroutine(TypeSentence(dialogueInformation[currentIndex])); 
        Debug.Log("Current Index: " + currentIndex);
        
    }

    public void DisplayPrevious(){
        if (currentIndex == dialogueInformation.Length -1){
            if (LocalizationSettings.SelectedLocale == LocalizationSettings.AvailableLocales.GetLocale("fr")){
                nextButtonText.text = "Suivant";
            } else{
                nextButtonText.text = "Next";
            }
        }
        if (currentIndex > 0){
            source.Stop();
            currentIndex --;
        
            StopAllCoroutines();
            StartCoroutine(ResizeFont());
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

        dialogueFinished.Invoke();
    }

    public void PlayAudio(){
        source.clip = audioClips[currentIndex];
        Debug.Log("playing" + audioClips[currentIndex].channels);
        if (source.isPlaying){
            source.Pause();
        } else {
            source.Play();
        }
    }

    IEnumerator ResizeFont(){
        textSizer.text = dialogueInformation[currentIndex];
        
        yield return new WaitForEndOfFrame();

        informationText.fontSize =  (int)(textSizer.cachedTextGenerator.fontSizeUsedForBestFit/2 + 4.5);
        Debug.Log((int)(textSizer.cachedTextGenerator.fontSizeUsedForBestFit/2 + 4.5));
        //informationText.fontSize =  (int)(textSizer.cachedTextGenerator.fontSizeUsedForBestFit * 0.555 + 0.005);
        //informationText.fontSize = textSizer.cachedTextGenerator.fontSizeUsedForBestFit;
    }

}
