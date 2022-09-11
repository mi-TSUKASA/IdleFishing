using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject tapText;
    public GameObject rod;
    public AudioSource audioSourceSE;
    public AudioClip audioClipRod;
    public bool throwRod = false;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && throwRod == false)
        {
            rod.SetActive(true);
            tapText.SetActive(false);
            audioSourceSE.PlayOneShot(audioClipRod);
            throwRod = true;
        }
    }
}
