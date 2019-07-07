using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Ranged
{
    public class DoomiteAssaultBlaster : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useStyle = 5;
            item.useAnimation = 19;
            item.useTime = 19;
            item.width = 52;
            item.height = 20;
            item.UseSound = SoundID.Item12;
            item.knockBack = 2;
            item.damage = 30;
            item.shootSpeed = 9f;
            item.noMelee = true;
            item.rare = 3;
            item.autoReuse = true;
            item.ranged = true;
            item.value = 20000;
            item.shoot = mod.ProjectileType<Projectiles.DoomiteVortex>();
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Doomite Assault Blaster");
        }
		
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Doomite", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
