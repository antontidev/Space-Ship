using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

//For future purposes
public class StaticBatcherUtility : MonoBehaviour
{
    private ObservableCollection<GameObject> batchedObjects;

    void Start()
    {
        batchedObjects = new ObservableCollection<GameObject>();

        batchedObjects.CollectionChanged += BatchedObjects_CollectionChanged;
    }

    private void BatchedObjects_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        var rootObj = new GameObject();

        rootObj.name = "BatcherRoot";

        if (e.NewItems != null)
        {
            foreach (Transform child in e.NewItems)
            {

            }
        }
        if (e.OldItems != null)
        {

        }
    }

    public void AddToBatch(List<GameObject> batchList)
    {
        var diff = batchedObjects.Except(batchList);

        batchedObjects.Concat(diff);
    }
}
