using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public float maxY, minY;
    public float speed;
    public string axis;
 
    // Use this for initialization
    void Start () 
    {
 
    }
    
    // Update is called once per frame
    void Update () 
    {
        float move = Input.GetAxis(axis) * speed * Time.deltaTime;
        float nextPos = transform.position.y + move;
        if (nextPos > maxY) {
            move = 0;
        }
        if (nextPos < minY) {
            move = 0;
        }
        transform.Translate (0, move, 0);
    }
}
