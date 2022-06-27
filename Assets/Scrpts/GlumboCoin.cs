using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlumboCoin : MonoBehaviour
{
    [SerializeField]float accel = 0.5f;
    [SerializeField]float decel = 0.1f;
    [SerializeField]float staticF = 5f;
    [SerializeField]float lerpSpd = 0.1f;
    [SerializeField] ParticleSystem particle;
    [SerializeField] Main main;
    [SerializeField] GameObject numberPopup;
    [SerializeField] float bounceLength;
    [SerializeField] float bounceSpeed;
    [SerializeField] float bounceStr;
    Smoothing bounceSmooth;
    Smoothing positionSmooth;
    Vector3 origin;
    Vector3 targetPos;
    [SerializeField] float moveSmoothTime;
    float bounceTime = 0f;
    float currentSpd = 0f;
    float rot = 0f;
    int maxGenerated = 1;
    void Start(){
        UpdateTransform();
        origin = transform.position;
    }

    void SetTargetPos(Vector3 newPos)
    {
        if (targetPos != newPos)
        {
            targetPos = newPos;
            positionSmooth = new Smoothing(0f, moveSmoothTime,Smoothing.smoothingTypes.InFastOutSlow);
        }
    }

    public void Spin(){
        currentSpd += accel;
        particle.Play();
        float coinsGenerated = Random.Range(1, maxGenerated+1);
        particle.emission.SetBurst(0, new ParticleSystem.Burst(0f, coinsGenerated));
        main.AddRemoveCoins(coinsGenerated);
        Instantiate(numberPopup, transform.position, Quaternion.identity).GetComponent<numberPopup>().SetNumValue(coinsGenerated);

        bounceSmooth = new Smoothing(0f, bounceLength, Smoothing.smoothingTypes.InFastOutSlow);
        
    }
    private void UpdateTransform(){
        transform.localRotation = Quaternion.Euler(0f,rot,0f);
    }
    void Update()
    {
        if(currentSpd>0){
            rot += currentSpd * Time.deltaTime;
            currentSpd -= currentSpd * decel * Time.deltaTime;
            rot%=360;
            UpdateTransform();
            if(currentSpd <= staticF){
                currentSpd = 0f;
            }
        }else{
            if(Mathf.Abs(Mathf.DeltaAngle(rot%=360, 0f))>1f){
                rot = Mathf.Lerp(rot, 360f,lerpSpd);
                UpdateTransform();
            }else{
                rot = 0f;
                UpdateTransform();
            }
        }

        if (bounceSmooth != null)
        {
            bounceTime += Time.deltaTime;
            float t = bounceSmooth.TickVal(Time.deltaTime);
            float bounceIntensity = 1f-t;
            SetTargetPos(origin + new Vector3(0f, Mathf.Sin(bounceSpeed * Mathf.Lerp(0,8*Mathf.PI,t)) * bounceIntensity * bounceStr, 0f));
            if (bounceTime > Mathf.PI * 2)
            {
                bounceTime -= Mathf.PI * 2;
            }
        }

        if (positionSmooth != null)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, positionSmooth.TickVal(Time.deltaTime));
        }
    }
}
