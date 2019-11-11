using Terraria;
using Terraria.ID;

namespace AAMod.Items.Pets
{
    public class PortaProbe : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			DisplayName.SetDefault("Porta-Probe");

			Tooltip.SetDefault("Take a little life-seeking robot with you!");
        }

		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.ShadowOrb);
			item.shoot = mod.ProjectileType("MiniProbe");
            item.buffType = mod.BuffType("MiniProbe");
		}

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0 && player.controlUseItem)
			{
				player.AddBuff(item.buffType, 90000, true);
            }
		}

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(BuffID.Spelunker, 2);
            player.AddBuff(BuffID.Spelunker, 2);
        }
    }
}