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
    float currentSpd = 0f;
    float rot = 0f;
    int maxGenerated = 1;
    void Start(){
        UpdateTransform();
    }

    public void Spin(){
        currentSpd += accel;
        particle.Play();
        int coinsGenerated = Random.Range(1, maxGenerated+1);
        particle.emission.SetBurst(0, new ParticleSystem.Burst(0f, coinsGenerated));
        main.AddRemoveCoins(coinsGenerated);
        Instantiate(numberPopup, transform.position, Quaternion.identity).GetComponent<numberPopup>().SetNumValue(coinsGenerated.ToString());

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
    }
}
