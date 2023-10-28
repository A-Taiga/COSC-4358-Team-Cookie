using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
   [SerializeField] 
   private Tilemap tilemap;

   [SerializeField]
   private List<TileData> tileDatas;

   private Dictionary<TileBase, TileData> dataFromTiles;
	private Vector3 playerPos;

	private TileBase tile;
	private playerMovement player;

	public AudioClip[] footstepSounds; // Array to hold footstep sound clips
    public float minTimeBetweenFootsteps = 0.3f; // Minimum time between footstep sounds
    public float maxTimeBetweenFootsteps = 0.6f; // Maximum time between footstep sounds

    private AudioSource audioSource; // Reference to the Audio Source component
    private float timeSinceLastFootstep; // Time since the last footstep sound

	private AudioClip footstepSound = null;

	protected void Awake()
	{
		audioSource = GetComponent<AudioSource>(); 
		player = GameObject.Find("Player").GetComponent<playerMovement>();
		dataFromTiles = new Dictionary<TileBase, TileData>();

		foreach(var tileData in tileDatas)
		{
			foreach(var tile in tileData.tiles)
			{
				dataFromTiles.Add(tile, tileData);
			}
		}
	}

	private void Update()
	{

		// print(GameObject.Find("Player").transform.position.x);

		playerPos = tilemap.WorldToCell(GameObject.Find("Player").transform.position);
		tile = tilemap.GetTile(Vector3Int.FloorToInt(playerPos));

		// print("Tile under player: " + tile.name);

		 if (player.getIsMoving())
        {
            // Check if enough time has passed to play the next footstep sound
            if (Time.time - timeSinceLastFootstep >= Random.Range(minTimeBetweenFootsteps, maxTimeBetweenFootsteps))
            {
				/* make sound based on current tile below player */
				switch(tile?.name)
				{
					case "TILE_3 inner corner_4":
						footstepSound = footstepSounds[0];
						break;
					case "TILE_3 inner corner_7":
					case "TILE_3 inner corner_1":
						footstepSound = footstepSounds[1];
						break;
					case "bridge_0":
					case "bridge_1":
					case "bridge_2":
					case "bridge_3":
					case "bridge_4":
					case "bridge_5":
						footstepSound = footstepSounds[2];
						break;
					default: 
						footstepSound = footstepSounds[0];
						break;
				}
                audioSource.PlayOneShot(footstepSound);

                timeSinceLastFootstep = Time.time; // Update the time since the last footstep sound
            }
        }

		// if(Input.GetMouseButtonDown(0))
		// {
		// 	Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		// 	Vector3Int gridPos = tilemap.WorldToCell(mousePos);
		// 	TileBase tile = tilemap.GetTile(gridPos);
		// 	print("At position" + gridPos + " there is a " + tile.name + "PLAYER POS: " + );
		// }
	}
}
