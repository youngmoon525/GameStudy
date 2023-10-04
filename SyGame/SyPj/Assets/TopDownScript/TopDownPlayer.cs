using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownPlayer : MonoBehaviour
{
    public static int speed =5;
    float h;
    float v;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal") * speed;
        v = Input.GetAxisRaw("Vertical") * speed;
    }
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h, v);
    }
}
