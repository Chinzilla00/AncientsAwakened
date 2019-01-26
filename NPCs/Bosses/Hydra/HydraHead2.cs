using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead2 : HydraHead1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            leftHead = false;
			middleHead = false;
			distFromBodyY = 70;
        }
    }
}