using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TASGoose
{
    class TASG : MonoBehaviour
    {

        public static ArrayList _UDO = new ArrayList();

        public GameObject _goose;

        public Vector3 _goosePos;

        public WireframeCollisions _wireframeCollisions;

        public bool _slowMo;

        float _deltaTime = 0.0f;

        public bool _menuEnabled;

        void SaveLocation()
        {
            _goosePos = _goose.transform.position;
        }

        void LoadLocation()
        {
            _goose.transform.position = _goosePos;
        }

        public void Start()
        {
            _goose = GameObject.Find("Goose");
            _goose.AddComponent<WireframeCollisions>();
        }

        void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                _menuEnabled = !_menuEnabled;
            }

            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                _slowMo = !_slowMo;
            }

            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B))
            {
                GameObject[] @Humans = GameObject.FindGameObjectsWithTag("Human");
                foreach(GameObject g in @Humans)
                {
                    GameObject.Destroy(g);
                }
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
            {
                SaveLocation();
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
            {
                LoadLocation();
            }

            if (_slowMo)
            {
                Time.timeScale = 0.2f;
            }
            if(!_slowMo)
            {
                Time.timeScale = 1;
            }
            if (_menuEnabled)
            {
                GUI.Box(new Rect(20, 20, Screen.width / 2, Screen.height / 2), "TAS Goose");
                GUI.Label(new Rect(30, 60, 2000, 20), "Slowmotion: CTRL+1");
                GUI.Label(new Rect(30, 100, 2000, 20), "Save Location: CTRL+S");
                GUI.Label(new Rect(30, 120, 2000, 20), "Load Location: CTRL+L");
            }

            if (!_menuEnabled)
            {
                GUI.Label(new Rect(20, 20, 2000, 200), "Position: " + _goose.gameObject.transform.position);
                GUI.Label(new Rect(20, 40, 2000, 200), "Rotation: " + _goose.gameObject.transform.rotation);
                GUI.Label(new Rect(20, 60, 2000, 200), "Velocity: " + _goose.GetComponent<Rigidbody>().velocity);
                GUI.Label(new Rect(20, 80, 2000, 200), "TASGoose 2.0p1");
            }
        }

        public void OnGUI()
        {

            int w = Screen.width, h = Screen.height;

            GUIStyle style = new GUIStyle();

            Rect rect = new Rect(0, 0, w, h * 2 / 100);
            style.alignment = TextAnchor.UpperRight;
            style.fontSize = h * 2 / 100;
            style.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            float msec = _deltaTime * 1000.0f;
            float fps = 1.0f / _deltaTime;
            string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
            GUI.Label(rect, text, style);

            if (!_menuEnabled)
            {
                GUI.Label(new Rect(20, 20, 2000, 200), "Position: (" + _goose.gameObject.transform.position + ")");
                GUI.Label(new Rect(20, 40, 2000, 200), "Rotation: (" + _goose.gameObject.transform.rotation + ")");
                GUI.Label(new Rect(20, 60, 2000, 200), "Velocity: " + _goose.GetComponent<Rigidbody>().velocity);
                GUI.Label(new Rect(20, 80, 2000, 200), "TASGoose 2.0p1");
            }

            if (_menuEnabled)
            {
                GUI.Box(new Rect(20, 20, Screen.width / 2, Screen.height / 2), "TAS Goose");
                GUI.Label(new Rect(30, 60, 2000, 20), "Slowmotion: CTRL+1");
                GUI.Label(new Rect(30, 100, 2000, 20), "Save Location: CTRL+S");
                GUI.Label(new Rect(30, 120, 2000, 20), "Load Location: CTRL+L");
            }
        }
    }
}
