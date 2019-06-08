using System;
using UnityEngine;

namespace Dev.Davide.UnityHelpers
{
    public class StaticCheck : MonoBehaviour
    {
        void Start()
        {
            foreach (GameObject obj in FindObjectsOfType<GameObject>())
            {
                if (!obj.isStatic)
                {
                    DebugManager.Info(String.Format("Not static: {0}", obj.name));
                }
            }
        }
    }
}
