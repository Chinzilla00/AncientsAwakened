using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class Voidsaber : BaseAAItem
	{
		public override void SetDefaults()
		{

			item.damage = 22;
			item.melee = true;
			item.width = 48;
			item.height = 48;
			item.useTime = 32;
			item.useAnimation = 32;
			item.useStyle = 1;
			item.knockBack = 3;
			item.value = 5400;
			item.rare = 2;
			item.UseSound = SoundID.Item15;
			item.autoReuse = true;
			item.useTurn = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Voidsaber");
			Tooltip.SetDefault("");
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 60);
		}
	}
}
