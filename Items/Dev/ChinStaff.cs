using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
	//imported from my tAPI mod because I'm lazy
	public class ChinStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Deformed Chair Leg");
            Tooltip.SetDefault(@"Behold My True Power?");
        }

		public override void SetDefaults()
		{
			item.damage = 1;
			item.summon = true;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.noMelee = true;
			item.knockBack = 0;
			item.value = Item.buyPrice(0, 20, 0, 0);
			item.rare = 10;
			item.UseSound = SoundID.Item44;
			item.shoot = mod.ProjectileType("ChairMinion");
			item.shootSpeed = 7f;
			item.buffType = mod.BuffType("ChairMinionBuff");	//The buff added to player after used the item
            item.buffTime = 18000;
		}
        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 128, 64);
                }
                if (line2.mod == "Terraria" && line2.Name == "Damage")
                {
                    line2.text = "1 summon damage";
                }
            }
        }
        public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			return player.altFunctionUse != 2;
		}
		
		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				player.MinionNPCTargetAim();
			}
			return base.UseItem(player);
		}
    }
}
