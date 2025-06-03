using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    [Header("Player SFX")]
    public AudioClip StepAudio;
    AudioSource PlayerAudio;
    public AudioClip RunAudio;
    void Start()
    {
         PlayerAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
        private void step(){
        // Debug.Log("step");
        PlayerAudio.clip = StepAudio;
        PlayerAudio.Play();
    }
    private void run(){
        // Debug.Log("run");
        PlayerAudio.clip = RunAudio;
        PlayerAudio.Play();
    }
}
