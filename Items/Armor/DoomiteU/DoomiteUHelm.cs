using Terraria;
using Terraria.ModLoader;
using Terraria.ID;


namespace AAMod.Items.Armor.DoomiteU
{
    [AutoloadEquip(EquipType.Head)]
	public class DoomiteUHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Doomite Helmet");
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
			player.setBonus = @"Increases max amount of minions by 1";
            player.maxMinions += 1;
        }
		
		public override void UpdateEquip(Player player)
		{
            player.minionDamage += 0.05f;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 2);
            recipe.AddIngredient(null, "DoomiteScrap", 6);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}