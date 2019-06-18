using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class MagicAcornEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dapper Acorn");
            Tooltip.SetDefault(@"Attracts squirrels to fight with you for glory.
'Multiplayer = big meme'
-Fargowilta
Magic Acorn EX");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("DapperSquirrel1");
            item.damage = 200;
            item.width = 20;
            item.height = 20;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 9;
            item.summon = true;
            item.mana = 10;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(189, 76, 15);
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int shootMe = Main.rand.Next(2);
            {
                switch (shootMe)
                {
                    case 0:
                        shootMe = mod.ProjectileType("DapperSquirrel1");
                        break;
                    case 1:
                        shootMe = mod.ProjectileType("DapperSquirrel2");
                        break;
                }
            }
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter, true);
            vector2.X = Main.mouseX + Main.screenPosition.X;
            vector2.Y = Main.mouseY + Main.screenPosition.Y;
            Projectile.NewProjectile(vector2.X, vector2.Y, 0, 0, shootMe, damage, knockBack, Main.myPlayer, 0f, 0f);
            return false;
        }
    }
}