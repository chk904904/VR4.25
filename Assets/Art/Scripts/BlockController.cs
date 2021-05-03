using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public string punchName;
    public float duration;
    private bool isCollided;
    private void Start()
    {
        isCollided = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isCollided)
        {
            isCollided = true;
            if (other.GetComponent<GloveFollowing>() != null && other.GetComponent<GloveFollowing>().isRecognizing)
            {
                other.GetComponent<GloveFollowing>().isHitted = true;
                other.GetComponent<GloveFollowing>().correctPunch = punchName;
                GameObject.Find("CenterEyeAnchor").GetComponent<headSoundController>().playSound(0);
                Destroy(this.gameObject);
            }
        }
        
    }
    void Update()
    {
        if (transform.position.z > 5f)
        {
            transform.position += new Vector3(0f, 0f, -(15 / (duration / 4)) * Time.deltaTime);
        }
        else
        {
            transform.position += new Vector3(0f, 0f, -(4 / (duration * 3 / 4)) * Time.deltaTime);
        }

        if (transform.position.z < 0)
        {
            GameObject.Find("OverallController").GetComponent<NewOverallController>().updateScore(punchName, false);
            Destroy(this.gameObject);
        }
    }
}
