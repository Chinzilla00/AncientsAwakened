using Terraria;
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
            item.rare = 4;
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
			player.setBonus = @"While submerged in liquids, your magic abilities and defense are enhanced";
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
            recipe.AddIngredient(mod.ItemType("DepthFukumen"));
            recipe.AddIngredient(mod.ItemType("VikingHelm"));
            recipe.AddIngredient(mod.ItemType("DoomiteUHelm"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod.ItemType("OceanHelm"));
            recipe.AddIngredient(mod.ItemType("KindledKabuto"));
            recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(mod.ItemType("DoomiteUHelm"));
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
	}
}