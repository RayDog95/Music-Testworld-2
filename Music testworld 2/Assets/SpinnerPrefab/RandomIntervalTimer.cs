
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;

public class RandomIntervalTimer : UdonSharpBehaviour
{
    
    bool timerRunning = false;

    float randomTime;

    float timer = 0f;

    [Tooltip("minimum and maximum time in seconds ")]
    public float min, max;

    [Tooltip("object with this component")]
    public ConstantForce ConstantForce;

    [Tooltip("The object that is spinning from the constant force")]
    public GameObject Spinner;

    private VRCPlayerApi _localplayer;

    public void Start()
    {
        _localplayer = Networking.LocalPlayer;
    }

    public override void Interact()
    {
        Networking.SetOwner(_localplayer, Spinner);
        randomTime = Random.Range(min, max);
        timer = 0f;
        timerRunning = true;
    }

    private void Update()
    {
        if (!timerRunning) return;

        timer += Time.deltaTime;

        if(timer >= randomTime) timerRunning = false;
        
        if (timerRunning)
            SendCustomEvent("Spin");
        if (timerRunning == false)
        {
            if(ConstantForce.enabled == true)
            {
                ConstantForce.enabled = false;
            }
        }

       
    }

    public void Spin()
    {
        ConstantForce.enabled = true;
    }
}
