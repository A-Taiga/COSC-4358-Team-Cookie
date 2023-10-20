using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class TileCollisionEffect : MonoBehaviour
{
    // Define what kind of script methods can handle our tile collision events.
    [System.Serializable]
    public class CollisionEvent : UnityEvent<Collision2D> { }

    // Create a data structure to pair up a particular tile with a particular effect.
    [System.Serializable]
    public struct TileEffect {
        public TileBase tile;
        public CollisionEvent effect;
    }

    // Expose in the inspector a list of tile-effect mappings.
    public TileEffect[] effects;
    Dictionary<TileBase, CollisionEvent> _effectMap;

    // Pack our map of tile effects into a dictionary for ease of lookups.
    private void OnEnable() {
        if (_effectMap != null)
            return;

        _effectMap = new Dictionary<TileBase, CollisionEvent>(effects.Length);
        foreach (var entry in effects)
            _effectMap.Add(entry.tile, entry.effect);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        // If you know in advance what tilemap you're going to collide with,
        // you can cache this reference instead of searching for it with GetComponent.        
        var map = collision.collider.GetComponent<Tilemap>();
        var grid = map.layoutGrid;

        // Find the coordinates of the tile we hit.
        var contact = collision.GetContact(0);
        Vector3 contactPoint = contact.point - 0.05f * contact.normal;
        Vector3 gridPosition = grid.transform.InverseTransformPoint(contactPoint);
        Vector3Int cell = grid.LocalToCell(gridPosition);

        // Extract the tile asset at that location.
        var tile = map.GetTile(cell);

        if(tile == null)
            return; // No valid tile! Abort!

        // Check if we have an effect for this tile type. If so, fire it!
        if (_effectMap.TryGetValue(tile, out CollisionEvent effect) && effect != null)
            effect.Invoke(collision);
    }

    public void Conveyor(Collision2D collision) {
        // TODO: apply conveyor effect.
        Debug.Log("Conveyor effect!");
    }

    public void Bounce(Collision2D collision) {
        // TODO: apply bouncy effect.
        Debug.Log("Bounce effect!");
    }
}