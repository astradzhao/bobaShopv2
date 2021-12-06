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

    private AudioSource mixingAudio0;
    private AudioSource mixingAudio1;
    private AudioSource mixingAudio2;

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

            mixingAudio0 = this.gameObject.transform.GetChild(0).GetComponent<AudioSource>();
            mixingAudio1 = this.gameObject.transform.GetChild(1).GetComponent<AudioSource>();
            mixingAudio2 = this.gameObject.transform.GetChild(2).GetComponent<AudioSource>();
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
            if (m == 0) {
                mixingAudio0.Play();
            } else if (m == 1) {
                mixingAudio1.Play();
            } else if (m == 2) {
                mixingAudio2.Play();
            }
        }
    }
    public void stopMixer(int m) {
        mEnabled[m] = false;
        if (m == 0) {
            mixingAudio0.Stop();
        } else if (m == 1) {
            mixingAudio1.Stop();
        } else if (m == 2) {
            mixingAudio1.Stop();
        }
    }

    public void addScore(int m) {
        DrinkManager dm = GameObject.Find("DrinkManager").GetComponent<DrinkManager>();
        int n = m + 1;
        string stationString = "MixingScene" + n;
        if (dm.getStationDrinks(stationString).Count != 0) {
            Drink currentDrink = dm.getStationDrinks(stationString)[0];
            int score = (int) (1000 - ((Mathf.Abs(mixers[m] - 50)) / 50 * 1000));
            currentDrink.mix(score);
        }
    }

    public void reset(int m) {
        mEnabled[m] = false;
        mixers[m] = 0;
    }
}
