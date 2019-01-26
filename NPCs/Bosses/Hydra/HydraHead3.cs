using Terraria;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace AAMod.NPCs.Bosses.Hydra
{
    [AutoloadBossHead]
    public class HydraHead3 : HydraHead1
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            leftHead = true;
			middleHead = false;		
			distFromBodyY = 70;			
        }
    }
}