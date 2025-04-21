using System;
using System.Collections.Generic;
using UI.BusinessNode;
using UI.Score;

namespace UI
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