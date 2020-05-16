using Terraria.ModLoader;
using Terraria.ID;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class RajahMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Rajah Rabbit Mask");
		}

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 26;
            item.rare = ItemRarityID.Green;
            item.vanity = true;
        }
    }
}