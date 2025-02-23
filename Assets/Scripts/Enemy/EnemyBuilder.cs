using UnityEngine;
using UnityEngine.Splines;

using utilities;
public partial class EnemyBuilder
{
    GameObject enemyPrefab;
    SplineContainer spline;
    GameObject weaponPrefab;
    float speed;

    public EnemyBuilder SetBasePrefab(GameObject prefab)
    { 
        enemyPrefab = prefab;
        return this;
    }

    public EnemyBuilder SetSpline(SplineContainer spline) 
    {
        this.spline = spline;
        return this;
    }

    public EnemyBuilder SetWeaponPrefab(GameObject weaponPrefab)
    {
        this.weaponPrefab = weaponPrefab;
        return this;
    }

    public EnemyBuilder SetSpeed(float speed)
    {
        this.speed = speed;
        return this;
    }


    public GameObject BuildBase() 
    { 
        GameObject instace = GameObject.Instantiate(enemyPrefab);

        SplineAnimate splineAnimate = instace.GetOrAdd<SplineAnimate>();
        splineAnimate.Container = spline;
        splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.ZAxis;
        splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeYAxis;
        splineAnimate.MaxSpeed = speed;

        splineAnimate.Play();

        instace.transform.position = spline.EvaluatePosition(0f);



        return instace;
    }

    public GameObject BuildOnLine() 
    {
        GameObject instace = GameObject.Instantiate(enemyPrefab);

        SplineAnimate splineAnimate = instace.GetOrAdd<SplineAnimate>();
        splineAnimate.Container = spline;
        splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.ZAxis;
        splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeYAxis;
        splineAnimate.MaxSpeed = speed;
        splineAnimate.Loop = SplineAnimate.LoopMode.Loop;

        splineAnimate.Play();

        instace.transform.position = spline.EvaluatePosition(UnityEngine.Random.Range(0f,1f));



        return instace;
    }

    public GameObject BuildPingPong()
    {
        GameObject instace = GameObject.Instantiate(enemyPrefab);

        SplineAnimate splineAnimate = instace.GetOrAdd<SplineAnimate>();
        splineAnimate.Container = spline;
        splineAnimate.AnimationMethod = SplineAnimate.Method.Speed;
        splineAnimate.ObjectUpAxis = SplineComponent.AlignAxis.ZAxis;
        splineAnimate.ObjectForwardAxis = SplineComponent.AlignAxis.NegativeYAxis;
        splineAnimate.MaxSpeed = speed;
        splineAnimate.Loop = SplineAnimate.LoopMode.PingPong;

        splineAnimate.Play();

        instace.transform.position = spline.EvaluatePosition(UnityEngine.Random.Range(0f, 1f));



        return instace;
    }
}
