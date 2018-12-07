using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Ankh
{
    [AutoloadEquip(EquipType.Neck)]
    public class AnkhScarf : ModItem
	{
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AnkhShield);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.thrownVelocity += 0.085f;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ankh Scarf");
            Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
8.5% throwing velocity");
        }
	}
}
