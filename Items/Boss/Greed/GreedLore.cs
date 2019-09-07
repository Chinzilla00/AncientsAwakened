using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class GreedLore : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greed");
			Tooltip.SetDefault(@"What is this..? Another worm like...him?
Impossible, they were all purged except for the Devourer because he escaped into a--
...hmm...what if he wasn't the only one with that ability..?");
		}

        public override void UpdateInventory(Player player)
        {
            if (ModLoader.GetMod("CalamityMod") == null)
            {
                item.TurnToAir();
            }
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (ModLoader.GetMod("CalamityMod") == null)
            {
                item.active = false;
            }
        }

        public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.rare = 7;
			item.consumable = false;
		}

		public override bool CanUseItem(Player player)
		{
			return false;
		}
	}
}
