using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
using UnityEngine;

namespace TowerDefence.Towers
{
    public class SetTowersEvent : CustomEvent
    {
        public TowerSettings Settings;
        public TowersManager TowersManager;
    }
}
