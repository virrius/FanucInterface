using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;
using UnityEngine.EventSystems;

public class FanucScript : MonoBehaviour
{
    FanucModel model = new FanucModel();

    float speed = 15.0f;
    float[] jointAngles = new float[6] { 0f, 0f, 0f, 0f, -90f, 0f };
    float[] jointAnglesInc = new float[6];
    string[] Axis = new string[]{"First","Second","Third","Fourth", "Fifth","Sixth"};
    //float[] worldPos = new float[6] { 985f, 0f, 940f, -180f, 0f, 0f };
    int mode;
    public Transform first;
    public Transform second;
    public Transform third;
    public Transform fourth;
    public Transform fifth;
    public Transform sixth;

    public Text modeName;
    public Text jointCoord;
    public Text worldCoord;

   
    // MyClassWrapper m;
    void Start()
    {
        modeName.text = "Mode: Joints";
        mode = 0;
        fifth.transform.localRotation = Quaternion.Euler(0, jointAngles[4], 0);
        //   m = new MyClassWrapper();
        //  Debug.Log(m.DoSomething(12));
    }

    
    public void IncCoord(int n)
    {
        if (mode == 0)
        {
            jointAngles[n] += Time.deltaTime * speed;
        }

    }

    public void DecCoord(int n)
    {
        if (mode == 0)
        {
            jointAngles[n] -= Time.deltaTime * speed;
        }
    }

    public void IncSpead(int n)
    {
        speed += 5;
    }

    public void DecSpeed(int n)
    {
        speed -= 5;
    }
    public void CollisionLimiter()
    {
        Debug.Log("Collision!");
        for (int i = 0; i < 6; ++i)
        {
            jointAngles[i] -= 2*jointAnglesInc[i];
        }
        first.transform.localRotation = Quaternion.Euler(0, 0, -jointAngles[0]);
        second.transform.localRotation = Quaternion.Euler(0, -jointAngles[1], 0);
        third.transform.localRotation = Quaternion.Euler(0, jointAngles[2] + jointAngles[1], 0);
        fourth.transform.localRotation = Quaternion.Euler(-jointAngles[3], 0, 0);
        fifth.transform.localRotation = Quaternion.Euler(0, jointAngles[4], 0);
        sixth.transform.localRotation = Quaternion.Euler(jointAngles[5], 0, 0);
    }
    void FixedUpdate()
    {
        if (Input.anyKey)
        {
            first.transform.localRotation = Quaternion.Euler(0, 0, -jointAngles[0]);
            second.transform.localRotation = Quaternion.Euler(0, -jointAngles[1], 0);
            third.transform.localRotation = Quaternion.Euler(0, jointAngles[2] + jointAngles[1], 0);
            fourth.transform.localRotation = Quaternion.Euler(-jointAngles[3], 0, 0);
            fifth.transform.localRotation = Quaternion.Euler(0, jointAngles[4], 0);
            sixth.transform.localRotation = Quaternion.Euler(jointAngles[5], 0, 0);
            for (int i=0;i<6;++i)
            {
                jointAnglesInc[i] = Input.GetAxis(Axis[i]) * speed * Time.deltaTime;
                jointAngles[i] += jointAnglesInc[i];
            }
            //jointAngles[0] += Input.GetAxis("First") * speed * Time.deltaTime;
            //jointAngles[1] += Input.GetAxis("Second") * speed * Time.deltaTime;
            //jointAngles[2] += Input.GetAxis("Third") * speed * Time.deltaTime;
            //jointAngles[3] += Input.GetAxis("Fourth") * speed * Time.deltaTime;
            //jointAngles[4] += Input.GetAxis("Fifth") * speed * Time.deltaTime;
            //jointAngles[5] += Input.GetAxis("Sixth") * speed * Time.deltaTime;
            
        }
        


        

        string outputAngles = "";
        string outputForwardTask = "";
        var res = FanucModel.GetCoordsFromMat(model.fanucForwardTask(ref jointAngles));
        foreach (float j in jointAngles)
        {
            outputAngles += (j.ToString("0.00") + ", ");
        }

        jointCoord.text = outputAngles;
        foreach (float r in res)
        {
            outputForwardTask += (r.ToString("0.00") + ", ");
        }

        worldCoord.text = outputForwardTask;
      //  Debug.Log(outputAngles + "| " + outputForwardTask);
    }

}
