﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GIF_Editor
{
    static class BitmapResize
    {
        public static Bitmap Shrink(this Bitmap original, int width, int height)
        {
            if (width > original.Width || height > original.Height)
                throw new ArgumentOutOfRangeException("New width and height must be less than or equal to original width and height");

            int widthSpace = (int)Math.Round(original.Width / (double)width);
            int heightSpace = (int)Math.Round(original.Height / (double)height);
            var finalBitmap = new Bitmap(width, height);
            int avgRed;
            int avgBlue;
            int avgGreen;
            int count;
            int x = 0;
            int y = 0;

            for (int i = 0; i < width; i += widthSpace, x++)
            {
                y = 0;
                for (int j = 0; j < height; j += heightSpace, y++)
                {
                    avgRed = 0;
                    avgBlue = 0;
                    avgGreen = 0;
                    count = 0;

                    for (int w = i; w < i + widthSpace; w++)
                        for (int h = j; h < heightSpace; h++, count++)
                        {
                            avgRed += original.GetPixel(w, h).R;
                            avgGreen += original.GetPixel(w, h).G;
                            avgBlue += original.GetPixel(w, h).B;
                        }

                    try
                    {
                        avgRed /= count;
                        avgBlue /= count;
                        avgGreen /= count;
                    }
                    catch { }

                    finalBitmap.SetPixel(x, y, Color.FromArgb(avgRed, avgGreen, avgBlue));
                }
            }

            return finalBitmap;
        }
    }
}