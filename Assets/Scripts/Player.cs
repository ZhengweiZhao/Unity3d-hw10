using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Tank{
    public delegate void DestroyPlayer(); // game over!
    public static event DestroyPlayer destroyEvent;
    void Start () {
        setHP(1000);
	}
	
	// Update is called once per frame
	void Update () {
		if(getHP() <= 0)    // game over!
        {
            this.gameObject.SetActive(false);
            destroyEvent();
        }
	}

    //键盘w，s控制前后移动
    public void moveForward()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 20;
    }
    public void moveBackWard()
    {
        gameObject.GetComponent<Rigidbody>().velocity = gameObject.transform.forward * -20;
    }

    //键盘a，d控制原地左右旋转的方向。通过水平轴上的增量，改变玩家坦克的欧拉角，从而实现坦克转向
    public void turn(float offsetX)
    {
        float x = gameObject.transform.localEulerAngles.x;
        float y = gameObject.transform.localEulerAngles.y + offsetX*2;
        gameObject.transform.localEulerAngles = new Vector3(x, y, 0);
    }
}
