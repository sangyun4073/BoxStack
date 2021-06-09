using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BoxPointer_collision : MonoBehaviour
{
    public BoxStack8_sy_20210608 agent_script;
    void Start()
    {
        agent_script = GameObject.Find("BoxAgent").GetComponent<BoxStack8_sy_20210608>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Pointer collide with wall, Set Reward -1");
            agent_script.AddReward(-1.0f);
            agent_script.EndEpisode();
        }

    }
}
