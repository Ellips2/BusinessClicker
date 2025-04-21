using System;
using System.Collections.Generic;
using UI.Business;
using UI.Score;

namespace SaveLoad
{
    [Serializable]
    public class PlayerProgress
    {
        public ScoreData ScoreData;
        public List<BusinessNodeData> BusinessNodes;

        public PlayerProgress()
        {
            ScoreData = new ScoreData();
            BusinessNodes = new List<BusinessNodeData>();
        }
    }
}