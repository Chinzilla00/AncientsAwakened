using AAMod.Buffs;
using Terraria;
using Terraria.ModLoader;

namespace AAMod.Backgrounds
{
    class Fooooog : ModWorld
    {
        public override void PostUpdate()
        {
            ScreenFog.Update(mod.GetTexture("Backgrounds/fog"));
        }

        public override void PostDrawTiles()
        {
            Player player = Main.player[Main.myPlayer];
            if (player.HasBuff(mod.BuffType<Clueless>()) && Main.netMode == 0)
            {
                ScreenFog.Draw(mod.GetTexture("Backgrounds/fog"), 0.3f, 0.1f);
            }
        }
    }
}