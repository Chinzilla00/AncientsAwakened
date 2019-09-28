using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class ThunderLordEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thunder Lord");
            Tooltip.SetDefault(@"Fires off Thundershots and has a rare chance to shoot a Supercharged Thundershot that calls down Thunder from the sky

Storm Rifle EX");
        }

        public override void SetDefaults()
        {
            item.damage = 375;
            item.noMelee = true;
            item.ranged = true; 
            item.width = 90; 
            item.height = 30;
            item.useTime = 3; 
            item.useAnimation = 5; 
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("SThunderBullet");
            item.knockBack = 3;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
       	    item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Thunderlord");
            item.autoReuse = true; 
            item.shootSpeed = 9f;
            item.useAmmo = AmmoID.Bullet;
            item.crit = 10; 
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            type = Main.rand.Next(20) == 0 ? mod.ProjectileType("SThunderBullet") : mod.ProjectileType("ThunderBullet");
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 2f, 2f);
            return false;
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
