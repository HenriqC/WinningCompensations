using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class KinectDataManager : MonoBehaviour {

    private KinectData kinectData;

    public BodyWrapper therapist;
    public BodyWrapper patient;

	// Use this for initialization
	void Start () {
        kinectData = new KinectData();
        kinectData.Start();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey("escape"))
            Application.Quit();

        List<Body> activeBodies = kinectData.getActiveBodies();
        if (activeBodies.Count == 1) {
            patient.setBody(activeBodies[0]);
        }
        if (activeBodies.Count == 2) {
            for(int i = 0; i < activeBodies.Count; i++) {
                Body body = activeBodies[i];
                if (body.TrackingId != patient.getId()) {
                    therapist.setBody(body);
                }
            }
        }

        if (Time.frameCount % 30 == 0) {
            //System.GC.Collect();
        }
        //Debug.Log(kinectData.getActiveBodies().Count);
    }

    void OnApplicationQuit() {
        kinectData.Close();
        if (!Application.isEditor)
            System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}
