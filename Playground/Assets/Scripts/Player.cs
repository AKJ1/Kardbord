using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private float movementSpeed = 2f;
    private float projectileSpeed = 5f;
    public float xAxis, yAxis;
    public GameObject Projectile;
    
	// Use this for initialization
	void Start ()
	{
	    
	}
	
	// Update is called once per frame
	void Update () {
	    HandleInput();
        Move();
	}

    private void FirePrefab()
    {
        GameObject go = (GameObject)GameObject.Instantiate(Projectile, transform.position +  transform.forward, Quaternion.identity);
        StartCoroutine(PropogateProjectile(go, transform.forward));
    }

    IEnumerator PropogateProjectile(GameObject obj, Vector3 direction)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (obj != null)
            {
                if ((obj.transform.position - transform.position).magnitude > 100 || time > 15f)
                {
                    Destroy(obj);
                    yield break;
                }

                obj.transform.Translate(direction * projectileSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield break;
            }
      

        }
    }

    void Move()
    {
        
        Vector3 inputVector = new Vector3(xAxis, 0, yAxis);
        inputVector = transform.TransformVector(inputVector);
        transform.Translate((inputVector * movementSpeed) * Time.deltaTime);
    }

    void HandleInput()
    {
//        Debug.Log("X : " + xAxis + " Y : " + yAxis);

        this.xAxis = Input.GetAxis("Horizontal");

        this.yAxis = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Fire"))
        {
            FirePrefab();
        }
        
    }
}
