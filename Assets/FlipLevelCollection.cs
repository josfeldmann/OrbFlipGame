using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FlipLevelCollection")]
public class FlipLevelCollection : ScriptableObject {
    public List<FlipLevels> levels = new List<FlipLevels>();
}
