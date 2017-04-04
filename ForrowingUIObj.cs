using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ForrowingUIObj : MonoBehaviour {

    public float sensitivity;

    List<float> sampleArray = new List<float>();

    Camera mainCamera;

    int count = 0;

    bool isRecentering = false;

    float yTargetAngle = 0.0f;

    float yAngleVelocity = 0.0f;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        //UIを正面に動かす
        if(isRecentering)
        {
            float y = Mathf.SmoothDampAngle(transform.eulerAngles.y, yTargetAngle, ref yAngleVelocity, 0.5f);

            transform.rotation = Quaternion.Euler(0F, y, 0F);
            if (Mathf.Abs(yAngleVelocity) < 0.1f)
            {
                isRecentering = false;
            }
        }

        //60フレーム分データがそろっているか
		if(sampleArray.Count >= 60)
        {
            //30フレーム静止していなくて、UIオブジェクトを注視していなくて、現状動いていなければ
            if(!isRecentering && !IsPlayerFocusingToUI() && IsPlayerStarting(sampleArray))
            {
                //30フレーム静止しているか
                if(count > 30)
                {
                    isRecentering = true;
                    yTargetAngle = mainCamera.transform.eulerAngles.y;
                }
                else
                {
                    //カウントを増やす
                    count++;
                }
            }
            else
            {
                //カウントを初期化
                count = 0;
            }

            //一番古いデータを取り除く
            sampleArray.RemoveAt(0);
        }

        sampleArray.Add(mainCamera.transform.eulerAngles.y);

        Debug.Log("count :" + sampleArray.Count);
	}

    bool IsPlayerFocusingToUI()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        bool isHit = Physics.Raycast(ray, 1500.0f);
        Debug.Log("IsHit : " + isHit);
        return isHit;
    }

    bool IsPlayerStarting(List<float> samples)
    {
        var average = samples.Average();
        var dispersion = samples.Select(sample => Mathf.Pow(sample - average, 2f)).Average();

        Debug.Log("dispersion: " + dispersion);
        return dispersion < sensitivity;
    }

}
