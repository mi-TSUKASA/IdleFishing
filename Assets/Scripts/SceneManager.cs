using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject tapText;
    public GameObject rod;
    public AudioSource audioSourceSE;
    public AudioClip audioClipRod;

    public bool throwRod = false; //竿を投げたかどうか

    //釣果
    public GameObject hotate;
    public GameObject fish;

    private void Update()
    {
        //画面をタップした時
        if (Input.GetMouseButtonDown(0) && throwRod == false)
        {
            ThrowRod();
        }
    }

    void ThrowRod()
    {
        rod.SetActive(true);
        tapText.SetActive(false);
        audioSourceSE.PlayOneShot(audioClipRod);
        throwRod = true;
        HitFishes();
    }

    void HitFishes()
    {
        int hitFish = 0;
        int hitHotate = 1;
        int num = Random.Range(0, 100);
        Debug.Log(num);
        

        while (num != hitFish && num != hitHotate)
        {
            num = Random.Range(0, 100);
            Debug.Log(num);
            if (num == hitFish)
            {
                Catch(hitFish);
                break;
            }
            else if (num == hitHotate)
            {
                Catch(hitHotate);
                break;
            }
        }
        
    }

    void Catch(int hitNum)
    {
        List<GameObject> fishes = new List<GameObject>() { hotate, fish };
        fishes[hitNum].SetActive(true);
    }
}
