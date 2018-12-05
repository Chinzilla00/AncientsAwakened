
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Boss.Grips
{
    public class ClawBaton : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chaos Claw Baton");
            Tooltip.SetDefault(@"Summons 2 chaos claws to fight for you
Requires 2 minion slots");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.shootSpeed = 14f;
            item.shoot = mod.ProjectileType("ClawBatonSpin");
            item.damage = 12;
            item.width = 42;
            item.height = 44;
            item.UseSound = SoundID.Item44;
            item.useAnimation = 30;
            item.useTime = 30;
            item.noMelee = true;
            item.value = Item.sellPrice(0, 0, 27, 0);
            item.knockBack = 7.5f;
            item.rare = 2;
            item.summon = true;
            item.mana = 5;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;
            float slotsUsed = 0;

            Main.projectile.Where(x => x.active && x.owner == player.whoAmI && x.minionSlots > 0).ToList().ForEach(x => { slotsUsed += x.minionSlots; });

            if (player.maxMinions - slotsUsed < 1) return true;
            int Claw1 = mod.ProjectileType<DragonClaw>();
            int Claw2 = mod.ProjectileType<HydraClaw>();
            int num76 = item.damage;
            float num77 = item.knockBack;
            int num154 = (int)((float)Main.mouseX + Main.screenPosition.X) / 16;
            int num155 = (int)((float)Main.mouseY + Main.screenPosition.Y) / 16;
            if (player.gravDir == -1f)
            {
                num155 = (int)(Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY) / 16;
            }
            Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, Claw1, num76, num77, i, 0f, 0f);
            Projectile.NewProjectile((float)Main.mouseX + Main.screenPosition.X, (float)(num155 * 16 - 24), 0f, 15f, Claw2, num76, num77, i, 0f, 0f);
            

            return true;
        }

        public override void AddRecipes()  //How to craft this sword
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "IncineriteBar", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}