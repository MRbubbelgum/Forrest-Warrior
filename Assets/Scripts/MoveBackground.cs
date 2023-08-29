using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {



	public float speed;
	private float x;
	public float PontoDeDestino;
	public float PontoOriginal;




	
	void Start () {
		
	}
	
	
	void Update () {


       if (Input.GetKey(KeyCode.A) && (Input.GetKey(KeyCode.D) == true))
        {
            x = transform.position.x;
            x += speed * 0;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.D) == true) 

        {
			x = transform.position.x;
			x += speed * Time.deltaTime;
			transform.position = new Vector3(x, transform.position.y, transform.position.z);



			if (x <= PontoDeDestino)
			{

				Debug.Log("hhhh");
				x = PontoOriginal;
				transform.position = new Vector3(x, transform.position.y, transform.position.z);
			}
		}
		else if (Input.GetKey(KeyCode.A) == true)
		{

            x = transform.position.x;
            x -= speed * Time.deltaTime;
            transform.position = new Vector3(x, transform.position.y, transform.position.z);



            if (x <= PontoDeDestino)
            {

                Debug.Log("hhhh");
                x = PontoOriginal;
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }
        }
		


    }
}
