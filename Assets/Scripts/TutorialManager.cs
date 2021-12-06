using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialPrompt;
    public GameObject yes;
    public GameObject no;

    public GameObject tutorialslides;
    public GameObject slide1;
    public GameObject slide2;
    public GameObject slide3;
    public GameObject slide4;
    public GameObject slide5;
    public GameObject slide6;

    public GameObject nextBttn;
    public GameObject backBttn;
    public GameObject playBttn;
    public GameObject tutorialPlayBttn;

    // Not tutorial but too lazy
    public GameObject creditsBackground;
    public GameObject exitCredits;
    
    void Awake() {
        tutorialPrompt.SetActive(false);
        yes.SetActive(false);
        no.SetActive(false);
        tutorialslides.SetActive(false);
        slide1.SetActive(false);
        slide2.SetActive(false);
        slide3.SetActive(false);
        slide4.SetActive(false);
        slide5.SetActive(false);
        slide6.SetActive(false);
        nextBttn.SetActive(false);
        backBttn.SetActive(false);
        tutorialPlayBttn.SetActive(false);

        creditsBackground.SetActive(false);
        exitCredits.SetActive(false);
    }

    public void TutorialPrompt() {
        tutorialPrompt.SetActive(true);
        yes.SetActive(true);
        no.SetActive(true);
    }

    public void StartTutorial() {
        tutorialslides.SetActive(true);
        nextBttn.SetActive(true);
        backBttn.SetActive(true);
        tutorialPlayBttn.SetActive(true);
        slide1.SetActive(true);
    }

    public void NextSlide() {
        if (slide1.activeSelf) {
            slide1.SetActive(false);
            slide2.SetActive(true);
        } else if (slide2.activeSelf) {
            slide2.SetActive(false);
            slide3.SetActive(true);
        } else if (slide3.activeSelf) {
            slide3.SetActive(false);
            slide4.SetActive(true);
        } else if (slide4.activeSelf) {
            slide4.SetActive(false);
            slide5.SetActive(true);
        } else if (slide5.activeSelf) {
            slide5.SetActive(false);
            slide6.SetActive(true);
        } else if (slide6.activeSelf) {
            slide6.SetActive(false);
            slide1.SetActive(true);
        }
    }

    public void PreviousSlide() {
        if (slide1.activeSelf) {
            slide1.SetActive(false);
            slide6.SetActive(true);
        } else if (slide6.activeSelf) {
            slide6.SetActive(false);
            slide5.SetActive(true);
        } else if (slide5.activeSelf) {
            slide5.SetActive(false);
            slide4.SetActive(true);
        } else if (slide4.activeSelf) {
            slide4.SetActive(false);
            slide3.SetActive(true);
        } else if (slide3.activeSelf) {
            slide3.SetActive(false);
            slide2.SetActive(true);
        } else if (slide2.activeSelf) {
            slide2.SetActive(false);
            slide1.SetActive(true);
        }
    }

    public void ShowCredits() {
        creditsBackground.SetActive(true);
        exitCredits.SetActive(true);
    }

    public void ExitCredits() {
        creditsBackground.SetActive(false);
        exitCredits.SetActive(false);
    }
}
