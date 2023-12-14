using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineSurface : MonoBehaviour {

    public GameObject segment;
    public GameObject root;
    List<Transform> LineA;
    List<Transform> LineB;
    List<Transform> LineC;
    public bool liveUpdate = false;
    public int uDivs, vDivs;

    public int petalSegments = 5;
    public float petalLength = 1;
    public float petalCurl;

    public Vector4 petalBend;

    public Vector4 noiseInfo;
    float id;
    bool rebuild;
    public Color A = Color.black;
    public Color B = Color.white;

    void Start () {
        // Rebuild();
	}
	
	void Update () {
        if (liveUpdate)
        {
            Rebuild();
            Destroy(GetComponent<MeshFilter>().mesh);
            GetComponent<MeshFilter>().mesh = Grid.Generate(uDivs, vDivs, BezierSurface);
        }

	}

    public void Rebuild(){
        if(root!=null){
            Destroy(root);
        }
        LineA = new List<Transform>();
        LineB= new List<Transform>();
        LineC = new List<Transform>();
        root = Instantiate(segment);
        root.name = "Root";
        root.transform.localPosition = Vector3.zero;
        root.transform.localEulerAngles = Vector3.zero;
        root.transform.localScale = Vector3.one;
        id = Random.value*1e6f;
        BuildPetal(root.transform,0);
        SetupLines(root.transform,0);
        GetComponent<MeshFilter>().mesh = Grid.Generate(uDivs, vDivs, BezierSurface, BezierSurfaceColor);
        Destroy(root);
    }

    void BuildPetal(Transform t, int index){
        index++;
        GameObject joint = Instantiate(segment, t);
        joint.transform.localPosition = new Vector3(0,petalLength,0);
        joint.transform.GetChild(0).GetChild(0).localEulerAngles = new Vector3(0,Mathf.Min(petalBend.y,index*petalBend.x)*-1,0);
        joint.transform.GetChild(0).GetChild(1).localEulerAngles = new Vector3(0,Mathf.Min(petalBend.w,index*petalBend.z),0);
        joint.transform.localScale = Vector3.one;
        joint.transform.localEulerAngles = new Vector3(
            petalCurl + (Mathf.PerlinNoise((float)index*noiseInfo.x,id)-.5f)*noiseInfo.y,
            (Mathf.PerlinNoise((float)index*noiseInfo.z,id*10)-.5f)*noiseInfo.w,
            0);
        
        if(index<petalSegments){
            BuildPetal(joint.transform,index);
        }
    }

    void SetupLines(Transform t, int index){
        LineB.Add(t);
        LineA.Add(t.GetChild(0).GetChild(0).GetChild(0));
        LineC.Add(t.GetChild(0).GetChild(1).GetChild(0));
        index++;
        if(t.childCount>1 && index<1000){
            SetupLines(t.GetChild(1), index);
        }
    }

    Vector3 BezierSurface(float u, float v){

        return CatmullRomSpline.GetSplinePos(new Vector3[]{
        CatmullRomSpline.GetSplinePos(LineA.ToArray(), v),
        CatmullRomSpline.GetSplinePos(LineB.ToArray(), v),
        CatmullRomSpline.GetSplinePos(LineC.ToArray(), v)},u);
            
    }

    Color BezierSurfaceColor(float u, float v){
        return Color.Lerp(A,B,v);     
    }
}
