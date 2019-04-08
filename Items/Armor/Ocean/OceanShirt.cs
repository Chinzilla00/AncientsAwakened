using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Items.Armor.Ocean
{
    [AutoloadEquip(EquipType.Body)]
	public class OceanShirt : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ocean Chestplate");
			Tooltip.SetDefault(@"Increases magic damage by 10%
It feels so light");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = 3;
			item.defense = 4;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.magicDamage += 0.1f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("OceanHelm") && legs.type == mod.ItemType("OceanBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"20% increased magic damage while submerged in liquids
15% damage reduction while submerged in liquids";
			if (player.wet)
			{
				player.magicDamage += 0.2f;
				player.endurance += 0.15f;
			}
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Coral, 6);
			recipe.AddIngredient(ItemID.Starfish, 2);
			recipe.AddIngredient(ItemID.Seashell, 3);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}