using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionShaderEffect : MonoBehaviour
{
    // Start is called before the first frame update
    public Material m;
    private Vector2 ShearCentre;
    [Range(0,1)]
    public float ShearValue=0f;
    private bool is_Dream= false;
    void Start()
    {   
        ShearCentre= new Vector2(0.5f,0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(is_Dream==true){
            ShearValue = Mathf.Lerp(ShearValue,1.0f,Time.deltaTime);
        }
        //if(Input.GetKeyDown(KeyCode.T)){
          //  is_Dream=true;
        //}
    }
    void OnRenderImage(RenderTexture a, RenderTexture b){
        if (m!= null){
            m.SetFloat("_ShearValue", ShearValue);
            m.SetVector("_ShearCentre", ShearCentre);
            Graphics.Blit(a,b,m);
        }else{
            Graphics.Blit(a,b);
        }
    }

    public void Dreaming(){
        is_Dream=true;
    }

}
