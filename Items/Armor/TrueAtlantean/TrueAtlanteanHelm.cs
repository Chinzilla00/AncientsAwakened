using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueAtlantean
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueAtlanteanHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("True Atlantean Helmet");
            Tooltip.SetDefault(@"Decreases mana usage by 25%
Allows to breath underwater");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.defense = 16;
        }
		
		public override void UpdateEquip(Player player)
		{
			player.manaCost -= 0.25f;
            player.gills = true;
		}
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("TrueAtlanteanPlate") && legs.type == mod.ItemType("TrueAtlanteanBoots");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = @"While submerged in liquids, player is getting special buff for 30 seconds";
			if (player.wet)
			{
				player.AddBuff(mod.BuffType("AtlanteanBuff"), 1800);
			}
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("AtlanteanHelm"));
			recipe.AddIngredient(null, "OceanCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}