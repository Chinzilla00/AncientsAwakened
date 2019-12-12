using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using AAMod.Items.Blocks;
using BaseMod;
using System;

namespace AAMod.Items.Boss.Greed.WKG
{
    public class OreCannon : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ore Cannon");
            Tooltip.SetDefault(@"Uses Ore as Ammunition
Certain ores have special effects when shot");
        }

        public override void SetDefaults()
        {

            item.damage = 300;
            item.noMelee = true;
            item.ranged = true;
            item.width = 50;
            item.height = 20;
            item.useTime = 45;
            item.useAnimation = 45;
            item.useStyle = 5;
            item.knockBack = 0;
            item.value = Item.sellPrice(5, 0, 0, 0);
			item.shoot = 10;
            item.rare = 11;
            item.UseSound = SoundID.Item14;
            item.shootSpeed = 14f;
            item.expert = true; 
			item.expertOnly = true;
            item.autoReuse = true;
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
            int p = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("OreChunk"), damage + Damage(), knockBack, player.whoAmI);
			Main.projectile[p].ai[1] = projType;
            if (Main.projectile[p].ai[1] == ItemID.CrimtaneOre)
            {
                Main.projectile[p].knockBack *= 1.5f;
            }
            if (Main.projectile[p].ai[1] == ItemID.TitaniumOre)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20));
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage + Damage(), knockBack, player.whoAmI, 0, 5);
                }
            }
            return false;
		}

        public int Damage()
        {
            int orevalue = 0;
            if(Config.LuckyOre.TryGetValue(projType, out orevalue))
            {
                return (int)Math.Exp(orevalue * 0.84/100);
            }
            else if(projType == ItemID.Hellstone)
            {
                return (int)Math.Exp(500 * 0.84/100);
            }
            else
            {
                return (int)Math.Exp(100 * 0.84/100);
            }
        }
    }
}
