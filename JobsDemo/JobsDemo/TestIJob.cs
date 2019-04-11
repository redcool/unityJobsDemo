using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class TestIJob : MonoBehaviour
{

    struct MyJob : IJob
    {
        [ReadOnly]public float a;
        [ReadOnly]public float b;
        public NativeArray<float> results;
        public void Execute()
        {
            results[0] = a + b;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        var results = new NativeArray<float>(1, Allocator.TempJob);
        var job = new MyJob
        {
            a = 1, b = 123,
            results = results
        };

        var h = job.Schedule();
        
        h.Complete();

        //job.Run();
        Debug.Log(results[0]);

        results.Dispose();
    }

}


