using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
            item.buffTime = 3600;
            item.summon = true;
            item.rare = 10;
            item.value = Item.sellPrice(1, 0, 0, 0);

            glowmaskTexture = "Glowmasks/" + GetType().Name + "_Glow"; //the glowmask texture path.
            glowmaskDrawType = BaseAAItem.GLOWMASKTYPE_SWORD; //what type it is when drawn in the hand, _NONE == no draw, _SWORD == like a sword, _GUN == like a gun	
            glowmaskDrawColor = Color.White;  //glowmask draw color
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = AAColor.Akuma;
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            // from the orange
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            float velocityX = (float)Main.mouseX + Main.screenPosition.X - vector2.X;
            float velocityY = (float)Main.mouseY + Main.screenPosition.Y - vector2.Y;

            int head = -1;
            int tail = -1;
            for (int i = 0; i < Main.projectile.Length; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer)
                {
                    if (head == -1 && Main.projectile[i].type == mod.ProjectileType("LungHead"))
                    {
                        head = i;
                    }
                    if (tail == -1 && Main.projectile[i].type == mod.ProjectileType("LungTail"))
                    {
                        tail = i;
                    }
                    if (head != -1 && tail != -1)
                    {
                        break;
                    }
                }
            }
            if (head == -1 && tail == -1)
            {
                velocityX = 0f;
                velocityY = 0f;
                vector2.X = (float)Main.mouseX + Main.screenPosition.X;
                vector2.Y = (float)Main.mouseY + Main.screenPosition.Y;

                int current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("LungHead"), damage, knockBack, Main.myPlayer);

                int previous = current;
                current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("LungBody"), damage, knockBack, Main.myPlayer, (float)previous);

                previous = current;
                current = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityX, mod.ProjectileType("LungTail"), damage, knockBack, Main.myPlayer, (float)previous);
                Main.projectile[previous].localAI[1] = (float)current;
                Main.projectile[previous].netUpdate = true;
            }
            else if (head != -1 && tail != -1)
            {
                int body = Projectile.NewProjectile(vector2.X, vector2.Y, velocityX, velocityY, mod.ProjectileType("LungBody"), damage, knockBack, Main.myPlayer, Main.projectile[tail].ai[0]);

                Main.projectile[body].localAI[1] = (float)tail;
                Main.projectile[body].ai[1] = 1f;
                Main.projectile[body].netUpdate = true;


                Main.projectile[tail].ai[0] = (float)body;
                Main.projectile[tail].netUpdate = true;
                Main.projectile[tail].ai[1] = 1f;
            }
            return false;
            /*
            //to fix tail disapearing meme
            float slotsUsed = 0;

            Main.projectile.Where(x => x.active && x.owner == player.whoAmI && x.minionSlots > 0).ToList().ForEach(x => { slotsUsed += x.minionSlots; });

            if (player.maxMinions - slotsUsed < 1) return false;

            int headCheck = -1;
            int tailCheck = -1;

            for (int i = 0; i < 1000; i++)
            {
                Projectile proj = Main.projectile[i];
                if (proj.active && proj.owner == player.whoAmI)
                {
                    if (headCheck == -1 && proj.type == mod.ProjectileType("LungHead")) headCheck = i;
                    if (tailCheck == -1 && proj.type == mod.ProjectileType("LungTail")) tailCheck = i;
                    if (headCheck != -1 && tailCheck != -1) break;
                }
            }

            //initial spawn
            if (headCheck == -1 && tailCheck == -1)
            {
                int current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("LungHead"), damage, knockBack, player.whoAmI, 0f, 0f);

                int previous = 0;

                for (int i = 0; i < 1; i++)
                {
                    current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("LungBody"), damage, knockBack, player.whoAmI, current, 0f);
                    previous = current;
                }

                current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("LungTail"), damage, knockBack, player.whoAmI, current, 0f);

                Main.projectile[previous].localAI[1] = current;
                Main.projectile[previous].netUpdate = true;
            }
            //spawn more body segments
            else
            {
                int previous = (int) Main.projectile[tailCheck].ai[0];
                int current = 0;

                for (int i = 0; i < 4; i++)
                {
                    current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EaterBody"), damage, knockBack, player.whoAmI,
                        Projectile.GetByUUID(Main.myPlayer, previous), 0f);

                    previous = current;
                }

                Main.projectile[current].localAI[1] = tailCheck;

                Main.projectile[tailCheck].ai[0] = current;
                Main.projectile[tailCheck].netUpdate = true;
                Main.projectile[tailCheck].ai[1] = 1f;
            }

            return false;*/
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
