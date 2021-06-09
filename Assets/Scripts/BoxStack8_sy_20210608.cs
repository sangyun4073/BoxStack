/*
 * 수정사항 2021.06.03 ver 1.1
 * [] OnActionReceived > success경우 좌표 출력기능 추가
 * [] 
 * 
 * 
 * 
 */

// act[0] : BoxPointer의 방향을 정해주는 값 (0~5) 1~4는 x,z 이동 5는 Box 생성
// act[1] : Box가 놓일 것인가 놓이지 않을 것인가?(1이 놓이는 것)
// Add Boxpointer Collider
// BoxPointer에 박스 놓기 and Index 설정해주기(observation )BufferSensor추가

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using UnityEngine.Assertions.Must;
using UnityEditor.Build;
using System;

public class BoxStack8_sy_20210608 : Agent
{

    public GameObject Box0;
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;
    public GameObject Box4;
    public GameObject Box5;
    public GameObject Box6;
    public GameObject Box7;

    public GameObject BoxPointer;
    public Rigidbody r_BoxPointer;

    public Rigidbody r_Box0;
    public Rigidbody r_Box1;
    public Rigidbody r_Box2;
    public Rigidbody r_Box3;
    public Rigidbody r_Box4;
    public Rigidbody r_Box5;
    public Rigidbody r_Box6;
    public Rigidbody r_Box7;

    public List<GameObject> Box_list;
    public List<Rigidbody> rb_list;
    public List<bool> Box_Stacked_list;
    //public bool Wall_Collider;

    BufferSensorComponent m_BufferSensor;

    public int speed = 1;
    public int BoxIndex;
    public int CallAction;
    public float gravity;
    //private int TryForNextSuccess = 0;//2021.06.03 ver 1.1
    public override void Initialize()
   
    {
        m_BufferSensor = GetComponent<BufferSensorComponent>();

        Box0 = GameObject.Find("Box0"); r_Box0 = GameObject.Find("Box0").GetComponent<Rigidbody>();
        Box1 = GameObject.Find("Box1"); r_Box1 = GameObject.Find("Box1").GetComponent<Rigidbody>();
        Box2 = GameObject.Find("Box2"); r_Box2 = GameObject.Find("Box2").GetComponent<Rigidbody>();
        Box3 = GameObject.Find("Box3"); r_Box3 = GameObject.Find("Box3").GetComponent<Rigidbody>();
        Box4 = GameObject.Find("Box4"); r_Box4 = GameObject.Find("Box4").GetComponent<Rigidbody>();
        Box5 = GameObject.Find("Box5"); r_Box5 = GameObject.Find("Box5").GetComponent<Rigidbody>();
        Box6 = GameObject.Find("Box6"); r_Box6 = GameObject.Find("Box6").GetComponent<Rigidbody>();
        Box7 = GameObject.Find("Box7"); r_Box7 = GameObject.Find("Box7").GetComponent<Rigidbody>();
  
        BoxPointer = GameObject.Find("BoxPointer"); r_BoxPointer = GameObject.Find("BoxPointer").GetComponent<Rigidbody>();
    }

