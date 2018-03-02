using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject pinPrefab;

    void Update()
    {
        bool bFired = false;

        // Check if we are running either in the Unity editor or in a standalone build.
#if UNITY_STANDALONE || UNITY_WEBPLAYER
       
        if (Input.GetButtonDown("Fire1"))
        {
            bFired = true;
        }

        // Check if we are running on iOS, Android, Windows Phone 8 or Unity iPhone
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

        if (Input.touchCount > 0)
        {
            Touch myTouch = Input.touches[0];

            if (myTouch.phase == TouchPhase.Began)
            {
                bFired = true;
            }
        }

#endif

        if (bFired)
        {
            SpawnPin();
        }
    }

    void SpawnPin()
    {
        Instantiate(pinPrefab, transform.position, transform.rotation);
    }

}
