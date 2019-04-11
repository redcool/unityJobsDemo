using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class TestIJobParallelForTransform : MonoBehaviour
{

    public float[] speeds;
    public Transform[] trs;

    NativeArray<float> nativeSpeeds;
    MyJob job;
    JobHandle jobHandle;
    TransformAccessArray transAccessArr;
    struct MyJob : IJobParallelForTransform
    {
        [ReadOnly]public NativeArray<float> speeds;
        [ReadOnly]public float deltaTime;
        [ReadOnly]public Vector3 dir;
        public void Execute(int index, TransformAccess transform)
        {
            transform.localPosition += dir * speeds[index] * deltaTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nativeSpeeds = new NativeArray<float>(speeds, Allocator.TempJob);
        transAccessArr = new TransformAccessArray(trs);

        job = new MyJob
        {
            deltaTime = Time.deltaTime,
            speeds = nativeSpeeds
        };
    }

    // Update is called once per frame
    void Update()
    {
        jobHandle.Complete();

        job.dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        jobHandle = job.Schedule(transAccessArr);
    }

    private void OnDestroy()
    {
        if(transAccessArr.isCreated)
            transAccessArr.Dispose();
        if(nativeSpeeds.IsCreated)
            nativeSpeeds.Dispose();
    }
}
