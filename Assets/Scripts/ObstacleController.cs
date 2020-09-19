using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float maxY, minY;
    public float speed;
    float delta;
    
    // Start is called before the first frame update
    void Start()
    {
        delta = maxY - minY;
    }

    // Update is called once per frame
    void Update()
    {
        float y = minY + Mathf.PingPong(speed * Time.time, delta);
        Vector3 pos = new Vector3(transform.position.x, y, transform.position.z);
        transform.position = pos;
    }
}
