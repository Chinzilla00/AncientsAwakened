using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace AAMod.Items.Boss.Serpent
{
    public class SnakeStaff : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Snake Staff");
            Tooltip.SetDefault(@"Summons a Snow Serpent to fight for you
Summons 2 segments for each minion slot");
        }

        public override void SetDefaults()
        {
            item.mana = 10;
            item.damage = 11;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.shootSpeed = 10f;
            item.shoot = mod.ProjectileType("SerpentHead");
            item.width = 26;
            item.height = 28;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 36;
            item.useTime = 36;
            item.rare = ItemRarityID.Green;
            item.value = Item.sellPrice(0, 5, 0, 0);
            item.noMelee = true;
            item.knockBack = 2f;
            item.buffType = mod.BuffType("SnakeMinion");
            item.summon = true;
        }

		public override void UseStyle(Player player)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(item.buffType, 3600, true);
			}
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
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
                    if (headCheck == -1 && proj.type == mod.ProjectileType("SerpentHead")) headCheck = i;
                    if (tailCheck == -1 && proj.type == mod.ProjectileType("SerpentTail")) tailCheck = i;
                    if (headCheck != -1 && tailCheck != -1) break;
                }
            }

            //initial spawn
            if (headCheck == -1 && tailCheck == -1)
            {
                int current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("SerpentHead"), damage, knockBack, player.whoAmI, 0f, 0f);

                int previous = 0;

                for (int i = 0; i < 3; i++)
                {
                    current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("SerpentBody"), damage, knockBack, player.whoAmI, current, 0f);
                    previous = current;
                }

                current = Projectile.NewProjectile(position.X, position.Y, 0, 0, mod.ProjectileType("SerpentTail"), damage, knockBack, player.whoAmI, current, 0f);

                Main.projectile[previous].localAI[1] = current;
                Main.projectile[previous].netUpdate = true;
            }
            //spawn more body segments
            else
            {
                int previous = (int) Main.projectile[tailCheck].ai[0];
                int current = 0;

                for (int i = 0; i < 2; i++)
                {
                    current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("SerpentBody"), damage, knockBack, player.whoAmI,
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
