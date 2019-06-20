using Terraria;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class ThunderLordEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Lord");
            Tooltip.SetDefault(@"Fires off Thundershots
Static Rifle EX");
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType<Projectiles.ThunderBullet>(), damage, knockBack, Main.myPlayer, 0, 0);
            return false;
        }

        public override void SetDefaults()
        {
            item.damage = 250;
            item.noMelee = true;
            item.ranged = true; 
            item.width = 70; 
            item.height = 24;
            item.useTime = 30; 
            item.useAnimation = 30; 
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("ThunderBullet");
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
            item.UseSound = mod.GetLegacySoundSlot(Terraria.ModLoader.SoundType.Custom, "Sounds/Sounds/Thunderlord");
            item.autoReuse = false; 
            item.shootSpeed = 9f;
            item.useAmmo = AmmoID.Bullet;

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow";
			glowmaskDrawType = GLOWMASKTYPE_GUN;
			glowmaskDrawColor = AAColor.COLOR_WHITEFADE1;
			customNameColor = new Color(0, 0, 255);			
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ThunderLord", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}