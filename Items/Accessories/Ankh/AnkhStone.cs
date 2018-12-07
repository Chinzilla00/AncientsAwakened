using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories.Ankh
{
    public class AnkhStone : ModItem
	{
        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.AnkhShield);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.manaCost -= 0.1f;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ankh Stone");
            Tooltip.SetDefault(@"Grants immunity to knockback and fire blocks
Grants immunity to most debuffs
10% reduced mana usage");
        }
	}
}
