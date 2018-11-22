using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
	public class Bloody_Mary : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloody Mary");
			Tooltip.SetDefault("It's just folklore.");
		}
		
		public override void SetDefaults()
		{
			item.damage = 18;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = Item.buyPrice(0, 1, 50, 0);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Bleeding, 120);
		}
	}
}
