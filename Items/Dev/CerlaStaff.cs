using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    //imported from my tAPI mod because I'm lazy
    public class CerlaStaff : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("The Lifegiver's Gift ");
            Tooltip.SetDefault(@"Summons a Cerla Flower that fires petal bolts at enemies
Only one Cerla Flower may be active at a time
Takes up a Minion Slot");
        }

		public override void SetDefaults()
		{
			item.damage = 100;
			item.summon = true;
			item.mana = 20;
			item.width = 64;
			item.height = 64;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 20, 0, 0);
			item.rare = 8;
            item.expert = true; item.expertOnly = true;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("CerlaFlower");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("CerlaFlower");	//The buff added to player after used the item
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (player.ownedProjectileCounts[mod.ProjectileType("CerlaFlower")] > 0 || player.ownedProjectileCounts[mod.ProjectileType("CerlaFlowerEX")] > 0)
			{
				return false;
			}
			return true;
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}
		
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(145, 191, 255);
                }
            }
        }
    }
}
