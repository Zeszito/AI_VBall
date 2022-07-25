using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class Agent_AI : Agent
{
    public Rigidbody rgBody;
    public float force;
 

    public GameObject win;
    public GameObject obsParent;
    public Transform[] obs;

    public Quaternion myInrot;
    public Vector3 myInPos;

    //para eu mexer
    public void Start()
    {
      
         obs = obsParent.GetComponentsInChildren<Transform>();
         myInrot = this.transform.rotation;
        myInPos = this.transform.position;


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
        float z = actions.ContinuousActions[0]* force;
   
        float x = actions.ContinuousActions[1] * force;



        
          this.rgBody.AddForce(new Vector3(x, 0, 0), ForceMode.Acceleration);
        
       
        

        this.rgBody.transform.Rotate(new Vector3(0, 0, z));
    }

    public override void OnEpisodeBegin()
    {
        this.transform.position = myInPos;
        this.transform.rotation = myInrot;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.transform.rotation.x);
     

        // Vector from agent to goal
        Vector3 toWin= new Vector3((win.transform.position.x - this.transform.position.x),
        (win.transform.position.y - this.transform.position.y),
        (win.transform.position.z - this.transform.position.z));


        //vectorAgent to goal (3)
        sensor.AddObservation(toWin.normalized);

        // Distance from the goal (1 float)
        sensor.AddObservation(toWin.magnitude);


        //obestacles
        foreach (Transform o in obs) //7 * 16
        {
            Vector3 toO = new Vector3((o.position.x - this.transform.position.x),
            (o.position.y - this.transform.position.y),
            (o.position.z - this.transform.position.z));


            //vectorAgent to goal
            sensor.AddObservation(toO.normalized);//3

            // Distance from the goal (1 float)
            sensor.AddObservation(toO.magnitude);//1

            //Rotarion
            sensor.AddObservation(o.rotation.x);//1
            sensor.AddObservation(o.rotation.y);//1
            sensor.AddObservation(o.rotation.z);//1
        }


        // Agent velocity (3 floats)
        sensor.AddObservation(rgBody.velocity);

    
    }

    //collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "dead")
        {
            AddReward(-1);
            EndEpisode();
        }
        if (collision.gameObject.tag == "win")
        {
            AddReward(1);
            EndEpisode();
        }
    }

    //help
 
}
