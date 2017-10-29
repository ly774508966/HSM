using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerActor : MonoBehaviour {

    TestMiner2 test;
    bool isUpdate;
    void Start()
    {
        test = new TestMiner2();
        test.Go();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.U))
        {
            if (isUpdate) {
                isUpdate = false;
            }else if(!isUpdate) {
                isUpdate = true;
            }
        }
        if (isUpdate)
        {
            test.Update();
        }


    }
}
