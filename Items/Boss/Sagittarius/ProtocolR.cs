using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace AAMod.Items.Boss.Sagittarius
{
    public class ProtocolR : BaseAAItem
    { 

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protocol-R");
            Tooltip.SetDefault(@"fires a barrage of special rockets");
        }       

        public override void SetDefaults()
		{
			item.damage = 25;
			item.ranged = true;
			item.width = 50;
			item.height = 30;
			item.useTime = 3;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; 
			item.knockBack = 0;
                        item.value = Item.sellPrice(0, 7, 0, 0);
                        item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Dayshot");
			item.autoReuse = true;
                        item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 6;
                        item.reuseDelay = 100;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25, -9);
        }

        int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(60));
            //    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SagRocketF"), damage, knockBack, player.whoAmI);
            //}
            if (shoot++ > 6) shoot = 0;

            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30)) * .5f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SagRocketF"), damage, knockBack, player.whoAmI);
            }

            if (Main.rand.Next(3) == 0)
            {
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                //Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SagRocketF"), damage * 2, knockBack, player.whoAmI);
                //shoot = 0;
                for (int i = 0; i < Main.rand.Next(2); i++)
                {
                    Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(30));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, mod.ProjectileType("SagRocketF"), (int)(damage * 1.5f), knockBack, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "Doomite", 40);
                recipe.AddIngredient(null, "DoomiteScrap", 30);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
        }
    }
}
