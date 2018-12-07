using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Ankh
{
    public class AnkhNecklace : ModItem
	{
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AnkhShield);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().ammo20percentdown = true;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ankh Necklace");
            Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
20% chance not to consume ammo");
        }
	}
}
