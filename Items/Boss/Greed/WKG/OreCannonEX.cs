using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using Terraria.ModLoader;
using AAMod.Projectiles.Greed.WKG;
using AAMod.Items.Blocks;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class OreCannonEX : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ultimate Ore Cannon");
            Tooltip.SetDefault(@"Uses Any Ore as Ammunition
Certain ores have special effects when shot
Legendary Weapon
OreCannonEX");
        }

        public override void SetDefaults()
        {
            item.damage = 700;
            item.noMelee = true;
            item.ranged = true;
            item.width = 50;
            item.height = 20;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.knockBack = 0;
			item.shoot = ProjectileID.PurificationPowder;
            item.UseSound = SoundID.Item14;
            item.shootSpeed = 14f;
            item.expert = true; 
			item.expertOnly = true;
            item.autoReuse = true;
            item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = Globals.AAColor.Rarity12;
                }
            }
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, -3);
        }

        public int projType = -1;

        public override bool CanUseItem(Player player)
        {
			if (player.itemAnimation == 0)
			{
                bool flag = false;
                int oreindex = -1;
                for (int m = 0; m < 50; m++)
                {
                    Item item = player.inventory[m];
                    
                    if (item != null && (Config.LuckyOre.TryGetValue(item.type, out oreindex) || item.type == ItemID.Hellstone) && item.stack > 0) 
                    {
                        oreindex = m;
                        projType = item.type;
                        flag = true;
                        break;
                    }
                }
				if (flag)
				{
					player.inventory[oreindex].stack -= 1;
                    if (player.inventory[oreindex].stack <= 0)
                    {
                        player.inventory[oreindex].TurnToAir();
                    }
                    return true;
				}
			}
            return false;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, ModContent.ProjectileType<OreChunk>(), damage + Damage(), knockBack, player.whoAmI);
            Main.projectile[p].ai[1] = projType;
            if (Main.projectile[p].ai[1] == ItemID.TinOre || Main.projectile[p].ai[1] == ItemID.CopperOre)
            {
                Main.projectile[p].velocity *= .5f;
                if (Main.projectile[p].ai[1] == ItemID.TinOre)
                {
                    Main.projectile[p].knockBack *= 1.3f;
                }
            }
            else if(Main.projectile[p].ai[1] == ItemID.SilverOre)
            {
                Main.projectile[p].penetrate = 2;
            }
            else if (Main.projectile[p].ai[1] == ItemID.CrimtaneOre)
            {
                Main.projectile[p].knockBack *= 1.5f;
            }
            else if (Main.projectile[p].ai[1] == ItemID.Meteorite)
            {
                int num90 = 3;
                if (Main.rand.Next(3) == 0)
                {
                    num90 ++;
                }
                for (int num91 = 0; num91 < num90; num91++)
                {
                    Vector2 vector2 = new Vector2(player.position.X + player.width * 0.5f + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
                    vector2.X = (vector2.X * 10f + player.Center.X) / 11f + Main.rand.Next(-100, 101);
                    vector2.Y -= 150 * num91;
                    float num82 = Main.mouseX + Main.screenPosition.X - vector2.X;
                    float num83 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
                    if (num83 < 0f)
                    {
                        num83 *= -1f;
                    }
                    if (num83 < 20f)
                    {
                        num83 = 20f;
                    }
                    float num92 = num82 + Main.rand.Next(-40, 41) * 0.03f;
                    float speedY2 = num83 + Main.rand.Next(-40, 41) * 0.03f;
                    num92 *= Main.rand.Next(75, 150) * 0.01f;
                    vector2.X += Main.rand.Next(-50, 51);
                    Vector2 speedfinal = Vector2.Normalize(new Vector2(num92, speedY2)) * (new Vector2(speedX, speedY)).Length();
                    Projectile.NewProjectile(vector2.X, vector2.Y, speedfinal.X, speedfinal.Y, ModContent.ProjectileType<OreChunk>(), damage + Damage(), knockBack, player.whoAmI, 0f, ItemID.Meteorite);
                }
            }
            else if (Main.projectile[p].ai[1] == ItemID.CobaltOre)
            {
                Main.projectile[p].velocity *= 1.5f;
            }
            else if (Main.projectile[p].ai[1] == ItemID.PalladiumOre)
            {
                Main.projectile[p].velocity *= 1.3f;
            }
            else if (Main.projectile[p].ai[1] == ItemID.AdamantiteOre)
            {
                Main.projectile[p].scale *= 1.5f;
                Main.projectile[p].width *= 2;
                Main.projectile[p].height *= 2;
                Main.projectile[p].damage = (int)(Main.projectile[p].damage * 1.3);
            }
            else if (Main.projectile[p].ai[1] == ItemID.TitaniumOre)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<OreChunk>(), damage + (int)(Damage() * 0.8), knockBack, player.whoAmI, 0, ItemID.TitaniumOre);
                }
            }
            else if(Main.projectile[p].ai[1] == ItemID.LunarOre)
            {
                Main.projectile[p].velocity *= 2;
            }
            else if(Main.projectile[p].ai[1] == ModContent.ItemType<RadiumOre>())
            {
                Main.projectile[p].damage = (int)(Main.projectile[p].damage / 1.3);
                Main.projectile[p].velocity /= 2;
            }
            return false;
		}

        public int Damage()
        {
            if (Config.LuckyOre.TryGetValue(projType, out int orevalue))
            {
                return (int)Math.Exp(orevalue * 0.94 / 100);
            }
            else if (projType == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.94 / 100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.94 / 100);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OreCannon", 1);
            recipe.AddIngredient(null, "EXSoul", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
