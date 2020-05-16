using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Summoning
{
    public class DragonriderStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Pike");
            Tooltip.SetDefault(@"Summons a fire dragon to fight for you");
        }

        public override void SetDefaults()
        {
            item.mana = 20;
            item.damage = 50;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("DragonHead");
            item.width = 64;
            item.height = 64;
            item.UseSound = SoundID.Item44;
            item.rare = ItemRarityID.Yellow;
            item.useAnimation = 24;
            item.useTime = 24;
            item.noMelee = true;
            item.knockBack = 2f;
            item.buffType = mod.BuffType("DragonMinion");
            item.summon = true;
            item.value = Item.sellPrice(0, 8, 0, 0);
        }
		
		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2 && player.controlUseItem && player.releaseUseItem)
            {
                player.MinionNPCTargetAim();
                player.UpdateMinionTarget();
            }
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2)
            {
                return false;
            }
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
                    if (headCheck == -1 && proj.type == mod.ProjectileType("DragonHead")) headCheck = i;
                    if (tailCheck == -1 && proj.type == mod.ProjectileType("DragonTail")) tailCheck = i;
                    if (headCheck != -1 && tailCheck != -1) break;
                }
            }

            //initial spawn
            if (headCheck == -1 && tailCheck == -1)
            {
                int current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("DragonHead"), damage, knockBack, player.whoAmI, 0f, 0f);

                int previous = 0;

                for (int i = 0; i < 1; i++)
                {
                    current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("DragonBody"), damage, knockBack, player.whoAmI, current, 0f);
                    previous = current;
                }

                current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("DragonTail"), damage, knockBack, player.whoAmI, current, 0f);

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
                    current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonBody"), damage, knockBack, player.whoAmI,
                        Projectile.GetByUUID(Main.myPlayer, previous), 0f);

                    previous = current;
                }

                Main.projectile[current].localAI[1] = tailCheck;
                
                Main.projectile[tailCheck].ai[0] = current;
                Main.projectile[tailCheck].netUpdate = true;
                Main.projectile[tailCheck].ai[1] = 1f;
            }

            return false;
        }
    }
}