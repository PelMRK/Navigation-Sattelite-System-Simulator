using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    int sat_amount = 0;
    int det_amount = 0;

    int tot_sum_nums = 0;
    int sum_det_amnt = 0;

    int Summ(List<int> nums)
    {
        int summ = 0;
        foreach (int el in nums)
        {
            summ += el;
        }
        return summ;
    }

    float Sin(float angle)
    {
        var deg = angle / 180 * Mathf.PI;
        return (float)System.Math.Sin(deg);
    }

    float Cos(float angle)
    {
        var deg = angle / 180 * Mathf.PI;
        return (float)System.Math.Cos(deg);
    }

    float Sqrt(float number)
    {
        return (float)System.Math.Sqrt(number);
    }

    public Transform objEarth;  // для объекта-Земли
    public Transform pointPrefab;  // для объекта-детектора
    public float radius = 6371f;  // радиус расположения детекторов
    public int detNumber = 6;  // количество детекторов
    public float size = 200f;  // размер детектора

    void Awake()
    {
        Vector3 scale = Vector3.one * size;
        Vector3 position;
        // Равномерное расположение детекторов по сфере Земли по решётке Фибоначчи
        for (float i = 0f; i < (float)detNumber; i += 1f)
        {
            float goldenRatio = (1f + Sqrt(5f)) / 2f;

            float phi = (float)System.Math.Acos(1f - 2f * (i + 0.5f) / (float)detNumber);
            float theta = 2 * Mathf.PI * i * goldenRatio;

            // Создаем клон детектора
            Transform point = Instantiate(pointPrefab);            
            position.x = radius * (float)System.Math.Cos(theta) * (float)System.Math.Sin(phi);
            position.y = radius * (float)System.Math.Sin(theta) * (float)System.Math.Sin(phi);
            position.z = radius * (float)System.Math.Cos(phi);
            point.localPosition = position;
            point.localScale = scale;
            point.transform.parent = objEarth.transform;
        }
    }

    void Start()
    {
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Celestial"))
        {
            sat_amount += 1;  // Получаем общее количество спутников
        }
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Detectors"))
        {
            det_amount += 1;  // Получаем общее количество детекторов
        }
    }

    void FixedUpdate()
    {
        var nums = new List<int>();  // Список количеств видимых спутников с каждого детектора
        foreach (GameObject a in GameObject.FindGameObjectsWithTag("Detectors"))
        {
            int num = a.GetComponent<Detecting>().number;  // Получаем число видимых спутников от каждого детектора 
            nums.Add(num); 
        }

        float avr = (float)Summ(nums) / nums.Count;  // Среднее количество видимых спутников в данный момент времени
        if (avr > 1) 
        {
            tot_sum_nums += Summ(nums);
            sum_det_amnt += det_amount;
            float general_avr = (float)tot_sum_nums / (float)sum_det_amnt; // Среднее количество видимых спутников в среднем за всё время 
            float efficiency = general_avr / sat_amount * 100;  // Показатель эффективности системы. Чем больше, тем лучше
            Debug.Log(string.Join(", ", nums));  
            print(general_avr + " - среднее количество видимых спутников за всё время");
            print(efficiency + "% - часть спутников, всегда видимых с Земли");
        }
    }
}
