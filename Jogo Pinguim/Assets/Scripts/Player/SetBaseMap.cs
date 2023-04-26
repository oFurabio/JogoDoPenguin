using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBaseMap : StateMachineBehaviour
{
    [SerializeField] Material material;
    [SerializeField] Texture2D baseMap;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        material.SetTexture("_BaseMap", baseMap);
    }
}
