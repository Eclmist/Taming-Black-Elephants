using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Office_1 : MonoBehaviour
{
    public void DoWork()
    {
        LevelTransition.LoadScene("BunnyOffice_Before_Lunch", 1);
    }

    public void DoWorkAgain()
    {
        LevelTransition.LoadScene("BunnyOffice_After_Work", 1);
    }

}
