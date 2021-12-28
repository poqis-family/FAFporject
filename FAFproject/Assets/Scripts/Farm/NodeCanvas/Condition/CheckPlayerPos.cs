using System;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using ParadoxNotion;

namespace NodeCanvas.Tasks.Conditions
{
    [Name("CheckPlayerPos")]
    [Category("Farm")]
    public class CheckPlayerPos : ConditionTask<Transform>
    {
        
        public BBParameter<float> distance;
        public CompareMethod checkType = CompareMethod.EqualTo;
        private float dis;
        
        [SliderField(0, 0.1f)]
        public float differenceThreshold = 0.05f;
        protected override string info {
            get { return "PlayerDistance" + OperationTools.GetCompareString(checkType) + distance; }
        }

        protected override bool OnCheck()
        {
            dis = (agent.gameObject.transform.position - Player._Instance.transform.position).magnitude;
            Debug.Log(dis);
            return OperationTools.Compare((float)dis, (float)distance.value, checkType,differenceThreshold);
        }
    }
}