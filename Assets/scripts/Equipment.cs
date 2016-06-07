using UnityEngine;
using System.Collections;

public class Equipment : MonoBehaviour {

    public Transform[] able; //可放置位置
    public Transform wear; //可放置位置
    Vector3[] postion;
    float minDistance;
    Transform orign;   //初始位置
    int swapIndex;    //交换位置下标
	// Use this for initialization
	void Start () {
        minDistance = sqrt(able[0].position, able[1].position) * 0.5f;
        orign = transform;
        postion = new Vector3[able.Length];
        for (int i = 0; i < postion.Length; i++)
            postion[i] = able[i].position;
    }
    float sqrt(Vector3 a, Vector3 b)
    {
        return Mathf.Sqrt(Mathf.Pow((a.x - b.x), 2) + Mathf.Pow((a.y - b.y), 2));
    }

    IEnumerator OnMouseDown()
    {
       

        var camera = Camera.main;

        if (camera)
        {
            Debug.Log("fff");
            //转换对象到当前屏幕位置

            Vector3 screenPosition = camera.WorldToScreenPoint(transform.position);


            //鼠标屏幕坐标

            Vector3 mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);

            //获得鼠标和对象之间的偏移量,拖拽时相机应该保持不动

            Vector3 offset = transform.position - camera.ScreenToWorldPoint(mScreenPosition);
            offset.z = offset.z - 30;
            print("drag starting:" + transform.name);



            //若鼠标左键一直按着则循环继续

            while (Input.GetMouseButton(0))
            {


                //鼠标屏幕上新位置

                mScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
                // 对象新坐标 

                transform.position = offset + camera.ScreenToWorldPoint(mScreenPosition);




                //协同，等待下一帧继续

                yield return new WaitForEndOfFrame();

            }

            //计算是否可移动，并移动
            float min = 2000;
            for (int index = 0; index < able.Length; index++)
            {

                float f = sqrt(transform.position, postion[index]);
                print("" + f);
                if (f < min)
                {
                    min = f;
                    swapIndex = index;
                }

               
            }

            //可穿戴位置
            float m = sqrt(transform.position, wear.position); 
            print("" + min+" "+ m);
            if (m < min)
            {//穿戴
                min = m;
                if (min < minDistance)
                {
                    print(""+ min);
                    wear.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    gameObject.GetComponent<SpriteRenderer>().sprite = null;
                    //transform.position = orign.position;
                }
            }
            else
            {  //交换格子
                print("" + min + "mind" + minDistance);
                if (min < minDistance)
                {
                    
                    Sprite temp= able[swapIndex].GetComponent<SpriteRenderer>().sprite;
                    able[swapIndex].GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    gameObject.GetComponent<SpriteRenderer>().sprite = temp;
                    // transform.position = orign.position;
                    
                }
            }

            transform.position = orign.position;

            //if(transform.position)

            print("drag compeleted");


        }

    }
    // Update is called once per frame
    void Update () {
	
	}
}