    public override void OnEpisodeBegin()
    {
        //Debug.Log("Episode Begin");
        CallAction = 0;
        BoxIndex = 0;
        //Wall_Collider = false;
        Physics.gravity = new Vector3(0, gravity, 0);


        Box_Stacked_list = new List<bool>() { false, false, false, false, false, false, false, false };
        Box_list = new List<GameObject>() { Box0, Box1, Box2, Box3, Box4, Box5, Box6, Box7 };
        rb_list = new List<Rigidbody>() { r_Box0, r_Box1, r_Box2, r_Box3, r_Box4, r_Box5, r_Box6, r_Box7 };


        BoxPointer.transform.position = new Vector3(0f, 0.55f, 0f);


        Box_list[0].transform.position = new Vector3(-2f, 1f, -2f);
        Box_list[1].transform.position = new Vector3(-2f, 1f, -3.5f);
        Box_list[2].transform.position = new Vector3(-3.5f, 1f, -2f);
        Box_list[3].transform.position = new Vector3(-3.5f, 1f, -3.5f);
        Box_list[4].transform.position = new Vector3(-2f, 2.5f, -2f);
        Box_list[5].transform.position = new Vector3(-2f, 2.5f, -3.5f);
        Box_list[6].transform.position = new Vector3(-3.5f, 2.5f, -2f);
        Box_list[7].transform.position = new Vector3(-3.5f, 2.5f, -3.5f);

        foreach (GameObject Box in Box_list)
        {
            Box.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        foreach (Rigidbody r_Box in rb_list)
        {
            r_Box.useGravity = false;
            r_Box.constraints = RigidbodyConstraints.FreezeAll;
        }

        r_BoxPointer.velocity = Vector3.zero;
        r_BoxPointer.angularVelocity = Vector3.zero;

        //[x]StackAgent8.cs > 
        //Debug.Log("abc"+"efg");
    }
    //public override void BufferSensorComponent.AppendObservation()
    //쌓여진 index만큼만 추가를 한다. 
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(BoxPointer.transform.position / 10f) ;
        float[] listObservation = new float[8 * 3];

        for (int i = 0; i<BoxIndex; i++ )
        {
            listObservation[3 * i] = Box_list[i].transform.position.x /10f ;
            listObservation[3 * i + 1] = Box_list[i].transform.position.y /10f ;
            listObservation[3 * i + 2] = Box_list[i].transform.position.z /10f ;
            m_BufferSensor.AppendObservation(listObservation);

        }



        //BufferSensorComponent.AppendObservation(BoxPointer.transform.position);
        //sensor.AddObservation(BoxPointer.transform.position);
        ////sensor.AddObservation(BoxPointer.transform.position.z);
        //sensor.AddObservation(Box0.transform.position);
        //sensor.AddObservation(Box1.transform.position);
        //sensor.AddObservation(Box2.transform.position);
        //sensor.AddObservation(Box3.transform.position);
        //sensor.AddObservation(Box4.transform.position);
        //sensor.AddObservation(Box5.transform.position);
        //sensor.AddObservation(Box6.transform.position);
        //sensor.AddObservation(Box7.transform.position);
        //Debug.Log("[x] 8_1 > CollectObservation > " + BoxIndex.ToString());
        //sensor.AddObservation(BoxIndex);
    }

    public void MoveAgent(ActionSegment<int> act)
    {
        /*
        Debug.Log("[x] StackAgent8 > MoveAgent >"+
            " action:"+ act[0].ToString()+
            " stop:"+ act[1].ToString()+
            " telbox:"+ act[2].ToString()+
            " speed:"+ act[3].ToString());
        */
        //AddReward(-0.0005f);
        var dirToGo = Vector3.zero;
        var action = act[0];
        //int speed = act[3];
        //Debug.Log("Speed : " + speed);
        switch (action)
        {
            case 1:
                dirToGo = transform.forward * 0.05f;
                break;
            case 2:
                dirToGo = -transform.forward * 0.05f;
                break;
            case 3:
                dirToGo = transform.right * speed * 0.05f;
                break;
            case 4:
                dirToGo = -transform.right * speed * 0.05f;
                break;
            



        }

        BoxPointer.transform.position = BoxPointer.transform.position + dirToGo;
    }
    public void SetBox(ActionSegment<int> act)
    {
      
        
        int action = act[1];
        switch (action)
        {
            case 1:
                Box_list[BoxIndex].transform.position = new Vector3(BoxPointer.transform.position.x, BoxIndex * 1.5f, BoxPointer.transform.position.z);
                rb_list[BoxIndex].useGravity = true;
                Box_Stacked_list[BoxIndex] = true;
                BoxIndex++;
                break;
            case 2:
                break;
        }
    }


