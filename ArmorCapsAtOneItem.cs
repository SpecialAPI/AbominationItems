using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using ItemAPI;
using Gungeon;

namespace AbominationItems
{
    class ArmorCapsAtOneItem : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Broken Armor";
            string resourceName = "AbominationItems/Resources/BrokenArmor";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<ArmorCapsAtOneItem>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "So Sturdy!";
            string longDesc = "The armor of the bearer cannot go above 1.\n\nYour armor was made out of pure plastic and even through it (doesn't) looks fancy, it breaks easily.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "spapi");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        protected override void Update()
        {
            base.Update();
            if(this.m_pickedUp && this.m_owner != null)
            {
                if(this.m_owner.healthHaver != null && this.m_owner.healthHaver.Armor > 1)
                {
                    this.m_owner.healthHaver.Armor = 1;
                }
            }
        }
    }
}
