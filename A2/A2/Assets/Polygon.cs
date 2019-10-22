using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon 
{
    const double NoiseDensity = 40.0;
    const double MarginRatio = 0.03;
    public static GameObject createRectangle(Vector3 position, Vector3 scale, Vector2 size,Color clr,string name = "rectangle")
    {
        GameObject rectangle = new GameObject(name);
        SpriteRenderer sr = rectangle.AddComponent<SpriteRenderer>(); // add a sprite renderer to the gameobject, on what we draw
        Texture2D texture = new Texture2D((int)size.x, (int)size.y); // create a texture larger than your maximum polygon size

        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, clr);
            }
        }

        texture.Apply();
        sr.sprite = Sprite.Create(texture, new Rect(0, 0, (int)size.x, (int)size.y), Vector2.zero, 1);
        rectangle.SetActive(true);
        rectangle.transform.position = position;
        rectangle.transform.localScale = scale;

        return rectangle;
    }
    public static GameObject createPerlinRectangle(Vector3 position,Vector3 scale,Vector2 size,string name = "perlinRectangle")
    {
        
        GameObject rectangle = new GameObject(name);
        SpriteRenderer sr = rectangle.AddComponent<SpriteRenderer>(); // add a sprite renderer to the gameobject, on what we draw
        Texture2D texture = new Texture2D((int)size.x, (int)size.y); // create a texture larger than your maximum polygon size

        //divide the rectangle, compute the margin for extra draw space
        var margin = System.Math.Max((int)(size.x * MarginRatio), (int)(size.y * MarginRatio));
        var width = (int)(size.x -2*margin);
        var height = (int)(size.y - 2 * margin);
        
        //perlin noise itself
        PerlinNoise noise;

        //set texture initial color, all 0 opacity (transparent)
        for (int i = 0; i < texture.width; i++)
        {
            for (int j = 0; j < texture.height; j++)
            {
                texture.SetPixel(i, j, new Color(0, 0, 0, 0));
            }
        }

        //draw up line
        noise = new PerlinNoise(width, margin);
        for (int i = margin; i < texture.width-margin; i++)
        {
            texture.SetPixel(i, (int)noise.getNoise(i/NoiseDensity)+height+margin, Color.black);
        }

        //draw down line
        noise = new PerlinNoise(width, margin);
        for (int i = margin; i < texture.width - margin; i++)
        {
            texture.SetPixel(i, (int)noise.getNoise(i / NoiseDensity) + margin, Color.black);
        }

        //draw left line
        noise = new PerlinNoise(height, margin);
        for (int i = margin; i < texture.height - margin; i++)
        {
            texture.SetPixel((int)noise.getNoise(i / NoiseDensity)+margin, i, Color.black);
        }

        //draw right line
        noise = new PerlinNoise(height, margin);
        for (int i = margin; i < texture.height - margin; i++)
        {
            texture.SetPixel((int)noise.getNoise(i / NoiseDensity) + margin+width, i, Color.black);
        }


        texture.Apply();
        sr.sprite = Sprite.Create(texture, new Rect(0, 0, (int)size.x, (int)size.y), Vector2.zero, 1);
        rectangle.SetActive(true);
        rectangle.transform.position = position;
        rectangle.transform.localScale = scale;
        return rectangle;
    }
    
}
