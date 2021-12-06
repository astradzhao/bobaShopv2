using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager singleton;

    private AudioSource buttonPressed;
    private AudioSource pouring;
    private AudioSource bottleSqueeze;
    private AudioSource powder;
    private AudioSource customerOrder;
    private AudioSource trashCan;


    // private void Awake() {
    //     buttonPressed = this.transform.GetChild(0).GetComponent<AudioSource>();
    //     pouring = this.transform.GetChild(1).GetComponent<AudioSource>();
    //     bottleSqueeze = this.transform.GetChild(2).GetComponent<AudioSource>();
    //     powder = this.transform.GetChild(3).GetComponent<AudioSource>();
    //     customerOrder = this.transform.GetChild(4).GetComponent<AudioSource>();
    //     trashCan = this.transform.GetChild(5).GetComponent<AudioSource>();
    // }

    private void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            buttonPressed = this.transform.GetChild(0).GetComponent<AudioSource>();
            pouring = this.transform.GetChild(1).GetComponent<AudioSource>();
            bottleSqueeze = this.transform.GetChild(2).GetComponent<AudioSource>();
            powder = this.transform.GetChild(3).GetComponent<AudioSource>();
            customerOrder = this.transform.GetChild(4).GetComponent<AudioSource>();
            trashCan = this.transform.GetChild(5).GetComponent<AudioSource>();
        }
        else if (singleton != this)
        {
            Destroy (gameObject);
        }
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        String sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != "StartScene") {
            Button OrderScene = GameObject.Find("OrderScene").GetComponent<Button>();
            Button BaseScene = GameObject.Find("BaseScene").GetComponent<Button>();
            Button IngredScene = GameObject.Find("IngredScene").GetComponent<Button>();
            Button MixingScene = GameObject.Find("MixingScene").GetComponent<Button>();
            Button ToppingsScene = GameObject.Find("ToppingsScene").GetComponent<Button>();
            Button SealingScene = GameObject.Find("SealingScene").GetComponent<Button>();
            Button MyOrders = GameObject.Find("My Orders").GetComponent<Button>();

            OrderScene.onClick.AddListener(PlayAudioBttn);
            BaseScene.onClick.AddListener(PlayAudioBttn);
            IngredScene.onClick.AddListener(PlayAudioBttn);
            MixingScene.onClick.AddListener(PlayAudioBttn);
            ToppingsScene.onClick.AddListener(PlayAudioBttn);
            SealingScene.onClick.AddListener(PlayAudioBttn);
            MyOrders.onClick.AddListener(PlayAudioBttn);
        }

        if (sceneName == "TeaBaseScene") {
            Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
            Button moveToIngred = GameObject.Find("MoveToIng").GetComponent<Button>();
            trashCan.onClick.AddListener(PlayTrashCan);
            moveToIngred.onClick.AddListener(PlayAudioBttn);
        } else if (sceneName == "IngredientsScene") {
            Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
            Button moveToMixing = GameObject.Find("MoveToMixing").GetComponent<Button>();
            trashCan.onClick.AddListener(PlayTrashCan);
            moveToMixing.onClick.AddListener(PlayAudioBttn);
        } else if (sceneName == "MixingScene") {
            Button moveToTopping1 = GameObject.Find("MoveToToppings1").GetComponent<Button>();
            Button moveToTopping2 = GameObject.Find("MoveToToppings2").GetComponent<Button>();
            Button moveToTopping3 = GameObject.Find("MoveToToppings3").GetComponent<Button>();
            moveToTopping1.onClick.AddListener(PlayAudioBttn);
            moveToTopping2.onClick.AddListener(PlayAudioBttn);
            moveToTopping3.onClick.AddListener(PlayAudioBttn);
        } else if (sceneName == "ToppingsScene") {
            Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
            Button moveToSeal = GameObject.Find("MoveToSeal").GetComponent<Button>();
            trashCan.onClick.AddListener(PlayTrashCan);
            moveToSeal.onClick.AddListener(PlayAudioBttn);
        } else if (sceneName == "SealingScene") {
            Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
            Button moveToOrder = GameObject.Find("MoveToOrder").GetComponent<Button>();
            trashCan.onClick.AddListener(PlayTrashCan);
            moveToOrder.onClick.AddListener(PlayAudioBttn);
        }

        // if (sceneName == "TeaBaseScene") {
        //     Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
        //     Button moveToIngred = GameObject.Find("MoveToIng").GetComponent<Button>();
        //     Button oolongTea = GameObject.Find("Oolong Tea").GetComponent<Button>();
        //     Button roseTea = GameObject.Find("Rose Tea").GetComponent<Button>();
        //     Button blackTea = GameObject.Find("Black Tea").GetComponent<Button>();
        //     Button greenTea = GameObject.Find("Green Tea").GetComponent<Button>();
        //     Button herbalTea = GameObject.Find("Herbal Tea").GetComponent<Button>();
        //     Button thaiTea = GameObject.Find("Thai Tea").GetComponent<Button>();
        //     Button milk = GameObject.Find("Milk").GetComponent<Button>();

        //     trashCan.onClick.AddListener(PlayTrashCan);
        //     moveToIngred.onClick.AddListener(PlayAudioBttn);
        //     oolongTea.onClick.AddListener(PlayAudioPouring);
        //     roseTea.onClick.AddListener(PlayAudioPouring);
        //     blackTea.onClick.AddListener(PlayAudioPouring);
        //     greenTea.onClick.AddListener(PlayAudioPouring);
        //     herbalTea.onClick.AddListener(PlayAudioPouring);
        //     thaiTea.onClick.AddListener(PlayAudioPouring);
        //     milk.onClick.AddListener(PlayAudioPouring);

        // } else if (sceneName == "IngredientsScene") {
        //     Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
        //     Button moveToMixing = GameObject.Find("MoveToMixing").GetComponent<Button>();
        //     Button mango = GameObject.Find("Mango").GetComponent<Button>();
        //     Button strawberry = GameObject.Find("Strawberry").GetComponent<Button>();
        //     Button lychee = GameObject.Find("Lychee").GetComponent<Button>();
        //     Button matcha = GameObject.Find("Matcha").GetComponent<Button>();
        //     Button taro = GameObject.Find("Taro").GetComponent<Button>();
        //     Button brownSugar = GameObject.Find("BrownSugar").GetComponent<Button>();

        //     trashCan.onClick.AddListener(PlayTrashCan);
        //     moveToMixing.onClick.AddListener(PlayAudioBttn);
        //     mango.onClick.AddListener(PlayAudioSqueeze);
        //     strawberry.onClick.AddListener(PlayAudioSqueeze);
        //     lychee.onClick.AddListener(PlayAudioSqueeze);
        //     matcha.onClick.AddListener(PlayAudioPowder);
        //     taro.onClick.AddListener(PlayAudioPowder);
        //     brownSugar.onClick.AddListener(PlayAudioPowder);

        // } else if (sceneName == "MixingScene") {
        //     Button moveToTopping1 = GameObject.Find("MoveToToppings1").GetComponent<Button>();
        //     Button moveToTopping2 = GameObject.Find("MoveToToppings2").GetComponent<Button>();
        //     Button moveToTopping3 = GameObject.Find("MoveToToppings3").GetComponent<Button>();
        //     Button startMixer0 = GameObject.Find("StartMixer0").GetComponent<Button>();
        //     Button startMixer1 = GameObject.Find("StartMixer1").GetComponent<Button>();
        //     Button startMixer2 = GameObject.Find("StartMixer2").GetComponent<Button>();
        //     Button stopMixer0 = GameObject.Find("StopMixer0").GetComponent<Button>();
        //     Button stopMixer1 = GameObject.Find("StopMixer1").GetComponent<Button>();
        //     Button stopMixer2 = GameObject.Find("StopMixer2").GetComponent<Button>();

        //     moveToTopping1.onClick.AddListener(PlayAudioBttn);
        //     moveToTopping2.onClick.AddListener(PlayAudioBttn);
        //     moveToTopping3.onClick.AddListener(PlayAudioBttn);
        //     startMixer0.onClick.AddListener(PlayAudioBttn);
        //     startMixer1.onClick.AddListener(PlayAudioBttn);
        //     startMixer2.onClick.AddListener(PlayAudioBttn);
        //     stopMixer0.onClick.AddListener(PlayAudioBttn);
        //     stopMixer1.onClick.AddListener(PlayAudioBttn);
        //     stopMixer2.onClick.AddListener(PlayAudioBttn);
            
        // } else if (sceneName == "ToppingsScene") {
        //     Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
        //     Button moveToSeal = GameObject.Find("MoveToSeal").GetComponent<Button>();
        //     Button tapicoaPearls = GameObject.Find("Tapioca Pearls").GetComponent<Button>();
        //     Button crystalBoba = GameObject.Find("Crystal Boba").GetComponent<Button>();
        //     Button redBean = GameObject.Find("Red Bean").GetComponent<Button>();
        //     Button custardPudding = GameObject.Find("Custard Pudding").GetComponent<Button>();
        //     Button lycheeJelly = GameObject.Find("Lychee Jelly").GetComponent<Button>();
        //     Button strawberryStars = GameObject.Find("Strawberry Stars").GetComponent<Button>();
        //     Button pineappleJelly = GameObject.Find("Pineapple Jelly").GetComponent<Button>();
        //     Button coconutJelly = GameObject.Find("Coconut Jelly").GetComponent<Button>();

        //     trashCan.onClick.AddListener(PlayTrashCan);
        //     moveToSeal.onClick.AddListener(PlayAudioBttn);
        //     tapicoaPearls.onClick.AddListener(PlayAudioBttn);
        //     crystalBoba.onClick.AddListener(PlayAudioBttn);
        //     redBean.onClick.AddListener(PlayAudioBttn);
        //     custardPudding.onClick.AddListener(PlayAudioBttn);
        //     lycheeJelly.onClick.AddListener(PlayAudioBttn);
        //     strawberryStars.onClick.AddListener(PlayAudioBttn);
        //     pineappleJelly.onClick.AddListener(PlayAudioBttn);
        //     coconutJelly.onClick.AddListener(PlayAudioBttn);

        // } else if (sceneName == "SealingScene") {
        //     Button trashCan = GameObject.Find("TrashCan").GetComponent<Button>();
        //     Button moveToOrder = GameObject.Find("MoveToOrder").GetComponent<Button>();
        //     Button stop = GameObject.Find("SealButton").GetComponent<Button>();

        //     trashCan.onClick.AddListener(PlayTrashCan);
        //     moveToOrder.onClick.AddListener(PlayAudioBttn);
        //     stop.onClick.AddListener(PlayAudioBttn);
        // }
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
}
