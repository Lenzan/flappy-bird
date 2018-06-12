using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPanel : MonoBehaviour {

    public float speed;

    private PlayUIPanel playUI;

    private void Update()
    {
        if (transform.localPosition.x < -1080)
            transform.localPosition = new Vector2(0, 0);
        transform.Translate(-transform.right * speed * Time.deltaTime);
        //transform.localPosition += -Vector3.right * speed *2 * Time.deltaTime;
    }
}
