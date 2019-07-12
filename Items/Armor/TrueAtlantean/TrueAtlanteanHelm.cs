using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueAtlantean
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueAtlanteanHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Triton Helmet");
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
			player.setBonus = @"While submerged in water, your magic abilities are increased drastically
You can swim and water does not affect your movement";
            if (player.wet && !player.lavaWet && !player.honeyWet)
            {
                player.AddBuff(mod.BuffType("AtlanteanBuff"), 2);
                player.accFlipper = true;
                player.ignoreWater = true;
            }
            else
            {
                player.accFlipper = false;
                player.ignoreWater = false;
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