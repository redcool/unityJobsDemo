using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequireComponent(typeof(GameObjectEntity))]
public class Mover : MonoBehaviour
{
    public float speed = 4;
}

public class MoverSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var dt = Time.deltaTime;
        var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        foreach (var item in GetEntities<Components>())
        {
            Move(item, dir, dt);
        }
    }

    void Move(Components item,Vector3 dir,float deltaTime)
    {
        item.tr.Translate(dir * item.mover.speed * deltaTime);
    }

    struct Components
    {
        public Mover mover;
        public Transform tr;
    }
}
