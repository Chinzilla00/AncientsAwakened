using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class HydraMask1 : BaseAAItem
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
            item.rare = 2;
            item.vanity = true;
        }
    }
}