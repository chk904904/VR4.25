using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolNoodleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;
    public string dodgeName;
    private float startTime;
    void Start()
    {
        //transform.parent.rotation = Quaternion.Euler(0, 45, 0);
        startTime = Time.time;
    }

    private Vector3 LerpRotation(Vector3 fromRot, Vector3 toRot)
    {
        if((Time.time - startTime) / duration >= 1.0f)
        {
            return (toRot);
        } else
        {
            return (Vector3.Lerp(fromRot, toRot, (Time.time - startTime) / duration));
        }
    }
    private void Update()
    {
        switch (dodgeName)
        {
            case "Duck":
                if (transform.parent.rotation.y <= -Math.PI / 18)
                {
                    if (Math.Abs(GameObject.Find("CenterEyeAnchor").transform.position.x - transform.parent.position.x) < 0.5)
                    {
                        GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(dodgeName, true);
                    }
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    transform.parent.Rotate(LerpRotation(Vector3.zero, new Vector3(0, -150 / duration, 0)) * Time.deltaTime);
                }
                break;
            case "RL":
                if (transform.parent.rotation.y <= -Math.PI / 18)
                {
                    if ((GameObject.Find("CenterEyeAnchor").transform.position.x - transform.parent.position.x) < -0.5)
                    {
                        GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(dodgeName, true);
                    }
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    transform.parent.Rotate(LerpRotation(Vector3.zero, new Vector3(0, -150 / duration, 0)) * Time.deltaTime);
                }
                break;
            case "SL":
                if (transform.parent.position.z <= 2.5)
                {
                    if ((GameObject.Find("CenterEyeAnchor").transform.position.x - transform.parent.position.x) < -0.5)
                    {
                        GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(dodgeName, true);
                    }
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    transform.parent.Translate(LerpRotation(Vector3.zero, new Vector3(0, 0, -10 / duration)) * Time.deltaTime);
                }
                break;
            case "RR":
                if (transform.parent.rotation.y >= Math.PI / 18)
                {
                    if ((GameObject.Find("CenterEyeAnchor").transform.position.x - transform.parent.position.x) > 0.5)
                    {
                        GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(dodgeName, true);
                    }
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    transform.parent.Rotate(LerpRotation(Vector3.zero, new Vector3(0, 150 / duration, 0)) * Time.deltaTime);
                }
                break;
            case "SR":
                if (transform.parent.position.z <= 2.5)
                {
                    if ((GameObject.Find("CenterEyeAnchor").transform.position.x - transform.parent.position.x) > 0.5)
                    {
                        GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(dodgeName, true);
                    }
                    Destroy(transform.parent.gameObject);
                }
                else
                {
                    transform.parent.Translate(LerpRotation(Vector3.zero, new Vector3(0, 0, -10 / duration)) * Time.deltaTime);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("CenterEyeAnchor"))
        {
            GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(dodgeName, false);
            GameObject.Find("CenterEyeAnchor").GetComponent<headSoundController>().playSound(1);
            Destroy(transform.parent.gameObject);
        }
    }
}
