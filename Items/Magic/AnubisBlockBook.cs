using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using AAMod.Projectiles.Anubis;

namespace AAMod.Items.Magic
{
    public class AnubisBlockBook : BaseAAItem
    {
        public override void SetDefaults()
        {
            item.damage = 130;                        
            item.magic = true;                     
            item.width = 24;
            item.height = 28;
            item.useTime = 90;
            item.useAnimation = 90;
            item.useStyle = ItemUseStyleID.HoldingOut;        
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 8;
            item.mana = 20;             
            item.UseSound = SoundID.Item21;            
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<BlockA>();  
            item.shootSpeed = 11f;
            item.rare = ItemRarityID.Yellow;
        }   

        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault(
@"The Life And Epic Adventures
of Anubis the Wonder Dog
~Special Edition~");
          Tooltip.SetDefault(@"Left click to summon blocks that crush at your cursor's position Horizontally
Right click for vertical blocks instead");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("BlockA")] >= 1 || player.ownedProjectileCounts[mod.ProjectileType("BlockA1")] >= 1)
            {
                return false;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float X = Main.mouseX + Main.screenPosition.X;

            float Y = Main.mouseY + Main.screenPosition.Y;
            if (player.gravDir == -1f)
            {
                Y = Main.screenPosition.Y + Main.screenHeight - Main.mouseY;
            }

            if (player.altFunctionUse != 2)
            {
                int l = Projectile.NewProjectile(new Vector2(X - 600, Y), Vector2.Zero, ModContent.ProjectileType<BlockA>(), damage, knockBack, Main.myPlayer, 0, 0);
                int r = Projectile.NewProjectile(new Vector2(X + 600, Y), Vector2.Zero, ModContent.ProjectileType<BlockA>(), damage, knockBack, Main.myPlayer, 1, 0);
                Main.projectile[l].ai[1] = r;
                Main.projectile[l].Center = new Vector2(X - 600, Y);
                Main.projectile[r].ai[1] = l;
                Main.projectile[r].Center = new Vector2(X + 600, Y);
            }
            else
            {
                int u = Projectile.NewProjectile(new Vector2(X, Y - 600), Vector2.Zero, ModContent.ProjectileType<BlockA1>(), damage, knockBack, Main.myPlayer, 0, 0);
                int d = Projectile.NewProjectile(new Vector2(X, Y + 600), Vector2.Zero, ModContent.ProjectileType<BlockA1>(), damage, knockBack, Main.myPlayer, 1, 0);
                Main.projectile[u].ai[1] = d;
                Main.projectile[u].Center = new Vector2(X, Y - 600);
                Main.projectile[d].ai[1] = u;
                Main.projectile[d].Center = new Vector2(X, Y + 600);
            }
            return false;
        }
    }
}
