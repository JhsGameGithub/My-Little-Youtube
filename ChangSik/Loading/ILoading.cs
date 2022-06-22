using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoading 
{
    // public CallbackEvent callback;

    public void SetAddTime(float add_time);

    public void SetCallback(CallbackEvent _callback);
}
