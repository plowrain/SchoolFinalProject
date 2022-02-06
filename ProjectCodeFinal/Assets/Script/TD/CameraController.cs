using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float shakeTime;
    public float _maxTime;
    Vector3 Pos;
    private void Start()
    {
        _maxTime = 0.2f;
        shakeTime = _maxTime;
        Pos = transform.position;
    }
    void Update()
    {
       if(shakeTime < _maxTime)
        {

            float x = Random.Range(Pos.x-0.3f, Pos.x+0.3f) * 0.4f;
            float y = Random.Range(Pos.y - 0.3f, Pos.y + 0.3f);
            transform.position = new Vector3(x, y, -10);
            shakeTime += Time.deltaTime;
        }
        else transform.position = new Vector3(Pos.x, Pos.y, Pos.z);

    }

    public void Shake(float _time)
    {
        shakeTime = _time;
    }
    public IEnumerator CameraShakeCa(float _maxTime, float _amount)
    {
        Vector3 originalPos = transform.localPosition;
        float shakeTime = 0.0f;
        while(shakeTime < _maxTime)
        {
            //Debug.Log(" transform.position = " + transform.position);
            float x = Random.Range(-1f, 1f) * _amount;
            float y = Random.Range(-1f, 1f) * _amount;
            transform.position = new Vector3(x, y, originalPos.z);
            shakeTime += Time.deltaTime;
        }
        yield return new WaitForSeconds(0f);
    }
}
