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
        //�����ƶ��˵ľ���
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        //�������λ�ó���һ��0~1�ı��� �� ����
        float distanceToMove = cam.transform.position.x * parallaxEffect;
        //���� ֮�������ƶ����������������������λ��ƫ����
        transform.position = new Vector3(xPosition + distanceToMove, transform.position.y);

        //��ƫ�������˱����ĳ��� �ͻ� ֱ���ñ����ƶ���ǰ�棬�Դ�ѭ����ȥ
        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}
