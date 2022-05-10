using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour 
{
    // �������� ���������������: 20 �������� ����� � ������ ������� �������������
    readonly float M1_M2_G_km = 573968442240000f;  // M1 * M2 * G (20min, km); M �������� = 1000 ��
    GameObject[] celestials;

    float Cos(float angle)
    {
        var deg = angle / 180 * Mathf.PI;
        return (float)System.Math.Cos(deg);
    }

    float Sin(float angle)
    {
        var deg = angle / 180 * Mathf.PI;
        return (float)System.Math.Sin(deg);
    }

    void Start()
    {  
		celestials = GameObject.FindGameObjectsWithTag("Celestial");
    }

    private void FixedUpdate()
    {
        Gravity();
        // ������� �����
        transform.Rotate(0, -5.0137f * Time.deltaTime, 0);  // 5 �������� � �������
    }

	void Gravity()
    {
        foreach(GameObject a in celestials)
        {   
            // �������� ���� ��������� ���� ���������� �����
            float r = Vector3.Magnitude(a.transform.position);
            a.GetComponent<Rigidbody>().AddForce((- a.transform.position).normalized * M1_M2_G_km / (r * r));              
        }
    }
}
