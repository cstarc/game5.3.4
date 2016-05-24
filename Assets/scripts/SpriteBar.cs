using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteBar : MonoBehaviour {
    public SpriteRenderer barR;
    public Text barValue;
    public Sprite barOrign;
    public bool isHealth;
    Sprite bar;
    
    //[Range(0, 1)]
    //public float value;
    
    int width;
    int height;
    Color[] orignColor;
    void Awake()
    {
        bar = barR.sprite;
        //bar = barR.sprite;
        //Color[] transparent = new Color[w * h];
        width = bar.texture.width;
        height = bar.texture.height;

        orignColor = new Color[width * height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Debug.Log(""+ d);
                orignColor[x+y*width] = barOrign.texture.GetPixel(x, y);
         
            }
        }
    }
    // Use this for initialization
    void Start () {
 
    }
    public void updateBar( float value,int v)
    {
        barValue.text = ""+v; //更新bar的值

        int d;
        d = (int)(value * height);
        //d = height / 2;
        //image.sprite = bar;
        int w = width;
        int h = height - d;


        //Color[] transparent = new Color[w * h];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < d; y++)
            {
                if (!isHealth)
                {
                    bar.texture.SetPixel(x, y, orignColor[x + y * width]);
                }
                else
                {
                    Color c = bar.texture.GetPixel(x, y);
                    //transparent[x  + (y - d) * w] = new Color(c.r, c.g, c.b, 0);
                    bar.texture.SetPixel(x, y, new Color(c.r, c.g, c.b, 0));
                }
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = d; y < height; y++)
            {
                if (!isHealth)
                {
                    Color c = bar.texture.GetPixel(x, y);
                    //transparent[x  + (y - d) * w] = new Color(c.r, c.g, c.b, 0);
                    bar.texture.SetPixel(x, y, new Color(c.r, c.g, c.b, 0));

                }
                else
                {
                    bar.texture.SetPixel(x, y, orignColor[x + y * width]);
                }
            }
        }
        //bar.texture.SetPixels(0, d, w, h, transparent);
        bar.texture.Apply();
        // Debug.Log("" + bar.texture.GetPixel(50, 59).a);
    }

    // Update is called once per frame
    void Update() {

    }
}

