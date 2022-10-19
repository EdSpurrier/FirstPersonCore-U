﻿using UnityEngine;
using System.Collections;

public class MaterialType : MonoBehaviour {

    public MaterialTypeEnum TypeOfMaterial = MaterialTypeEnum.Plaster;

    [System.Serializable]
	public enum MaterialTypeEnum
	{
        Plaster,
	    Metal,
        Folliage,
        Rock,
        Wood,
        Brick,
        Concrete,
        Dirt,
        Glass,
        Water
	}
}