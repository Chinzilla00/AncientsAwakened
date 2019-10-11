using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Akuma
{
    public class YOTD : BaseAAItem
    {


        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Year of the Dragon");
            Tooltip.SetDefault(@"Fires dazzling fireworks to amaze your friends and blow your enemies away!
'Akuma brand fireworks is not responsible for damage done by shooting fireworks point-blank into enemies' faces.'");
        }

        public override void SetDefaults()
        {
            item.crit = 14;
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 25;
            item.useTime = 25;
            item.useAmmo = AmmoID.Rocket;
            item.width = 50;
            item.height = 20;
            item.shoot = 134;
            item.UseSound = SoundID.Item11;
            item.damage = 600;
            item.shootSpeed = 20f;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.knockBack = 2f;
            item.rare = 9;
            AARarity = 13;
            item.ranged = true;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Rarity13;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            for (int num211 = 0; num211 < 2; num211++)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<Projectiles.Akuma.YotD>(), item.damage, item.knockBack, Main.myPlayer, 0f, 1f);
            }
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(ItemID.FireworksLauncher);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
