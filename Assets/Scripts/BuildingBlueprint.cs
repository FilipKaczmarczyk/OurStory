using UnityEngine;

public class BuildingBlueprint : MonoBehaviour
{
    [SerializeField] private GameObject building;
    [SerializeField] private BuildingBlueprintVisual blueprintVisual;

    private bool _canBuild = true;
    private Vector3 _blueprintColliderSize;
    
    private readonly Vector3 _pivotOffset = new Vector3(0, 0.5f, 0);

    private Color _canBuildColor = new Color(0, 1, 0, 0.5f);
    private Color _cantBuildColor = new Color(1, 0, 0, 0.5f);
    
    private readonly Collider[] _overlappingColliders = new Collider[1];
    
    private void Awake()
    {
        _blueprintColliderSize = GetComponent<BoxCollider>().size;
    }

    private void Update()
    {
        MoveToMouse();
        
        CheckCollision();

        if (Input.GetMouseButtonDown(0))
        {
            if (!_canBuild)
                return;
            
            Build();
            Destroy(gameObject);
        }
    }

    private void MoveToMouse()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            transform.position = hit.point;
        }
    }

    private void CheckCollision()
    {
        var size = Physics.OverlapBoxNonAlloc(transform.position + _pivotOffset, _blueprintColliderSize / 2f, 
            _overlappingColliders, Quaternion.identity, LayerMask.GetMask("Building")); 

        if (size == 0 || _overlappingColliders[0] == null)
        {
            if (!_canBuild)
            {
                UpdateBuildStatus(true);
            }
            
            return;
        }

        if (_canBuild)
        {
            UpdateBuildStatus(false);
        }
    }

    private void UpdateBuildStatus(bool canBuild)
    {
        _canBuild = canBuild;
        
        blueprintVisual.UpdateColor(_canBuild ? _canBuildColor : _cantBuildColor);
    }

    private void Build()
    {
        Instantiate(building, transform.position, transform.rotation);
    }
}
