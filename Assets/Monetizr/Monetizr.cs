﻿//
//  Monetizr.cs
//  Monetizr iOS integration script for Unity
//
//  Created by Ben Thornburg on 11/17/16.
//  Copyright © 2016 TheMonetizr. All rights reserved.
//

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Monetizr : MonoBehaviour {

	[Tooltip("If you are using Apple Pay, fill in your Merchant ID here. Otherwise, leave blank. See documentation for additional steps in Xcode.")]public string ApplePayMerchantID;
	[Tooltip("Your Channel ID, as provided by TheMonetizr")]public string ChannelID;
	[Tooltip("The Shop Domain, as provided by TheMonetizr")]public string ShopDomain;
	[Tooltip("Your API Key, as provided by TheMonetizr")]public string APIKey;

	string[] plistArray;

	/// <summary>
	/// Shows the purchase screen for the product with provided ID
	/// </summary>
	/// <param name="productID">Product ID</param>
	/// <param name="numPairs">Number of key/value pairs for settings dictionary</param>
	/// <param name="pKeyValues">Key/value pairs for settings dictionary</param>
	[DllImport ("__Internal")]
	private static extern void _ShowProductWithID(string productID, int numPairs, string[] pKeyValues);

	#region Singletonness
	// Sets up class to only allow one instance
	public static Monetizr Instance
	{
		get
		{
			return instance;
		}
	}

	private static Monetizr instance = null;

	void Awake ()
	{
		// Be sure this is the only instance of this GameObject
		if (instance)
		{
			DestroyImmediate(gameObject);
			return;
		}	

		instance = this;

		DontDestroyOnLoad(gameObject);
	}
	#endregion

	void Start()
	{
		// setup plist on game start
		plistArray = new string[] {"applePayMerchantId",ApplePayMerchantID,"channelId",ChannelID,"shopDomain",ShopDomain,"apiKey",APIKey};
	}

	// launches product purchase page from Unity
	public void ShowProductWithID(string productID)
	{
		if (!Application.isEditor)
		{
			#if UNITY_IOS
			// call native iOS function
			_ShowProductWithID(productID, plistArray.Length / 2, plistArray);
			#endif
		} else {
			// show fake message in editor
			Debug.Log("Showing Product ID " + productID + " (NOT SUPPORTED IN EDITOR)");
		}
	}
}
