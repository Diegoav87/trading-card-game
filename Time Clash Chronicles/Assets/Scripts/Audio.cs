using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    AudioManager audioManager;

    void Awake()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void Start()
    {
        audioManager.Play("Menu");
    }
}
