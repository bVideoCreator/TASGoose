using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace TASGoose
{
    class Wireframe : MonoBehaviour
    {

        private bool _wireframe;

        void Update()
        {
            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.W))
            {
                _wireframe = !_wireframe;
            }

            if(_wireframe)
            {
                GL.wireframe = true;
            }

            if(!_wireframe)
            {
                GL.wireframe = false;
            }
        }

    }
}
