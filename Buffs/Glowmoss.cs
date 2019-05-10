using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Glowmoss : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Glowmoss Ball");
            Description.SetDefault("Don't ask what makes it glows. Trust me.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>(mod).Glowmoss = true;
            player.buffTime[buffIndex] = 18000;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Glowmoss")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("Glowmoss"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}