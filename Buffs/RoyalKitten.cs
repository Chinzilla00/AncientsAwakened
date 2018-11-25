using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class RoyalKitten : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Royal Kitten");
            Description.SetDefault("'She's so pretty!'");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>(mod).RoyalKitten = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("RoyalKitten")] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("RoyalKitten"), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}