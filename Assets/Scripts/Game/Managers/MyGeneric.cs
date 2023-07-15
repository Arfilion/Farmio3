using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyGeneric
{
    public static class MyGeneric
    {
        public static T FindNearestObject<T>(Vector3 sourcePosition, List<T> objects, Func<T, float> distanceCalculation)
        {
            T nearestObject = default(T);
            float nearestDistance = float.MaxValue;

            foreach (T obj in objects)
            {
                float distance = distanceCalculation(obj);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestObject = obj;
                }
            }
            return nearestObject;
        }
    }
}

