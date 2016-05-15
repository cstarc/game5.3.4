using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {
    public Texture2D orig;
    Texture2D bar;
    public Material mater;
    [Range(0, 1)]
    public float value;
    int d;
    int width;
    int height;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //实例化以防修改源资源
        bar = Instantiate(orig);
        mater.SetTexture("_Mask", bar);
        width = bar.width;
        height = bar.height;
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
                // Debug.Log(""+ d);
                Color c = bar.GetPixel(x, y);
                //transparent[x  + (y - d) * w] = new Color(c.r, c.g, c.b, 0);
                bar.SetPixel(x, y, new Color(c.r, c.g, c.b, 0));
            }
        }
        //bar.texture.SetPixels(0, d, w, h, transparent);
        bar.Apply();
        // Debug.Log("" + bar.texture.GetPixel(50, 59).a);
       // image = bar;
    }
}
