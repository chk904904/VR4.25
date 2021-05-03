using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NewOverallController : MonoBehaviour
{
	public Text[] scores = new Text[7];
    public Transform[] Pad;
    public Transform poolNoodle;
    public int BPM;
    public AudioClip BGM;
    public Material trackMaterial;
    public Material trackMaterialOld;
    public GameObject[] tracks = new GameObject[3];
    public Animator wrongAct;
    

    private int[] currentScore = new int[7];
    private float startTime;  
    private int beats;
    private float beatDuration;
    private int curPunch;
    private int curDodge;
    private AudioSource ac;
    private bool isStarted;
    private GameObject instructionAnchor;
    private string[][] movements;
    private int nextDisMov;
    private int[] movScores = new int[7];
    private string[][] punches =
    {
        new string[] {"16", "M", "Jab" },
        new string[] {"18", "M", "Cross" },
        new string[] {"24", "M", "Jab" },
        new string[] {"26", "M", "Cross" },
        new string[] {"32", "M", "Jab" },
        new string[] {"33", "M", "Cross" },
        new string[] {"34", "M", "Jab" },
        new string[] {"40", "M", "Cross" },
        new string[] {"41", "M", "Jab" },
        new string[] {"42", "M", "Cross" },
        new string[] {"68", "L", "Left Hook" },
        new string[] {"70", "L", "Cross" },
        new string[] {"76", "M", "Cross" },
        new string[] {"78", "M", "Left Hook" },
        new string[] {"82", "L", "Left Hook" },
        new string[] {"86", "M", "Cross" },
        new string[] {"90", "R", "Cross" },
        new string[] {"94", "M", "Left Hook" },
        new string[] {"112", "M", "Jab" },
        new string[] {"114", "M", "Cross" },
        new string[] {"118", "R", "Cross" },
        new string[] {"120", "R", "Left Hook" },
        new string[] {"122", "R", "Cross" },
        new string[] {"126", "M", "Left Hook" },
        new string[] {"128", "M", "Jab" },
        new string[] {"130", "M", "Cross" },
        new string[] {"134", "L", "Left Hook" },
        new string[] {"136", "L", "Cross" },
        new string[] {"138", "L", "Left Hook" },
        new string[] {"142", "M", "Cross" },
        new string[] {"144", "M", "Jab" },
        new string[] {"145", "M", "Cross" },
        new string[] {"147", "L", "Left Hook" },
        new string[] {"148", "L", "Cross" },
        new string[] {"149", "L", "Jab" },
        new string[] {"151", "M", "Cross" },
        new string[] {"152", "M", "Jab" },
        new string[] {"153", "M", "Cross" },
        new string[] {"155", "R", "Cross" },
        new string[] {"156", "R", "Left Hook" },
        new string[] {"157", "R", "Cross" },
        new string[] {"159", "M", "Left Hook" },
        new string[] {"160", "M", "Jab" },
        new string[] {"161", "M", "Cross" },
        new string[] {"163", "R", "Cross" },
        new string[] {"164", "R", "Left Hook" },
        new string[] {"165", "R", "Cross" },
        new string[] {"167", "M", "Left Hook" },
        new string[] {"168", "M", "Jab" },
        new string[] {"169", "M", "Cross" },
        new string[] {"171", "L", "Left Hook" },
        new string[] {"172", "L", "Cross" },
        new string[] {"173", "L", "Jab" },
        new string[] {"175", "M", "Cross" },

    };
    private string[][] dodges =
    {
        new string[] {"4", "M", "Duck" },
        new string[] {"10", "M", "Duck" },
        new string[] {"14", "M", "Duck" },
        new string[] {"20", "M", "Duck" },
        new string[] {"28", "M", "Duck" },
        new string[] {"36", "M", "Duck" },
        new string[] {"44", "M", "Duck" },
        new string[] {"48", "M", "RL" },
        new string[] {"52", "L", "RR" },
        new string[] {"56", "M", "RR" },
        new string[] {"60", "R", "RL" },
        new string[] {"64", "M", "RL" },
        new string[] {"72", "L", "RR" },
        new string[] {"80", "M", "RL" },
        new string[] {"84", "L", "RR" },
        new string[] {"88", "M", "RR" },
        new string[] {"92", "R", "RL" },
        new string[] {"96", "M", "SL" },
        new string[] {"100", "L", "SR" },
        new string[] {"104", "M", "SR" },
        new string[] {"108", "R", "SL" },
        new string[] {"116", "M", "SR" },
        new string[] {"124", "R", "SL" },
        new string[] {"132", "M", "SL" },
        new string[] {"140", "L", "SR" },
        new string[] {"146", "M", "SL" },
        new string[] {"150", "L", "RR" },
        new string[] {"154", "M", "SR" },
        new string[] {"158", "R", "RL" },
        new string[] {"162", "M", "SR" },
        new string[] {"166", "R", "RL" },
        new string[] {"170", "M", "SL" },
        new string[] {"174", "L", "RR" },

    };
    private Vector3[] iniPadPos = {
        new Vector3(-1.05f, 1.35f, 20f),
        new Vector3(-0.05f, 1.35f, 20f),
        new Vector3(0.88f, 1.35f, 20f)
    };
    private Vector3[] iniPoolPos = {
        new Vector3(-1.1f, 1.8f, 2.6f),
        new Vector3(-0.15f, 1.8f, 2.6f),
        new Vector3(0.8f, 1.8f, 2.6f)
    };


    // Start is called before the first frame update
    void Start()
    {
        isStarted = false;
        ac = GetComponent<AudioSource>();
        instructionAnchor = GameObject.Find("InstructionsSet");
        beatDuration = 60f / BPM;
        curPunch = 0;
        curDodge = 0;
        beats = 1;
        movements = new string[punches.Length + dodges.Length][];
        movementsInOrder();
        getMovScores();
        StartCoroutine(startGame());
        //for(int i = 1; i < punches.Length; i++)
        //{
        //    punches[i][0] = (8 + 4 * i).ToString();
        //    if(i % 3 == 0)
        //    {
        //        punches[i][1] = "M";
        //    } else if(i % 3 == 1)
        //    {
        //        punches[i][1] = "L";
        //    } else
        //    {
        //        punches[i][1] = "R";
        //    }
        //}
    }

    IEnumerator startGame()
    {
        yield return new WaitForSeconds(3.0f);
        isStarted = true;
        displayTrack(1);
        displayInstruction(5);
        nextDisMov = 0;
        ac.PlayOneShot(BGM);
        startTime = Time.time;
    }

    private void getMovScores()
    {
        for(int i = 0; i < movements.Length; i++)
        {
            switch (movements[i][2])
            {
                case "Jab":
                    movScores[0]++;
                    break;
                case "Cross":
                    movScores[1]++;
                    break;
                case "Left Hook":
                    movScores[2]++;
                    break;
                case "Right Hook":
                    movScores[2]++;
                    break;
                case "Duck":
                    movScores[3]++;
                    break;
                case "RL":
                    movScores[4]++;
                    break;
                case "SL":
                    movScores[5]++;
                    break;
                case "RR":
                    movScores[4]++;
                    break;
                case "SR":
                    movScores[5]++;
                    break;
            }
        }
        movScores[6] = movements.Length;
        for (int i = 0; i < currentScore.Length; i++)
        {
            scores[i].text = currentScore[i].ToString() + "/" + movScores[i].ToString();
        }
    }

    void Update()
    {
        if (isStarted)
        {
            int curBeats = (int)(Math.Floor((Time.time - startTime) / beatDuration));
            if (curBeats != beats)
            {
                beats = curBeats;
                if (curPunch != punches.Length)
                {
                    if ((beats + 4).ToString().Equals(punches[curPunch][0]))
                    {
                        createMovement(getTrackNumber(punches[curPunch][1]), punches[curPunch][2]);
                        curPunch++;
                    }
                }
                if (curDodge != dodges.Length)
                {
                    if ((beats + 2).ToString().Equals(dodges[curDodge][0]))
                    {
                        createMovement(getTrackNumber(dodges[curDodge][1]), dodges[curDodge][2]);
                        curDodge++;
                    }
                }
                if (nextDisMov != movements.Length)
                {
                    if (beats.ToString().Equals(movements[nextDisMov][0]))
                    {
                        if (nextDisMov == movements.Length - 1)
                        {
                            displayInstruction(0);
                        } else
                        {
                            nextDisMov++;
                            displayInstruction(getMovementNumber(movements[nextDisMov][2]));
                        }
                    }
                }
            }
        }
    }

    private void createMovement(int track, string mov)
    {
        Transform temp;
        switch (mov)
        {
            case "Jab":
                temp = Instantiate(Pad[0]);
                temp.position = iniPadPos[track];
                temp.GetComponent<BlockController>().punchName = mov;
                temp.GetComponent<BlockController>().duration = 4 * beatDuration;
                break;
            case "Cross":
                temp = Instantiate(Pad[1]);
                temp.position = iniPadPos[track];
                temp.GetComponent<BlockController>().punchName = mov;
                temp.GetComponent<BlockController>().duration = 4 * beatDuration;
                break;
            case "Left Hook":
                temp = Instantiate(Pad[2]);
                temp.position = iniPadPos[track];
                temp.GetComponent<BlockController>().punchName = mov;
                temp.GetComponent<BlockController>().duration = 4 * beatDuration;
                break;
            case "Right Hook":
                temp = Instantiate(Pad[3]);
                temp.position = iniPadPos[track];
                temp.GetComponent<BlockController>().punchName = mov;
                temp.GetComponent<BlockController>().duration = 4 * beatDuration;
                break;
            case "Duck":
                temp = Instantiate(poolNoodle);
                temp.position = iniPoolPos[track];
                temp.rotation = Quaternion.Euler(0, 90, 0);
                temp.GetComponentInChildren<PoolNoodleController>().duration = 2 * beatDuration;
                temp.GetComponentInChildren<PoolNoodleController>().dodgeName = mov;
                break;
            case "RL":
                displayTrack(track - 1);
                temp = Instantiate(poolNoodle);
                temp.position = iniPoolPos[track];
                temp.rotation = Quaternion.Euler(0, 90, 0);
                temp.GetComponentInChildren<PoolNoodleController>().duration = 2 * beatDuration;
                temp.GetComponentInChildren<PoolNoodleController>().dodgeName = mov;
                break;
            case "SL":
                displayTrack(track - 1);
                temp = Instantiate(poolNoodle);
                temp.position = new Vector3(iniPoolPos[track].x, iniPoolPos[track].y, 9f);
                temp.GetComponentInChildren<PoolNoodleController>().duration = 2 * beatDuration;
                temp.GetComponentInChildren<PoolNoodleController>().dodgeName = mov;
                break;
            case "RR":
                displayTrack(track + 1);
                temp = Instantiate(poolNoodle);
                temp.position = iniPoolPos[track];
                temp.rotation = Quaternion.Euler(0, -90, 0);
                temp.GetComponentInChildren<PoolNoodleController>().duration = 2 * beatDuration;
                temp.GetComponentInChildren<PoolNoodleController>().dodgeName = mov;
                break;
            case "SR":
                displayTrack(track + 1);
                temp = Instantiate(poolNoodle);
                temp.position = new Vector3(iniPoolPos[track].x, iniPoolPos[track].y, 9f);
                temp.GetComponentInChildren<PoolNoodleController>().duration = 2 * beatDuration;
                temp.GetComponentInChildren<PoolNoodleController>().dodgeName = mov;
                break;
        }
    }

    private int getTrackNumber(string trackName)
    {
        int temp = -1;
        switch (trackName)
        {
            case "L":
                temp = 0;
                break;
            case "M":
                temp = 1;
                break;
            case "R":
                temp = 2;
                break;
            default:
                break;
        }
        return temp;
    }

    private void movementsInOrder()
    {
        int punch = 0;
        int dodge = 0;
        int mov = 0;
        while (punch < punches.Length || dodge < dodges.Length)
        {
            if(punch == punches.Length)
            {
                movements[mov] = dodges[dodge];
                dodge++;
                mov++;
            } else if (dodge == dodges.Length)
            {
                movements[mov] = punches[punch];
                punch++;
                mov++;
            } else
            {
                if (int.Parse(punches[punch][0]) < int.Parse(dodges[dodge][0]))
                {
                    movements[mov] = punches[punch];
                    punch++;
                    mov++;
                } else
                {
                    movements[mov] = dodges[dodge];
                    dodge++;
                    mov++;
                }
            }


        }
    }
    private int getMovementNumber(string punchName)
    {
        switch (punchName)
        {
            case "Jab":
                return 1;
            case "Cross":
                return 2;
            case "Left Hook":
                return 3;
            case "Right Hook":
                return 4;
            case "Duck":
                return 5;
            case "RL":
                return 6;
            case "RR":
                return 7;
            case "SL":
                return 8;
            case "SR":
                return 9;
            default:
                return -1;
        }
    }

    private void displayInstruction(int instructionNumber)
    {
        for (int i = 0; i < instructionAnchor.transform.childCount; i++)
        {
            if(i == instructionNumber)
            {
                instructionAnchor.transform.GetChild(i).gameObject.SetActive(true);
            } else
            {
                instructionAnchor.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    public void updateScore(string movName, bool isCorrect){
        if (isCorrect)
        {
            switch (movName)
            {
                case "Jab":
                    currentScore[0]++;
                    break;
                case "Cross":
                    currentScore[1]++;
                    break;
                case "Left Hook":
                    currentScore[2]++;
                    break;
                case "Right Hook":
                    currentScore[2]++;
                    break;
                case "Duck":
                    currentScore[3]++;
                    break;
                case "RL":
                    currentScore[4]++;
                    break;
                case "RR":
                    currentScore[4]++;
                    break;
                case "SL":
                    currentScore[5]++;
                    break;
                case "SR":
                    currentScore[5]++;
                    break;
            }
            currentScore[6] = currentScore[0]+ currentScore[1]+ currentScore[2]+ currentScore[3]+ currentScore[4]+ currentScore[5];
            for (int i = 0; i < currentScore.Length; i++)
            {
                scores[i].text = currentScore[i].ToString() + "/" + movScores[i].ToString();
            }
        } else
        {
            wrongAct.Play("wrongAct");
        }
        
    }

    private void displayTrack(int trackNumber)
    {
        for (int i = 0; i < tracks.Length; i++)
        {
            if(i == trackNumber)
            {
                tracks[i].GetComponent<MeshRenderer>().material = trackMaterial;
            } else
            {
                tracks[i].GetComponent<MeshRenderer>().material = trackMaterialOld;
            }
        }
        StartCoroutine(trackFlashing(trackNumber));
    }

    IEnumerator trackFlashing(int track)
    {
        yield return new WaitForSeconds(0.3f);
        tracks[track].GetComponent<MeshRenderer>().material = trackMaterialOld;
        yield return new WaitForSeconds(0.3f);
        tracks[track].GetComponent<MeshRenderer>().material = trackMaterial;
        yield return new WaitForSeconds(0.3f);
        tracks[track].GetComponent<MeshRenderer>().material = trackMaterialOld;
        yield return new WaitForSeconds(0.3f);
        tracks[track].GetComponent<MeshRenderer>().material = trackMaterial;
    }
}
