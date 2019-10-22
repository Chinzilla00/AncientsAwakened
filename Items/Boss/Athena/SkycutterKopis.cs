using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Athena
{
    public class SkycutterKopis : BaseAAItem
	{
		public override void SetDefaults()
		{
            DisplayName.SetDefault("Skycutter Kopis");
			item.damage = 70;
			item.melee = true;
			item.width = 40;
			item.height = 50;
            item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 4;
			item.value = Item.sellPrice(gold: 1);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("Skyblade");
		}
	}
}
