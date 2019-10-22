using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Athena
{
    public class Olympia : BaseAAItem
	{
		public override void SetDefaults()
		{
			item.damage = 150;
			item.melee = true;
			item.width = 52;
			item.height = 52;
            item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 7;
			item.value = Item.sellPrice(gold: 10);
			item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType("Skyrazor");
		}
	}
}
