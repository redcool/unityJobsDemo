using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TestIJobParallel : MonoBehaviour
{

    struct MyJob : IJobParallelFor
    {
        [ReadOnly]public NativeArray<float> a, b;
        public NativeArray<float> results;
        public void Execute(int index)
        {
            results[index] = a[index] + b[index];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var items1 = new NativeArray<float>(new float[] { 1, 2, 3 }, Allocator.TempJob);
        var items2 = new NativeArray<float>(new float[] { 4,5,6}, Allocator.TempJob);
        var results = new NativeArray<float>(3,Allocator.TempJob);
        var job = new MyJob
        {
            a = items1,
            b = items2,
            results = results
        };
        var h = job.Schedule(items1.Length,3);
        h.Complete();

        foreach (var item in results)
        {
            Debug.Log(item);
        }
        items1.Dispose();
        items2.Dispose();
        results.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
