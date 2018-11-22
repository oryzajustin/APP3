using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour {
    private Vector2 look;
    private Vector2 smooth;
    public float sensitivity;
    public float smoothing;
    public bool frozen;

    public GameObject player;

    //for head bob while walking/running
    private float timer = 0.0f;
    private float bobbingSpeed = 0.30f;
    private float bobbingAmount = 0.1f;
    private float midpoint = 0f;
    private float waveslice = 0.0f;
    private float horizontal;
    private float vertical;

    void Start()
    {
        frozen = false;
    }

    // Update is called once per frame
    void Update () {
        if (!frozen)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            var dir = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            dir = Vector2.Scale(dir, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smooth.x = Mathf.Lerp(smooth.x, dir.x, 1.0f / smoothing);
            smooth.y = Mathf.Lerp(smooth.y, dir.y, 1.0f / smoothing);
            look += smooth;
            look.y = Mathf.Clamp(look.y, 40.0f * -1, 50.0f);

            transform.localRotation = Quaternion.AngleAxis(look.y * -1, Vector3.right);
            player.transform.localRotation = Quaternion.AngleAxis(look.x, player.transform.up);

            HeadBob();
        }
    }

    void HeadBob(){
        Vector3 pos = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer = timer + bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer = timer - (Mathf.PI * 2);
            }
        }
        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            pos.y = midpoint + translateChange;
        }
        else
        {
            pos.y = midpoint;
        }

        transform.localPosition = pos;
    }
    public void RIP(){
        bobbingSpeed = 0;
        bobbingAmount = 0;
        sensitivity = 0;
        smoothing = 0;
    }
}
