using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoverSystem : MonoBehaviour
{
    SimpleMover[] movers;
    public void OnStart()
    {
        movers = GetComponentsInChildren<SimpleMover>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movers == null || movers.Length == 0)
            return;

        var deltaTime = Time.deltaTime;
        var dir = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        foreach (var item in movers)
        {
            Move(item, dir, deltaTime);
        }
    }

    private void Move(SimpleMover mover,Vector3 dir,float deltaTime)
    {
        mover.transform.Translate(dir * deltaTime * mover.speed);
    }
}
