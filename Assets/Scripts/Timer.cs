using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour{
    [SerializeField]
    TextMeshProUGUI timerText;
    float startTime;
    string sec;
    bool stopped;

    // Start is called before the first frame update
    void Start(){
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update(){
        if (!stopped) RunTimer();
        else StopTimer();
    }

    void RunTimer() {
        float t = Time.time - startTime;
        sec = t.ToString("f2");

        timerText.text = sec;
    }

    public void StopTimer() {
        stopped = true;
        timerText.text = sec;
    }
}
