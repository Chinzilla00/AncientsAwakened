using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace AAMod.Items.Armor.DoomiteU
{
    [AutoloadEquip(EquipType.Head)]
	public class DoomiteUHelm : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Dark Doomite Helmet");
            Tooltip.SetDefault(@"Increases minion damage by 5%");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 0, 5, 0);
            item.rare = 3;
            item.defense = 3;
        }
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("DoomiteUPlate") && legs.type == mod.ItemType("DoomiteUGreaves");
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Lang.ArmorBonus("DoomiteUHelmBonus");
        }
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage += 0.05f;
			player.minionKB += 1f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DoomiteScrap", 6);
            recipe.AddIngredient(null, "Doomite", 2);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}