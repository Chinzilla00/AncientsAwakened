using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class HydraMask2 : BaseAAItem
    {
        public static int type;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Hydra Mask");
		}

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.rare = ItemRarityID.Green;
            item.vanity = true;
        }
    }
}