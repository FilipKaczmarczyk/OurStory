using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Buildings
{
    public class Building : MonoBehaviour, ISelectable
    {
        [SerializeField] private List<Mesh> buildingPhases;
        [SerializeField] private int currentPhase;
        [SerializeField] private int buildPointsToComplete;
        [SerializeField] private float buildingDistance;
        
        [SerializeField] private Image selectionCircle;
        
        private MeshFilter _meshFilter;

        private int buildProgress;
        private bool _buildComplete;
        
        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            Upgrade();
        }

        public void Build()
        {
            buildProgress += 5;

            var buildCompletePercent = (float)buildProgress / buildPointsToComplete;
            var nextPhaseOfBuildingPercent = (float)(currentPhase + 1) / buildingPhases.Count;

            if (buildCompletePercent >= nextPhaseOfBuildingPercent)
            {
                Upgrade();
            }
        }

        [Button]
        private void Upgrade()
        {
            currentPhase++;
            _meshFilter.mesh = buildingPhases[currentPhase];
            
            if (currentPhase >= buildingPhases.Count - 1)
            {
                BuildComplete();
            }
        }

        private void BuildComplete()
        {
            Debug.Log("Build complete!");
            _buildComplete = true;
        }

        public bool GetBuildComplete()
        {
            return _buildComplete;
        }

        public Vector3 GetClosestPoint(Vector3 position)
        {
            var collider = GetComponent<Collider>();
            var closestPoint = collider.ClosestPoint(position);

            return closestPoint;
        }

        public float GetBuildingDistance()
        {
            return buildingDistance;
        }

        public void Select()
        {
            selectionCircle.enabled = true;
        }

        public void Deselect()
        {
            selectionCircle.enabled = false;
        }

        public string GetName()
        {
            return "Building";
        }
    }
}
