using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour 
{
    // Скорость воспроизведения: 20 реальных минут в каждой секунде моделирования
    readonly float M1_M2_G_km = 573968442240000f;  // M1 * M2 * G (20min, km); M спутника = 1000 кг
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
        // Вращаем Землю
        transform.Rotate(0, -5.0137f * Time.deltaTime, 0);  // 5 градусов в секунду
    }

	void Gravity()
    {
        foreach(GameObject a in celestials)
        {   
            // Сообщаем всем спутникам Силу притяжения Земли
            float r = Vector3.Magnitude(a.transform.position);
            a.GetComponent<Rigidbody>().AddForce((- a.transform.position).normalized * M1_M2_G_km / (r * r));              
        }
    }
}
