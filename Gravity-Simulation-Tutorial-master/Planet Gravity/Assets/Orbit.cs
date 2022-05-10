using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
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

	public Transform pointPrefab;
	public float radius = 20000;  // радиус орбиты
	public int satNumber = 8;  // количество спутников
	public float A = 0.1f;  // A, B, C - координаты вектора-нормали к плоскости орбиты
	public float B = 0.1f;
	public float C = 0.1f;
	public float phase = 0;  // начальная фаза

	void Awake()
	{
		Vector3 scale = Vector3.one * 400f;  // размеры спутника в км (диаметр)
		Vector3 position;		

		for (float t = 0 + phase; t < 360 + phase; t += 360 / satNumber)
		{
			float x = radius / Sqrt(A * A + C * C) * (C * Cos(t) - A * B * Sin(t) / Sqrt(A * A + B * B + C * C));
			float y = radius * Sqrt(A * A + C * C) / Sqrt(A * A + B * B + C * C) * Sin(t);
			float z = - radius / Sqrt(A * A + C * C) * (A * Cos(t) + B * C * Sin(t) / Sqrt(A * A + B * B + C * C));

			// Создаем клон спутника
			Transform point = Instantiate(pointPrefab);
			position.x = x;
			position.y = y;
			position.z = z;
			point.localPosition = position;
			point.localScale = scale;

			// Задаём всем спутникам начальную орбитальную скорость
			float v = Sqrt(9.80665f / 1000 * 3600 * 20 * 20 * 6371 * 6371 / radius);  // вместо G*Mз пишу g*Rз*Rз чтобы работать не с большими числами
			Vector3 vect0 = point.transform.position;
			Vector3 vectA = UnityEngine.Vector3.Cross(vect0, Vector3.right);
			Vector3 vectB = UnityEngine.Vector3.Cross(vect0, Vector3.up);
			Vector3 vectC = UnityEngine.Vector3.Cross(vect0, Vector3.forward);
			Vector3 vectAA = UnityEngine.Vector3.Cross(vect0, Vector3.left);
			Vector3 vectBB = UnityEngine.Vector3.Cross(vect0, Vector3.down);
			Vector3 vectCC = UnityEngine.Vector3.Cross(vect0, Vector3.back);
			Vector3 vect90 = (vectA*A - vectAA*A + vectB*B - vectBB*B + vectC*C-vectCC*C).normalized * v;
			point.GetComponent<Rigidbody>().velocity = vect90;
		}
	}
}

