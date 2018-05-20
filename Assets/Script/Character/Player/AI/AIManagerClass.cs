using System;
using System.Collections.Generic;



public class AIManagerClass : SingletonClass<AIManagerClass>
{



	AIParserClass parser;

	UICPUClass m_cpu;

	AISequenceBTClass root = new AISequenceBTClass();

//	AISequenceBTClass moveRootSequence = new AISequenceBTClass ();
	AISelectorBTClass attackRootSelector = new AISelectorBTClass ();

	AISelectorBTClass moveSelector = new AISelectorBTClass();
	AISequenceBTClass moveSequence = new AISequenceBTClass ();
	AISequenceBTClass desSequence = new AISequenceBTClass();

	AISequenceBTClass attackSequence = new AISequenceBTClass();
	AISelectorBTClass attackSelector = new AISelectorBTClass ();
	AISequenceBTClass stateSequence = new AISequenceBTClass();
	AISequenceBTClass ammoSequence = new AISequenceBTClass();
	AISequenceBTClass attackActSequence = new AISequenceBTClass();

	AISequenceBTClass captureSequence = new AISequenceBTClass ();

	AISelectorBTClass skillSelector = new AISelectorBTClass ();

	AISequenceBTClass enemyViewSeq = new AISequenceBTClass();
	AISequenceBTClass allyViewSeq = new AISequenceBTClass();
	AISequenceBTClass flagViewSeq = new AISequenceBTClass();

	AISequenceBTClass skillUseSeq = new AISequenceBTClass();

	AISelectorBTClass rangeSel = new AISelectorBTClass ();
	AISelectorBTClass hpSel = new AISelectorBTClass ();
	AISequenceBTClass chaseSeq = new AISequenceBTClass ();

	AIActionBTClass idleAction = new AIIdleActionBTClass();

//	AISelectorBTClass destinationSelector = new AISelectorBTClass();

	//각종 액션 묶음
//	AISelectorBTClass selector1 = new AISelectorBTClass();
//	AISelectorBTClass selector2 = new AISelectorBTClass();
//	AISelectorBTClass selector3 = new AISelectorBTClass();
//	AISelectorBTClass selector4 = new AISelectorBTClass();
//	AISelectorBTClass selector5 = new AISelectorBTClass();

//
//	AISequenceBTClass sequence1 = new AISequenceBTClass();
//	AISequenceBTClass sequence2 = new AISequenceBTClass();
//	AISequenceBTClass sequence3 = new AISequenceBTClass();
//	AISequenceBTClass sequence4 = new AISequenceBTClass();
//	AISequenceBTClass sequence5 = new AISequenceBTClass();



	//각종 액션 삽입
//	AIActionBTClass action1 = new AIActionBTClass();
//	AIActionBTClass action2 = new AIActionBTClass();
//	AIActionBTClass action3 = new AIActionBTClass();
//	AIActionBTClass action4 = new AIActionBTClass();
//	AIActionBTClass action5 = new AIActionBTClass();




	public AIManagerClass(){
//		parser = new AIParserClass ();
		initParse();
	}

	void initParse(){
		//XML로 입력된 AI 규칙 parsing 해서 삽입

		//루트
		root.addChild(new AIInverterBTClass(new AIIsDummyActionBTClass()));
		root.addChild(new AIIsGameRunActionBTClass());
		root.addChild(moveSelector);
		root.addChild(attackRootSelector);



		//목적지 이동 BT

		moveSelector.addChild (moveSequence);

		moveSequence.addChild (desSequence);

		desSequence.addChild (new AIIsDestinationActionBTClass ());
		desSequence.addChild (new AIInverterBTClass(new AIIsDestinationArriveActionBTClass()));
		desSequence.addChild (new AIMoveActionBTClass());


		moveSelector.addChild (captureSequence);

		captureSequence.addChild (new AIIsCapturePosActionBTClass ());
		captureSequence.addChild (new AIInverterBTClass(new AIIsFlagTeamActionBTClass ()));
		captureSequence.addChild (idleAction);

		moveSelector.addChild (new AISetDestinationActionBTClass ());

		moveSelector.addChild (idleAction);



		//스킬 BT
		attackRootSelector.addChild(skillSelector);

		skillSelector.addChild (enemyViewSeq);
		skillSelector.addChild (allyViewSeq);
//		skillSelector.addChild (flagViewSeq);

		enemyViewSeq.addChild (new AIIsSetEnemyActionBTClass());
		enemyViewSeq.addChild (skillUseSeq);
		allyViewSeq.addChild (new AIIsSetAllyActionBTClass());
		allyViewSeq.addChild (skillUseSeq);
//		flagViewSeq.addChild (new AIIsSetFlagActionBTClass());
//		flagViewSeq.addChild (skillUseSeq);

		skillUseSeq.addChild (new AIInverterBTClass(new AIIsSkillUsedActionBTClass ()));
		skillUseSeq.addChild (new AIIsSkillProbabilityActionBTClass ());
		skillUseSeq.addChild (new AISkillSearchActionBTClass ());
		skillUseSeq.addChild (new AIIsSkillRangeActionBTClass ());
		skillUseSeq.addChild (new AISkillActionBTClass ());

		//


		//공격 BT
		attackRootSelector.addChild (attackSequence);

		attackSequence.addChild (stateSequence);
		attackSequence.addChild (attackSelector);

		stateSequence.addChild (new AIIsEnemySearchActionBTClass ());

		attackSelector.addChild (ammoSequence);
		attackSelector.addChild (attackActSequence);

		ammoSequence.addChild (new AIInverterBTClass (new AIIsAmmoActionBTClass ()));
		ammoSequence.addChild (new AIReloadActionBTClass ());

		attackActSequence.addChild (new AIIsAttackDelayActionBTClass ());
		attackActSequence.addChild (new AIAttackActionBTClass ());

		attackSelector.addChild (idleAction);

		//추적
		stateSequence.addChild(hpSel);
		//rangeSel.addChild (new AIInverterBTClass (new AIIsWeaponRangeActionBTClass ()));

		//rangeSel.addChild (hpSel);

		hpSel.addChild (chaseSeq);

		chaseSeq.addChild (new AIInverterBTClass (new AIIsLowHPActionBTClass ()));
		chaseSeq.addChild (new AIIsEnemyViewActionBTClass ());
//		chaseSeq.addChild (new AIIsWeaponRangeActionBTClass ());
		chaseSeq.addChild (new AISetEnemyDestinationActionBTClass ());



	}

	/// <summary>
	/// 인공지능 행동트리 받기
	/// </summary>
	/// <returns>The B.</returns>
	public AINodeClass getBT(){
		return root;
	}

}


