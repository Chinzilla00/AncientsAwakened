using Terraria.ModLoader;

namespace AAMod.Items.Boss.EFish
{
	[AutoloadEquip(EquipType.Head)]
	public class EFishMask : ModItem
	{
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Emperor Fishron Mask");
		}
		
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 30;
			item.rare = 11;
			item.vanity = true;
		}

		public override bool DrawHead()
		{
			return false;
		}
	}
}