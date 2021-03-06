using UnityEngine;
using System.Collections;

public class DartThrow : MonoBehaviour
{
    public GameObject Score, Board;
    public float DragSpeed, ThrowSpeed, RotationSpeed;
    int[] scores = { 20, 1, 18, 4, 13, 6, 10, 15, 2, 17, 3, 19, 7, 16, 8, 11, 14, 9, 12, 5 };
    int points = 0;
    WebSocketinputDart webSocketinputDart;
    

    private bool throwD;
    public int throwCount;
    public GameObject Dart;
    GameObject GameScript;
    //Test test;
    ArrowObject arrowObject;
    GameObject arrow;

     private Rigidbody rb;

    float padx = 50;
    float pady = 100;
    float padz = 1000;

    void Start()
    {
        if (tag == "Re")
        { tag = "Untagged"; }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GameScript = GameObject.Find("PointScript");
        arrow = GameObject.Find("Dart");
        if (throwCount >= 24)
        { tag = "Finish"; }

        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        throwD = false;
        
    }

    void Update()
    {
        if ((Input.GetMouseButton(0)) & !CompareTag("Finish"))
            if (Input.GetMouseButton(1))
            {
                Vector3 angles = new Vector3(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"), 0);
                transform.Rotate(angles * RotationSpeed * Time.deltaTime);
            }
            else
            {
                Vector3 direction = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"), 0);
                transform.Translate(direction * DragSpeed * Time.deltaTime);
            }
        if (!throwD)
        {
            //スクリプトをつけたオブジェクトに角度をつける
            //transform.rotation = Quaternion.Euler(SmartPhoneInput.PadGradY * padx, SmartPhoneInput.PadGradZ * pady, SmartPhoneInput.PadGradX * padz).normalized;
        }
       

    }

    [SerializeField]
    private AccelerationScript accelerationScript;

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ThrowSpeed = ThrowSpeed + 1;


        }
        if (Input.GetMouseButtonUp(0))
        {
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ThrowSpeed, ForceMode.Impulse);
            transform.LookAt(GetComponent<Rigidbody>().velocity.normalized);
            //arrowObject.Shot(Vector3.forward * ThrowSpeed);
            GetComponent<Rigidbody>().useGravity = true;
        }
        //webSocketinputDart.PHONE_attack02_dart();


        Debug.Log("SmartPhoneInput.PadGradY = " + SmartPhoneInput.PadGradY);
        if (SmartPhoneInput.PadGradY> 0.4 && SmartPhoneInput.PadX == 0)
        {
            throwD = true;
            //var velocity = accelerationScript.CalcVelocity();
            //var velocity = arrow.GetComponent<AccelerationScript>().CalcVelocity();
            //rb.velocity = velocity;
            rb.useGravity = true;
            //transform.LookAt(GetComponent<Rigidbody>().velocity.normalized);
            //Vector3 ko = new Vector3(0,0, SmartPhoneInput.PadGradY);
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * ThrowSpeed * SmartPhoneInput.PadGradY, ForceMode.Impulse);
            //GetComponent<Rigidbody>().AddRelativeForce(new Vector3(SmartPhoneInput.PadGradX * padz, SmartPhoneInput.PadGradY , SmartPhoneInput.PadGradZ * pady), ForceMode.Impulse);
            //arrowObject.Shot(Vector3.forward * 20* SmartPhoneInput.PadGradY);
            //transform.LookAt(GetComponent<Rigidbody>().velocity.normalized);
            //arrowObject.Shot(ko);
            //Arrowangle();


        }

       
    }

        private void Arrowangle()
        {
            float angularVelocity = Random.Range(-100, 100);
            // 進行方向を向かせる
            var look = Quaternion.LookRotation(rb.velocity);
            var z = transform.eulerAngles.z;
            var angle = Quaternion.Euler(0, 0, z + angularVelocity * Time.deltaTime);
            transform.rotation = angle;
        }
    //if (throwD)
    //{ GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 5, ForceMode.Impulse); }



     void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))   //もしプレイヤータグと接触したら...
        {


            Debug.Log(collision.gameObject.name + "と衝突しました!!!");
            GameScript.GetComponent<Points>().AddPoints();
            throwCount = throwCount + 1;
            var obj=Instantiate(Dart, new Vector3(0, 0, -10), Quaternion.identity) as GameObject;
            obj.name = Dart.name;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

          
            
            //tag = "Respawn";
            tag = "Re"; 
            Destroy(this.gameObject);
        }

       
    }



    //void AddPoints()
    //{
    //    throwCount++;
    //    points += CountPoints();
    //    Score.GetComponent<GUIText>().text = points.ToString();
    //}

    public int CountPoints()
    {
        Vector2 v = new Vector2(transform.position.x, transform.position.y);

        //sector
        float a = Vector2.Angle(Vector2.up, v);
        if ((v.x < 0) && (a > 9))
            a = 360 - a;
        float b = (a + 9) / 18;
        int i = Mathf.FloorToInt(b);
        int result = scores[i];

        //distance
        float d = Vector2.Distance(Vector2.zero, v) / Board.transform.localScale.x;
        d *= 200;

        if (d < 80)
        {
            if (d < 72)
            {
                if (d < 50)
                {
                    result *= 2;
                    if (d < 45)
                    {
                        if (d < 13)
                        {
                            result = 50;
                            if (d < 7)
                                result = 100;
                        }
                    }
                }
            }
        }
        else
            result = 0;

        return result;
    }



    public float GetThrowSpeed()
    {
        return ThrowSpeed;
    }
}
