using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSceneManager : MonoBehaviour
{
    GameObject prefab;
    GameObject obj;

    void Start()
    {
        Catch(MainSceneManager.fishesId);
    }


    void Catch(int hitNum)
    {
        prefab = (GameObject)Resources.Load("Prefabs/Catch/" + MainSceneManager.fishes[hitNum]);
        obj = Instantiate(prefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity); //辞書からhitNumに対応したプレハブを生成する
        StartCoroutine("ScaleUp");
    }

    IEnumerator ScaleUp()
    {
        for (float i = 0.1f; i < 0.45; i += 0.05f)
        {
            obj.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.05f);
        }
    }
}
