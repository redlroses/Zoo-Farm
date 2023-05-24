using UnityEngine;

namespace CodeBase.StaticData
{
    [CreateAssetMenu(fileName = "MoneyPackConfig", menuName = "Static Data/MoneyPack")]
    public class MoneyPackConfig : ScriptableObject
    {
        public int AmountMoneyInPack;
    }
}