using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class DaybringerMask : ModItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Daybringer Mask");
		}

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 24;
            item.rare = 2;
            item.vanity = true;
        }
    }
}