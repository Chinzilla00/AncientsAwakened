using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace AAMod.Items.Dev
{
    public class TimeTeller : ModItem
    {
        public override void SetDefaults()
        {
            item.useTime = 25;
            item.CloneDefaults(ItemID.Terrarian);

            item.damage = 120;
            item.value = 1000000;
            item.rare = 11;
            item.knockBack = 1;
            item.channel = true;
            item.useStyle = 5;
            item.useAnimation = 18;
            item.useTime = 18;
            item.shoot = mod.ProjectileType("TimeTeller");
        }

        public override void PostUpdate()
        {
            item.damage = Damage();
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Chilled, 1000);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Time Teller");
            Tooltip.SetDefault("Slows time for enemies hit \n" +
                "Time to Die \n" +
                "-Dallin");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(181, 38, 38);
                }
            }
        }

        public override void UpdateInventory(Player player)
        {
            if (player.accWatch < 3)
                player.accWatch = 3;
        }

        public int Damage()
        {
            double num4 = (float)Main.time;
            if (!Main.dayTime)
            {
                num4 += 54000.0;
            }
            num4 = num4 / 86400.0 * 24.0;
            double num5 = 7.5;
            num4 = num4 - num5 - 12.0;
            if (num4 < 0.0)
            {
                num4 += 24.0;
            }
            if (num4 == 1)
            {
                return 30;
            }
            else if (num4 == 2)
            {
                return 60;
            }
            else if (num4 == 3)
            {
                return 90;
            }
            else if (num4 == 4)
            {
                return 120;
            }
            else if (num4 == 5)
            {
                return 150;
            }
            else if (num4 == 6)
            {
                return 180;
            }
            else if (num4 == 7)
            {
                return 210;
            }
            else if (num4 == 8)
            {
                return 240;
            }
            else if (num4 == 9)
            {
                return 270;
            }
            else if (num4 == 10)
            {
                return 300;
            }
            else if (num4 == 11)
            {
                return 330;
            }
            else if (num4 == 12)
            {
                return 360;
            }
            else if (num4 == 13)
            {
                return 30;
            }
            else if (num4 == 14)
            {
                return 60;
            }
            else if (num4 == 15)
            {
                return 90;
            }
            else if (num4 == 16)
            {
                return 120;
            }
            else if (num4 == 17)
            {
                return 150;
            }
            else if (num4 == 18)
            {
                return 180;
            }
            else if (num4 == 19)
            {
                return 210;
            }
            else if (num4 == 20)
            {
                return 240;
            }
            else if (num4 == 21)
            {
                return 270;
            }
            else if (num4 == 22)
            {
                return 300;
            }
            else if (num4 == 23)
            {
                return 330;
            }
            else
            {
                return 360;
            }
        }

    }
}