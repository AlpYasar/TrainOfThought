using System;
using UnityEngine;
using NaughtyAttributes;
using PathCreation;
using PathEnds;

public class ForkController : PathEnd
{
    //[SerializeField, BoxGroup("Paths")] private PathCreator basePathCreator;
    [SerializeField, BoxGroup("Paths"), HorizontalLine(color: EColor.Blue)] private Path firstForkPath;
    [SerializeField, BoxGroup("Paths")] private Path secondForkPath;
    [SerializeField, ReadOnly] private bool isFirstFork = true;
    
    //Properties 
    //public PathCreator BasePathCreator => basePathCreator;
    public PathCreator AvailablePathCreator => isFirstFork ? firstForkPath.pathCreator : secondForkPath.pathCreator;
    public PathEnd PathEnd => isFirstFork ? firstForkPath.pathEnd : secondForkPath.pathEnd;
    
    [Button]
    public void ChangePath()
    {
        isFirstFork = !isFirstFork;
        firstForkPath.pathObjectForked.SetActive(isFirstFork);
        firstForkPath.pathObjectUnForked.SetActive(!isFirstFork);
        secondForkPath.pathObjectForked.SetActive(!isFirstFork);
        secondForkPath.pathObjectUnForked.SetActive(isFirstFork);
    }

    public override Type GetTypeOfObject()
    {
        return typeof(ForkController);
    }
}

[Serializable]
public class Path
{
    public PathCreator pathCreator;
    public GameObject pathObjectForked;
    public GameObject pathObjectUnForked;
    public PathEnd pathEnd;
}
