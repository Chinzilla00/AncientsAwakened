/*using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework; using Microsoft.Xna.Framework.Graphics; using Terraria.ModLoader;


namespace AAMod.Items.Armor.Valkyrie
{
    [AutoloadEquip(EquipType.Legs)]
	public class ValkyrieBoots : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Valkyrie Boots");
			Tooltip.SetDefault(@"20% increased movement speed
18% increased melee speed
50% increased throwing velocity
Enemies are more likely to target you
Hard as ice, light as snow");

		}

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(102, 204, 255);
                }
            }
        }

        public override void SetDefaults()
		{
			item.width = 22;
			item.height = 18;
			item.value = 3000000;
			item.rare = 11;
			item.defense = 25;
		}

		public override void UpdateEquip(Player player)
		{
			player.moveSpeed += 0.2f;
			player.meleeSpeed += 0.18f;
			player.thrownVelocity += 0.5f;
			player.aggro += 5;
            player.iceSkate = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LunarBar, 15);
			recipe.AddIngredient(ItemID.FrostCore, 2);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}*/