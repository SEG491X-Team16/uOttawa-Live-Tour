using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class GeeGeeAnimations : MonoBehaviour
{

    Animator animator;
    Scene currentScene;
    int currentIndex;
    public SceneSwitch sceneSwitch;

    // Start is called before the first frame update
    void Start(){
        animator = GetComponent<Animator>();
        currentIndex = 1;
        
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "TourFinish"){
            animator.SetInteger("Action", 11); //congrats
        } else{
            PlayAnimation();
        }
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void NextAnimation(int max){

        if (currentScene.name == "TourFinish"){
            StartCoroutine(Finish());
        }

        if (currentIndex < max){
            currentIndex ++;
            if (currentIndex == max - 1){
                animator.SetInteger("Action", 6); //pumped
            } else{
                PlayAnimation();
            }
        }   
    }

    public void PreviousAnimation(int max){
        if (currentIndex > 1){
            currentIndex --;
            PlayAnimation();
        }
    }

    void PlayAnimation(){
        switch (currentIndex){
            case 0:
                animator.SetInteger("Action", 0); //idle
                break;
            case 1:
                animator.SetInteger("Action", 1); //wave
                break;
            case 2:
                animator.SetInteger("Action", 2); //behold
                break;
            case 3:
                animator.SetInteger("Action", 3); //talk
                break;
            case 4:
                animator.SetInteger("Action", 4); //hmmm
                break;
            case 5:
                animator.SetInteger("Action", 5); //gunshow
                break;
            case 10:
                animator.SetInteger("Action", 7); //backflip
                break;
            default:
                int random = Random.Range(2,5);
                animator.SetInteger("Action", random); //random
                break;
        }

    }

    IEnumerator Finish(){
        animator.SetInteger("Action", 1);
        yield return new WaitForSecondsRealtime(3);
        sceneSwitch.loadMainMenu();

    }
}
