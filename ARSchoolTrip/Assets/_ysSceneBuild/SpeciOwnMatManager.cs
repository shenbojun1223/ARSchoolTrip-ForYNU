using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeciOwnMatManager : MonoBehaviour {

  
    [SerializeField]
    Material ownRawMat;

    [SerializeField]
    Material holoMat;

    Material[] mixMats= new Material[2];

    public void setMatAsOwn()
    {
        //双材质添加
        mixMats[0] = ownRawMat;
        mixMats[1] = holoMat;
        this.GetComponent<MeshRenderer>().materials = mixMats;

    }

	
}
