using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionController : MonoBehaviour
{	
	public GameObject idle; 
	public GameObject rightCross; 
	public GameObject leftJab; 
	public GameObject rightHook; 
	public GameObject leftHook; 
	public GameObject leftUppercut; 
	public GameObject rightUppercut; 

	private GameObject currentInstruction; 
    // Start is called before the first frame update
    void Start()
    {
    	idle.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        currentInstruction = idle; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showInstruction(string instructionName){
    	if(instructionName == "Right Hook"){
    		hidePrevInstruction();
    		rightHook.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    		currentInstruction = rightHook; 
    	}
    	else if(instructionName == "Jab"){
    		hidePrevInstruction();
    		leftJab.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    		currentInstruction = leftJab; 
    	}
    	else if(instructionName == "Left Hook"){
    		hidePrevInstruction();
    		leftHook.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    		currentInstruction = leftHook; 
    	}
    	else{
    		hidePrevInstruction();
    		idle.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    		currentInstruction = idle; 
    	}
    }

    void hidePrevInstruction(){
    	currentInstruction.transform.localPosition = new Vector3(0.0f, 0.0f, -2000.0f);
    }
}
