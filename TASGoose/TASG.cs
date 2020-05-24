﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace TASGoose
{
    class TASG : MonoBehaviour
    {

        public static ArrayList _UDO = new ArrayList();

        public GameObject _goose;

        public bool _advancedOutput;

        public bool _noclip;

        public bool _disableHumans;

        public Vector3 _goosePos;

        //public ShowCollisions _wireframeCollisions;

        public bool _slowMo;

        public float _gooseMass;

        float _deltaTime = 0.0f;

        public bool _menuEnabled;

        public GUIStyle _guiStyle = null;

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
            _goose.AddComponent<ShowCollisions>();
            Camera.main.gameObject.AddComponent<Wireframe>();
            _gooseMass = _goose.GetComponent<Rigidbody>().mass;
            _guiStyle = new GUIStyle();
            _guiStyle.fontSize = Screen.height * 2 / 100;
            _guiStyle.normal.textColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }

        void Update()
        {
            _deltaTime += (Time.unscaledDeltaTime - _deltaTime) * 0.1f;

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.B))
            {
                _disableHumans = !_disableHumans;
            }

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
            {
                _noclip = !_noclip;
            }

            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O))
            {
                _advancedOutput = !_advancedOutput;
            }

            if(_noclip)
            {
                _goose.GetComponent<Rigidbody>().isKinematic = true;
                _goose.GetComponent<Rigidbody>().useGravity = false;
                _goose.GetComponent<Rigidbody>().mass = 0;
                Collider[] @colliders = _goose.GetComponents<Collider>();
                foreach(Collider c in @colliders)
                {
                    c.enabled = false;
                }
                if(Input.GetKey(KeyCode.Space))
                {
                    _goose.transform.position += new Vector3(0, 0.05f, 0);
                }
                if(Input.GetKey(KeyCode.LeftControl))
                {
                    _goose.transform.position += new Vector3(0, -0.05f, 0);
                }
                if (Input.GetKey(KeyCode.W))
                {

                    _goose.transform.position += _goose.transform.forward * 0.05f;
                }
                if (Input.GetKey(KeyCode.S))
                {

                    _goose.transform.position += -(_goose.transform.forward * 0.05f);
                }
                if (Input.GetKey(KeyCode.D))
                {

                    _goose.transform.position += (_goose.transform.right * 0.05f);
                }
                if (Input.GetKey(KeyCode.A))
                {

                    _goose.transform.position += -(_goose.transform.right * 0.05f);
                }
            }

            if(!_noclip)
            {
                _goose.GetComponent<Rigidbody>().isKinematic = false;
                _goose.GetComponent<Rigidbody>().useGravity = true;
                _goose.GetComponent<Rigidbody>().mass = _gooseMass;
                Collider[] @colliders = _goose.GetComponents<Collider>();
                foreach (Collider c in @colliders)
                {
                    c.enabled = true;
                }
            }

            if(_disableHumans)
            {
                GameObject[] @objs = GameObject.FindGameObjectsWithTag("Human");
                foreach(GameObject g in @objs)
                {
                    g.transform.position = new Vector3(100000, 100000, 10000);
                }
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                _menuEnabled = !_menuEnabled;
            }

            if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                _slowMo = !_slowMo;
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
                if (!_advancedOutput)
                {
                    GUI.Label(new Rect(20, 20, 2000, 200), "Position: (" + _goose.gameObject.transform.position + ")");
                    GUI.Label(new Rect(20, 40, 2000, 200), "Rotation: (" + _goose.gameObject.transform.rotation + ")");
                    GUI.Label(new Rect(20, 60, 2000, 200), "Velocity: " + _goose.GetComponent<Rigidbody>().velocity);
                    GUI.Label(new Rect(20, 80, 2000, 200), "Noclip: " + _noclip);
                    GUI.Label(new Rect(20, 100, 2000, 200), "TASGoose 2.1");
                }
                else
                {
                    Vector3 _gs_position = _goose.gameObject.transform.position;
                    Quaternion _gs_rotation = _goose.gameObject.transform.rotation;
                    Vector3 _gs_velocity = _goose.GetComponent<Rigidbody>().velocity;

                    string _position_string = string.Format("Position: ({0}, {1}, {2})", _gs_position.x.ToString("F4"), _gs_position.y.ToString("F4"), _gs_position.z.ToString("F4"));
                    string _rotation_string = string.Format("Rotation: ({0}, {1}, {2}, {3})", _gs_rotation.x.ToString("F4"), _gs_rotation.y.ToString("F4"), _gs_rotation.z.ToString("F4"), _gs_rotation.w.ToString("F4"));
                    string _velocity_string = string.Format("Velocity: ({0}, {1}, {2})", _gs_velocity.x.ToString("F4"), _gs_velocity.y.ToString("F4"), _gs_velocity.z.ToString("F4"));

                    GUI.Label(new Rect(20, 30, 2000, 200), _position_string, _guiStyle);
                    GUI.Label(new Rect(20, 60, 2000, 200), _rotation_string, _guiStyle);
                    GUI.Label(new Rect(20, 90, 2000, 200), _velocity_string, _guiStyle);
                    GUI.Label(new Rect(20, 120, 2000, 200), "Noclip: " + _noclip);
                    GUI.Label(new Rect(20, 140, 2000, 200), "TASGoose 2.1");
                }
            }

            if (_menuEnabled)
            {
                GUI.Box(new Rect(20, 20, Screen.width / 2, Screen.height / 2), "TAS Goose");
                GUI.Label(new Rect(30, 60, 2000, 20), "Slowmotion: CTRL+1");
                GUI.Label(new Rect(30, 100, 2000, 20), "Save Location: CTRL+S");
                GUI.Label(new Rect(30, 120, 2000, 20), "Load Location: CTRL+L");
                GUI.Label(new Rect(30, 140, 2000, 20), "Wireframe View: CTRL+W");
                GUI.Label(new Rect(30, 160, 2000, 20), "Delete All Humans: CTRL+B");
                GUI.Label(new Rect(30, 200, 2000, 20), "Advanced Output: CTRL+O");
            }
        }
    }
}
