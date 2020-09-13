using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ItemAPI;

namespace AbominationItems
{
    public class AbominationModule : ETGModule
    {
        public override void Init()
        {
        }

        public override void Start()
        {
            FakePrefabHooks.Init();
            ItemBuilder.Init();
            EternalPain.Init();
            FailRounds.Init();
            ArmorCapsAtOneItem.Init();
        }

        public override void Exit()
        {
        }
    }
}
