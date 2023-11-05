using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class ShopMoney : MonoBehaviour ,IDetailedStoreListener
{
    IStoreController m_StoreController;
     
    public ConsumableItem[] cItems;
   


    private void Start()
    {
        SetupBuilder();
    }

    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(cItems[0].id, ProductType.Consumable);
        builder.AddProduct(cItems[1].id, ProductType.Consumable);
        builder.AddProduct(cItems[2].id, ProductType.Consumable);
        builder.AddProduct(cItems[3].id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
    }


    public void consumable0 ()
    {
        m_StoreController.InitiatePurchase(cItems[0].id);
    }
    public void consumable1()
    {
        m_StoreController.InitiatePurchase(cItems[1].id);
    }
    public void consumable2()
    {
        m_StoreController.InitiatePurchase(cItems[2].id);
    }
    public void consumable3()
    {
        m_StoreController.InitiatePurchase(cItems[3].id);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;
        print("Purchase Complete: " + product.definition.id);

        if(product.definition.id == cItems[0].id)
        {
            AddCoins(250);
        }
       else if (product.definition.id == cItems[1].id)
        {
            AddCoins(500);
        }
        else if (product.definition.id == cItems[2].id)
        {
            AddCoins(750);
        }
        else if (product.definition.id == cItems[3].id)
        {
            AddCoins(1000);
        }

        return PurchaseProcessingResult.Complete;
    }



    public void AddCoins(int amount)
    {
        PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + amount);

    }



    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("initialize Failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("initialize Failed: " + error + message);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed: ");
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        print("Purchase Failed: ");
    }
}
