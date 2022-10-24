using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

namespace Crimson
{
    public static class TaskHelper
    {


        public static async Task WaitFrame()
        {
            int current = Time.frameCount;
            while (current == Time.frameCount)
            {
                await Task.Yield();
            }
        }
        public static async Task WaitUntil(Func<bool> condition)
        {
            while (!condition())
            {
                await Task.Yield();
            }
        }
    }

    public class MinMaxF
    {
        [SerializeField] float min;
        [SerializeField] float max;
        public float Random => UnityEngine.Random.Range(min, max);
        public MinMaxF(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }

    public class MinMax
    {
        [SerializeField] int min;
        [SerializeField] int max;
        public int Random => UnityEngine.Random.Range(min, max);
        public MinMax(int min, int max)
        {
            this.min = min;
            this.max = max;
        }
    }
}
