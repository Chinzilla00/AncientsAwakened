using Terraria.ModLoader;

namespace AAMod.Items.Vanity.Mask
{
    [AutoloadEquip(EquipType.Head)]
	public class FungusMask : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            DisplayName.SetDefault("Feudal Fungus Mask");
		}

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 26;
            item.rare = 2;
            item.vanity = true;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = false;
        }
    }
}