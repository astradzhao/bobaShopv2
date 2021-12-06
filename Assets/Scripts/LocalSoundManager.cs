using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSoundManager : MonoBehaviour
{
    private AudioSource buttonPressed;
    private AudioSource pouring;
    private AudioSource bottleSqueeze;
    private AudioSource powder;
    private AudioSource customerOrder;
    private AudioSource trashCan;
    private AudioSource toppingsDrop;

    private void Awake() {
        buttonPressed = this.transform.GetChild(0).GetComponent<AudioSource>();
        pouring = this.transform.GetChild(1).GetComponent<AudioSource>();
        bottleSqueeze = this.transform.GetChild(2).GetComponent<AudioSource>();
        powder = this.transform.GetChild(3).GetComponent<AudioSource>();
        customerOrder = this.transform.GetChild(4).GetComponent<AudioSource>();
        trashCan = this.transform.GetChild(5).GetComponent<AudioSource>();
        toppingsDrop = this.transform.GetChild(6).GetComponent<AudioSource>();
    }

    public void PlayAudioBttn() {
        buttonPressed.Play();
    }

    public void PlayAudioPouring() {
        pouring.Play();
    }

    public void PlayAudioSqueeze() {
        bottleSqueeze.Play();
    }

    public void PlayAudioPowder() {
        powder.Play();
    }

    public void PlayAudioCustomerOrder() {
        customerOrder.Play();
    }

    public void PlayTrashCan() {
        trashCan.Play();
    }

    public void PlayToppingsDrop() {
        toppingsDrop.Play();
    }
}
