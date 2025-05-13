using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject conePrefab;
    [SerializeField] private float cloneDuration;
    [Space]
    [SerializeField] private bool canAttack;
    
    public void CreateClone(Transform _clonePosition)
    {
        GameObject newClone = Instantiate(conePrefab);
        
        newClone.GetComponent<Clone_Skill_Controller>().SetupClone(_clonePosition,cloneDuration,canAttack);
    }
}
