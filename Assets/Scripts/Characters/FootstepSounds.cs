﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSounds : MonoBehaviour
{
    public TextureType[] textureTypes;

    public AudioSource audioS;

    SoundController sc;
    void Start()
    {
        GameObject check = GameObject.FindGameObjectWithTag("Sound Controller");

        if (check != null)
        {
            sc = check.GetComponent<SoundController>();
        }
    }

	void PlayFootstepSound()
	{
		RaycastHit hit;
		Vector3 start = transform.position + transform.up;
		Vector3 dir = Vector3.down;

		if (Physics.Raycast(start, dir, out hit, 1.3f))
		{
			if (hit.collider.GetComponent<MeshRenderer>())
			{
				PlayMeshSound(hit.collider.GetComponent<MeshRenderer>());
			}
			/*else if (hit.collider.GetComponent<Terrain>())
			{
				PlayTerrainSound(hit.collider.GetComponent<Terrain>(), hit.point);
			}*/
		}
	}

	void PlayMeshSound(MeshRenderer renderer)
	{

		if (audioS == null)
		{
			Debug.LogError("PlayMeshSound -- We have no audio source to play the sound from.");
			return;
		}

		if (sc == null)
		{
			Debug.LogError("PlayMeshSound -- No sound manager!!!");
			return;
		}

		if (textureTypes.Length > 0)
		{
			foreach (TextureType type in textureTypes)
			{

				if (type.footstepSounds.Length == 0)
				{
					return;
				}

				foreach (Texture tex in type.textures)
				{
					if (renderer.material.mainTexture == tex)
					{
						sc.PlaySound(audioS, type.footstepSounds[Random.Range(0, type.footstepSounds.Length)], true, 1, 1.2f);
					}
				}
			}
		}
	}
}
[System.Serializable]
public class TextureType
{
    public string name;
    public Texture[] textures;
    public AudioClip[] footstepSounds;
}
