using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainscript : MonoBehaviour
{

    GameObject[] tagObjects;

    public GameObject Dart;
    public int score_num;
    
    // Start is called before the first frame update
    void Start()
    {
        // スコアのロード
        //score_num = PlayerPrefs.GetInt("SCORE", 0);
        //Dart = GameObject.Find("Dart");
    }

    // Update is called once per frame
    void Update()
    {


        if (Check("Finish") == 1)

        {
            Invoke("Finish", 10);
        }

        if (Check("Re") >= 1)

        {
            Invoke("Respawn", 3);
        }
        
    }

   

    void Finish()
    {



        //現在のScene名を取得する
        Scene loadScene = SceneManager.GetActiveScene();



        // Sceneの読み直し
        SceneManager.LoadScene(loadScene.name);

        



    }

    void Respawn()
    {


        //Dart.gameObject.transform.position = new Vector3(0, -9, -14);
        Dart.tag = "Untagged";
        //Instantiate(Dart, new Vector3(0, 0, -10), Quaternion.identity);
        CancelInvoke();
    }

    //シーン上のBlockタグが付いたオブジェクトを数える
    int Check(string tagname)
    {
        tagObjects = GameObject.FindGameObjectsWithTag(tagname);
        Debug.Log(tagObjects.Length); //tagObjects.Lengthはオブジェクトの数
        if (tagObjects.Length == 0)
        {
            Debug.Log(tagname + "タグがついたオブジェクトはありません");
        }

        return tagObjects.Length;
    }

   
    //public int CountPoints()
    //{
    //    Vector2 v = new Vector2(transform.position.x, transform.position.y);

    //    //sector
    //    float a = Vector2.Angle(Vector2.up, v);
    //    if ((v.x < 0) && (a > 9))
    //        a = 360 - a;
    //    float b = (a + 9) / 18;
    //    int i = Mathf.FloorToInt(b);
    //    int result = scores[i];

    //    //distance
    //    float d = Vector2.Distance(Dart.transform.localScale, v) / Board.transform.localScale.x;
    //    d *= 200;

    //    if (d < 80)
    //    {
    //        if (d < 72)
    //        {
    //            if (d < 50)
    //            {
    //                result *= 2;
    //                if (d < 45)
    //                {
    //                    if (d < 13)
    //                    {
    //                        result = 50;
    //                        if (d < 7)
    //                            result = 100;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    else
    //        result = 0;

    //    return result;
    //}


    //// GameControllerインスタンスの実体
    //private static Mainscript instance = null;

    //// GameControllerインスタンスのプロパティーは、実体が存在しないとき（＝初回参照時）実体を探して登録する
    //public static Mainscript Instance => instance
    //    ?? (instance = GameObject.FindWithTag("Mainscript").GetComponent<Mainscript>());

    //private void Awake()
    //{
    //    // もしインスタンスが複数存在するなら、自らを破棄する
    //    if (this != Instance)
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }

    //    // 唯一のインスタンスなら、シーン遷移しても残す
    //    DontDestroyOnLoad(this.gameObject);
    //}

    //private void OnDestroy()
    //{
    //    // 破棄時に、登録した実体の解除を行う
    //    if (this == Instance) instance = null;
    //}
}
