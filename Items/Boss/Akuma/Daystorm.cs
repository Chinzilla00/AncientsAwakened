using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
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
			item.mana = 6;
			item.width = 100;
			item.height = 100;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 0;
            item.value = Item.sellPrice(0, 7, 0, 0);
            item.rare = 8;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 10f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }

        int shoot = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireshotF"), damage, knockBack, player.whoAmI);
            }
            shoot++;

            if (shoot % 6 != 0) return false;

            if (shoot >= 6)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("AMeteor"), (int)(damage * 2), knockBack, player.whoAmI);
                shoot = 0;
            }
            shoot = 0;
            return false;
        }

        public override void AddRecipes()  //How to craft this sword
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
