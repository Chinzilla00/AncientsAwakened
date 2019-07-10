using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Zero
{
    public class Gigataser : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigataser");
            Tooltip.SetDefault(@"Fires void lightning
Hold to charge the Taser
the longer the taser is charged, the more it penetrates");
        }

        public override void SetDefaults()
        {
            item.noUseGraphic = true;
            item.damage = 400;
            item.noMelee = true;
            item.ranged = true;
            item.width = 74;
            item.height = 24;
            item.useTime = 65;
            item.useAnimation = 65; 
            item.useStyle = 5; 
            item.shoot = mod.ProjectileType("ZeroTaze");
            item.knockBack = 12;
            item.value = Item.sellPrice(0, 30, 0, 0);
            item.rare = 9;
            item.shootSpeed = 12f;
            item.crit += 5;
            item.rare = 9;
            AARarity = 13;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int[] array4 = new int[5];
            Vector2[] array5 = new Vector2[5];
            int num838 = 0;
            float num839 = 2000f;
            for (int num840 = 0; num840 < Main.maxNPCs; num840++)
            {
                if (Main.npc[num840].active)
                {
                    Vector2 center9 = Main.npc[num840].Center;
                    float num841 = Vector2.Distance(center9, position);
                    if (num841 < num839 && Collision.CanHit(position, 1, 1, center9, 1, 1))
                    {
                        array4[num838] = num840;
                        array5[num838] = center9;
                        if (++num838 >= array5.Length)
                        {
                            break;
                        }
                    }
                }
            }
            for (int num842 = 0; num842 < num838; num842++)
            {
                Vector2 vector82 = array5[num842] - position;
                float ai = Main.rand.Next(100);
                for (int i = 0; i < 3; i++)
                {
                    Vector2 vector83 = Vector2.Normalize(vector82.RotatedByRandom(0.78539818525314331)) * 14f;
                    Projectile.NewProjectile(position.X, position.Y, vector83.X * 2, vector83.Y * 2, mod.ProjectileType<Projectiles.Zero.ZeroTaze>(), damage, 0f, Main.myPlayer, vector82.ToRotation(), ai);
                }
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ApocalyptitePlate", 5);
            recipe.AddIngredient(null, "UnstableSingularity", 5);
            recipe.AddIngredient(null, "FulguriteTazerblaster");
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}