using UnityEngine;
using System.Collections;

public class MotionBehaviour : StateMachineBehaviour {

	public bool changeflg = false;   // モーションチェンジフラグ

	// 次のステートに移り変わる直前に実行される
	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		changeflg = true;
	}
}