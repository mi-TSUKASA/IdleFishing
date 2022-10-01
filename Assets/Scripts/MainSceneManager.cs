using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    public GameObject tapText;
    public GameObject rod;
    public float nowPosi; //竿の位置
    public AudioSource audioSourceSE;
    public AudioClip audioClipRod;

    //竿を投げたかどうか
    public bool throwRod = false;

    //釣果の名前を保存する辞書
    public static Dictionary<int, string> fishes;

    //釣果の出る確率を保存する辞書
    Dictionary<int, float> fishesProb;

    public static int fishesId; //当たった魚の判別用の値（0はハズレ）

    public float timer = 0; //ヒットしてから10秒計測するタイマー

    public bool canCatch = false; //ゲットできるかどうか判定

    public int tapCount; //タップした回数

    private void Start()
    {
        nowPosi = rod.transform.position.y; //竿のy位置を取得
        fishesId = 0; //初期化
        canCatch = false; //初期化
    }

    private void Update()
    {
        rod.transform.position = new Vector3(rod.transform.position.x, nowPosi, rod.transform.position.z); //竿の位置をnowPosiに合わせる

        //画面をタップした時竿を投げる処理
        if (Input.GetMouseButtonDown(0) && throwRod == false)
        {
            rod.SetActive(true);
            tapText.SetActive(false);
            audioSourceSE.PlayOneShot(audioClipRod);
            throwRod = true;
            StartCoroutine("ThrowRod");
        }

        if(fishesId != 0)
        { 
            timer += Time.deltaTime;
            canCatch = true;
            if (timer > 5) //制限時間を超えると失敗
            {
                canCatch = false;
                fishesId = 0;
                nowPosi = 1.0f;
            }
        }

        //画面をタップした時、竿が上下に振動する処理
        if (Input.GetMouseButtonDown(0) && canCatch == true)
        {
            tapCount++;
            if (nowPosi == -0.4f)
            {
                nowPosi = -0.5f;
            }
            else
            {
                nowPosi = -0.4f;
            }
        }

        //制限時間以内に画面を10タップしたらゲット
        if (canCatch == true && tapCount == 10)
        {
            SceneManager.LoadScene("GetScene");
        }


    }

    private IEnumerator ThrowRod()
    {
        InitializeDicts();

        fishesId = Choose();
        Debug.Log(fishesId);
        while (true)
        {
            if (fishesId != 0)
            {
                break;
            }

            yield return new WaitForSeconds(1.0f); //１秒毎に抽選
            fishesId = Choose();
            Debug.Log(fishesId);
        }
    }

    void InitializeDicts()
    {
        fishes = new Dictionary<int, string>();
        fishes.Add(0, null);
        fishes.Add(1, "fish");
        fishes.Add(2, "hotate");

        fishesProb = new Dictionary<int, float>();
        fishesProb.Add(0, 900.0f);
        fishesProb.Add(1, 55.0f);
        fishesProb.Add(2, 45.0f);
    }

    int Choose() //抽選
    {
        //確率の合計値を収納」
        float total = 0;
        
        //釣果の確率用辞書からドロップ率を合計
        foreach(KeyValuePair<int,float> elem in fishesProb)
        {
            total += elem.Value;
        }

        // Random.valueでは0から1までのfloat値を返すので
        // そこにドロップ率の合計を掛ける
        float randomPoint = Random.value * total;

        // randomPointの位置に該当するキーを返す
        foreach(KeyValuePair<int,float> elem in fishesProb)
        {
            if(randomPoint < elem.Value)
            {
                return elem.Key;
            }
            else
            {
                randomPoint -= elem.Value;
            }
        }

        // Random.valueで1.0fも含まれるため
        return 0;
    }
}
