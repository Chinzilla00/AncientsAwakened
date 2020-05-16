using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class Earthbreaker : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            DisplayName.SetDefault("Earthbreaker");
            Tooltip.SetDefault(@"Hitting an airborne always crits and sends the target flying into the ground
Concussive force of the hit also has a 50% chance to confuse the struck enemy
If the enemy hits the ground after being hit, they will take damage");
        }
		public override void SetDefaults()
		{
			item.damage = 240;
			item.melee = true;
			item.width = 80;
			item.height = 90;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 20;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void ModifyHitNPC(Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
        {
            if (Main.rand.Next(2) == 0)
            {
                target.AddBuff(BuffID.Confused, 300);
            }
            if (target.velocity.Y != 0)
            {
                crit = true;
                if (target.knockBackResist > 0 || !target.boss)
                {
                    target.AddBuff(ModContent.BuffType<Buffs.Falling>(), 120);
                    target.GetGlobalNPC<Buffs.FallDamage>().damage = damage;
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            if (target.velocity.Y != 0)
            {
                if (target.knockBackResist > 0 || !target.boss)
                {
                    target.velocity.Y += knockBack * 1.5f * target.knockBackResist;
                    target.velocity.X = 0;
                }
                int num = 4;
                for (int k = 0; k < 10; k++)
                {
                    Dust dust = Main.dust[Dust.NewDust(target.position, target.width, target.height, DustID.Gold)];
                    Dust expr_16B_cp_0 = dust;
                    expr_16B_cp_0.velocity.Y -= 3f + num * 1.5f;
                    Dust expr_18D_cp_0 = dust;
                    expr_18D_cp_0.velocity.Y *= Main.rand.NextFloat();
                    dust.scale += num * 0.03f;
                }
                for (int l = 0; l < 10; l++)
                {
                    Dust dust2 = Main.dust[Dust.NewDust(target.position, target.width, target.height, DustID.Gold)];
                    Dust expr_1EA_cp_0 = dust2;
                    expr_1EA_cp_0.velocity.Y -= 1f + num;
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
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "StoneSlammer", 1);
            recipe.AddIngredient(null, "CovetiteBar", 10);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
