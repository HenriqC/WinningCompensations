using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class KinectDataManager : MonoBehaviour {

    private KinectData kinectData;

    public BodyWrapper therapist;
    public BodyWrapper patient;
    public static bool leftArmSelected;
    public static double result;
    public struct _Joint
    {
        JointType JointType;
        CameraSpacePoint Position;
        TrackingState TrackingState;
    }
    public static bool isLeftArmSelected()
    {
        return leftArmSelected;
    }

    public static bool isRightArmSelected()
    {
        return !leftArmSelected;
    }

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
        //Debug.Log(kinectData.getActiveBodies().Count)

    }


    // Não está a funcionar.... ---------------------------------------------------- //
    public void OnBodyFrameArrived(object sender, BodyFrameArrivedEventArgs e)
    {
        Debug.Log("Entrou1");
        List<Body> activeBodies = kinectData.getActiveBodies();

        using (var frame = e.FrameReference.AcquireFrame())
        {
            Debug.Log("Entrou2");
            if (frame != null)
            {
                Debug.Log("Entrou3");
                frame.GetAndRefreshBodyData(activeBodies);

                var trackedBody = activeBodies.Where(b => b.IsTracked).FirstOrDefault();

                if (trackedBody != null)
                {
                    Debug.Log("Entrou4");
                    if (leftArmSelected)
                    {
                        List<Joint> myJoints = new List<Joint>();
                        myJoints.Add(trackedBody.Joints[JointType.HandLeft]);
                        myJoints.Add(trackedBody.Joints[JointType.ElbowLeft]);
                        myJoints.Add(trackedBody.Joints[JointType.ShoulderLeft]);

                        if (myJoints.TrueForAll(x => x.TrackingState == TrackingState.Tracked))
                        {
                            Vector3 ShoulderLeft = new Vector3(trackedBody.Joints[JointType.ShoulderLeft].Position.X, trackedBody.Joints[JointType.ShoulderLeft].Position.Y, trackedBody.Joints[JointType.ShoulderLeft].Position.Z);
                            Vector3 ElbowLeft = new Vector3(trackedBody.Joints[JointType.ElbowLeft].Position.X, trackedBody.Joints[JointType.ElbowLeft].Position.Y, trackedBody.Joints[JointType.ElbowLeft].Position.Z);
                            Vector3 HandLeft = new Vector3(trackedBody.Joints[JointType.HandLeft].Position.X, trackedBody.Joints[JointType.HandLeft].Position.Y, trackedBody.Joints[JointType.HandLeft].Position.Z);

                            Debug.LogWarning("#1: " + AngleBetweenTwoVectors(ElbowLeft - ShoulderLeft, ElbowLeft - HandLeft));
                        }
                    }
                    else if (!leftArmSelected)
                    {
                        Debug.Log("Entrou5");
                        List<Joint> myJoints = new List<Joint>();
                        myJoints.Add(trackedBody.Joints[JointType.HandRight]);
                        myJoints.Add(trackedBody.Joints[JointType.ElbowRight]);
                        myJoints.Add(trackedBody.Joints[JointType.ShoulderRight]);

                        if (myJoints.TrueForAll(x => x.TrackingState == TrackingState.Tracked))
                        {
                            Vector3 ShoulderRight = new Vector3(trackedBody.Joints[JointType.ShoulderRight].Position.X, trackedBody.Joints[JointType.ShoulderRight].Position.Y, trackedBody.Joints[JointType.ShoulderRight].Position.Z);
                            Vector3 ElbowRight = new Vector3(trackedBody.Joints[JointType.ElbowRight].Position.X, trackedBody.Joints[JointType.ElbowRight].Position.Y, trackedBody.Joints[JointType.ElbowRight].Position.Z);
                            Vector3 HandRight = new Vector3(trackedBody.Joints[JointType.HandRight].Position.X, trackedBody.Joints[JointType.HandRight].Position.Y, trackedBody.Joints[JointType.HandRight].Position.Z);

                            Debug.LogWarning("#1: " + AngleBetweenTwoVectors(ElbowRight - ShoulderRight, ElbowRight - HandRight));
                        }
                    }
                    
                }
                else
                {
                    this.OnTrackingIdLost(null, null);
                }
            }
        }
    }

    // Não está a funcionar.... ---------------------------------------------------- //

    private void OnTrackingIdLost(object p1, object p2)
    {
        throw new NotImplementedException();
    }

    public double AngleBetweenTwoVectors(Vector3 vectorA, Vector3 vectorB)
    {
        float dotProduct;
        vectorA.Normalize();
        vectorB.Normalize();
        dotProduct = Vector3.Dot(vectorA, vectorB);

        double result = Mathf.Acos(dotProduct) / Mathf.PI * 180;
        return result;
    }

    void OnApplicationQuit() {
        kinectData.Close();
        if (!Application.isEditor)
            System.Diagnostics.Process.GetCurrentProcess().Kill();
    }
}
