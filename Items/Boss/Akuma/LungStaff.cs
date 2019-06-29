using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Projectiles.Akuma.Lung;

namespace AAMod.Items.Boss.Akuma
{
    public class LungStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancient Lung Staff");
            Tooltip.SetDefault(
                @"Summons an ancient lung to fight for you");
        }

        public override void SetDefaults()
        {
            item.mana = 20;
            item.damage = 90;
            item.useStyle = 1;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("LungHead");
            item.width = 64;
            item.height = 64;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 24;
            item.useTime = 24;
            item.noMelee = true;
            item.knockBack = 2f;
            item.buffType = mod.BuffType("LungMinion");
            item.summon = true;
            item.rare = 9;
            AARarity = 13;
            item.value = Item.sellPrice(0, 30, 0, 0);

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

        public override void ModifyTooltips(List<TooltipLine> list)
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
            int num184 = -1;
            int num185 = -1;
            int num74 = item.shoot;
            int num76 = damage;
            float num77 = item.knockBack;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float num81 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num82 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            for (int num186 = 0; num186 < 1000; num186++)
            {
                if (Main.projectile[num186].active && Main.projectile[num186].owner == Main.myPlayer)
                {
                    if (num184 == -1 && Main.projectile[num186].type == mod.ProjectileType<LungHead>())
                    {
                        num184 = num186;
                    }
                    if (num185 == -1 && Main.projectile[num186].type == mod.ProjectileType<LungTail>())
                    {
                        num185 = num186;
                    }
                    if (num184 != -1 && num185 != -1)
                    {
                        break;
                    }
                }
            }
            if (num184 == -1 && num185 == -1)
            {
                num81 = 0f;
                num82 = 0f;
                vector2.X = Main.mouseX + Main.screenPosition.X;
                vector2.Y = Main.mouseY + Main.screenPosition.Y;
                int num187 = Projectile.NewProjectile(vector2.X, vector2.Y, num81, num82, num74, num76, num77, Main.myPlayer, 0f, 0f);
                num187 = Projectile.NewProjectile(vector2.X, vector2.Y, num81, num82, mod.ProjectileType<LungBody>(), num76, num77, Main.myPlayer, num187, 0f);
                int num188 = num187;
                num187 = Projectile.NewProjectile(vector2.X, vector2.Y, num81, num82, mod.ProjectileType<LungBody1>(), num76, num77, Main.myPlayer, num187, 0f);
                Main.projectile[num188].localAI[1] = num187;
                num188 = num187;
                num187 = Projectile.NewProjectile(vector2.X, vector2.Y, num81, num82, mod.ProjectileType<LungTail>(), num76, num77, Main.myPlayer, num187, 0f);
                Main.projectile[num188].localAI[1] = num187;
            }
            else if (num184 != -1 && num185 != -1)
            {
                int num189 = Projectile.NewProjectile(vector2.X, vector2.Y, num81, num82, mod.ProjectileType<LungBody>(), num76, num77, Main.myPlayer, Projectile.GetByUUID(Main.myPlayer, Main.projectile[num185].ai[0]), 0f);
                int num190 = num189;
                num189 = Projectile.NewProjectile(vector2.X, vector2.Y, num81, num82, mod.ProjectileType<LungBody1>(), num76, num77, Main.myPlayer, num189, 0f);
                Main.projectile[num190].localAI[1] = num189;
                Main.projectile[num190].netUpdate = true;
                Main.projectile[num190].ai[1] = 1f;
                Main.projectile[num189].localAI[1] = num185;
                Main.projectile[num189].netUpdate = true;
                Main.projectile[num189].ai[1] = 1f;
                Main.projectile[num185].ai[0] = Main.projectile[num189].projUUID;
                Main.projectile[num185].netUpdate = true;
                Main.projectile[num185].ai[1] = 1f;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "DaybreakIncinerite", 5);
            recipe.AddIngredient(null, "CrucibleScale", 5);
            recipe.AddIngredient(null, "DragonriderStaff", 1);
            recipe.AddTile(null, "ACS");
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}
