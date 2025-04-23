using UnityEngine;

namespace Logic.Score
{
    [CreateAssetMenu(fileName = "ScoreStaticData", menuName = "Static Data/Score")]
    public class ScoreStaticData : ScriptableObject
    {
        public string ScoreLabel = "Score";
    }
}