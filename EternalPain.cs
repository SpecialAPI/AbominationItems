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
    class EternalPain : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Eternal Pain";
            string resourceName = "AbominationItems/Resources/ETERNAL_SUFFERING";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<EternalPain>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "LET'S FUCKING GOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO";
            string longDesc = "Respawns the owner unlimited amount of times.\n\nYou are doomed to suffer the eternal pain.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "spapi");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            if (this.m_pickedUp)
            {
                return;
            }
            player.healthHaver.OnPreDeath += this.HandlePreDeath;
            base.Pickup(player);
        }

        private void HandlePreDeath(Vector2 damageDirection)
        {
            if (this.m_owner)
            {
                if (this.m_owner.IsInMinecart)
                {
                    this.m_owner.currentMineCart.EvacuateSpecificPlayer(this.m_owner, true);
                }
                for (int i = 0; i < this.m_owner.passiveItems.Count; i++)
                {
                    if (this.m_owner.passiveItems[i] is CompanionItem && this.m_owner.passiveItems[i].DisplayName == "Pig")
                    {
                        return;
                    }
                    if (this.m_owner.passiveItems[i] is ExtraLifeItem)
                    {
                        ExtraLifeItem extraLifeItem = this.m_owner.passiveItems[i] as ExtraLifeItem;
                        if (extraLifeItem.extraLifeMode == ExtraLifeItem.ExtraLifeMode.DARK_SOULS)
                        {
                            return;
                        }
                    }
                }
            }
            if (this.m_owner.IsInMinecart)
            {
                this.m_owner.currentMineCart.EvacuateSpecificPlayer(this.m_owner, true);
            }
            this.HandleCloneStyle();
        }

        public override DebrisObject Drop(PlayerController player)
        {
            DebrisObject debrisObject = base.Drop(player);
            player.healthHaver.OnPreDeath -= this.HandlePreDeath;
            debrisObject.GetComponent<EternalPain>().m_pickedUpThisRun = true;
            return debrisObject;
        }

        private void HandleCloneStyle()
        {
            this.m_owner.HandleCloneItem(null);
        }
    }
}
