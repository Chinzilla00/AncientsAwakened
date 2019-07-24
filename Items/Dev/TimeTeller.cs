using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;
using BaseMod;

namespace AAMod.Items.Dev
{
    public class TimeTeller : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.useTime = 25;
            item.CloneDefaults(ItemID.Terrarian);

            item.damage = 200;
            item.value = 1000000;
            item.rare = 11;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shoot = mod.ProjectileType("TimeTeller");
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            mult *= ((ModSupportPlayer)player.GetModPlayer(mod, "ModSupportPlayer")).Thorium_radiantBoost;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 1000);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Teller");
            Tooltip.SetDefault("Damage changes based on time of day\n" +
				               "Damage is greatest at Midday and Midnight\n" +
                               "'Time to Die!'\n" +
                               "-Dallin");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(181, 38, 38);
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (player.accWatch < 3)
                player.accWatch = 3;
        }
		
		public float CalcDamageMultiplierFromTimeOfDay(int baseDamage)
		{
			int minDamage = baseDamage; //this is the damage you set in SetDefaults.
			int maxDamage = 350; //this is the damage you get at midday/midnight.

			float maxMultiplier = maxDamage / (float)minDamage;		
			float time = (int)Main.time;
			float calcTimeMax = 0f;
			if(Main.dayTime)
				calcTimeMax = 54000f; //max time in a day
			else
				calcTimeMax = 32400f; //max time in a night

			return BaseUtility.MultiLerp(time / calcTimeMax, 1f, maxMultiplier, 1f);
		}
    }
}