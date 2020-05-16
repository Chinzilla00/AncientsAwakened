using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.Atlantean
{
    [AutoloadEquip(EquipType.Head)]
	public class AtlanteanHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Atlantean Helmet");
            Tooltip.SetDefault(@"Decreases mana usage by 15%
Allows to breath underwater");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = ItemRarityID.LightRed;
            item.defense = 6;
        }
		
		public override void UpdateEquip(Player player)
		{
			player.manaCost -= 0.15f;
            player.gills = true;
		}
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("AtlanteanPlate") && legs.type == mod.ItemType("AtlanteanGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAMod.Common.AtlanteanBonus");
			if (player.wet)
			{
				player.AddBuff(mod.BuffType("AtlanteanBuff"), 2);
			}
        }
		
		public override void AddRecipes()
        {
            ModRecipe recipe;
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanHelm"));
            recipe.AddIngredient(mod.ItemType("HydraHide"), 5);
            recipe.AddIngredient(null, "RelicBar", 5);
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanHelm"));
            recipe.AddIngredient(mod.ItemType("BroodScale"), 5);
            recipe.AddIngredient(ItemID.FossilOre, 5);
            recipe.AddIngredient(null, "Doomite", 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}