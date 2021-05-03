﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GloveFollowing : MonoBehaviour
{
    public Transform root;
    public OVRInput.Controller m_controller;
    public bool isFollowing = true;
    public AudioClip woosh;
    public bool isHitted;
    public string correctPunch;
    private AudioSource ac;
    private Vector3 oldPosition = Vector3.zero;
    private int frameCounter = 0;
    private bool isPlayed = false;
    private Text debug;
    private Text instruction;
    private ScoreController SC;
    public bool isRecognizing;
    private string curPunch;

    private void Start()
    {
        ac = GetComponent<AudioSource>();
        StartCoroutine(displaySpeed());
        if (GameObject.Find("debug") != null)
        {
            debug = GameObject.Find("debug").GetComponent<Text>();
        }
    }
    void Update()
    {
        if (isFollowing)
        {
            if (transform.localPosition.magnitude != 0.15f)
            {
                transform.localPosition = new Vector3(0, 0, -0.15f);
                if (this.gameObject.name.Equals("LeftGlove"))
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 90);
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, -90);
                }
            }
        }

    }

    IEnumerator PlayWoosh()
    {
        isPlayed = true;
        ac.PlayOneShot(woosh, 0.3f);
        yield return new WaitForSeconds(0.4f);
        isPlayed = false;
    }
    public Vector3 RelativePosition()
    {
        Vector3 distance = transform.position - root.position;
        Vector3 relativePosition = Vector3.zero;
        relativePosition.x = Vector3.Dot(distance, root.right.normalized);
        relativePosition.y = Vector3.Dot(distance, root.up.normalized);
        relativePosition.z = Vector3.Dot(distance, root.forward.normalized);
        return relativePosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BlockController>() != null)
        {
            StartCoroutine(TriggerVibration());
        }
    }
    IEnumerator TriggerVibration()
    {
        OVRInput.SetControllerVibration(1f, 1f, m_controller);
        yield return new WaitForSeconds(0.1f);
        OVRInput.SetControllerVibration(1f, 0f, m_controller);
    }
    
    IEnumerator displaySpeed()
    {
        float maxX = 0;
        float maxZ = 0;
        while (true)
        {
            Vector3 oldPosition = RelativePosition();
            yield return new WaitForSeconds(0.1f);
            Vector3 newPosition = RelativePosition();
            if (this.gameObject.name.Equals("RightGlove"))
            {
                //debug.text = "Speed: " + ((newPosition - oldPosition) * 10).magnitude.ToString() + "; " + "Position: " + newPosition.ToString() + " " + newPosition.magnitude.ToString();
                if ((newPosition - oldPosition).magnitude > 0.1)
                {
                    isRecognizing = true;
                    //if (!isPlayed)
                    //{
                    //    StartCoroutine(PlayWoosh());
                    //}
                }
                else
                {
                    isRecognizing = false;
                }
                if (isRecognizing)
                {
                    if (Math.Abs(newPosition.x) > maxX)
                    {
                        maxX = Math.Abs(newPosition.x);
                    }
                    if (Math.Abs(newPosition.z) > maxZ)
                    {
                        maxZ = Math.Abs(newPosition.z);
                    }
                    //debug.text = new Vector3(maxX, maxY, maxZ).ToString();
                }
                else
                {
                    //if (maxX > 0.4)
                    //{
                    //    curPunch = "Right Hook";
                    //}
                    //else 
                    if (maxZ > 0.2)
                    {
                        curPunch = "Cross";
                    }
                    else
                    {
                        curPunch = "No Punch";
                    }
                    if (isHitted)
                    {
                        if (curPunch.Equals(correctPunch))
                        {
                            GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(correctPunch, true);
                        }
                        else
                        {
                            GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(correctPunch, false);
                            GameObject.Find("CenterEyeAnchor").GetComponent<headSoundController>().playSound(1);
                        }
                    }
                    isHitted = false;
                    maxX = 0;
                    maxZ = 0;
                }
            }
            else
            {
                //debug.text = "Speed: " + ((newPosition - oldPosition) * 10).magnitude.ToString() + "; " + "Position: " + newPosition.ToString() + " " + newPosition.magnitude.ToString();
                if ((newPosition - oldPosition).magnitude > 0.15)
                {
                    isRecognizing = true;
                    //if (!isPlayed)
                    //{
                    //    StartCoroutine(PlayWoosh());
                    //}
                }
                else
                {
                    isRecognizing = false;
                }
                if (isRecognizing)
                {
                    if (Math.Abs(newPosition.x) > maxX)
                    {
                        maxX = Math.Abs(newPosition.x);
                    }
                    if (Math.Abs(newPosition.z) > maxZ)
                    {
                        maxZ = Math.Abs(newPosition.z);
                    }
                    //debug.text = new Vector3(maxX, maxY, maxZ).ToString();
                }
                else
                {
                    if (maxX > 0.4)
                    {
                        curPunch = "Left Hook";
                    }
                    else if (maxZ > 0.3)
                    {
                        curPunch = "Jab";
                    }
                    else
                    {
                        curPunch = "No Punch";
                    }
                    if (isHitted)
                    {
                        if (curPunch.Equals(correctPunch))
                        {
                            GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(correctPunch, true);
                        } else
                        {
                            GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(correctPunch, false);
                            GameObject.Find("CenterEyeAnchor").GetComponent<headSoundController>().playSound(1);
                        }

                    }
                    isHitted = false;
                    maxX = 0;
                    maxZ = 0;
                }
            }


        }
    }
}
