using UnityEngine;

public class DartsScript : MonoBehaviour
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float angularVelocity = Random.Range(-100, 100);
        // 進行方向を向かせる
        var look = Quaternion.LookRotation(rb.velocity);
        var z = transform.eulerAngles.z;
        var angle = Quaternion.Euler(0, 0, z + angularVelocity * Time.deltaTime);
        transform.rotation = look * angle;
    }
}
