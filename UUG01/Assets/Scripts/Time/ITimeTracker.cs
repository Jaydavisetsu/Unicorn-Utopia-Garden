using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeTracker //Not sure I will need this anymore because I have to use TimeManager for the time.
{
    void ClockUpdate(GameTimestamp timestamp); // Function will be triggered every time TimeManager does an update. Any object that wants to listen in for any changes in the time state will use the interface. 
}
//Source: https://www.youtube.com/watch?v=jNbOXAGJbZ4&list=PLgXA5L5ma2Bu1sWc_-ZGRuPSUEAKPktHF&index=8
