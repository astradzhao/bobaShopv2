using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MixingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MixingManager singleton;

    private static float[] mixers;
    private static bool[] mEnabled;

    public string currentScene;

    void Awake() {
        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
            mixers = new float[3];
            mEnabled = new bool[3];
            mixers[0] = 0;
            mixers[1] = 0;
            mixers[2] = 0;
            mEnabled[0] = false;
            mEnabled[0] = false;
            mEnabled[0] = false;
            currentScene = SceneManager.GetActiveScene().name;
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
        currentScene = SceneManager.GetActiveScene().name;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < mixers.Length; i++) {
            if (mEnabled[i]) {
                mixers[i] += Time.deltaTime;
            }
            if (currentScene == "MixingScene") {
                updateSlider(i, mixers[i]);
            }
        }
    }
    public void updateSlider(int m, float val) {
        string sliderString = "MixerSlider" + m;
        GameObject ms = GameObject.Find(sliderString);
        if (ms != null) {
            Slider mSlider = ms.GetComponent<Slider>();
            mSlider.value = val;
        }
    }
    public void startMixer(int m) {
        DrinkManager dm = GameObject.Find("DrinkManager").GetComponent<DrinkManager>();
        int n = m + 1;
        if (dm.getStationDrinks("MixingScene" + n).Count != 0) {
            mEnabled[m] = true;
        }
    }
    public void stopMixer(int m) {
        mEnabled[m] = false;
    }
    public void reset(int m) {
        mEnabled[m] = false;
        mixers[m] = 0;
    }
}
