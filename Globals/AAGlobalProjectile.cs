using Terraria;
using Terraria.ModLoader;

namespace AAMod
{
    public class AAGlobalProjectile : GlobalProjectile
    {
        public static int CountProjectiles(int Type)
        {
            int num = 0;
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
                {
                    num++;
                }
            }
            return num;
        }

        public static bool AnyProjectiless(int Type)
        {
            for (int i = 0; i < 200; i++)
            {
                if (Main.projectile[i].active && Main.projectile[i].type == Type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
