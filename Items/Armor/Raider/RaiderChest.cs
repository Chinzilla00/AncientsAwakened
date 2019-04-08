using Terraria.Graphics.Shaders;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using Terraria.ID;

namespace AAMod.Items.Armor.Raider
{
    [AutoloadEquip(EquipType.Body)]
	public class RaiderChest : ModItem
	{
		public static int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raider Chestplate");
			Tooltip.SetDefault(@"Increases melee damage by 15%");

		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
			item.value = Item.sellPrice (0, 0, 5, 0);
			item.rare = 4;
			item.defense = 9;
		}
		
		public override void UpdateEquip(Player player)
		{
            player.meleeDamage += 0.15f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return head.type == mod.ItemType("RaiderHelm") && legs.type == mod.ItemType("RaiderLegs");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"You are immune to Chilled, Frozen and Frostburn debuffs
You quickly regenerate your HP while staying";
			player.buffImmune[44] = true;
			player.buffImmune[46] = true;
			player.buffImmune[47] = true;
			if (player.velocity.X == 0f && player.velocity.Y == 0f)
			{
				if (player.statLife < player.statLifeMax2)
				{
					if (counter >= 5)
					{
						counter = 0;
						player.statLife += 1;
						player.HealEffect(1, true);
					}
					counter++;
				}
			}
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("VikingPlate"));
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}