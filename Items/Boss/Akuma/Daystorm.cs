using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class Daystorm : BaseAAItem
    { 

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daystorm");
            Tooltip.SetDefault(@"Incinerate your enemies in a storm of scorching fiery mayhem");
        }       

        public override void SetDefaults()
		{
			item.damage = 225;
			item.magic = true;
			item.mana = 9;
			item.width = 100;
			item.height = 100;
			item.useTime = 7;
			item.useAnimation = 7;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; 
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = ItemRarityID.Yellow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Sounds/Dayshot");
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 30;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //for (int i = 0; i < 3; i++)
            //{
            //    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10));
            //    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Dayser"), damage, knockBack, player.whoAmI);
            //}
            if (shoot++ > 6) shoot = 0;

            for (int i = 0; i < 4; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15)) * .5f;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("Daystormbullet"), damage, knockBack, player.whoAmI);
            }

            if (Main.rand.Next(3) == 0)
            {
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(5));
                //Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("Dayser"), damage * 2, knockBack, player.whoAmI);
                //shoot = 0;
                for (int i = 0; i < Main.rand.Next(2); i++)
                {
                    Vector2 perturbedSpeed2 = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed2.X, perturbedSpeed2.Y, mod.ProjectileType("DaystormbulletA"), (int)(damage * 1.5f), knockBack, player.whoAmI);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.LaserMachinegun);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
