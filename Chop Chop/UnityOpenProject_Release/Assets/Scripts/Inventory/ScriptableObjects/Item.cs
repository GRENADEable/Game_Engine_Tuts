﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
// Created with collaboration from:
// https://forum.unity.com/threads/inventory-system.980646/
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 51)]
public class Item : ScriptableObject
{
	[Tooltip("The name of the item")]
	[SerializeField]
	private LocalizedString _name;

	[Tooltip("A preview image for the item")]
	[SerializeField]
	private Sprite _previewImage;

	[Tooltip("A description of the item")]
	[SerializeField]
	private LocalizedString _description;


	[Tooltip("The type of item")]
	[SerializeField]
	private ItemType _itemType;

	[Tooltip("A prefab reference for the model of the item")]
	[SerializeField]
	private GameObject _prefab;

	[Tooltip("The list of the ingredients necessary to the recipe")]
	[SerializeField]
	private List<ItemStack> _ingredientsList = new List<ItemStack>();

	[Tooltip("The resulting dish to the recipe")]
	[SerializeField]
	private Item _resultingDish;



	public LocalizedString Name => _name;
	public Sprite PreviewImage => _previewImage;
	public LocalizedString Description => _description;
	public ItemType ItemType => _itemType;
	public GameObject Prefab => _prefab;
	public List<ItemStack> IngredientsList => _ingredientsList;
	public Item ResultingDish => _resultingDish;

}
