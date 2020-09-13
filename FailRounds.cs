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
    class FailRounds : PassiveItem
    {
        public static void Init()
        {
            string itemName = "Fail Rounds";
            string resourceName = "AbominationItems/Resources/FailRounds";
            GameObject obj = new GameObject(itemName);
            var item = obj.AddComponent<FailRounds>();
            ItemBuilder.AddSpriteToObject(itemName, resourceName, obj);
            string shortDesc = "Where Can I Do Shoot!?";
            string longDesc = "Your bullets will shoot backwards, as well as deal damage to the owner.\n\nThese bullets failed all their shooting exams, and bought the diplom on gBay for 1 hegemony credit. They are still bad at everything through.";
            ItemBuilder.SetupItem(item, shortDesc, longDesc, "spapi");
            item.quality = PickupObject.ItemQuality.EXCLUDED;
        }

        public override void Pickup(PlayerController player)
        {
            base.Pickup(player);
            player.PostProcessProjectile += this.PostProcessProjectile;
        }

        private void PostProcessProjectile(Projectile proj, float f)
        {
            proj.baseData.speed = -proj.baseData.speed;
            proj.UpdateSpeed();
            proj.collidesWithPlayer = true;
            proj.allowSelfShooting = true;
            proj.SetNewShooter(this.m_owner.specRigidbody);
            proj.UpdateCollisionMask();
        }

        public override DebrisObject Drop(PlayerController player)
        {
            player.PostProcessProjectile -= this.PostProcessProjectile;
            return base.Drop(player);
        }
    }
}
