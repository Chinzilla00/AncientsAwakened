using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Athena.Olympian
{
    public class GoddessHarp : BaseAAItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goddess Harp");
			Tooltip.SetDefault(@"Summons the seraph queen herself to fight with you
Athena is boosted by minion damage");
        }

	    public override void SetDefaults()
	    {
	        item.width = 20;
	        item.height = 26;
            item.value = Item.buyPrice(0, 15, 0, 0);
            item.rare = ItemRarityID.Purple;
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
			if (player.whoAmI == Main.myPlayer)
			{
                if (!hideVisual)
                {
                    if (player.FindBuffIndex(mod.BuffType("Athena")) == -1)
                    {
                        player.AddBuff(mod.BuffType("Athena"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[mod.ProjectileType("Athena")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("Athena"), (int)(100f * player.minionDamage), 2f, Main.myPlayer, 0f, 0f);
                    }
                }
			}
		}
	}
}
