using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TASGoose
{
    public class Injector
    {
        public static void Inject()
        {
            bool flag = GameObject.Find("TASG");
            if (flag)
            {
                foreach (object obj in TASG._UDO)
                {
                    UnityEngine.Object @object = (UnityEngine.Object)obj;
                    UnityEngine.Object.Destroy(@object);
                }
                UnityEngine.Object.Destroy(GameObject.Find("TASG").gameObject);
            }
            GameObject gameObject = new GameObject("TASG");
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<TASG>();
        }

        public static void Unhook()
        {
            foreach (object obj in TASG._UDO)
            {
                UnityEngine.Object @object = (UnityEngine.Object)obj;
                UnityEngine.Object.Destroy(@object);
            }
            UnityEngine.Object.Destroy(GameObject.Find("TASG").gameObject);
        }
    }
}
