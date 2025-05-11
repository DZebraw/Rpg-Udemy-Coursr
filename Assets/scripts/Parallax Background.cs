using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float xPosition;
    private float length;
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    void Update()
    {
        //物体移动了的距离
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //摄像机的位置乘以一个0~1的倍率 给 背景
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        //所以 之后随着移动，背景会慢慢的与摄像机位置偏差变大
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        //当偏差距离大到了背景的长度 就会 直接让背景移动到前面，以此循环下去
        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}
