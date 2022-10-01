using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Catch(MainSceneManager.fishesId);
    }


    void Catch(int hitNum)
    {
        GameObject prefab = (GameObject)Resources.Load("Prefabs/Catch/" + MainSceneManager.fishes[hitNum]);
        Instantiate(prefab, new Vector3(0.0f, 2.0f, 0.0f), Quaternion.identity); //辞書からhitNumに対応したプレハブを生成する
    }

}
