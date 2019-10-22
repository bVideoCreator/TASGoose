using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;

namespace TASGoose
{
    class WireframeCollisions : MonoBehaviour
    {

        private String _collision;
        private String _collisionTag;

        void OnCollisionEnter(Collision collider)
        {
            _collision = collider.gameObject.name;
            _collisionTag = collider.gameObject.tag;
        }

        void OnGUI()
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 2000, 20), _collision);
            GUI.Label(new Rect(Screen.width / 2, (Screen.height / 2) - 100, 2000, 20), _collisionTag);
        }

    }
}
