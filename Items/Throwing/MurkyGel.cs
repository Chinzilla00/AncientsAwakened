using Terraria;
using Terraria.ID;

namespace AAMod.Items.Throwing
{
    public class MurkyGel : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 21;
			item.ranged = true;
			item.width = 20;
			item.height = 18;
			item.noUseGraphic = true;
			item.maxStack = 999;
			item.consumable = true;
			item.useTime = 20;
			item.useAnimation = 20;
			item.shoot = mod.ProjectileType("MurkyGelP");
			item.shootSpeed = 9f;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = Item.sellPrice(0, 0, 0, 25);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.noMelee = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Murky Gel");
			Tooltip.SetDefault("Inflicts Oiled debuff on hit");
		}
	}
}
