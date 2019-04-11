using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class MoverJob : MonoBehaviour
{
    public float speed = 4;
}


public class MoverJobSystem : ComponentSystem
{

    TransformAccessArray transAccessArray;
    NativeArray<float> nativeSpeeds;

    MyJob job;
    JobHandle jobHandle;

    protected override void OnStartRunning()
    {
        var comps = GetEntities<Components>();
        if (comps.Length == 0)
            return;

        var speeds = new float[comps.Length];
        var trs = new Transform[comps.Length];

        for (int i = 0; i < trs.Length; i++)
        {
            speeds[i] = comps[i].mover.speed;
            trs[i] = comps[i].tr;
        }
        
        transAccessArray = new TransformAccessArray(trs);
        nativeSpeeds = new NativeArray<float>(speeds, Allocator.TempJob);

        job = new MyJob
        {
            speeds = nativeSpeeds
        };
    }

    protected override void OnStopRunning()
    {
        if (transAccessArray.isCreated)
            transAccessArray.Dispose();

        if (nativeSpeeds.IsCreated)
            nativeSpeeds.Dispose();
    }
    protected override void OnUpdate()
    {
        if (!transAccessArray.isCreated || transAccessArray.length < 1)
            return;

        job.deltaTime = Time.deltaTime;
        job.dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        // schedule
        jobHandle = job.Schedule(transAccessArray);
        jobHandle.Complete();
    }

    struct Components
    {
        public MoverJob mover;
        public Transform tr;
    }

    struct MyJob : IJobParallelForTransform
    {
        public NativeArray<float> speeds;
        public Vector3 dir;
        public float deltaTime;

        public void Execute(int index, TransformAccess transform)
        {
            transform.localPosition += speeds[index] * dir * deltaTime;
        }
    }

}