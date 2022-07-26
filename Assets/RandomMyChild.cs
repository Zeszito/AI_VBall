using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMyChild : MonoBehaviour
{
    // Start is called before the first frame update
   public List<Transform> myChilds;

    void Start()
    {
        myChilds.AddRange(gameObject.GetComponentsInChildren<Transform>());
    }

    public void RandMyChild()
    {
        int stRage = -60;
        int finalrang = 15;
        foreach (Transform t in myChilds)
        {
            t.gameObject.SetActive(true);
        }

            foreach (Transform t in myChilds)
        {
            foreach (Transform f in myChilds)
            {
                if (t.gameObject.tag == "dead" && f.gameObject.tag == "dead" && t.gameObject != this.gameObject && f.gameObject != this.gameObject && t.gameObject != f.gameObject)
                {
                    float po = Random.Range(stRage, finalrang);
                
                    t.localPosition = new Vector3(po , t.localPosition.y, t.localPosition.z);
                    
                    if (t.GetComponent<BoxCollider>().bounds.Intersects((f.GetComponent<BoxCollider>().bounds)) && t.gameObject != f.gameObject){
                        f.gameObject.SetActive(false);
                    }
                    if ( Mathf.Abs(f.localPosition.x- t.localPosition.x) < 1 && t.gameObject != f.gameObject && t.gameObject != this.gameObject && f.gameObject != this.gameObject)
                    {
                        f.gameObject.SetActive(false);
                    }


                }
                
                
            }
        }

  

    }

    public void RandDeact()
    {
        foreach (Transform t in myChilds)
        {
            t.gameObject.SetActive(true);
        }

        foreach (Transform t in myChilds)
        {
            int Rnd = Random.Range(1, 6);
            if(Rnd == 1 && t.gameObject.tag == "dead")
            {
                t.gameObject.SetActive(false);
            }
            
        }
        foreach (Transform t in myChilds)
        {
            int Rnd = Random.Range(1, 6);
            if (Rnd == 1 && t.gameObject.tag == "bonus")
            {
                t.gameObject.SetActive(false);
            }

        }
    }
}
