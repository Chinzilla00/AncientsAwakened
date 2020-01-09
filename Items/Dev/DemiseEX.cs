using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Dev
{
    public class DemiseEX : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Catastrophe");
			Tooltip.SetDefault(@"An almighty greatblade that was once wielded by the demon lord
Left Click to unleash destructive demonic energy
Right Click to unleash catastrophic blades that fall from the sky
True Melee Strikes have a chance to instantly devour an enemy�s soul
Demise EX");
		}
		public override void SetDefaults()
		{
			item.damage = 350;
			item.melee = true;
			item.width = 58;
			item.height = 58;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 1;
			item.knockBack = 5;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.rare = 9;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = mod.ProjectileType("DemiseSphereEX");
            item.shootSpeed = 13f;
            item.expert = true;
            item.expertOnly = true;
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.staff[item.type] = false;
                item.useStyle = 1;
                item.noMelee = false;
                item.shoot = mod.ProjectileType("DemiseBladeEX");
                item.shootSpeed = 15f;
            }
            else
            {
                Item.staff[item.type] = true;
                item.useStyle = 5;
                item.noMelee = true;
                item.shoot = mod.ProjectileType("DemiseSphereEX");
                item.shootSpeed = 13f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                Vector2 vector12 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                float num75 = item.shootSpeed;
                for (int num120 = 0; num120 < 3; num120++)
                {
                    Vector2 vector2 = player.Center + new Vector2(-(float)Main.rand.Next(0, 401) * player.direction, -600f);
                    vector2.Y -= 100 * num120;
                    Vector2 vector13 = vector12 - vector2;
                    if (vector13.Y < 0f)
                    {
                        vector13.Y *= -1f;
                    }
                    if (vector13.Y < 20f)
                    {
                        vector13.Y = 20f;
                    }
                    vector13.Normalize();
                    vector13 *= num75;
                    float num82 = vector13.X;
                    float num83 = vector13.Y;
                    float speedX5 = num82;
                    float speedY6 = num83 + Main.rand.Next(-40, 41) * 0.02f;
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedX5, speedY6, mod.ProjectileType("DemiseBladeEX"), damage * 3 / 2, knockBack, Main.myPlayer);
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                    int p = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("DemiseSphereEX"), damage, knockBack, player.whoAmI);
                    Main.projectile[p].Center = player.Center;
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Demise");
            recipe.AddIngredient(null, "EXSoul");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
