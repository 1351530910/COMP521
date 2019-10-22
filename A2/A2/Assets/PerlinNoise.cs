using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
public class PerlinNoise 
{
    //since 1d, double is enough, dont need vector2
    private List<double> gradientVectors;
    private double amplitude;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="n"> number of points </param>
    /// <param name="amplitude"> height of the function, if 1 then range from -0.5 to 0.5 </param>
    public PerlinNoise(int n,double amplitude)
    {
        this.amplitude = amplitude;
        gradientVectors = new List<double>();
        System.Random r = new System.Random(System.DateTime.Now.Millisecond);
        for (int i = 0; i <= n; i++)
        {
            gradientVectors.Add(r.NextDouble() /4.0+0.00001);//smoother curve than vertical lines and avoid division by 0
        }
    }
    /// <summary>
    /// get the noise of the target x using interpolation
    /// </summary>
    /// <param name="x">exact point</param>
    /// <returns></returns>
    public double getNoise(double x)
    {
        int low = (int)Math.Floor(x);
        int high = (int)Math.Ceiling(x);
        double dist = x - low;
        var lowpos = gradientVectors[low];
        var highpos = gradientVectors[high];
        var u = fade(dist);
        return lerp(lowpos,highpos,u)*amplitude*4-amplitude/2;
    }
    //as defined in Ken Perlin's implementation
    public static double fade(double t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);        
    }
    //linear interpolation
    static double lerp(double min,double max,double x)
    {
        return min + x * (max - min);
    }
}
