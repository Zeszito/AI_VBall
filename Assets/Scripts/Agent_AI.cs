using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.UI;

public class Agent_AI : Agent
{
    public Rigidbody rgBody;
    public float force;
    public float forceh;
    public float force3x;


    public GameObject win;
    public GameObject obsParent;
    public Transform[] obs;

    public Quaternion myInrot;
    public Vector3 myInPos;

    public Text wintxt;
    int winVal = 0;
    public Text losetxt;
    int looseVal = 1;

    //para eu mexer
    public void Start()
    {
      
         obs = obsParent.GetComponentsInChildren<Transform>();
         myInrot = this.transform.localRotation;
        myInPos = this.transform.localPosition ;
        forceh = force / 2;
        if(force3x == 0)
        {
            force3x = force * 3;
        }
        
        wintxt.text = winVal.ToString();
        losetxt.text = looseVal.ToString();

    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var contAct = actionsOut.ContinuousActions;
        contAct[0] = Input.GetAxis("Horizontal") != 0 ? Input.GetAxis("Horizontal") : 0;
        contAct[1] = Input.GetAxis("Vertical") != 0 ? Input.GetAxis("Vertical") : 0;
 


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        
        // base.OnActionReceived(actions);
        float z = actions.ContinuousActions[0] * force3x;
   
        float x = actions.ContinuousActions[1] * forceh;



        
          this.rgBody.AddForce(new Vector3(x, 0, 0), ForceMode.Impulse);
        
       
        

        this.rgBody.transform.Rotate(new Vector3(0, 0, z));
    }

    public override void OnEpisodeBegin()
    {
        this.transform.localPosition  = myInPos;
        this.transform.localRotation = myInrot;

        wintxt.text = winVal.ToString();
        losetxt.text = looseVal.ToString();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.rotation.x);
     

        // Vector from agent to goal
        Vector3 toWin= new Vector3((win.transform.localPosition .x - this.transform.localPosition .x),
        (win.transform.localPosition .y - this.transform.localPosition .y),
        (win.transform.localPosition .z - this.transform.localPosition .z));


        //vectorAgent to goal (3)
        sensor.AddObservation(toWin.normalized);

        // Distance from the goal (1 float)
        sensor.AddObservation(toWin.magnitude);


        //obestacles
        foreach (Transform o in obs) //7 * 16
        {
            Vector3 toO = new Vector3((o.localPosition .x - this.transform.localPosition .x),
            (o.localPosition .y - this.transform.localPosition .y),
            (o.localPosition .z - this.transform.localPosition .z));


            //vectorAgent to goal
            sensor.AddObservation(toO.normalized);//3

            // Distance from the goal (1 float)
            sensor.AddObservation(toO.magnitude);//1

            //Rotarion
            sensor.AddObservation(o.localRotation.x);//1
            sensor.AddObservation(o.localRotation.y);//1
            sensor.AddObservation(o.localRotation.z);//1
        }


        // Agent velocity (3 floats)
        sensor.AddObservation(rgBody.velocity);

    
    }

    //collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dead")
        {
            AddReward(-0.6f);
            looseVal++;
            EndEpisode();
        }
        if (collision.gameObject.tag == "prize")
        {
            AddReward(1);
            winVal++;
            EndEpisode();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "bonus")
        {
            AddReward(0.5f);
            
        }
        if (other.gameObject.tag == "prize")
        {
            AddReward(1);
            EndEpisode();
        }
    }
    //help

}
