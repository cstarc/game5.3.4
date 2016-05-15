using UnityEngine;
using System.Collections;

public class BarContr : MonoBehaviour {
    public Sprite origBar;
    Sprite bar;
    public SpriteRenderer image;
    public int d;
    int width;
    int height;
    // Use this for initialization
    void Start () {
        bar = Instantiate(origBar);
        width = bar.texture.width;
        height = bar.texture.height;
        if (d > height)
            d = height;
        //d = height / 2;
        //image.sprite = bar;
        int w = width;
        int h = height - d;
        //Color[] transparent = new Color[w * h];


        for (int x = 0; x < width; x++)
        {
            for (int y = d; y < height; y++)
            {
                // Debug.Log(""+ d);
                Color c = bar.texture.GetPixel(x, y);
                //transparent[x  + (y - d) * w] = new Color(c.r, c.g, c.b, 0);
                bar.texture.SetPixel(x, y, new Color(c.r, c.g, c.b, 0));
            }
        }
        //bar.texture.SetPixels(0, d, w, h, transparent);
        bar.texture.Apply();
        d++;
        // Debug.Log("" + bar.texture.GetPixel(50, 59).a);
        image.sprite = bar;
    }
    // Update is called once per frame
    void Update () {

     
    }
}
