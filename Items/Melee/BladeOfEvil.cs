using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Melee
{
    public class BladeOfEvil : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 46;
            item.melee = true;
            item.width = 52;
            item.height = 52;
            item.useTime = 30;
            item.useAnimation = 30;     
            item.useStyle = 1;
            item.knockBack = 4;
            item.value = 10000;        
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
            item.shoot = ModContent.ProjectileType<Projectiles.EvilFlare>();
            item.shootSpeed = 9;
        }

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			if (Main.rand.NextBool(2))
			{
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 14);
				Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 117);
			}
		}
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Blade of Evil");
		  Tooltip.SetDefault(@"The perfect balance between Corruption and Crimson
Shoots alternating fireballs of Ichor and Cursed Flames");
		}

        int Shot = 0;

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Shot++;
            int proj = 0;
            if (Shot % 2 == 0)
            {
                proj = 1;
            }
            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, Main.myPlayer, proj);
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);      
			recipe.AddIngredient(ItemID.CrimtaneBar, 8);
            recipe.AddIngredient(ItemID.DemoniteBar, 8);
            recipe.AddIngredient(ItemID.Ichor, 10);
            recipe.AddIngredient(ItemID.CursedFlame, 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}
