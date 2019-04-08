using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Armor.TrueDoomite
{
    [AutoloadEquip(EquipType.Head)]
	public class TrueDoomiteHelm : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("True Doomite Visor");
            Tooltip.SetDefault(@"30% increased minion damage");
        }

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 24;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 7;
            item.defense = 18;
        }
		
		public override void UpdateEquip(Player player)
        {
            player.minionDamage += 0.3f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("TrueDoomitePlate") && legs.type == mod.ItemType("TrueDoomiteBoots");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"+4 Minion slots
Void searchers fight by your side";
            player.maxMinions += 4;
            player.GetModPlayer<AAPlayer>(mod).doomite = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("Searcher")) == -1)
                {
                    player.AddBuff(mod.BuffType("Searcher"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("Searcher")] < 3)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("Searcher"), 55, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DoomiteVisor"));
			recipe.AddIngredient(null, "VoidCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}