using UnityEngine;
using UnityEngine.AI;

namespace Cobble.Util {
    public class NavUtils {

        public static float GetPathLenght(NavMeshPath path) {
            var dist = 0f;
            var lastCorner = path.corners[0];
            for (var i = 1; i < path.corners.Length; i++) {
                var currentCorner = path.corners[i];
                dist += Vector3.Distance(lastCorner, currentCorner);
                lastCorner = currentCorner;
            }
            return dist;
        }
        
    }
}