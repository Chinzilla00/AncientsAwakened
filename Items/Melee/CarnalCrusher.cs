using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class CarnalCrusher : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 110;
            item.melee = true;
            item.width = 100;
            item.height = 98;
            item.useTime = 45;
            item.useAnimation = 45;     
            item.useStyle = 1;
            item.knockBack = 5;
            item.value = 200000;        
            item.rare = 6;
            item.crit = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carnal Crusher");
            Tooltip.SetDefault("Critical Hits heal you");
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.Next(2) == 0)
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Blood);
			}
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            if (target.type == NPCID.TargetDummy)
            {
                return;
            }
            float num = (float)damage * 0.075f;
            if ((int)num == 0)
            {
                return;
            }
            if (Main.player[Main.myPlayer].lifeSteal <= 0f)
            {
                return;
            }
            Main.player[Main.myPlayer].lifeSteal -= num;
            int num2 = item.owner;
            if (crit)
            {
                Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, item.owner, (float)num2, num);
            }
        }
		
        public override void AddRecipes()  //How to craft this sword
        {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "FleshrendClaymore");
			recipe.AddIngredient(ItemID.LunarTabletFragment, 30);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
