using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : Defense {

	public float delayTimeToShoot_ = 2f;
	public float bulletSpeed_ = 2f;

	[SerializeField] private AutoSight sight_;
	[SerializeField] private GameObject bullet_;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (sight_.targetLocked_ && delayTimeToShoot_ <= 0f) {
			Shoot ();
		}
		if (delayTimeToShoot_ > 0)
			delayTimeToShoot_ -= Time.deltaTime;
	}

	public void Shoot()
	{
		GameObject newBullet = Instantiate (bullet_, this.transform.position, this.transform.rotation) as GameObject;
		newBullet.GetComponent<Bullet> ().Init (sight_.onSight.transform.position);
		/*Vector3 lookAtPos = new Vector3 (sight_.onSight.transform.position.x, sight_.onSight.transform.position.y, 0f);

		Vector3 diff = sight_.onSight.transform.position - newBullet.transform.position;
		diff.Normalize();

		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		newBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
	
		newBullet.GetComponent<Rigidbody2D> ().add*/
		delayTimeToShoot_ = 2f;
	}
}
