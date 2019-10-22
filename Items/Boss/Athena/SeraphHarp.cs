using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Athena
{
    public class SeraphHarp : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Seraph Harp");
			Tooltip.SetDefault("Summons a seraph to fight for you");
		}

	    public override void SetDefaults()
	    {
	        item.width = 20;
	        item.height = 26;
            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = 8;
	        item.accessory = true;
            item.expert = true;
            item.expertOnly = true;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (modPlayer.Seraph)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.Athena = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(mod.BuffType("Seraph")) == -1)
				{
					player.AddBuff(mod.BuffType("Seraph"), 3600, true);
				}
				if (player.ownedProjectileCounts[mod.ProjectileType("Seraph")] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("Seraph"), (int)(100f * player.minionDamage), 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}
	}
}
