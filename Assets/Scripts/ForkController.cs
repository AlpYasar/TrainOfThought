using System;
using DefaultNamespace;
using UnityEngine;
using NaughtyAttributes;
using PathCreation;

public class ForkController : LineEnd
{
    //[SerializeField, BoxGroup("Paths")] private PathCreator basePathCreator;
    [SerializeField, BoxGroup("Paths"), HorizontalLine(color: EColor.Blue)] private Path firstForkPath;
    [SerializeField, BoxGroup("Paths")] private Path secondForkPath;
    [SerializeField, ReadOnly] private bool isFirstFork = true;
    
    //Properties 
    //public PathCreator BasePathCreator => basePathCreator;
    public PathCreator AvailablePathCreator => isFirstFork ? firstForkPath.pathCreator : secondForkPath.pathCreator;
    
    [Button]
    public void ChangePath()
    {
        isFirstFork = !isFirstFork;
        firstForkPath.pathObjectForked.SetActive(isFirstFork);
        firstForkPath.pathObjectUnForked.SetActive(!isFirstFork);
        secondForkPath.pathObjectForked.SetActive(!isFirstFork);
        secondForkPath.pathObjectUnForked.SetActive(isFirstFork);
    }
    
}

[Serializable]
public class Path
{
    public PathCreator pathCreator;
    public GameObject pathObjectForked;
    public GameObject pathObjectUnForked;
}
