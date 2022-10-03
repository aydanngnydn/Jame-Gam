using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFly : MonoBehaviour
{
    public float alpha;
    public bool sw;
    public Color dudescolor;
    // Use this for initialization
    void Start () {
        //must have a transparent shader To work !!!
        GetComponent<Renderer>().material.shader=Shader.Find("Transparent/Diffuse");
        //lets remember the color we started with!!!!
        dudescolor =GetComponent<Renderer>().material.color;
 
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        
     
        if (sw){alpha+=Time.deltaTime;
            if(alpha>1){sw=!sw;}}
        if(!sw){alpha+=-Time.deltaTime;
            if(alpha<0){sw=!sw;}}
       
        dudescolor.a = alpha;
        GetComponent<Renderer>().material.color = dudescolor;
    }
}
