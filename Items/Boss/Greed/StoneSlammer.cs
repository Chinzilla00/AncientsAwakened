using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed
{
    public class StoneSlammer : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Stone Slammer");
            Tooltip.SetDefault(@"Hitting an airborne always crits and sends the target flying into the ground");
        }
		public override void SetDefaults()
		{
			item.damage = 60;
			item.melee = true;
			item.width = 40;
			item.height = 42;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 1;
			item.knockBack = 14;
            item.value = Item.sellPrice(0, 3, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.scale *= 1.3f;
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (target.velocity.Y != 0)
            {
                crit = true;
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.velocity.Y != 0 && target.knockBackResist > 0)
            {
                target.velocity.Y += (knockBack * 1.5f) * target.knockBackResist;
                target.velocity.X = 0;
                int num = 4;
                for (int k = 0; k < 10; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(target.position, target.width, target.height, DustID.Dirt)];
                    Dust expr_16B_cp_0 = dust;
                    expr_16B_cp_0.velocity.Y -= (3f + num * 1.5f);
                    Dust expr_18D_cp_0 = dust;
                    expr_18D_cp_0.velocity.Y *= Main.rand.NextFloat();
                    dust.scale += num * 0.03f;
                }
                for (int l = 0; l < 10; l++)
                {
                    Dust dust2 = Main.dust[Dust.NewDust(target.position, target.width, target.height, DustID.Dirt)];
                    Dust expr_1EA_cp_0 = dust2;
                    expr_1EA_cp_0.velocity.Y -= (1f + num);
                    Dust expr_206_cp_0 = dust2;
                    expr_206_cp_0.velocity.Y *= Main.rand.NextFloat();
                }

                Vector2 bottom = target.Bottom;
                for (float num3 = 0f; num3 < 10; num3++)
                {
                    Dust dust3 = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Stone, 0f, 0f, 0, default, 1f);
                    dust3.alpha = 0;
                    dust3.position = bottom;
                    Dust expr_336_cp_0 = dust3;
                    expr_336_cp_0.velocity.Y -= 3f;
                    Dust expr_34E_cp_0 = dust3;
                    expr_34E_cp_0.velocity.X *= 0.5f;
                    dust3.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
                }
                for (float num4 = 0f; num4 < 10; num4++)
                {
                    Dust dust4 = Dust.NewDustDirect(target.position, target.width, target.height, DustID.Stone, 0f, 0f, 0, default, 1f);
                    dust4.position = bottom;
                    Dust expr_433_cp_0 = dust4;
                    expr_433_cp_0.velocity.Y -= 5f;
                    Dust expr_44B_cp_0 = dust4;
                    expr_44B_cp_0.velocity.X *= 0.8f;
                    dust4.fadeIn = 0.5f + Main.rand.NextFloat() * 0.5f;
                }
            }
        }
	}
}
