using UnityEngine;
using System.Collections;

/* Base code for all shot objects.  All shots disapppear when they move offscreen
 */
public class Shot : Entity 
{
	GameObject target;
	
	//Destroys the shot object if it moves off screen
	public void OffScreenBehavior()
	{
		if(GameObject.FindWithTag("Player"))
		{
			target = GameObject.FindWithTag("Player");
			if((int)Vector2.Distance(target.transform.position, transform.position) > 75)
				Clear();
		}
	}

	public virtual void Clear(){
		Destroy(gameObject);
	}
}
