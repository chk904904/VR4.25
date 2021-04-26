using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	public float block_speed; 
    public string punchName;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<GloveFollowing>().isHitted = true;
        other.GetComponent<GloveFollowing>().correctPunch = punchName;
        transform.localPosition = new Vector3(0, 0, -2000);
    }
    // Update is called once per frame
    void Update()
    {
    	block_speed = -1f;
    	Debug.Log(block_speed);
        transform.position += new Vector3(0f, 0f, block_speed * Time.deltaTime);
    }
}
