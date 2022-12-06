using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController_v2 : MonoBehaviour
{
    [SerializeField]
    private GameObject _ToggledObject;
    [SerializeField]
    private float doorSpeed=1f;
    [SerializeField]
    private float max_door_movement=2;

    private bool _isPressed=false;
    private float doorLimitPosition;
    private float initialDoorPosition;
    private bool _onTopPosition=false;

    // Start is called before the first frame update
    void Start()
    {
        doorLimitPosition=_ToggledObject.transform.position.y+max_door_movement;
        initialDoorPosition=_ToggledObject.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isPressed && _onTopPosition==false && _ToggledObject.transform.position.y<doorLimitPosition){
        _ToggledObject.transform.position= _ToggledObject.transform.position+new Vector3(0, doorSpeed * Time.deltaTime, 0);
        }
        if(_isPressed && _onTopPosition && _ToggledObject.transform.position.y>initialDoorPosition){
            _ToggledObject.transform.position= _ToggledObject.transform.position-new Vector3(0, doorSpeed * Time.deltaTime, 0);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is on top of the button
        if (collision.contacts[0].normal.y < 0 || collision.contacts[0].normal.z!=0)
        {
            //_ToggledObject.SetActive(false);
            _isPressed=true;
             var renderer = transform.GetComponent<Renderer>();
            renderer.material.color = Color.green;
        }
    }
     void OnCollisionExit(Collision collision)
    {
        _isPressed=false;
        if (_onTopPosition){
            _onTopPosition=false;
        }else{
            _onTopPosition=true;
        }
     }
}
