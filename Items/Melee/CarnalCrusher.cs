using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class CarnalCrusher : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 260;
            item.melee = true;
            item.width = 90;
            item.height = 90;
            item.useTime = 45;
            item.useAnimation = 45;     
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 5;
            item.value = 200000;        
            item.rare = ItemRarityID.LightPurple;
            item.crit = 10;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Carnal Crusher");
            Tooltip.SetDefault("Critical Hits heal you");
        }
		
		public override void UseStyle(Player player)
        {
            player.itemLocation +=
                new Vector2(-8 * player.direction, 16 * player.gravDir).RotatedBy(player.itemRotation);
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
            float num = damage * 0.075f;
            if ((int)num == 0)
            {
                return;
            }
            if (Main.LocalPlayer.lifeSteal <= 0f)
            {
                return;
            }
            Main.LocalPlayer.lifeSteal -= num;
            int num2 = item.owner;
            if (crit)
            {
                Projectile.NewProjectile(target.position.X, target.position.Y, 0f, 0f, ProjectileID.VampireHeal, 0, 0f, item.owner, num2, num);
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
