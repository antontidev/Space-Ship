using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastManager : MonoBehaviour
{
    [SerializeField]
    public Camera cam;

    private Mouse mouse;

    private void Start()
    {
        mouse = Mouse.current;
    }

    // Start is called before the first frame update
    public void OnClickHandler(InputAction.CallbackContext context)
    {
        RaycastHit raycastHit;

        Vector3 mousePos = new Vector3(mouse.position.x.clampConstant, mouse.position.y.clampConstant, 0);

        var ray = cam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out raycastHit, 100))
        {


        }


    }
}
