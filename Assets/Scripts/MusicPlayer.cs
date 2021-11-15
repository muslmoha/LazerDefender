using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        if(FindObjectsOfType(GetType()).Length > 1)//<> get a specific type, GetType gets the type of THIS class. I.E. MusicPlayer
            //therefore if there is anymore MusicPlayer components this finds them
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);//Logic: if there is already a MusicPlayer instance, destroy self, else don't on next load
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
