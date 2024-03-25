// Written By Asaf Benjaminov @ JStudios 2022

using UnityEngine;

namespace JStudios.Scripts.Helpers
{
    public static class JUtilities
    {
        public static Color DarkColor = new(0.1647f, 0.1647f, 0.1647f, 1);

        public static Texture2D CreateTexture2D(int width, int height, Color col)
        {
            var pix = new Color[width * height];
            
            for(var i = 0; i < pix.Length; ++i )
            {
                pix[ i ] = col;
            }
            
            var result = new Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}