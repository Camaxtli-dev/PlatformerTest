using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierAIController : CachedComponents {
    [SerializeField] private Transform WaypointRoot;
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private GameObject target;
    private List<Waypoint> waypoints = new List<Waypoint>();
    private Waypoint targetWaypoint;
    private int numWaypoint;

    public event Action<Vector2> Move = delegate { };
    public event Action Jump = delegate { };
    public event Action Fire = delegate { };
    public event Action ChangeWeapons = delegate { };
    public event Action<GameObject> OnTarget = delegate { };
    
    void Start() {
        foreach (Transform wayp in WaypointRoot) {
            waypoints.Add(wayp.GetComponent<Waypoint>());
        }
        ChangeWeapons();
        NextWaypoint();
        StartCoroutine("FindObjectInFieldView");
    }

    private void FixedUpdate() {
        if (target.tag == "Player") {
            // Бежать за целью и стрелять
            Fire();
        }
        else if ((transform.position.x - targetWaypoint.transform.position.x) > 0) {
            Move(Vector2.right * -1);
        } else {
            Move(Vector2.right);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Waypoint" && collision.GetComponent<Waypoint>().Id == targetWaypoint.Id) {
            NextWaypoint();
        }
    }

    private void NextWaypoint() {
        if (numWaypoint >= waypoints.Count) {
            numWaypoint = 0;
        }
        targetWaypoint = waypoints[numWaypoint++];
        if (target == null || target.tag != "Player") {
            target = targetWaypoint.gameObject;
            OnTarget(targetWaypoint.gameObject);
        }
    }

    private IEnumerator FindObjectInFieldView() {
        Collider2D[] hitColliders2D = Physics2D.OverlapAreaAll(pointA.position, pointB.position, LayerMask.GetMask("Player"));
        if (hitColliders2D.Length > 0) {
            Debug.Log("HIT");
            target = hitColliders2D[0].gameObject;
            OnTarget(target);
        } else if(target.tag != "Waypoint") {
            target = null;
            NextWaypoint();
            OnTarget(target);
        }
        yield return new WaitForSeconds(0.2f);
        StartCoroutine("FindObjectInFieldView");
    }
}
