using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosVisor : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Chaos Visor");
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
            return body.type == mod.ItemType("ChaosDou") && legs.type == mod.ItemType("ChaosGreaves");
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"+4 Minion slots
A void scout hunts down your foes for you";
            player.maxMinions += 4;
            player.GetModPlayer<AAPlayer>(mod).doomite = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(mod.BuffType("ScoutMinion")) == -1)
                {
                    player.AddBuff(mod.BuffType("ScoutMinion"), 3600, true);
                }
                if (player.ownedProjectileCounts[mod.ProjectileType("ScoutMinion")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("ScoutMinion"), 55, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType("DoomiteVisor"));
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}