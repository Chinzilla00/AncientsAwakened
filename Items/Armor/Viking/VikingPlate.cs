using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Items.Armor.Viking
{
    [AutoloadEquip(EquipType.Body)]
	public class VikingPlate : ModItem
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Viking Platemail");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = 3;
			item.defense = 7;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.meleeDamage += 0.07f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("VikingHelm") && legs.type == mod.ItemType("VikingBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"4% Increased damage resistance";
            player.endurance += .04f;
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("RelicBar", 14);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}