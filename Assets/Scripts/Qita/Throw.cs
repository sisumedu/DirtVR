using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField]
    private GameObject dartsPrefab;
    [SerializeField]
    private AccelerationScript accelerationScript;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var velocity = accelerationScript.CalcVelocity();
            var dart = Instantiate(dartsPrefab);
            dart.GetComponent<Rigidbody>().velocity = velocity;
        }
    }
}
