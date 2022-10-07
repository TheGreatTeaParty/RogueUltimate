using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Purchasing;
//using Firebase.Analytics;

public class PurchaseManager : MonoBehaviour,IStoreListener
{
    IStoreController m_StoreController;

    private const string baseId = "com.thegreatteaparty.roguestales.skin.";
    private ItemsDatabase itemsDatabase;
   
    void Start()
    {
        itemsDatabase = GetComponent<ItemsDatabase>();
        InitializePurchasing();

        RestoreVariable();
    }

    void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        for(int i = 2; i < itemsDatabase.GetSkinLength(); ++i)
        {
            builder.AddProduct(itemsDatabase.GetSkinPurchaseID(i), ProductType.NonConsumable);
        }
        UnityPurchasing.Initialize(this, builder);
    }

    void RestoreVariable()
    {
        itemsDatabase.UpdateSkinInfo();
    }

    public void BuyProduct(string productName)
    {
        m_StoreController.InitiatePurchase(productName);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        var product = args.purchasedProduct;

        itemsDatabase.UnlockSkinByID(int.Parse(product.definition.id.Replace(baseId,"")));
        itemsDatabase.OnSkinUnlocked?.Invoke();
       // FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventPurchase,new Parameter(FirebaseAnalytics.ParameterTransactionId, product.definition.id.Replace(baseId, "")));
        return PurchaseProcessingResult.Complete;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log($"In-App Purchasing initialize failed: {error}");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"Purchase failed - Product: '{product.definition.id}', PurchaseFailureReason: {failureReason}");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("In-App Purchasing successfully initialized");
        m_StoreController = controller;
    }
}