using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDestroyable
{
    /// <summary>
    /// Wrapper method, must call `Terminate()` at the end of its execution
    /// Any extra steps or processing before destroying the object is done here.
    /// </summary>
    //void DecreaseHealth(float factor);

    /// <summary>
    /// Wrapper method, must call `Terminate()` at the end of its execution
    /// Any extra steps or processing before destroying the object is done here.
    /// </summary>
    void Destroy();

    /// <summary>
    /// Internal method to call a `Destroy()` routine on the implementing
    /// object
    /// </summary>
    IEnumerator Terminate();

}
