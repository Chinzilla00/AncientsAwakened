using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Melee
{
    public class BloodyMary : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloody Mary");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.melee = true;
			item.width = 46;
			item.height = 52;
			item.useTime = 19;
			item.useAnimation = 19;
			item.useStyle = 1;
			item.knockBack = 5;
			item.value = Item.buyPrice(gold: 1);
			item.rare = 2;
			item.UseSound = SoundID.Item1;
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(3) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, mod.DustType("BloodyDust"));
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Bleeding, 120);
		}
	}
}