    /*public void StopBoxPointer(ActionSegment<int> act)
    {
        //AddReward(0.001f);
        var Stop = act[1];
        if (Stop == 1)
        {
            StopPointer();
        }
        else { }
        //Debug.Log("Stop act[3] : " + Stop);

    }
    /*
    public void MoveBox(ActionSegment<int> act)
    {
        //AddReward(0.001f);
        var TeleportBox = act[2];
        if (TeleportBox == 1)
        {
            Vector3 pos_BoxPointer = new Vector3(BoxPointer.transform.position.x, BoxPointer.transform.position.y, BoxPointer.transform.position.z);
            Box_list[BoxIndex].transform.position = new Vector3(pos_BoxPointer.x, BoxIndex * 0.25f + 1.0f, pos_BoxPointer.z);
            rb_list[BoxIndex].constraints = RigidbodyConstraints.None;
            rb_list[BoxIndex].useGravity = true;

        }
        //Debug.Log("TeleportBox act[2] : " + TeleportBox);
    }
    */
    public override void OnActionReceived(ActionBuffers actions)
    {
        Debug.Log(actions.DiscreteActions);
        /*
        //Debug.Log("[x] StackAgent8>OnActionReceived"+actions.DiscreteActions.Length.ToString());
        if (BoxPointer.transform.position.x > 1 || BoxPointer.transform.position.x < -1 ||
            BoxPointer.transform.position.z > 1 || BoxPointer.transform.position.z < -1 ||
            BoxPointer.transform.position.y > 4 || BoxPointer.transform.position.y < 0)
        {
            SetReward(-0.01f);
            //Debug.Log("BoxPointer Out of range");
            TryForNextSuccess++;//2021.06.03 ver 1.1
            EndEpisode();
        }
        
        else { SetReward(-0.0001f); }
        */

        if (BoxIndex == 8 )
        {
            AddReward(1.0f);
            Debug.Log("Success");
            EndEpisode();
            //if (Wall_Collider == false)
            //{
                //Debug.Log("Try:" + TryForNextSuccess); TryForNextSuccess = 0;//2021.06.03 ver 1.1
                //Debug.Log("(Box0:" + Box0.transform.position.x + "," + Box0.transform.position.y + "," + Box0.transform.position.z + ")" + "  " +
                //    "(Box1:" + Box1.transform.position.x + "," + Box1.transform.position.y + "," + Box1.transform.position.z + ")" + "  " +
                //    "(Box2:" + Box2.transform.position.x + "," + Box2.transform.position.y + "," + Box2.transform.position.z + ")" + "  " +
                //    "(Box3:" + Box3.transform.position.x + "," + Box3.transform.position.y + "," + Box3.transform.position.z + ")" + "  " +
                //    "(Box4:" + Box4.transform.position.x + "," + Box4.transform.position.y + "," + Box4.transform.position.z + ")" + "  " +
                //    "(Box5:" + Box5.transform.position.x + "," + Box5.transform.position.y + "," + Box5.transform.position.z + ")" + "  " +
                //    "(Box6:" + Box6.transform.position.x + "," + Box6.transform.position.y + "," + Box6.transform.position.z + ")" + "  " +
                //    "(Box7:" + Box7.transform.position.x + "," + Box7.transform.position.y + "," + Box7.transform.position.z + ")");
            //    SetReward(1.0f); Debug.Log("Success!"); EndEpisode();
            //}
            //else
            //{
                //SetReward(-0.01f); Debug.Log("Failed"); EndEpisode();
                //TryForNextSuccess++;//2021.06.03 ver 1.1
            //}
        }
        //else
        //{
        //    MoveAgent(actions.DiscreteActions);
        //    SetBox(actions.DiscreteActions);
        //    if (!Box_Stacked_list[BoxIndex])
        //    {
        //        //StopBoxPointer(actions.DiscreteActions);
        //        //StopPointer();
        //        float[] BoxSize = box_size_scaler(Box_list[BoxIndex]);
        //        if (!CheckIsThereBox(BoxPointer, BoxSize))
        //        {
        //            //MoveBox(actions.DiscreteActions);
        //            //Vector3 pos_BoxPointer = new Vector3(BoxPointer.transform.position.x, BoxPointer.transform.position.y, BoxPointer.transform.position.z);
        //            //Box_list[BoxIndex].transform.position = new Vector3(pos_BoxPointer.x, 1.5f, pos_BoxPointer.z);
        //            //rb_list[BoxIndex].useGravity = true;
        //        }
        //        else
        //        {
        //            //Debug.Log("Can't stack Box");
        //        }
        //    }
        //    //if (CheckBoxBoundOut())
        //    //{
        //    //    //Debug.Log("Box bound out : " + CallAction.ToString() + " , " + BoxIndex.ToString() + " , " + TryForNextSuccess.ToString());
        //    //    //SetReward(-1.0f);
        //    //    TryForNextSuccess++;//2021.06.03 ver 1.1
        //    //    EndEpisode();
        //    //    Debug.Log("CheckBoxBoundOut");
        //    //}
        //    //SetReward(0.01f);
        //    if (CheckStacked(BoxIndex)) { BoxIndex++; }
        //}
        CallAction++;
    }
    bool CheckBoxBoundOut()
    {
        for (int BoxIndex = 0; BoxIndex < 8; BoxIndex++)
        {
            if (Box_list[BoxIndex].transform.position.x > 1 || Box_list[BoxIndex].transform.position.x < -1 ||
                Box_list[BoxIndex].transform.position.z > 1 || Box_list[BoxIndex].transform.position.z < -1)
            {
                return true;
            }
        }
        return false;

    }
    bool CheckStacked(int BoxIndex)
    {
        if (Box_Stacked_list[BoxIndex] == true)
        {
            return true;
        }
        else
            return false;
    }
    void StopPointer()
    {
        r_BoxPointer.velocity = Vector3.zero;
        r_BoxPointer.angularVelocity = Vector3.zero;
        //r_BoxPointer.constraints = RigidbodyConstraints.FreezePosition;
    }
    void Resume()
    {
        r_BoxPointer.constraints = RigidbodyConstraints.None;
    }
    bool checkBoxesY(List<GameObject> Bos_list)
    {
        for (int i = 0; i < 8; i++)
        {
            if (Box_list[i].transform.position.y > 1.5)
                return false;
        }
        return true;
    }
    /*
    GameObject box_seletcter(List<GameObject> Box_list)
    {
        GameObject RandomBox = Box_list[BoxIndex];
        Box_list.Remove(RandomBox);
        Rigidbody r_RandomBox = RandomBox.GetComponent<Rigidbody>();
        rb_list.Remove(r_RandomBox);
        return RandomBox;
    }

    public bool checkStackDone()
    {
        return Check_Stacked;
    }
    */
    bool CheckIsThereBox(GameObject obj, float[] Box_Size)
    {
        float MaxDistance_X = Box_Size[0] / 2;
        float MaxDistance_Y = Box_Size[1] / 2;
        float MaxDistance_Z = Box_Size[2] / 2;
        /*
        Debug.DrawRay(obj.transform.position, obj.transform.right, Color.red, MaxDistance_X);
        Debug.DrawRay(obj.transform.position, -obj.transform.right, Color.red, MaxDistance_X);

        Debug.DrawRay(obj.transform.position, obj.transform.forward, Color.blue, MaxDistance_Z);
        Debug.DrawRay(obj.transform.position, -obj.transform.forward, Color.blue, MaxDistance_Z);

        Debug.DrawRay(obj.transform.position, obj.transform.up, Color.green, MaxDistance_Y);
        Debug.DrawRay(obj.transform.position, -obj.transform.up, Color.green, MaxDistance_Y);
        */
        List<RaycastHit[]> hits_list;
        RaycastHit[] hit0, hit1, hit2, hit3, hit4, hit5;
        hit0 = Physics.RaycastAll(obj.transform.position, obj.transform.right, MaxDistance_X);
        hit1 = Physics.RaycastAll(obj.transform.position, -obj.transform.right, MaxDistance_X);
        hit2 = Physics.RaycastAll(obj.transform.position, obj.transform.forward, MaxDistance_Z);
        hit3 = Physics.RaycastAll(obj.transform.position, -obj.transform.forward, MaxDistance_Z);
        hit4 = Physics.RaycastAll(obj.transform.position, obj.transform.up, MaxDistance_Y);
        hit5 = Physics.RaycastAll(obj.transform.position, -obj.transform.up, MaxDistance_Y);

        hits_list = new List<RaycastHit[]>() { hit0, hit1, hit2, hit3, hit4, hit5 };

        foreach (RaycastHit[] hits in hits_list)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider != null && (hit.collider.CompareTag("Box")))
                {
                    //Debug.Log("there is a box");
                    return true;
                }
            }
        }
        return false;
    }
    float[] box_size_scaler(GameObject Box)
    {
        float[] Box_Size = new float[3]; // 가로, 세로, 높이
        Box_Size[0] = Box.transform.lossyScale.x;
        Box_Size[1] = Box.transform.lossyScale.y;
        Box_Size[2] = Box.transform.lossyScale.z;

        return Box_Size;
    }

    // act[0] : BoxPointer의 방향을 정해주는 값 (1~4)
    // act[1] : 1이면 BoxPointer가 정지, (1이면 box 텔레포트)
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        if (Input.GetKeyDown(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            discreteActionsOut[0] = 3;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            discreteActionsOut[0] = 4;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            discreteActionsOut[1] = 1;
        }

        ////if (Input.GetKey(KeyCode.LeftShift))
        //    discreteActionsOut[0] = 5;
        //if (Input.GetKey(KeyCode.LeftControl))
        //    discreteActionsOut[0] = 6;
        //if (Input.GetKey(KeyCode.S))
        //    discreteActionsOut[1] = 1;
        //if (Input.GetKey(KeyCode.Space))
        //{
        //    discreteActionsOut[2] = 1;
    

    }
    //IEnumerator WaitTime(float t)
    //{
    //    yield return new WaitForSeconds(t);
    //}
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.timeScale);
        //Time.timeScale = 30;
    }
}
